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

    }
}
