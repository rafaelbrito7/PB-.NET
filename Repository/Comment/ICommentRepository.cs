using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Repository
{
    public interface ICommentRepository
    {
        PBContext Context { get; set; }

        Task CreateComment(Comment comment);

        Task UpdateComment(Comment comment);

        Task RemoveComment(Comment comment);

        Task<Comment> GetById(Guid id);

        Task<List<Comment>> GetAllCommentsOfAPost(Guid postId);
    }
}
