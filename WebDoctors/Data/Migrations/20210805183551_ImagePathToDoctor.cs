using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDoctors.Data.Migrations
{
    public partial class ImagePathToDoctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentVM");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Doctors");

            migrationBuilder.CreateTable(
                name: "AppointmentVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    AppointmentType = table.Column<int>(type: "int", nullable: false),
                    AppointmentTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cancelled = table.Column<bool>(type: "bit", nullable: false),
                    DiagnosisFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DietPlanFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PrescriptionFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResultsFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentVM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentVM_AspNetUsers_PatientId",
                        column: x => x.PatientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppointmentVM_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentVM_DoctorId",
                table: "AppointmentVM",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentVM_PatientId",
                table: "AppointmentVM",
                column: "PatientId");
        }
    }
}
