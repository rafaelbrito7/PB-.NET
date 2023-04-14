﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository;

#nullable disable

namespace Repository.Migrations
{
    [DbContext(typeof(PBContext))]
    partial class PBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "content");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "postId");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "userId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");

                    b.HasAnnotation("Relational:JsonPropertyName", "comments");
                });

            modelBuilder.Entity("Entities.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "created_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "description");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "photoURL");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "userId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");

                    b.HasAnnotation("Relational:JsonPropertyName", "posts");
                });

            modelBuilder.Entity("Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "firstName");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "lastname");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "password");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "photoURL");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasAnnotation("Relational:JsonPropertyName", "userFollower");
                });

            modelBuilder.Entity("Entities.UsersFollowers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserFollowedId")
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "userFollowedId");

                    b.Property<Guid>("UserFollowerId")
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "userFollowerId");

                    b.HasKey("Id");

                    b.HasIndex("UserFollowedId");

                    b.HasIndex("UserFollowerId");

                    b.HasIndex("UserFollowedId", "UserFollowerId")
                        .IsUnique();

                    b.ToTable("UsersFollowers");

                    b.HasAnnotation("Relational:JsonPropertyName", "following");
                });

            modelBuilder.Entity("Entities.Comment", b =>
                {
                    b.HasOne("Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.Post", b =>
                {
                    b.HasOne("Entities.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.UsersFollowers", b =>
                {
                    b.HasOne("Entities.User", "UserFollowed")
                        .WithMany("Followers")
                        .HasForeignKey("UserFollowedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.User", "UserFollower")
                        .WithMany("Following")
                        .HasForeignKey("UserFollowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserFollowed");

                    b.Navigation("UserFollower");
                });

            modelBuilder.Entity("Entities.Post", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Entities.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Followers");

                    b.Navigation("Following");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
