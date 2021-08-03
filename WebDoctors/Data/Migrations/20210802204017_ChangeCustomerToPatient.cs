using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDoctors.Data.Migrations
{
    public partial class ChangeCustomerToPatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_CustomerId1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "CustomerId1",
                table: "Appointments",
                newName: "PatientId1");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Appointments",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_CustomerId1",
                table: "Appointments",
                newName: "IX_Appointments_PatientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_PatientId1",
                table: "Appointments",
                column: "PatientId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_PatientId1",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "PatientId1",
                table: "Appointments",
                newName: "CustomerId1");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Appointments",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_PatientId1",
                table: "Appointments",
                newName: "IX_Appointments_CustomerId1");

            migrationBuilder.AddColumn<string>(
                name: "MyProperty",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_CustomerId1",
                table: "Appointments",
                column: "CustomerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
