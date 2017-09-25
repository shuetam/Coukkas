using System;
using System.Threading.Tasks;
using Coukkas.Core;
using Coukkas.Core.Domain;
using Coukkas.Infrastructure;
using FluentAssertions;
using Xunit;

namespace Tests
{
    public class RepositoriesTests
    {
        [Fact]
        public async Task get_user_by_ID_after_register()
        {
            // Arrange
            var id = Guid.NewGuid();
            var user = new User(id, "mat@mat.kl", "mat", "paswordhard", "user");
            IUserRepository userrepository = new UserRepository();
             // Act
            await userrepository.AddAsync(user);
            //Assert
            var userbyId = await userrepository.GetAsync(id);
            Assert.Equal(user, userbyId);
        }

        [Fact]
        public async Task fence_repository_should_return_list_of_fences_by_location()
        {
            // Arrange
          //  IFenceRepository fencerepository = new FenceRepository();
            var fence1 = new Fence(Guid.NewGuid(), Guid.NewGuid(),"fence1","description1", DateTime.UtcNow,DateTime.UtcNow.AddDays(2),3,3,3);
            var fence2 = new Fence(Guid.NewGuid(), Guid.NewGuid(),"fence2","description2", DateTime.UtcNow,DateTime.UtcNow.AddDays(2),3,3,3);
            await fencerepository.AddAsync(fence1);
            await fencerepository.AddAsync(fence2);
            // Act

            var fences =  await fencerepository.GetAvailableAsync(new Location(3,3));
            
            //Assert
            fences.Should().HaveCount(2); 
        }
    }
}
