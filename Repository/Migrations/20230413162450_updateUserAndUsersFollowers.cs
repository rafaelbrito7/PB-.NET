using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class updateUserAndUsersFollowers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersFollowers_Users_FollowerId",
                table: "UsersFollowers");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersFollowers_Users_UserId",
                table: "UsersFollowers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersFollowers",
                table: "UsersFollowers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "FollowerId",
                table: "UsersFollowers",
                newName: "UserFollowerId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UsersFollowers",
                newName: "UserFollowedId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersFollowers_FollowerId",
                table: "UsersFollowers",
                newName: "IX_UsersFollowers_UserFollowerId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UsersFollowers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Posts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersFollowers",
                table: "UsersFollowers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsersFollowers_UserFollowedId",
                table: "UsersFollowers",
                column: "UserFollowedId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersFollowers_UserFollowedId_UserFollowerId",
                table: "UsersFollowers",
                columns: new[] { "UserFollowedId", "UserFollowerId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersFollowers_Users_UserFollowedId",
                table: "UsersFollowers",
                column: "UserFollowedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersFollowers_Users_UserFollowerId",
                table: "UsersFollowers",
                column: "UserFollowerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersFollowers_Users_UserFollowedId",
                table: "UsersFollowers");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersFollowers_Users_UserFollowerId",
                table: "UsersFollowers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersFollowers",
                table: "UsersFollowers");

            migrationBuilder.DropIndex(
                name: "IX_UsersFollowers_UserFollowedId",
                table: "UsersFollowers");

            migrationBuilder.DropIndex(
                name: "IX_UsersFollowers_UserFollowedId_UserFollowerId",
                table: "UsersFollowers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsersFollowers");

            migrationBuilder.RenameColumn(
                name: "UserFollowerId",
                table: "UsersFollowers",
                newName: "FollowerId");

            migrationBuilder.RenameColumn(
                name: "UserFollowedId",
                table: "UsersFollowers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersFollowers_UserFollowerId",
                table: "UsersFollowers",
                newName: "IX_UsersFollowers_FollowerId");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Posts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersFollowers",
                table: "UsersFollowers",
                columns: new[] { "UserId", "FollowerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsersFollowers_Users_FollowerId",
                table: "UsersFollowers",
                column: "FollowerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersFollowers_Users_UserId",
                table: "UsersFollowers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
