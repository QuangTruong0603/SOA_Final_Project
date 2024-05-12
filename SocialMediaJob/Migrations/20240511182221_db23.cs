using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaJob.Migrations
{
    public partial class db23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsersUserID",
                table: "Connections",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Connections_UsersUserID",
                table: "Connections",
                column: "UsersUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_Users_UsersUserID",
                table: "Connections",
                column: "UsersUserID",
                principalTable: "Users",
                principalColumn: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_Users_UsersUserID",
                table: "Connections");

            migrationBuilder.DropIndex(
                name: "IX_Connections_UsersUserID",
                table: "Connections");

            migrationBuilder.DropColumn(
                name: "UsersUserID",
                table: "Connections");
        }
    }
}
