using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Archysoft.Data.Enteties;
using Archysoft.Data.Repositories.Abstract;
using Archysoft.Domain.Model.Enums;
using Archysoft.Domain.Model.Exceptions;
using Archysoft.Domain.Model.Model.Auth;
using Archysoft.Domain.Model.Services.Abstract;
using Microsoft.IdentityModel.Tokens;

namespace Archysoft.Domain.Model.Services.Concrete
{
    public class AuthService:IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISettingsService _settingsService;

        public AuthService(IUserRepository userRepository, ISettingsService settingsService)
        {
            _userRepository = userRepository;
            _settingsService = settingsService;
        }
        public TokenModel Login(LoginModel loginModel)
        {
            var user = _userRepository.Get(loginModel.Login, loginModel.Password);
            if (user == null)
            {
                throw new BusinessException(OperationResultCode.Error, "Invalid User");
            }

            return GenerateToken(user);
        }

        private TokenModel GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settingsService.JwtSettings.Key));
            var signinCredentials=new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(_settingsService.JwtSettings.ExpireDays);

            var jwtToken=new JwtSecurityToken(
                _settingsService.JwtSettings.Issuer,
                null,
                claims,
                expires:expires,
                signingCredentials:signinCredentials);

            return new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                ExpiresIn = DateTime.UtcNow.AddDays(_settingsService.JwtSettings.ExpireDays)
            };
        }
    }
}
