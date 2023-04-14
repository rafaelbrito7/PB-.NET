using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IPostRepository
    {
        PBContext Context { get; set; }

        Task CreatePost(Post post);

        Task UpdatePost(Post post);

        Task RemovePost(Post post);

        Task<Post> GetById(Guid id);

        Task<List<Post>> GetAllPostsOfAUser(Guid userId);

        Task<List<Post>> GetFeed(User user);
    }
}
