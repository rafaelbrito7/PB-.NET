using Entities;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        public ICommentService CommentService { get; set; }
        public IPostService PostService { get; set; }

        public CommentController(ICommentService commentService, IPostService postService)
        {
            CommentService = commentService;
            PostService = postService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var comment = await CommentService.GetById(id);

            if (comment == null)
                return BadRequest();

            return Ok(comment);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCommentsOfAPost(Guid postId)
        {
            var post = await PostService.GetById(postId);

            if (post == null)
                return BadRequest();

            return Ok(await CommentService.GetAllCommentsOfAPost(postId));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CommentRequest commentReq)
        {
            Guid id = new Guid();

            if (commentReq.UserId == null || commentReq.PostId == null)
                return BadRequest();

            Comment commentRes = await CommentService.CreateComment(id, commentReq.UserId, commentReq.PostId, commentReq.Content);

            if (commentRes == null)
                return BadRequest();

            return Ok();
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool result = await CommentService.RemoveComment(id);

            if (!result)
                return BadRequest();

            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromQuery] Guid id, [FromBody] string content)
        {
            var commentFound = await CommentService.GetById(id);
            var newComment = commentFound;

            if (String.IsNullOrEmpty(content))
                return BadRequest();

            newComment.Content = content;

            bool result = await CommentService.UpdateComment(newComment);

            if (!result)
                return BadRequest();

            return Ok();
        }
    }
}
