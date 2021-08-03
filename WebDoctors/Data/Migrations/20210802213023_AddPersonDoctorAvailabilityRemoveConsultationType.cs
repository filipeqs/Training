using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDoctors.Data.Migrations
{
    public partial class AddPersonDoctorAvailabilityRemoveConsultationType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_PatientId1",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_ConsultationTypes_ConsultationTypeId",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "ConsultationTypes");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ConsultationTypeId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ConsultationTypeId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "PatientId1",
                table: "Appointments",
                newName: "PersonId1");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Appointments",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_PatientId1",
                table: "Appointments",
                newName: "IX_Appointments_PersonId1");

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    SpecializationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_AspNetUsers_PersonId1",
                        column: x => x.PersonId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doctors_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Availabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    Sunday = table.Column<bool>(type: "bit", nullable: false),
                    SundayHours = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Monday = table.Column<bool>(type: "bit", nullable: false),
                    MondayHours = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tuesday = table.Column<bool>(type: "bit", nullable: false),
                    TuesdayHours = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Wednesday = table.Column<bool>(type: "bit", nullable: false),
                    WednesdayHours = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Thursday = table.Column<bool>(type: "bit", nullable: false),
                    ThursdayHours = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Friday = table.Column<bool>(type: "bit", nullable: false),
                    FridayHours = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Saturday = table.Column<bool>(type: "bit", nullable: false),
                    SaturdayHours = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Availabilities_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_DoctorId",
                table: "Availabilities",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_PersonId1",
                table: "Doctors",
                column: "PersonId1");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecializationId",
                table: "Doctors",
                column: "SpecializationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_PersonId1",
                table: "Appointments",
                column: "PersonId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_PersonId1",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "Availabilities");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.RenameColumn(
                name: "PersonId1",
                table: "Appointments",
                newName: "PatientId1");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Appointments",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_PersonId1",
                table: "Appointments",
                newName: "IX_Appointments_PatientId1");

            migrationBuilder.AddColumn<int>(
                name: "ConsultationTypeId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ConsultationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultationTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ConsultationTypeId",
                table: "Appointments",
                column: "ConsultationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_PatientId1",
                table: "Appointments",
                column: "PatientId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_ConsultationTypes_ConsultationTypeId",
                table: "Appointments",
                column: "ConsultationTypeId",
                principalTable: "ConsultationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
