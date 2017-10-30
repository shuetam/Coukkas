using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Coukkas.Infrastructure.Repositories.DTOS;
using Coukkas.Core.Domain;
using Coukkas.Core;
using AutoMapper;
using Coukkas.Infrastructure.FromBodyCommands;
using System.Timers;

namespace Coukkas.Infrastructure.Services
{
    public class UserService : IUserService
    {
      private readonly IUserRepository _userRepository;
     private readonly IFenceRepository _fenceRepository;
      private readonly IMapper _autoMapper;
      private readonly ITokenHandler _tokenHandler;
   // public Timer timer;


            public UserService(IUserRepository userrepository, IMapper autoMapper, ITokenHandler tokenHandler, IFenceRepository fenceRepository)
            {
                _userRepository = userrepository;
                _autoMapper = autoMapper;
                _tokenHandler = tokenHandler;
                _fenceRepository = fenceRepository;
            }

      public async  Task <List<AccountDto>> GetAllAccountsAsync()
        {

              var users = await _userRepository.GetAllAsync();
               return _autoMapper.Map<List<AccountDto>>(users);
        }

        public async Task RegisterAsync(Guid Id, string email, string name, string password, string role)
        {
            var user = new User(Id, email, name, password, role);
            await _userRepository.AddAsync(user);
        }
           
        public async Task<TokenDto> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
        


            if (user == null)
            {
                throw new Exception("Login error.");
            }

            if (password != user.Password)
            {
                throw new Exception("Login error.");
            }

            return _tokenHandler.CreateToken(user.Id, user.Role);

        }

        public async Task<AccountDto> GetAccountAsync(Guid Id)
        {
            var user = await _userRepository.GetAsync(Id);
            if (user == null)
            {
                throw new Exception("User doesn't exist.");
            }
            return _autoMapper.Map<AccountDto>(user);
        }

        public async Task SetLocation(Guid UserID, double lat, double lon)
        {
        var user =   await _userRepository.GetAsync(UserID);
        user.SetLocation(lat,lon);
        await _userRepository.UpdateAsync(user);
        
        }
    }
}