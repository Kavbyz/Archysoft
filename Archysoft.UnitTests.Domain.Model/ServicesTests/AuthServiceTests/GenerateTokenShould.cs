using System;
using System.Reflection;
using Archysoft.Data.Enteties;
using Archysoft.Domain.Model.Model.Auth;
using Archysoft.Domain.Model.Model.Settings;
using Archysoft.Domain.Model.Services.Concrete;
using Xunit;

namespace Archysoft.UnitTests.Domain.Model.ServicesTests.AuthServiceTests
{
    public class GenerateTokenShould
    {
        public AuthServiceSut Sut { get; set; }

        public GenerateTokenShould()
        {
            Sut=new AuthServiceSut();
        }

        [Fact]
        public void ReturnAccessTokenNotNull()
        {
            // Arrange
            var user = new User
            {
                Email = "integration.test@d1.archysoft.com",
                Id = new Guid("9546482E-887A-4CAB-A403-AD9C326FFDA5"),
                UserName = "IntegrationTest"
            };

            Sut.SettingsService.SetupGet(x => x.JwtSettings).Returns(new JwtSettings
            {
                ExpireDays = 30,
                Issuer = "d1.archysoft.com",
                Key = "H@McQfTjWnZr4u7x!z%C*F-JaNdRgUkX"
            });

            MethodInfo methodInfo = typeof(AuthService).GetMethod("GenerateToken",
                BindingFlags.NonPublic | BindingFlags.Instance);

            // Action
            var actualResult = (TokenModel) methodInfo.Invoke(Sut.Instance, new object[] { user });

            // Assert
            Assert.NotNull(actualResult.AccessToken);
        }

        [Fact]
        public void ReturnExpiresIn()
        {
            // Arrange
            var expectedResult = DateTime.Now.AddDays(30);
            var user = new User
            {
                Email = "integration.test@d1.archysoft.com",
                Id = new Guid("9546482E-887A-4CAB-A403-AD9C326FFDA5"),
                UserName = "IntegrationTest"
            };

            Sut.SettingsService.SetupGet(x => x.JwtSettings).Returns(new JwtSettings
            {
                ExpireDays = 30,
                Issuer = "d1.archysoft.com",
                Key = "H@McQfTjWnZr4u7x!z%C*F-JaNdRgUkX"
            });

            MethodInfo methodInfo = typeof(AuthService).GetMethod("GenerateToken",
                BindingFlags.NonPublic | BindingFlags.Instance);

            // Action
            var actualResult = (TokenModel)methodInfo.Invoke(Sut.Instance, new object[] { user });

            // Assert
            Assert.Equal(expectedResult.Date, actualResult.ExpiresIn.Date);
        }
    }
}