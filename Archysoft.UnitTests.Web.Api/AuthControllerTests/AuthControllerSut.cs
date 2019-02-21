using Archysoft.Domain.Model.Services.Abstract;
using Archysoft.Web.Api.Controllers;
using Moq;

namespace Archysoft.UnitTests.Web.Api.AuthControllerTests
{
    public class AuthControllerSut
    {
        public AuthController Instance { get; set; }
        public Mock<IAuthService> AuthService { get; set; }

        public AuthControllerSut()
        {
            AuthService = new Mock<IAuthService>();
            Instance = new AuthController(AuthService.Object);
        }
    }
}
