using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDoctors.Data.Migrations
{
    public partial class DescriptionToDocto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Doctors");
        }
    }
}
