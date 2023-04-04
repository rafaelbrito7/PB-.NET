using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Mapping
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .HasOne<User>(entity => entity.User)
                .WithMany(entity => entity.Comments)
                .HasForeignKey(entity => entity.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne<Post>(entity => entity.Post)
                .WithMany(entity => entity.Comments)
                .HasForeignKey(entity => entity.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
