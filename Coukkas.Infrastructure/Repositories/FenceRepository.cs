using System;
using System.Threading.Tasks;
using Coukkas.Core;
using Coukkas.Core.Domain;
using System.Collections.Generic;
using System.Linq;
using Coukkas.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.IO;



namespace Coukkas.Infrastructure
{
    public class FenceRepository : IFenceRepository
    {
      
        private readonly CoukkasContext _context;

        public FenceRepository(CoukkasContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Fence fence)
        {
            await  _context.Fences.AddAsync(fence);
           await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Fence fence)
        {
            _context.Fences.Remove(fence);
            await _context.SaveChangesAsync();
        }

        public async Task<Fence> GetAsync(Guid FenceId)
        =>   await _context.Fences
            .Include(x=>x.Coupons).ThenInclude(v => v.location)
            .Include(x=>x.location)
            .SingleOrDefaultAsync(f => f.Id == FenceId);

        public async Task UpdateAllAsync()
        {
            var fences =  await _context.Fences
            .Include(x=>x.Coupons).ThenInclude(v => v.location)
            .Include(x=>x.location).Where(x=>x.Coupons.Count>0).ToListAsync();
            foreach(var f in fences)
            {
                await UpdateAsync(f);
            }
        }

         public async Task<Dictionary<string, double>> GetNotAvailableAsync (Location location) 
        {
        return await _context.Fences.Include(x=>x.location)
        .Where
        (f => f.Radius<location.GetDistanceTo(f.location)).ToDictionaryAsync
        (f=>f.Name,f=>f.location.GetDistanceTo(location));
        }
         
         public async Task<List<Fence>> GetAvailableAsync (Location location) 
        {
        return await _context.Fences.Include(x=>x.location).Include(x=>x.Coupons).ThenInclude(z=>z.location)
        .Where
        (f => f.Radius>=location.GetDistanceTo(f.location)).ToListAsync();
        }    
        
        public async Task UpdateAsync(Fence fence)
        {
            _context.Update(fence);
            await _context.SaveChangesAsync();
        }
        public async Task <List<Fence>> GetAsyncByOwner(Guid OwnerId)
            => await _context.Fences
            .Where(f => f.OwnerID == OwnerId)
            .Include(x=>x.location)
            .Include(f => f.Coupons).ThenInclude(c => c.location)
            .ToListAsync();

        public async Task<List<Fence>> GetAllFencesAsync()
        => await _context.Fences.Include(x=>x.location).ToListAsync();

        public async Task<List<Coupon>> GetAllCouponsAsync()
        => await _context.Coupons.Include(x=>x.location).Include(x=>x.fence).ToListAsync();

          public async Task TryFact (Location location, Fence fence) 
        {
         await _context.FactTryCatchCoupons.AddAsync(new FactTryCatchCoupon(DateTime.UtcNow, location, fence.Category)); // better send location
         await _context.SaveChangesAsync();
        }

        public async Task<MemoryStream> fileStreamResult(int id)
        {
        var image = await _context.Images.Where(x => x.ID == id).SingleAsync();
 
         return new MemoryStream(image.Data); 
        }      
    }
}
