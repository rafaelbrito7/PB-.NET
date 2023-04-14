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
            builder.HasKey(x => x.Id);

            builder.HasOne<User>(uf => uf.UserFollowed)
                .WithMany(u => u.Followers)
                .HasForeignKey(uf => uf.UserFollowedId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<User>(uf => uf.UserFollower)
                .WithMany(u => u.Following)
                .HasForeignKey(uf => uf.UserFollowerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(uf => new { uf.UserFollowedId, uf.UserFollowerId })
                .IsUnique();

            builder.HasIndex(uf => uf.UserFollowerId);
            builder.HasIndex(uf => uf.UserFollowedId);
        }
    }
}
