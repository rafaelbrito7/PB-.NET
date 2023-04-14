using Common;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Account;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using static MVC.Controllers.UserController;

namespace MVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private IAccountManagerRepository accountManagerRepository;

        public UserController(IAccountManagerRepository accountManagerRepository)
        {
            this.accountManagerRepository = accountManagerRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFollower(Guid userFollowed)
        {
            try
            {
                HttpContent content = new StringContent(string.Empty, Encoding.UTF8, "application/json");

                RequestAttributes requestAttributes = PrepareRequest();

                var response = requestAttributes.httpClient.PostAsync($"https://localhost:7054/api/user/{requestAttributes.currentUserId}/{userFollowed}/followers", content).Result;

                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception("Erro ao tentar chamar a API.");
                }

                var jsonString = response.Content.ReadAsStringAsync().Result;

                var result = JsonSerializer.Deserialize<List<User>>(jsonString);

                return RedirectToAction("Following", "User");
            }
            catch
            {
                return RedirectToAction("Following", "User");
            }
        }

        public async Task<IActionResult> RemoveFollower(Guid userFollowed)
        {
            try
            {
                HttpContent content = new StringContent(string.Empty, Encoding.UTF8, "application/json");

                RequestAttributes requestAttributes = PrepareRequest();

                var response = requestAttributes.httpClient.PutAsync($"https://localhost:7054/api/user/{requestAttributes.currentUserId}/{userFollowed}/followers", content).Result;

                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception("Erro ao tentar chamar a API.");
                }

                var jsonString = response.Content.ReadAsStringAsync().Result;

                var result = JsonSerializer.Deserialize<List<User>>(jsonString);

                return RedirectToAction("Following", "User");
            }
            catch
            {
                return RedirectToAction("Following", "User");
            }
        }

        public IActionResult Followers(Guid? id)
        {
            RequestAttributes requestAttributes = PrepareRequest();

            var userId = id != null ? id : requestAttributes.currentUserId;

            var response = requestAttributes.httpClient.GetAsync($"https://localhost:7054/api/user/profile/{userId}").Result;

            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception("Erro ao tentar chamar a API.");
            }

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<User>(jsonString);

            return View(result.Followers);
        }

        public IActionResult Following(Guid? id)
        {
            RequestAttributes requestAttributes = PrepareRequest();

            var userId = id != null ? id : requestAttributes.currentUserId;

            var response = requestAttributes.httpClient.GetAsync($"https://localhost:7054/api/user/profile/{userId}").Result;

            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception("Erro ao tentar chamar a API.");
            }

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<User>(jsonString);

            return View(result.Following);
        }

        public IActionResult Profile(Guid? id)
        {
            RequestAttributes requestAttributes = PrepareRequest();

            var userId = id == null ? requestAttributes.currentUserId : id.Value;

            var response = requestAttributes.httpClient.GetAsync($"https://localhost:7054/api/user/profile/{userId}").Result;

            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception("Erro ao tentar chamar a API.");
            } 

            var jsonString = response.Content?.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<User>(jsonString);

            ViewBag.UserId = userId;

            return View(result);
        }

        public IActionResult NotFollowing()
        {
            RequestAttributes requestAttributes = PrepareRequest();

            var response = requestAttributes.httpClient.GetAsync($"https://localhost:7054/api/User/{requestAttributes.currentUserId}").Result;

            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception("Erro ao tentar chamar a API.");
            }

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<List<User>>(jsonString);

            return View(result);
        }

        [AllowAnonymous]
        // GET: UserController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create([Bind("FirstName, Lastname, Email, Password, PhotoUrl")]User model, IFormFile PhotoUrl)
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

                var newUser = new User
                {
                    FirstName = model.FirstName,
                    Lastname = model.Lastname,
                    Email = model.Email,
                    Password = model.Password,
                    PhotoUrl = model.PhotoUrl
                };

                var json = JsonSerializer.Serialize<User>(newUser);
                HttpContent content = new StringContent(json, new MediaTypeHeaderValue("application/json"));

                RequestAttributes requestAttributes = PrepareRequest();

                var response = requestAttributes.httpClient.PostAsync($"https://localhost:7054/api/user", content).Result;

                if (response.IsSuccessStatusCode == true)
                {
                    return Redirect("/");
                }
                ViewBag.CreateError = "Error ao criar usuário. Email já utilizado.";
                return View();
            }
            catch
            {
                return View();
            }
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User model, IFormFile PhotoUrl)
        {
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

                var updatedUser = new User
                {
                    FirstName = model.FirstName,
                    Lastname = model.Lastname,
                    Email = model.Email,
                    Password = model.Password,
                    PhotoUrl = model.PhotoUrl
                };

                var json = JsonSerializer.Serialize<User>(updatedUser);
                HttpContent content = new StringContent(json, new MediaTypeHeaderValue("application/json"));

                RequestAttributes requestAttributes = PrepareRequest();

                var response = requestAttributes.httpClient.PutAsync($"https://localhost:7054/api/user/{requestAttributes.currentUserId}", content).Result;

                if (response.IsSuccessStatusCode == true)
                {
                    await accountManagerRepository.Logout();

                    return Redirect("/");
                }

                ViewBag.EditError = "Esse email já está sendo utilizado por outro usuário.";
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public IActionResult Edit()
        {
            RequestAttributes requestAttributes = PrepareRequest();

            var response = requestAttributes.httpClient.GetAsync($"https://localhost:7054/api/user/profile/{requestAttributes.currentUserId}").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a API do Usuário.");

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<User>(jsonString);

            return View(result);
        }

        

        // GET: UserController/Delete/5
        public IActionResult Delete()
        {
            RequestAttributes requestAttributes = PrepareRequest();

            var response = requestAttributes.httpClient.GetAsync($"https://localhost:7054/api/user/profile/{requestAttributes.currentUserId}").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a API do Usuário.");

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<User>(jsonString);

            return View(result);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                RequestAttributes requestAttributes = PrepareRequest();

                var response = requestAttributes.httpClient.DeleteAsync($"https://localhost:7054/api/user/{requestAttributes.currentUserId}").Result;

                if (response.IsSuccessStatusCode == false)
                    throw new Exception("Erro ao tentar chamar a API do Usuário.");

                await accountManagerRepository.Logout();

                return Redirect("/");
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
