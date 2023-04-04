using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Mapping
{
    public class UsersFollowersMapping : IEntityTypeConfiguration<UsersFollowers>
    {
        public void Configure(EntityTypeBuilder<UsersFollowers> builder)
        {
            builder.HasKey(entity => new { entity.UserId, entity.FollowerId });
            builder
                .HasOne<User>(entity => entity.UserOrFollower)
                .WithMany(entity => entity.Followers)
                .HasForeignKey(entity => entity.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasOne<User>(entity => entity.UserOrFollower)
                .WithMany(entity => entity.Followers)
                .HasForeignKey(entity => entity.FollowerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
