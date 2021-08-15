using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDoctors.Data.Migrations
{
    public partial class PriceAndIsPaidToAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Appointments",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Appointments");
        }
    }
}
