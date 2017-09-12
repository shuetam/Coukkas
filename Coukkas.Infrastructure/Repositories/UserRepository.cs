using System;
using System.Threading.Tasks;
using Coukkas.Core;
using Coukkas.Core.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coukkas.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        

        private static readonly ISet<User> _users  = new HashSet<User>();
        
        public async Task<User> GetAsync(Guid UserID)
        {
            return await Task.FromResult(_users.SingleOrDefault(x => x.Id == UserID));
        }

        public async Task<User> GetAsync(string UserEmail)
        {
            return await Task.FromResult(_users.SingleOrDefault(x => x.Email == UserEmail));
        }
        public async Task AddAsync(User user)
        {
           _users.Add(user);
           await Task.CompletedTask;
        }

        public async Task DeleteAsync(User user)
        {
            _users.Remove(user);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User user)
        {
            await Task.CompletedTask;
        }

    }
}
