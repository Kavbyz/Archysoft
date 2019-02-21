using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Archysoft.Domain.Model.Enums;
using Archysoft.Domain.Model.Model.Auth;
using Archysoft.IntegrationTests.Web.Api.Seeds;
using Archysoft.Web.Api.Model;
using Newtonsoft.Json;
using Xunit;

namespace Archysoft.IntegrationTests.Web.Api.AuthControllerTests
{
    [Collection("Web Api Test Collection")]
    public class LoginMethodShould
    {
        private readonly ApiSut _sut;

        public LoginMethodShould(ApiSut sut)
        {
            _sut = sut;
            _sut.SeedUsers();
        }

        [Fact]
        public async Task ReturnStatusMinusOneWhenLoginIsNull()
        {
            // Arrange
            var request = new LoginModel
            {
                Login = null,
                Password = "Password123"
            };
            var requestJson = JsonConvert.SerializeObject(request);
            var expectedResult = OperationResultCode.Error;

            // Action
            var response = await _sut.Client.PostAsync("/auth/login", new StringContent(requestJson, Encoding.UTF8, "application/json"));
            var resultJson = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse>(resultJson);

            // Assert
            Assert.Equal(expectedResult, result.Status);
        }
    }
}