using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class updateMapping4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersFollowers_Users_FollowerId",
                table: "UsersFollowers");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersFollowers_Users_FollowerId",
                table: "UsersFollowers");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersFollowers_Users_UserId",
                table: "UsersFollowers");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersFollowers_Users_FollowerId",
                table: "UsersFollowers",
                column: "FollowerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
