using System;
using Archysoft.Domain.Model.Enums;
using Archysoft.Domain.Model.Model.Auth;
using Archysoft.Web.Api.Model;
using Moq;
using Xunit;

namespace Archysoft.UnitTests.Web.Api.AuthControllerTests
{
    public class LoginMethodShould
    {
        public AuthControllerSut Sut { get; set; }

        public LoginMethodShould()
        {
            Sut = new AuthControllerSut();
        }

        [Fact]
        public void ReturnStatusOneWhenLoginModelIsNull()
        {
            // Arrange
            var expectedResult = new ApiResponse<TokenModel>
            {
                Status = OperationResultCode.Success,
                Message = "Success",
                Timestamp = 1234567,
                Model = null
            };

            // Action
            var actualResult = Sut.Instance.Login(null);

            // Assert
            Assert.Equal(expectedResult.Status, actualResult.Status);
        }

        [Fact]
        public void ReturnDescriptionSuccessWhenLoginModelIsNull()
        {
            // Arrange
            var expectedResult = new ApiResponse<TokenModel>
            {
                Status = OperationResultCode.Success,
                Message = "Success",
                Timestamp = 1234567
            };

            // Action
            var actualResult = Sut.Instance.Login(null);

            // Assert
            Assert.IsType<ApiResponse<TokenModel>>(actualResult);
            Assert.Equal(expectedResult.Message, actualResult.Message);
        }

        [Fact]
        public void ReturnModelNullWhenLoginModelIsNull()
        {
            // Arrange
            // Action
            var actual = Sut.Instance.Login(null);

            // Assert
            Assert.IsType<ApiResponse<TokenModel>>(actual);
            Assert.Null(actual.Model);
        }

        [Fact]
        public void ReturnValidAccessToken()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Login = "test@email.com",
                Password = "1234qwer"
            };

            var expectedResult = new ApiResponse<TokenModel>
            {
                Model = new TokenModel { AccessToken = "1234567890", ExpiresIn = DateTime.UtcNow.AddDays(1) },
                Status = OperationResultCode.Success,
                Message = "Success",
                Timestamp = 1234567
            };

            Sut.AuthService.Setup(x => x.Login(It.IsAny<LoginModel>())).Returns(new TokenModel { AccessToken = "1234567890", ExpiresIn = DateTime.UtcNow.AddDays(1) });

            // Action
            var actualResult = Sut.Instance.Login(loginModel);

            // Assert
            Assert.Equal(expectedResult.Model.AccessToken, actualResult.Model.AccessToken);
        }

        [Fact]
        public void ReturnValidExpirationDate()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Login = "test@email.com",
                Password = "1234qwer"
            };

            var expectedResult = new ApiResponse<TokenModel>
            {
                Model = new TokenModel { AccessToken = "1234567890", ExpiresIn = DateTime.UtcNow.AddDays(1) },
                Status = OperationResultCode.Success,
                Message = "Success",
                Timestamp = 1234567
            };

            Sut.AuthService.Setup(x => x.Login(It.IsAny<LoginModel>())).Returns(new TokenModel { AccessToken = "1234567890", ExpiresIn = DateTime.UtcNow.AddDays(1) });

            // Action
            var actualResult = Sut.Instance.Login(loginModel);

            // Assert
            Assert.Equal(expectedResult.Model.ExpiresIn.Date, actualResult.Model.ExpiresIn.Date);
        }
    }
}
