using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;

namespace MVC.Models.Account
{
    public class UserAccount
    {
        public const string SESSION_TOKEN_KEY = "UserAccountToken";

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato do email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório")]
        public string Password { get; set; }
        public string? Token { get; set; }

        public Guid? Id
        {
            get
            {
                if (string.IsNullOrEmpty(Token))
                    return null;

                var jwt = this.DecodeToken(Token);
                var id = jwt.Claims.FirstOrDefault(x => x.Type == "sub").Value;

                return Guid.Parse(id);
            }
        }

        public string? FirstName
        {
            get
            {
                if (string.IsNullOrEmpty(Token))
                    return String.Empty;

                var jwt = this.DecodeToken(Token);

                return jwt.Claims.FirstOrDefault(x => x.Type == "name").Value;
            }
        }

        private JwtSecurityToken DecodeToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            return jsonToken as JwtSecurityToken;
        }
    }
}
