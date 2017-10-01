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
    public class UserTests
    {
    [Fact]
    public void Should_return_exception()
    {
        
      Action Create = () =>   new User(Guid.NewGuid(),"Match@mat.pl","mat23","password","user");
        


       Create.ShouldThrow<Exception>().WithMessage("Wrong password format");
        
    }
        
    }
}