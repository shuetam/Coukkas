using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Coukkas.Core;
using Coukkas.Core.Domain;
using Coukkas.Infrastructure;
using Coukkas.Infrastructure.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Tests
{
    public class ServicesTests
    {
        /* [Fact]
        public async Task When_user_asks_for_fences_that_are_out_of_his_location_should_get_a_dictionary_with_their_names_and_distance_to_them()
        {
            //Arrange
            var user = new User(Guid.NewGuid(), "mat@mat.kl", "mat", "paswordhard", "user");
            user.SetLocation(5,5);
            
            var fence1 = new Fence(Guid.NewGuid(), Guid.NewGuid(),"fence1","description1", DateTime.UtcNow,DateTime.UtcNow.AddDays(2),3,3,3);
            var fence2 = new Fence(Guid.NewGuid(), Guid.NewGuid(),"fence2","description2", DateTime.UtcNow,DateTime.UtcNow.AddDays(2),3,3,3);

            var dicionary =  new Dictionary<string,double>();
            dicionary.Add(fence1.Name, fence1.location.GetDistanceTo(user.Location));
            dicionary.Add(fence2.Name, fence2.location.GetDistanceTo(user.Location));

            var fenceRepositoryMock = new Mock<IFenceRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var tokenMock = new Mock<ITokenHandler>();

            userRepositoryMock.Setup(x => x.GetAsync(user.Id)).ReturnsAsync(user);
            fenceRepositoryMock.Setup(x => x.GetNotAvailableAsync(user.Location)).ReturnsAsync(dicionary);


            //Act
            var fenceService = new FenceService(fenceRepositoryMock.Object, userRepositoryMock.Object , mapperMock.Object);
          //  var userService = new UserService(userRepositoryMock.Object,  mapperMock.Object, tokenMock.Object);
            var dicionaryfromservice = await fenceService.GetNotAvailableAsync(user.Id);


            //Assert
            dicionaryfromservice.Should().BeSameAs(dicionary);
        } */

    }

}