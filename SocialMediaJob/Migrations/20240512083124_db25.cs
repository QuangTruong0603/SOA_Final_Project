using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaJob.Migrations
{
    public partial class db25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Depcription",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Depcription",
                table: "Jobs");
        }
    }
}
