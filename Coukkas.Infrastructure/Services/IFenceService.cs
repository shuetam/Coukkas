using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Coukkas.Core.Domain;
using Coukkas.Infrastructure.Repositories.DTOS;

namespace Coukkas.Infrastructure.Services
{
    public interface IFenceService
    {
       /*  Task CreateAsync (Guid ID, Guid OwnerId, string Name, string Description, string Category, 
                        DateTime StartDate, DateTime EndDate, double lat, double lan, double Radius); */        Task <FenceDto> GetAsync (Guid Id);
        Task<List<FenceDto>> GetAvailableAsync (Guid UserId);
         Task<List<FenceData>> GetAllAsync ();
        Task<List<Fence>> GetByOwnerAsync (Guid OwnerId);
        
        Task AddCoupons (Guid FenceId,  int amount);
        Task<Dictionary<string, double>> GetNotAvailableAsync (Guid UserId);
        Task DeleteAsync(Guid Id);
        Task<MemoryStream> GetImage(int id);
        Task CreateAsync(Guid fenceID, Guid userId, string name, string description, string _category, DateTime utcNow, DateTime dateTime, double lat, double lon, double radius);
    }
}