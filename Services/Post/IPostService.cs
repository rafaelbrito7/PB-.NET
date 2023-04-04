using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IPostService
    {
        IPostRepository PostRepository { get; set; }

        Task<List<PostResponse>> BuildFeed(Guid userId);

        Task<Post> CreatePost(Guid Id, Guid UserId, string photoUrl, string description);

        Task<Post> GetById(Guid id);

        Task<List<PostResponse>> GetAllPostsOfAUser(Guid userId);

        Task<bool> UpdatePost(Post post);

        Task<bool> RemovePost(Guid id);
    }
}
