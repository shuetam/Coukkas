using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Coukkas.Core.Domain;
using Coukkas.Infrastructure.FromBodyCommands;
using Coukkas.Infrastructure.Repositories.DTOS;

namespace Coukkas.Infrastructure.Services
{
    public interface IUserService
    {
       Task <AccountDto> GetAccountAsync(Guid Id);
       Task <List<AccountDto>> GetAllAccountsAsync();
       Task RegisterAsync (Guid Id, string email, string name, string password, string role);
       Task <TokenDto> LoginAsync  (string email, string password);
       Task SetLocation(Guid UserID, double lat, double lon);
        Task <List<FactTryCatchCoupon>> GetTryFacts(int year, int first , int last);
        Task FillRandomFacts(int i);
    }
}