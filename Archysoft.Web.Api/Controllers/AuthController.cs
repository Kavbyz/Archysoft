using Archysoft.Domain.Model.Model.Auth;
using Archysoft.Domain.Model.Services.Abstract;
using Archysoft.Web.Api.Model;
using Archysoft.Web.Api.Routes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Archysoft.Web.Api.Controllers
{
    public class AuthController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(RoutePaths.Login)]
        public ApiResponse<TokenModel> Login([FromBody] LoginModel model)
        {
            TokenModel token = _authService.Login(model);
            return new ApiResponse<TokenModel>(token);
        }
    }
}
