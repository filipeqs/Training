using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDoctors.Data.Migrations
{
    public partial class AddDayOfTheWeekName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DayOfTheWeekName",
                table: "ScheduleTimes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfTheWeekName",
                table: "ScheduleTimes");
        }
    }
}
