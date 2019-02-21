using System;
using Microsoft.AspNetCore.Identity;

namespace Archysoft.Data.Enteties
{
    public class User : IdentityUser<Guid>
    {
        public UserProfile Profile { get; set; }
    }
}
