using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Coukkas.Infrastructure.Repositories.DTOS;

namespace Coukkas.Infrastructure.Services
{
    public interface ITokenHandler
{
     TokenDto CreateToken (Guid UserId, string role);
}

}