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

        public async Task UpdateUser(Entities.User user)
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

        public async Task<List<Entities.User>> GetUsersByNameOrLastname(string name, string lastname)
        {
            return await Context.Users.Where(user => user.FirstName.Contains(name) || user.Lastname.Contains(lastname)).ToListAsync();
        }

        public async Task<Entities.User> GetByEmail(string email)
        {
            return await Context.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<Entities.User> GetById(Guid id)
        {
            return await Context.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<List<Entities.User>> GetAll()
        {
            return await Context.Users.ToListAsync();
        }

        public async Task<List<UserFollowersResponse>> GetFollowers(Entities.User user)
        {
            user.Followers = await Context.UsersFollowers.Where(uf => uf.UserId == user.Id).ToListAsync();

            List<UserFollowersResponse> followers = new List<UserFollowersResponse>();

            foreach (UsersFollowers u in user.Followers)
            {
                Entities.User userFound = await Context.Users.FirstOrDefaultAsync(user => user.Id == u.FollowerId);
                followers.Add(new UserFollowersResponse
                {
                    Id = userFound.Id,
                    FirstName = userFound.FirstName,
                    Lastname = userFound.Lastname,
                    Email = userFound.Email,
                    Status = userFound.Status,
                    PhotoURL = userFound.PhotoURL
                });
            }

            return followers;
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
