using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PostRepository : IPostRepository
    {
        public PBContext Context { get; set; }

        public PostRepository(PBContext context)
        {
            Context = context;
        }

        public async Task CreatePost(Post post)
        {
            try
            {
                Context.Posts.Add(post);
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task UpdatePost(Post post)
        {
            try
            {
                Context.Posts.Update(post);
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task RemovePost(Post post)
        {
            try
            {
                Context.Posts.Remove(post);
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Post> GetById(Guid id)
        {
            var post = await Context.Posts
                .Include(post => post.User)
                .Include(post => post.Comments)
                .ThenInclude(comment => comment.User)
                .FirstOrDefaultAsync(post => post.Id == id);


            return post;
        }

        public async Task<List<Post>> GetAllPostsOfAUser(Guid userId)
        {
            return await Context.Posts.Where(post => post.UserId == userId).ToListAsync();
        }

        public async Task<List<Post>> GetFeed(User user)
        {
            var followedUserIds = user.Following.Select(f => f.UserFollowedId);
            var feed = await Context.Posts
                .Where(post => followedUserIds.Contains(post.UserId))
                .OrderByDescending(post => post.CreatedAt)
                .ToListAsync();

            return feed;
        }
    }
}
