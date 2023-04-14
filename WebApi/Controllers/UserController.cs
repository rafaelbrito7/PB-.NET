using Common;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Image;
using Services;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        public IUserService UserService { get; set; }
        public IImageService ImageService { get; set; }

        public UserController(IUserService userService, IImageService imageService)
        {
            UserService = userService;
            ImageService = imageService;
        }

        // GET: api/User
        [HttpGet("{id}")]

        public async Task<IActionResult> GetNotFollowing(Guid id)
        {
            return Ok(await UserService.GetNotFollowing(id));
        }

        // GET: api/User/5
        [HttpGet("profile/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            User user = await UserService.GetById(id);
            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] User userReq)
        {
            Guid Id = Guid.NewGuid();

            ImageProperties imageProperties = Utils.ConvertImageBase64StringToByteArr(userReq.PhotoUrl);

            string photoUrl = await ImageService.UploadFile("profile", Id, imageProperties.FileExtension, imageProperties.ImageBytes);

            var user = new User
            {
                Id = Id,
                FirstName = userReq.FirstName,
                Lastname = userReq.Lastname,
                Email = userReq.Email,
                Password = userReq.Password,
                PhotoUrl = photoUrl,
            };
            User userRes = await UserService.CreateUser(Id, user);

            if (userRes == null)
                return BadRequest();

            return Ok();
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] User userReq)
        {
            ImageProperties imageProperties = Utils.ConvertImageBase64StringToByteArr(userReq.PhotoUrl);

            string photoUrl = await ImageService.UploadFile("profile", id, imageProperties.FileExtension, imageProperties.ImageBytes);

            bool result = await UserService.UpdateUser(id, userReq.FirstName, userReq.Lastname, userReq.Email, userReq.Password, photoUrl);

            if (!result)
                return BadRequest();

            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            User user = await UserService.GetById(id);

            if (user == null)
                return BadRequest();

            bool result = await UserService.RemoveUser(user);

            if (!result)
                return BadRequest();

            return Ok();
        }

        [HttpPost]
        [Route("{userId}/{userFollowed}/followers")]
        public async Task<IActionResult> AddFollower([FromRoute] Guid userId, Guid userFollowed)
        {
            bool result = await UserService.AddFollower(userId, userFollowed);

            if (!result)
                return BadRequest();

            return Ok();
        }

        [HttpPut]
        [Route("{userId}/{userFollowed}/followers")]
        public async Task<IActionResult> RemoveFollower([FromRoute] Guid userId, Guid userFollowed)
        {
            bool result = await UserService.RemoveFollower(userId, userFollowed);

            if (!result)
                return BadRequest();

            return Ok();
        }
    }
}
