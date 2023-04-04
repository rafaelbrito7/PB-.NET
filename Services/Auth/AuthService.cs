using Microsoft.IdentityModel.Tokens;
using Entities;
using Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Services.Auth
{
    public class AuthService : IAuthService
    {
        public IUserService UserService { get; set; }

        public JwtSecurityTokenHandler TokenHandler { get; set; }

        public Settings settings { get; set; }

        public string key;

        public AuthService(IUserService userService, IConfiguration config)
        {
            UserService = userService;
            settings = new Settings(config);
            key = settings.GetJwtSecret();
            TokenHandler = new JwtSecurityTokenHandler();
        }

        private string GenerateToken(Guid Id, string email)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Audience = "hortagram",
                Issuer = "hortagram",
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(securityToken);
            var validToken = tokenHandler.WriteToken(token);

            return validToken;
        }

        public async Task<AuthenticationReturn> Authenticate(string email, string password)
        {
            User user = await UserService.GetUserByEmail(email);
            if (user == null)
                return new AuthenticationReturn { Status = false };

            if (password != user.Password)
                return new AuthenticationReturn { Status = false };

            string token = GenerateToken(user.Id, user.Email);

            return new AuthenticationReturn { Status = true, Token = token, Id = user.Id };
        }
    }
}
