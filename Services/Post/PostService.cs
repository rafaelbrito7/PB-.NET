using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PostService : IPostService
    {
        public IPostRepository PostRepository { get; set; }
        public ICommentService CommentService { get; set; }

        public PostService(IPostRepository postRepository, ICommentService commentService)
        {
            PostRepository = postRepository;
            CommentService = commentService;
        }

        public async Task<List<PostResponse>> BuildFeed(Guid userId)
        {
            List<Post> posts = await PostRepository.BuildFeed(userId);
            List<PostResponse> postsResponse = new List<PostResponse>();

            foreach (Post post in posts)
            {
                PostResponse postResponse = new PostResponse
                {
                    Description = post.Description,
                    Id = post.Id,
                    UserId = post.UserId,
                    PhotoUrl = post.PhotoUrl,
                    Comments = await CommentService.GetAllCommentsOfAPost(post.Id)
                };
                postsResponse.Add(postResponse);
            }

            return postsResponse;
        }

        public async Task<Post> CreatePost(Guid id, Guid userId, string photoUrl, string description)
        {
            Post post = new Post(id, userId, photoUrl, description);

            await PostRepository.CreatePost(post);
            return post;
        }

        public async Task<Post> GetById(Guid id)
        {
            return await PostRepository.GetById(id);
        }

        public async Task<List<PostResponse>> GetAllPostsOfAUser(Guid userId)
        {
            List<Post> posts = await PostRepository.GetAllPostsOfAUser(userId);
            List<PostResponse> postsResponse = new List<PostResponse>();

            foreach (Post post in posts)
            {
                PostResponse postResponse = new PostResponse
                {
                    Description = post.Description,
                    Id = post.Id,
                    UserId = post.UserId,
                    PhotoUrl = post.PhotoUrl,
                    Comments = await CommentService.GetAllCommentsOfAPost(post.Id)
                };
                postsResponse.Add(postResponse);
            }

            return postsResponse;
        }

        public async Task<bool> UpdatePost(Post post)
        {
            try
            {
                await PostRepository.UpdatePost(post);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> RemovePost(Guid id)
        {
            try
            {
                var post = await PostRepository.GetById(id);

                await PostRepository.RemovePost(post);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
