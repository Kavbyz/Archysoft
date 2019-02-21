using Archysoft.Data.Enteties;

namespace Archysoft.Data.Repositories.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        User Get(string email, string password);
    }
}
