using Archysoft.Domain.Model.Model.Settings;
using Archysoft.Domain.Model.Services.Abstract;

namespace Archysoft.Domain.Model.Services.Concrete
{
    public class SettingsService:ISettingsService
    {
        public JwtSettings JwtSettings { get; set; }

        public SettingsService(JwtSettings jwtSettings)
        {
            JwtSettings = jwtSettings;
        }
    }
}
