using System;
using System.ComponentModel.DataAnnotations;

namespace Coukkas.Core.Domain
{
    public abstract class Entity
    {
        
        public Guid Id {get; protected set;}
        public Entity()
        {
            Id = new Guid();
        }
        
    }
}