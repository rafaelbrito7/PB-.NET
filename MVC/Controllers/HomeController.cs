using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Models.Account;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            RequestAttributes requestAttributes = PrepareRequest();

            var response = requestAttributes.httpClient.GetAsync($"https://localhost:7054/api/Post/{requestAttributes.currentUserId}/feed").Result;

            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception("Erro ao tentar chamar a API.");
            }

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<List<Post>>(jsonString);

            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private RequestAttributes PrepareRequest()
        {
            var httpClient = new HttpClient();

            if (this.User.Identity.IsAuthenticated == true)
            {
                var token = this.HttpContext.Session.GetString(UserAccount.SESSION_TOKEN_KEY);

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");

                var jwt = new JwtSecurityToken(token);
                var currentUserId = Guid.Parse(jwt.Claims.FirstOrDefault(c => c.Type == "sub")?.Value);

                return new RequestAttributes(httpClient, currentUserId);
            }

            return new RequestAttributes(httpClient, null);
        }

        public record RequestAttributes(
            HttpClient httpClient,
            Guid? currentUserId
        );
    }
}