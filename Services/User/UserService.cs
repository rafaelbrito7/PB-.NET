using Entities;
using Repository;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        public IUserRepository UserRepository { get; set; }

        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public async Task<UserResponse> CreateUser(Guid Id, string FirstName, string Lastname, string email, string password, string photoUrl, bool status)
        {
            User user = new User
            {
                Id = Id,
                FirstName = FirstName,
                Lastname = Lastname,
                Email = email,
                Password = password,
                PhotoURL = photoUrl,
                Status = status
            };

            await UserRepository.CreateUser(user);
            UserResponse userResponse = Utils.ConvertUserToUserResponse(user);
            return userResponse;
        }

        public async Task<List<UserResponse>> GetUsersByNameOrLastname(string name, string lastname)
        {
            List<User> users = await UserRepository.GetUsersByNameOrLastname(name, lastname);
            List<UserResponse> usersResponse = new List<UserResponse>();

            foreach (User u in users)
            {
                usersResponse.Add(new UserResponse
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    Lastname = u.Lastname,
                    Email = u.Email,
                    Status = u.Status,
                    PhotoURL = u.PhotoURL
                });
            }

            return usersResponse;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            User user = await UserRepository.GetByEmail(email);
            return user;
        }

        public async Task<UserResponse> GetByEmail(string email)
        {
            User user = await UserRepository.GetByEmail(email);
            UserResponse userResponse = Utils.ConvertUserToUserResponse(user);
            return userResponse;
        }

        public async Task<User> GetUserById(Guid id)
        {
            User user = await UserRepository.GetById(id);
            return user;
        }

        public async Task<UserResponse> GetById(Guid id)
        {
            User user = await UserRepository.GetById(id);
            UserResponse userResponse = Utils.ConvertUserToUserResponse(user);
            return userResponse;
        }

        public async Task<List<UserResponse>> GetAll()
        {
            List<User> users = await UserRepository.GetAll();
            List<UserResponse> usersResponse = new List<UserResponse>();

            foreach (User u in users)
            {
                usersResponse.Add(new UserResponse
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    Lastname = u.Lastname,
                    Email = u.Email,
                    Status = u.Status,
                    PhotoURL = u.PhotoURL
                });
            }

            return usersResponse;
        }

        public async Task<bool> UpdateUser(Guid id, string firstName, string lastName, string email, string password, string photoUrl)
        {
            try
            {
                User user = await UserRepository.GetById(id);

                if (user == null)
                    return false;

                if (!string.IsNullOrEmpty(firstName))
                {
                    user.FirstName = firstName;
                }
                if (!string.IsNullOrEmpty(lastName))
                {
                    user.Lastname = lastName;
                }
                if (!string.IsNullOrEmpty(email))
                {
                    user.Email = email;
                }
                if (!string.IsNullOrEmpty(password))
                {
                    user.Password = password;
                }
                if (!string.IsNullOrEmpty(photoUrl))
                {
                    user.PhotoURL = photoUrl;
                }

                await UserRepository.UpdateUser(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveUser(User user)
        {
            try
            {
                user.Status = false;

                await UserRepository.UpdateUser(user);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<List<Entities.UserFollowersResponse>> GetFollowers(Guid userId)
        {
            try
            {
                User user = Utils.ConvertUserResponseToUser(await GetById(userId));

                if (user == null)
                    return null;

                List<UserFollowersResponse> followers = await UserRepository.GetFollowers(user);
                return followers;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<bool> AddFollower(Guid userId, Guid followerId)
        {
            if (userId == followerId)
                return false;

            User user = await UserRepository.GetById(userId);
            User follower = await UserRepository.GetById(followerId);

            if (user == null || follower == null)
                return false;

            UsersFollowers userFollower = new UsersFollowers { UserId = userId, FollowerId = followerId };
            await UserRepository.AddFollower(userFollower);
            return true;
        }

        public async Task<bool> RemoveFollower(Guid userId, Guid followerId)
        {
            User user = await UserRepository.GetById(userId);
            User follower = await UserRepository.GetById(followerId);
            if (user == null || follower == null)
                return false;

            UsersFollowers userFollower = new UsersFollowers { UserId = userId, FollowerId = followerId };
            await UserRepository.RemoveFollower(userFollower);
            return true;
        }
    }
}
