using Archysoft.Data.Repositories.Abstract;
using Archysoft.Domain.Model.Services.Abstract;
using Archysoft.Domain.Model.Services.Concrete;
using Moq;

namespace Archysoft.UnitTests.Domain.Model.ServicesTests.AuthServiceTests
{
    public class AuthServiceSut
    {
        public AuthService Instance { get; set; }
        public Mock<IUserRepository> UserRepository { get; set; }
        public Mock<ISettingsService> SettingsService { get; set; }

        public AuthServiceSut()
        {
            UserRepository=new Mock<IUserRepository>();
            SettingsService=new Mock<ISettingsService>();
            Instance=new AuthService(UserRepository.Object, SettingsService.Object);
        }

    }
}
