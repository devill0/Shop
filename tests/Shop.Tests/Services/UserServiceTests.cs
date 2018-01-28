using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using Shop.Core.Domain;
using Shop.Core.DTO;
using Shop.Core.Repositories;
using Shop.Core.Services;
using Xunit;

namespace Shop.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public void get_should_return_user()
        {
            //Arrange
            //Mock - dostarczenie obiektu, który nas interesuje i skonfigurowania czegoś
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var user = new User("test@test.com", "secret");

            userRepositoryMock.Setup(r => r.Get(user.Email)).Returns(user);
            mapperMock.Setup(m => m.Map<UserDTO>(user)).Returns(new UserDTO
            {
                Id = user.Id,
                Email = user.Email
            });      

            IUserService userService = new UserService(userRepositoryMock.Object, mapperMock.Object);

            //Act
            var userDTO = userService.Get(user.Email);

            //Assert
            user.Should().NotBeNull();
            userDTO.Id.ShouldBeEquivalentTo(user.Id);
            userRepositoryMock.Verify(x => x.Get(user.Email), Times.Once);
            mapperMock.Verify(x => x.Map<UserDTO>(user), Times.Once);
        }
    }
}
