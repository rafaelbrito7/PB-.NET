using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUserRepository
    {
        PBContext Context { get; set; }
        Task CreateUser(User user);
        Task UpdateUser(User user);
        Task<User> GetByEmail(string email);
        Task<User> GetById(Guid id);
        Task<List<User>> GetAll();
        Task<List<User>> GetNotFollowing(User user);
        Task AddFollower(UsersFollowers userFollower);
        Task RemoveFollower(UsersFollowers userFollower);
        Task <UsersFollowers> GetUsersFollowersById(Guid id);
        Task RemoveUser(User user);
    }
}
