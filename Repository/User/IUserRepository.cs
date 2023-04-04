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
        Task<List<User>> GetUsersByNameOrLastname(string name, string lastname);
        Task<User> GetByEmail(string email);
        Task<User> GetById(Guid id);
        Task<List<User>> GetAll();
        Task<List<UserFollowersResponse>> GetFollowers(User user);
        Task AddFollower(UsersFollowers userFollower);
        Task RemoveFollower(UsersFollowers userFollower);
    }
}
