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
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await UserService.GetAll());
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetUsersByNameOrLastname([FromQuery] string name, [FromQuery] string lastname)
        {
            return Ok(await UserService.GetUsersByNameOrLastname(name, lastname));
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await UserService.GetById(id));
        }

        // POST: api/User
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromForm] UserRequest userReq)
        {
            Guid Id = Guid.NewGuid();

            ImageProperties imageProperties = Utils.ConvertImageBase64StringToByteArr(userReq.ImageBase64);

            string photoUrl = await ImageService.UploadFile("profile", Id, imageProperties.FileExtension, imageProperties.ImageBytes);

            userReq.Status = true;
            UserResponse userRes = await UserService.CreateUser(Id, userReq.FirstName, userReq.Lastname, userReq.Email, userReq.Password, photoUrl, userReq.Status);

            if (userRes == null)
                return BadRequest();

            return Ok();
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] User userReq)
        {
            bool result = await UserService.UpdateUser(id, userReq.FirstName, userReq.Lastname, userReq.Email, userReq.Password, userReq.PhotoURL);

            if (!result)
                return BadRequest();

            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            User user = await UserService.GetUserById(id);

            if (user == null)
                return BadRequest();

            bool result = await UserService.RemoveUser(user);

            if (!result)
                return BadRequest();

            return Ok();
        }

        [HttpGet]
        [Route("{id}/followers")]
        public async Task<IActionResult> GetFollowers([FromRoute] Guid id)
        {
            return Ok(await UserService.GetFollowers(id));
        }

        [HttpPost]
        [Route("{id}/followers")]
        public async Task<IActionResult> AddFollower([FromRoute] Guid id, [FromBody] FollowerRequest follower)
        {
            bool result = await UserService.AddFollower(id, follower.Id);

            if (!result)
                return BadRequest();

            return Ok();
        }

        [HttpPut]
        [Route("{id}/followers")]
        public async Task<IActionResult> RemoveFollower([FromRoute] Guid id, [FromBody] FollowerRequest follower)
        {
            bool result = await UserService.RemoveFollower(id, follower.Id);

            if (!result)
                return BadRequest();

            return Ok();
        }
    }
}
