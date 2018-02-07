using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Coukkas.Core.Domain;

namespace Coukkas.Core
{
    public interface IFenceRepository
    {
       Task <Fence> GetAsync(Guid FenceId);
       Task <List<Fence>> GetAsyncByOwner (Guid OwnerId);
       Task<List<Fence>> GetAvailableAsync (Location location);
       Task<Dictionary<string, double>> GetNotAvailableAsync (Location location);
       Task AddAsync (Fence fence);
       Task DeleteAsync (Fence fence);
       Task UpdateAsync (Fence fence);
       Task UpdateAllAsync();
       Task <List<Fence>> GetAllFancesAsync();
       Task <MemoryStream> fileStreamResult(int id);
       
    }
}
    