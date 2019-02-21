using Archysoft.Domain.Model.Model.Settings;

namespace Archysoft.Domain.Model.Services.Abstract
{
    public interface ISettingsService
    {
        JwtSettings JwtSettings { get; set; }
    }
}
