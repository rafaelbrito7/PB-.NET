using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CommentService : ICommentService
    {
        public ICommentRepository CommentRepository { get; set; }

        public CommentService(ICommentRepository commentRepository)
        {
            CommentRepository = commentRepository;
        }

        public async Task<Comment> CreateComment(Guid id, Guid userId, Guid postId, string content)
        {
            var comment = new Comment(id, userId, postId, content);

            try
            {
                await CommentRepository.CreateComment(comment);
                return comment;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<Comment> GetById(Guid id)
        {
            try
            {
                return await CommentRepository.GetById(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<bool> RemoveComment(Guid id)
        {
            try
            {
                var comment = await CommentRepository.GetById(id);
                await CommentRepository.RemoveComment(comment);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> UpdateComment(Comment comment)
        {
            try
            {
                await CommentRepository.UpdateComment(comment);
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<List<CommentResponse>> GetAllCommentsOfAPost(Guid postId)
        {
            List<Comment> comments = await CommentRepository.GetAllCommentsOfAPost(postId);
            List<CommentResponse> commentsResponse = new List<CommentResponse>();

            foreach (Comment comment in comments)
            {
                CommentResponse commentResponse = new CommentResponse { Id = comment.Id, UserId = comment.UserId, PostId = comment.PostId, Content = comment.Content };
                commentsResponse.Add(commentResponse);
            }

            return commentsResponse;
        }
    }
}
