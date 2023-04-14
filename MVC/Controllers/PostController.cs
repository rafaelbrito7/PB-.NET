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
    public class PostController : Controller
    {
        public IActionResult Index(Guid? id)
        {
            RequestAttributes requestAttributes = PrepareRequest();

            var userId = id != null ? id : requestAttributes.currentUserId;

            var response = requestAttributes.httpClient.GetAsync($"https://localhost:7054/api/Post?userId={userId}").Result;

            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception("Erro ao tentar chamar a API.");
            }

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<List<Post>>(jsonString);

            ViewBag.CurrentUserId = requestAttributes.currentUserId;
            ViewBag.Id = id;

            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhotoUrl, Description")]Post model, IFormFile PhotoUrl)
        {
            if (ModelState.IsValid == false)
                return View(model);

            try
            {
                if (PhotoUrl != null && PhotoUrl.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await PhotoUrl.CopyToAsync(stream);
                        var fileExtension = Path.GetExtension(PhotoUrl.FileName).TrimStart('.');
                        var imageBase64 = $"data:image/{fileExtension};base64,{Convert.ToBase64String(stream.ToArray())}";
                        model.PhotoUrl = imageBase64;
                    }
                }

                RequestAttributes requestAttributes = PrepareRequest();

                var newPost = new Post
                {
                    Description = model.Description,
                    PhotoUrl = model.PhotoUrl,
                    UserId = requestAttributes.currentUserId.Value,
                };

                var json = JsonSerializer.Serialize<Post>(newPost);

                HttpContent content = new StringContent(json, new MediaTypeHeaderValue("application/json"));

                var response = requestAttributes.httpClient.PostAsync($"https://localhost:7054/api/Post", content).Result;

                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception("Erro ao tentar chamar a API.");
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Details(Guid id)
        {
            RequestAttributes requestAttributes = PrepareRequest();

            var response = requestAttributes.httpClient.GetAsync($"https://localhost:7054/api/post/{id}").Result;

            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception("Erro ao tentar chamar a API.");
            }

            var jsonString = response.Content?.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<Post>(jsonString);

            return View(result);
        }

        public IActionResult Delete(Guid id)
        {
            RequestAttributes requestAttributes = PrepareRequest();

            var response = requestAttributes.httpClient.GetAsync($"https://localhost:7054/api/Post/{id}").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a API do Usuário.");

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<User>(jsonString);

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

                var response = requestAttributes.httpClient.DeleteAsync($"https://localhost:7054/api/Post/{id}").Result;

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
