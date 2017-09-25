using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Coukkas.Core.Domain;

namespace Coukkas.Core
{
    public interface IUserRepository
    {
        Task <User> GetAsync(Guid UserID);
        Task <User> GetAsync(string UserEmail);
        Task AddAsync (User user);
        Task DeleteAsync (User user);
        Task UpdateAsync (User user);
      
    }
}

