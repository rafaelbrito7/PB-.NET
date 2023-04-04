using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CommentRepository : ICommentRepository
    {
        public PBContext Context { get; set; }

        public CommentRepository(PBContext context)
        {
            Context = context;
        }

        public async Task CreateComment(Comment comment)
        {
            try
            {
                Context.Comments.Add(comment);
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task UpdateComment(Comment comment)
        {
            try
            {
                Context.Comments.Update(comment);
                await Context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task RemoveComment(Comment comment)
        {
            try
            {
                Context.Comments.Remove(comment);
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Comment> GetById(Guid id)
        {

            return await Context.Comments.FirstOrDefaultAsync(comment => comment.Id == id);
        }

        public async Task<List<Comment>> GetAllCommentsOfAPost(Guid postId)
        {
            return await Context.Comments.Where(comment => comment.PostId == postId).ToListAsync();
        }
    }
}
