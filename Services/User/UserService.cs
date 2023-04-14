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

        public async Task<User> CreateUser(Guid Id, User user)
        {
            User newUser = new User
            {
                Id = Id,
                FirstName = user.FirstName,
                Lastname = user.Lastname,
                Email = user.Email,
                Password = user.Password,
                PhotoUrl = user.PhotoUrl,
            };

            var checkIfUserExists = await UserRepository.GetByEmail(user.Email);

            if(checkIfUserExists != null)
            {
                return null;
            }

            await UserRepository.CreateUser(newUser);
            User userResponse = newUser;
            return userResponse;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            User user = await UserRepository.GetByEmail(email);
            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            User user = await UserRepository.GetByEmail(email);
            return user;
        }

        public async Task<User> GetById(Guid id)
        {
            User user = await UserRepository.GetById(id);
            return user;
        }

        public async Task<List<User>> GetAll()
        {
            List<User> users = await UserRepository.GetAll();
            List<User> usersResponse = new List<User>();

            foreach (User u in users)
            {
                usersResponse.Add(new User
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    Lastname = u.Lastname,
                    Email = u.Email,
                    PhotoUrl = u.PhotoUrl
                });
            }

            return usersResponse;
        }

        public async Task<bool> UpdateUser(Guid id, string firstName, string lastName, string email, string password, string photoUrl)
        {
            try
            {
                User checkEmailsAvailability = await UserRepository.GetByEmail(email);

                if (checkEmailsAvailability != null && checkEmailsAvailability.Id != id)
                {
                    return false;
                }

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
                    user.PhotoUrl = photoUrl;
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
                await UserRepository.RemoveUser(user);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<List<User>> GetNotFollowing(Guid userId)
        {
            try
            {
                User user = await GetById(userId);

                if (user == null)
                    return null;

                List<User> followers = await UserRepository.GetNotFollowing(user);
                return followers;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<bool> AddFollower(Guid userId, Guid userFollowedId)
        {
            if (userId == userFollowedId)
                return false;

            User user = await UserRepository.GetById(userId);
            User follower = await UserRepository.GetById(userFollowedId);

            if (user == null || follower == null)
                return false;

            Guid Id = new Guid();

            UsersFollowers userFollower = new UsersFollowers { Id = Id, UserFollowedId = userFollowedId, UserFollowerId = userId };
            await UserRepository.AddFollower(userFollower);
            return true;
        }

        public async Task<bool> RemoveFollower(Guid userId, Guid userFollowedId)
        {
            var user = await UserRepository.GetById(userId);
            var userFollowed = await UserRepository.GetById(userFollowedId);


            if(user == null || userFollowed == null)
            {
                return false;
            }

            var usersFollowers = user.Following.FirstOrDefault(uf => uf.UserFollowerId == userId && uf.UserFollowedId == userFollowedId);

            if (usersFollowers == null)
            {
                return false;
            }

            await UserRepository.RemoveFollower(usersFollowers);
            return true;
        }
    }
}
