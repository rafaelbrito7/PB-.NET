using Microsoft.AspNetCore.Identity;
using MVC.Models.Account;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MVC.Repository
{
    public class AccountManagerRepository : IAccountManagerRepository
    {

        private SignInManager<UserAccount> SignInManager { get; set; }
        private HttpContext HttpContext { get; set; }

        public AccountManagerRepository(SignInManager<UserAccount> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            SignInManager = signInManager;
            HttpContext = httpContextAccessor.HttpContext;
        }

        public async Task<SignInResult> Login(string email, string password)
        {
            var token = GetToken(email, password);

            if (string.IsNullOrEmpty(token) == true)
            {
                return SignInResult.Failed;
            }

            var user = new UserAccount
            {
                Email = email,
                Password = password,
                Token = token

            };

            await SignInManager.SignInAsync(user, true);

            HttpContext.Session.SetString(UserAccount.SESSION_TOKEN_KEY, token);

            return SignInResult.Success;
        }

        public async Task Logout()
        {
            await SignInManager.SignOutAsync();
        }

        private string GetToken(string email, string password)
        {
            var user = new
            {
                Email = email,
                Password = password,
            };

            var body = new StringContent(JsonSerializer.Serialize(user), new MediaTypeHeaderValue("application/json"));

            HttpClient httpClient = new HttpClient();
            var response = httpClient.PostAsync("https://localhost:7054/api/token", body).Result;

            if (response.IsSuccessStatusCode == false)
                return String.Empty;

            var json = response.Content.ReadAsStringAsync().Result;
            var token = JsonSerializer.Deserialize<Token>(json);

            return token.token;
        }

        public class Token
        {
            [JsonPropertyName("token")]
            public string token { get; set; }

            [JsonPropertyName("id")]
            public string id { get; set; }
        }
    }
}
