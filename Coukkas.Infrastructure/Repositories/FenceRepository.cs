using System;
using System.Threading.Tasks;
using Coukkas.Core;
using Coukkas.Core.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coukkas.Infrastructure
{
    public class FenceRepository : IFenceRepository
    {
        public static readonly ISet<Fence> _fences = new HashSet<Fence> ();

        public async Task AddAsync(Fence fence)
        {
            _fences.Add(fence);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Fence fence)
        {
            _fences.Remove(fence);
            await Task.CompletedTask;
        }

        public async Task<Fence> GetAsync(Guid FenceId)
        {
            return await Task.FromResult(_fences.SingleOrDefault(f => f.Id == FenceId));
        }

       public async Task <IEnumerable<Fence>> GetAsync(string FenceName)
        {
            return await Task.FromResult(_fences.Where
            (f => f.Name.ToLowerInvariant().Contains(FenceName.ToLowerInvariant())));
        }

        public async Task<Dictionary<string, double>> GetNotAvailableAsync (Location location) 
        {
        return await Task.FromResult(_fences.Where
        (f => f.Radius<location.GetDistanceTo(f.location)).ToDictionary
        (f=>f.Name,f=>f.location.GetDistanceTo(location)));
        }
         
         public async Task<List<Fence>> GetAvailableAsync (Location location) 
        {
        return await Task.FromResult(_fences.Where
        (f => f.Radius>=location.GetDistanceTo(f.location)).ToList());
        }    
        
        public async Task UpdateAsync(Fence fence)
        {
         await  Task.CompletedTask;
        }
        public async Task <IEnumerable<Fence>> GetAsyncByOwner(Guid OwnerId)
        {
            return await  Task.FromResult( _fences.Where(f => f.OwnerID == OwnerId));
        }
    }
}
