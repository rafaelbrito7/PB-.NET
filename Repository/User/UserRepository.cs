using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        public PBContext Context { get; set; }

        public UserRepository(PBContext context)
        {
            Context = context;
        }

        public async Task CreateUser(Entities.User user)
        {
            try
            {
                await Context.Users.AddAsync(user);
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task RemoveUser(User user)
        {
            try
            {
                var userDb = Context.Users.Single(u => u.Id == user.Id);
                var followers = Context.UsersFollowers.Where(u => u.UserFollowerId == user.Id || u.UserFollowedId == user.Id).ToList();
                var comments = Context.Comments.Where(c => c.UserId == user.Id).ToList();

                Context.Users.Remove(user);

                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task UpdateUser(User user)
        {
            try
            {
                Context.Users.Update(user);
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<User> GetByEmail(string email)
        {
            return await Context.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User> GetById(Guid id)
        {
            User user = await Context.Users
            .Include(user => user.Followers)
                .ThenInclude(user => user.UserFollower)
                
            .Include(user => user.Following)
                .ThenInclude(user => user.UserFollowed)

            .Include(user => user.Posts)
            .FirstOrDefaultAsync(user => user.Id == id);

            return user;
        }

        public async Task<UsersFollowers> GetUsersFollowersById(Guid id)
        {
            UsersFollowers usersFollowers = await Context.UsersFollowers.FirstOrDefaultAsync(uf => uf.Id == id);
            return usersFollowers;
        }

        public async Task<List<User>> GetAll()
        {
            return await Context.Users.ToListAsync();
        }

        public async Task<List<User>> GetFollowers(User user)
        {
            user.Followers = await Context.UsersFollowers.Where(uf => uf.UserFollowedId == user.Id).ToListAsync();

            List<User> followers = new List<User>();

            foreach (UsersFollowers u in user.Followers)
            {
                Entities.User userFound = await Context.Users.FirstOrDefaultAsync(user => user.Id == u.UserFollowerId);
                followers.Add(new User
                {
                    Id = userFound.Id,
                    FirstName = userFound.FirstName,
                    Lastname = userFound.Lastname,
                    Email = userFound.Email,
                    PhotoUrl = userFound.PhotoUrl
                });
            }

            return followers;
        }

        public async Task<List<User>> GetNotFollowing(User user)
        {
            var followedUserIds = user.Following.Select(f => f.UserFollowedId);

            var notFollowedUsers = await Context.Users
                .Where(u => u.Id != user.Id && !followedUserIds.Contains(u.Id))
                .ToListAsync();

            return notFollowedUsers;
        }

        public async Task AddFollower(UsersFollowers userFollower)
        {
            try
            {
                await Context.UsersFollowers.AddAsync(userFollower);
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task RemoveFollower(UsersFollowers userFollower)
        {
            try
            {
                Context.UsersFollowers.Remove(userFollower);
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
