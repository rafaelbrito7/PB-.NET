using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Auth;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IAuthService AuthService { get; set; }

        public TokenController(IAuthService authService) { 
            AuthService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Token([FromBody] LoginAttributes loginAttributes)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            //AuthService
            AuthenticationReturn auth = await AuthService.Authenticate(loginAttributes.Email, loginAttributes.Password);
            if (!auth.Status)
                return Unauthorized();

            return Ok(new { token = auth.Token, id = auth.Id });
        }
    }

    public record LoginAttributes(
        [Required(ErrorMessage = "Email é obrigatório!")] string Email,
        [Required(ErrorMessage = "Password é obrigatório")] string Password);
}
