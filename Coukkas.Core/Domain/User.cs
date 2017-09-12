using System;

namespace Coukkas.Core.Domain
{
    public class User: Entity
    {
        public string Role {get; protected set;}
        public string Name {get; protected set;}
        public string Email {get; protected set;}
        public string Password {get; protected set;}
        public DateTime CreatedDate {get; protected set;}
        public Location Location {get; protected set;}

        protected User()
        {}

        public User(Guid Id, string email, string name, string password, string role)
        {
            this.Id = Id;
            Role = role;
            Name = name;
            Email = email;
            Password = password;
            CreatedDate = DateTime.UtcNow;
        }

        public void SetLocation(double lat, double lon)
        {   
            this.Location = new Location(lat, lon);
        }
    }
}

            