using System;

namespace Coukkas.Infrastructure.Repositories.DTOS
{
    public class AccountDto
    {
        public Guid Id {get; set;}
        public string Role {get;  set;}
        public string Name {get;  set;}
        public string Email {get;  set;}
      
    }
}