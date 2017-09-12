using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Coukkas.Core.Domain;
using Coukkas.Infrastructure.Repositories.DTOS;

namespace Coukkas.Infrastructure.Services
{
    public interface IFenceService
    {
        Task CreateAsync (Guid ID, Guid OwnerId, string Name, string Description, 
                        DateTime StartDate, DateTime EndDate, double lat, double lan, double Rad); 
        Task <FenceDto> GetAsync (Guid Id);
        Task<List<FenceDto>> GetAvailableAsync (Guid UserId);
        Task<List<Fence>> GetByOwnerAsync (Guid OwnerId);
        
        Task AddCoupons (Guid FenceId, double discount, int amount, DateTime end);

        Task<Dictionary<string, double>> GetNotAvailableAsync (Guid UserId);
        Task DeleteAsync(Guid Id);
    }
}