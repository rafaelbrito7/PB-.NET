using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Auth;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IAuthService AuthService { get; set; }

        public AuthController(IAuthService authService) { 
            AuthService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginAttributes loginAttributes)
        {
            //AuthService
            AuthenticationReturn auth = await AuthService.Authenticate(loginAttributes.Email, loginAttributes.Password);
            if (!auth.Status)
                return BadRequest(auth);

            return Ok(new { token = auth.Token, id = auth.Id });
        }
    }

    public class LoginAttributes
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginAttributes() { }

        public LoginAttributes(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
