using System;

namespace Archysoft.Domain.Model.Model.Auth
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}
