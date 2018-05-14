using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using AutoMapper;
using Coukkas.Core;
using Coukkas.Core.Domain;
using Coukkas.Infrastructure.Repositories.DTOS;

namespace Coukkas.Infrastructure.Services
{
    public class FenceService : IFenceService
    {

        private readonly IFenceRepository _fenceRepository;
        private readonly IMapper _autoMapper;
        private readonly IUserRepository _userRepository;

    
        public FenceService (IFenceRepository fenceRepository,  IUserRepository userRepository, IMapper autoMapper)
        {
            _fenceRepository = fenceRepository;
            _userRepository = userRepository;
            _autoMapper = autoMapper;
        }

        public async Task AddCoupons(Guid FenceId,  int amount)
        {
            var fence = await _fenceRepository.GetAsync(FenceId);
            fence.AddCoupons(amount);
            await _fenceRepository.UpdateAsync(fence); 
        }

        public async Task DeleteAsync(Guid Id)
        {
            var fence = await _fenceRepository.GetAsync(Id);
            await _fenceRepository.DeleteAsync(fence);
        }

        public  async Task<FenceDto> GetAsync(Guid Id)
        {
         var fen = await _fenceRepository.GetAsync(Id);
         return  _autoMapper.Map<FenceDto>(fen);
        }
        
        public async Task<List<Fence>> GetByOwnerAsync(Guid OwnerId)
        {
            var fences = await _fenceRepository.GetAsyncByOwner(OwnerId);
            return fences;
        }

        public async Task<List<FenceDto>> GetAvailableAsync(Guid UserId)
        {
            var user = await _userRepository.GetAsync(UserId);
            var fences = await _fenceRepository.GetAvailableAsync(user.Location);
            return fences.Select( f => _autoMapper.Map<FenceDto>(f)).ToList();
        }

        public async Task<Dictionary<string, double>> GetNotAvailableAsync(Guid UserId)
        {
            var user = await _userRepository.GetAsync(UserId);
            var fences = await _fenceRepository.GetNotAvailableAsync(user.Location);
            return fences;
        }

        public async Task<List<FenceData>> GetAllAsync()
        {
            var allFences = await _fenceRepository.GetAllFencesAsync();
            return  allFences.Select(f => _autoMapper.Map<FenceData>(f)).ToList();
        }

       



        public async Task<MemoryStream> GetImage(int id) => await _fenceRepository.fileStreamResult(id);

      
          public async Task CreateAsync(Guid ID, Guid OwnerId, string Name, string Description, string _category,
        DateTime StartDate, DateTime EndDate, double lat, double lan, double Radius)
        {
            await _fenceRepository.AddAsync(new Fence(ID, OwnerId, Name, Description, _category, StartDate, EndDate, lat, lan, Radius));
        }
    }
}
      