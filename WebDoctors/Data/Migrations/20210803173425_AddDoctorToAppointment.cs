using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDoctors.Data.Migrations
{
    public partial class AddDoctorToAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_PersonId1",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Specializations_SpecializationId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "SpecializationId",
                table: "Appointments",
                newName: "DoctorId");

            migrationBuilder.RenameColumn(
                name: "PersonId1",
                table: "Appointments",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_SpecializationId",
                table: "Appointments",
                newName: "IX_Appointments_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_PersonId1",
                table: "Appointments",
                newName: "IX_Appointments_PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_PatientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Appointments",
                newName: "PersonId1");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Appointments",
                newName: "SpecializationId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                newName: "IX_Appointments_PersonId1");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                newName: "IX_Appointments_SpecializationId");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_PersonId1",
                table: "Appointments",
                column: "PersonId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Specializations_SpecializationId",
                table: "Appointments",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
