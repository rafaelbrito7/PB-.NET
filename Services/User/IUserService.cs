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

        Task<UserResponse> CreateUser(Guid Id, string firstName, string lastName, string email, string password, string photoUrl, bool status);

        Task<List<UserResponse>> GetUsersByNameOrLastname(string name, string lastname);

        Task<User> GetUserByEmail(string email);

        Task<UserResponse> GetByEmail(string email);

        Task<User> GetUserById(Guid id);

        Task<UserResponse> GetById(Guid id);

        Task<List<UserResponse>> GetAll();

        Task<bool> UpdateUser(Guid id, string firstName, string lastName, string email, string password, string photoUrl);

        Task<bool> RemoveUser(User user);

        Task<List<UserFollowersResponse>> GetFollowers(Guid userId);

        Task<bool> AddFollower(Guid userId, Guid followerId);

        Task<bool> RemoveFollower(Guid userId, Guid followerId);
    }
}
