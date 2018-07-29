using System;
using System.Threading.Tasks;
using Coukkas.Core;
using Coukkas.Core.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coukkas.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Coukkas.Infrastructure.Repositories;

namespace Coukkas.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        
        private readonly CoukkasContext _context;
        
        public UserRepository(CoukkasContext context)
        {
            _context = context;
        }

        public async Task <List<User>> GetAllAsync()
        {
            return await _context.Users.Where(user => user.Role != ".").ToListAsync();
        }
        public async Task<User> GetAsync(Guid UserID)
        {
            return await _context.Users.Include(x=>x.Location).SingleOrDefaultAsync(x=> x.Id == UserID);
        }

        public async Task<User> GetAsync(string UserEmail)
        {
            return await _context.Users.Include(x=>x.Location).SingleAsync(x => x.Email == UserEmail);
        }
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
          var user1 = await GetAsync(user.Id);
            _context.Users.Remove(user1);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task <List<FactTryCatchCoupon>> GetTryFacts(int year, int first , int last)
        {

            var start_date = new DateTime(year,1,1).AddDays(first - 1);
            var end_date =  new DateTime(year,1,1).AddDays(last - 1);

            var facts = await _context.FactTryCatchCoupons.Include(f => f.location)
            .Where(x => DateTime.Compare(start_date, x.TryDate) < 0 && DateTime.Compare(x.TryDate, end_date) < 0)
            .ToListAsync();
            return facts;
        }

        public double GetRandomNumber(double minimum, double maximum)
        { 
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

         public async Task FillRandomFacts(int i)
        {
            var mockFence = new MockFence(12000,50.0646501, 19.9449799);

                var date = new  DateTime(2017,1,1).AddDays(i-1);
                var random = new Random();
                var fences_amount = random.Next(40,80);

                for(int j=0;j<fences_amount;j++)
                {
                mockFence.Latitude = GetRandomNumber(50.001031, 50.099744);
                mockFence.Longitude = GetRandomNumber(19.819009, 20.135514);
                mockFence.Radius = new Random().Next(1500, 4000);
                var facts_amount = random.Next(5,50);
                for(int l=0;l<facts_amount;l++)
                {
                var location =  MockData.GetMockLocation(mockFence);
                int random_cat = new Random().Next(0, Categories.Names.Count);
                var category = Categories.Names[random_cat];
                var fact = new FactTryCatchCoupon(date, location, category);
                await _context.FactTryCatchCoupons.AddAsync(fact);
                await _context.SaveChangesAsync(); // maybe saves shoud be after loop? we will see...
                }
                }
             
        } 

    }
}
