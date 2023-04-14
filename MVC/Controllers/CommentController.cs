using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Account;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MVC.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(Guid id)
        {
            if(id == Guid.Empty || id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Content, PostId")]Comment model)
        {
            if (ModelState.IsValid == false)
                return View(model);

            try
            {
                RequestAttributes requestAttributes = PrepareRequest();

                var newComment = new Comment
                {
                    Content = model.Content,
                    PostId = model.PostId,
                    UserId = requestAttributes.currentUserId.Value,
                };

                var json = JsonSerializer.Serialize<Comment>(newComment);

                HttpContent content = new StringContent(json, new MediaTypeHeaderValue("application/json"));

                var response = requestAttributes.httpClient.PostAsync($"https://localhost:7054/api/Comment", content).Result;

                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception("Erro ao tentar chamar a API.");
                }
                return RedirectToAction("Details", "Post", new { id = model.PostId });
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Delete(Guid id)
        {
            RequestAttributes requestAttributes = PrepareRequest();

            var response = requestAttributes.httpClient.GetAsync($"https://localhost:7054/api/comment/{id}").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a API do Usuário.");

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<Comment>(jsonString);

            return View(result);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                RequestAttributes requestAttributes = PrepareRequest();

                var response = requestAttributes.httpClient.DeleteAsync($"https://localhost:7054/api/comment/{id}").Result;

                if (response.IsSuccessStatusCode == false)
                    throw new Exception("Erro ao tentar chamar a API do Usuário.");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private RequestAttributes PrepareRequest()
        {
            var token = this.HttpContext.Session.GetString(UserAccount.SESSION_TOKEN_KEY);

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");

            var jwt = new JwtSecurityToken(token);
            var currentUserId = Guid.Parse(jwt.Claims.FirstOrDefault(c => c.Type == "sub")?.Value);

            return new RequestAttributes(httpClient, currentUserId);
        }

        public record RequestAttributes(
            HttpClient httpClient,
            Guid? currentUserId
        );
    }
}
