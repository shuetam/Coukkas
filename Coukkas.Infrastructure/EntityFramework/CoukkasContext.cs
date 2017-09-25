using System;
using Coukkas.Core.Domain;
using Coukkas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq;

namespace Coukkas.Infrastructure.EntityFramework
{
    public class CoukkasContext : DbContext
    {

        public DbSet<User> Users {get; set;}
       public DbSet<Location> Locations {get; set;}
       public DbSet<Fence> Fences {get; set;}
       public DbSet<Coupon> Coupons {get; set;}

        private readonly SqlConnectingSettings _sqlSettings;
        
        public CoukkasContext(DbContextOptions<CoukkasContext> options, SqlConnectingSettings SqlSettings) : base(options)
        {
            _sqlSettings = SqlSettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_sqlSettings.InMemory)
            {
                optionsBuilder.UseInMemoryDatabase();
                return;
            }
           
            optionsBuilder.UseSqlServer(_sqlSettings.ConnectionString);
        }

           
           
           
            
        
            

        
    }
}