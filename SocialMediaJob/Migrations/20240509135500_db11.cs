using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaJob.Migrations
{
    public partial class db11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Users_UsersUserID",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Post",
                table: "Post");

            migrationBuilder.RenameTable(
                name: "Post",
                newName: "Posts");

            migrationBuilder.RenameColumn(
                name: "Contentfie",
                table: "Posts",
                newName: "Contentfile");

            migrationBuilder.RenameIndex(
                name: "IX_Post_UsersUserID",
                table: "Posts",
                newName: "IX_Posts_UsersUserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UsersUserID",
                table: "Posts",
                column: "UsersUserID",
                principalTable: "Users",
                principalColumn: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UsersUserID",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "Post");

            migrationBuilder.RenameColumn(
                name: "Contentfile",
                table: "Post",
                newName: "Contentfie");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_UsersUserID",
                table: "Post",
                newName: "IX_Post_UsersUserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Post",
                table: "Post",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Users_UsersUserID",
                table: "Post",
                column: "UsersUserID",
                principalTable: "Users",
                principalColumn: "UserID");
        }
    }
}
