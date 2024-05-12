using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaJob.Migrations
{
    public partial class db14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Requirements",
                table: "Jobs",
                newName: "RequirementSkill");

            migrationBuilder.AddColumn<string>(
                name: "EmploymenType",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequirementLevel",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmploymenType",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "RequirementLevel",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "RequirementSkill",
                table: "Jobs",
                newName: "Requirements");
        }
    }
}
