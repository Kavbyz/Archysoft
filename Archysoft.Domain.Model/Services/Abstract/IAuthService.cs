using Archysoft.Domain.Model.Model.Auth;

namespace Archysoft.Domain.Model.Services.Abstract
{
    public interface IAuthService
    {
        TokenModel Login(LoginModel loginModel);
    }
}
