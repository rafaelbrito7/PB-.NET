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

        public async Task<List<Post>> BuildFeed(Guid userId)
        {
            List<UsersFollowers> followers = await Context.UsersFollowers.Where(uf => uf.UserId == userId).ToListAsync();
            List<Guid> followersGuids = new List<Guid>();
            foreach (UsersFollowers uf in followers)
            {
                followersGuids.Add(uf.FollowerId);
            }
            return await Context.Posts.Where(post => post.UserId != userId && followersGuids.Any(followerGuid => followerGuid == post.UserId)).ToListAsync();
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
            return await Context.Posts.FirstOrDefaultAsync(post => post.Id == id);
        }

        public async Task<List<Post>> GetAllPostsOfAUser(Guid userId)
        {
            return await Context.Posts.Where(post => post.UserId == userId).ToListAsync();
        }
    }
}
