using Archysoft.Domain.Model.Model.Common;
using Archysoft.Domain.Model.Model.Users;

namespace Archysoft.Domain.Model.Services.Abstract
{
    public interface IUserService
    {
        SearchResult<UserGridModel> Get(BaseFilter filter);
    }
}
