using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaJob.Migrations
{
    public partial class db24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Connections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Connections_userId",
                table: "Connections",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_Users_userId",
                table: "Connections",
                column: "userId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_Users_userId",
                table: "Connections");

            migrationBuilder.DropIndex(
                name: "IX_Connections_userId",
                table: "Connections");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Connections");

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
    }
}
