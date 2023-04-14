using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserService
    {
        IUserRepository UserRepository { get; set; }

        Task<User> CreateUser(Guid Id, User user);

        Task<User> GetUserByEmail(string email);

        Task<User> GetByEmail(string email);

        Task<User> GetById(Guid id);

        Task<List<User>> GetAll();

        Task<bool> UpdateUser(Guid id, string firstName, string lastname, string email, string password, string photoURL);

        Task<bool> RemoveUser(User user);

        Task<List<User>> GetNotFollowing(Guid userId);

        Task<bool> AddFollower(Guid userId, Guid userFollowedId);

        Task<bool> RemoveFollower(Guid userId, Guid userFollowedId);
    }
}
