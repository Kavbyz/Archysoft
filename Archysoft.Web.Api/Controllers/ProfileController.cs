using Archysoft.Domain.Model.Services.Abstract;
using Archysoft.Web.Api.Model;

namespace Archysoft.Web.Api.Controllers
{
    public class ProfileController
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public ApiResponse GetProfile()
        {
            return null;
        }
    }
}
