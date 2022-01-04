using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RatTracker.Migrations
{
    public partial class addadditionalschoolinformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "AppSchools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "AppSchools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SchoolFamily",
                table: "AppSchools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SchoolType",
                table: "AppSchools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "AppSchools",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "AppSchools");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "AppSchools");

            migrationBuilder.DropColumn(
                name: "SchoolFamily",
                table: "AppSchools");

            migrationBuilder.DropColumn(
                name: "SchoolType",
                table: "AppSchools");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "AppSchools");
        }
    }
}
