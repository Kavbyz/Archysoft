using Archysoft.Domain.Model.Model.Common;
using Archysoft.Domain.Model.Model.Users;
using Archysoft.Domain.Model.Services.Abstract;
using Archysoft.Web.Api.Model;
using Archysoft.Web.Api.Routes;
using Microsoft.AspNetCore.Mvc;

namespace Archysoft.Web.Api.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route(RoutePaths.Users)]
        public ApiResponse<SearchResult<UserGridModel>> GetUsers(BaseFilter filter)
        {
            SearchResult<UserGridModel>  users = _userService.Get(filter);
            return new ApiResponse<SearchResult<UserGridModel>>(users);
        }
    }
}
