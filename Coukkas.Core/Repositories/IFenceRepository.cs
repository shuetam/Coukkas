using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Coukkas.Core.Domain;

namespace Coukkas.Core
{
    public interface IFenceRepository
    {
       Task <Fence> GetAsync(Guid FenceId);
       Task <IEnumerable<Fence>> GetAsyncByOwner (Guid OwnerId);
       Task<List<Fence>> GetAvailableAsync (Location location);
       Task<Dictionary<string, double>> GetNotAvailableAsync (Location location);
       Task AddAsync (Fence fence);
       Task DeleteAsync (Fence fence);
       Task UpdateAsync (Fence fence);
       Task UpdateAllAsync();
       Task ChangeCouponsLocationsAsync();
    }
}
    