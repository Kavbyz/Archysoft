using System;
using Archysoft.Data.Enteties;
using Archysoft.Domain.Model.Exceptions;
using Archysoft.Domain.Model.Model.Auth;
using Archysoft.Domain.Model.Model.Settings;
using Moq;
using Xunit;

namespace Archysoft.UnitTests.Domain.Model.ServicesTests.AuthServiceTests
{
    public class LoginMethodShould
    {
        public AuthServiceSut Sut { get; set; }

        public LoginMethodShould()
        {
            Sut = new AuthServiceSut();
        }

        [Fact]
        public void ReturnAccessTokenNotNullWhenUserExist()
        {
            // Arrange
            var loginModel =new LoginModel
            {
                Login = "integration.test@d1.archysoft.com",
                Password = "123",
                RememberMe = false
            };

            Sut.UserRepository.Setup(x => x.Get(It.IsAny<String>(), It.IsAny<String>())).Returns(new User
            {
                Email = "integration.test@d1.archysoft.com",
                Id = new Guid("9546482E-887A-4CAB-A403-AD9C326FFDA5"),
                UserName = "IntegrationTest"
            });

            Sut.SettingsService.SetupGet(x => x.JwtSettings).Returns(new JwtSettings
            {
                ExpireDays = 30,
                Issuer = "d1.archysoft.com",
                Key = "H@McQfTjWnZr4u7x!z%C*F-JaNdRgUkX"
            });

            // Action
            var actualResult = Sut.Instance.Login(loginModel);

            // Assert
            Assert.NotNull(actualResult.AccessToken);
        }

        [Fact]
        public void ReturnExpiresInNotNullWhenUserExist()
        {
            // Arrange
            var expectedResult = DateTime.Now.AddDays(30);

            var loginModel = new LoginModel
            {
                Login = "integration.test@d1.archysoft.com",
                Password = "123",
                RememberMe = false
            };

            Sut.UserRepository.Setup(x => x.Get(It.IsAny<String>(), It.IsAny<String>())).Returns(new User
            {
                Email = "integration.test@d1.archysoft.com",
                Id = new Guid("9546482E-887A-4CAB-A403-AD9C326FFDA5"),
                UserName = "IntegrationTest"
            });

            Sut.SettingsService.SetupGet(x => x.JwtSettings).Returns(new JwtSettings
            {
                ExpireDays = 30,
                Issuer = "d1.archysoft.com",
                Key = "H@McQfTjWnZr4u7x!z%C*F-JaNdRgUkX"
            });

            // Action
            var actualResult = Sut.Instance.Login(loginModel);

            // Assert
            Assert.Equal(expectedResult.Date, actualResult.ExpiresIn.Date);
        }

        [Fact]
        public void ReturnAccessTokenExceptionWhenUserNotExist()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Login = "int.test@d1.archysoft.com",
                Password = "123",
                RememberMe = false
            };

            Sut.SettingsService.SetupGet(x => x.JwtSettings).Returns(new JwtSettings
            {
                ExpireDays = 30,
                Issuer = "d1.archysoft.com",
                Key = "H@McQfTjWnZr4u7x!z%C*F-JaNdRgUkX"
            });

            // Action
            // Assert
            Assert.Throws<BusinessException>(()=>Sut.Instance.Login(loginModel));
        }
    }
}
