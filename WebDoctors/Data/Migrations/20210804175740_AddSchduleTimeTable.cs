using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDoctors.Data.Migrations
{
    public partial class AddSchduleTimeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Availabilities");

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsultationTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    DayOfTheWeek = table.Column<int>(type: "int", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleTimes_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_DoctorId",
                table: "Schedules",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTimes_ScheduleId",
                table: "ScheduleTimes",
                column: "ScheduleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleTimes");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.CreateTable(
                name: "Availabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsultationTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    Friday = table.Column<bool>(type: "bit", nullable: false),
                    FridayEndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    FridayStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Monday = table.Column<bool>(type: "bit", nullable: false),
                    MondayEndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    MondayStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Saturday = table.Column<bool>(type: "bit", nullable: false),
                    SaturdayEndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    SaturdayStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Sunday = table.Column<bool>(type: "bit", nullable: false),
                    SundayEndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    SundayStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Thursday = table.Column<bool>(type: "bit", nullable: false),
                    ThursdayEndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ThursdayStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Tuesday = table.Column<bool>(type: "bit", nullable: false),
                    TuesdayEndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    TuesdayStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Wednesday = table.Column<bool>(type: "bit", nullable: false),
                    WednesdayEndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    WednesdayStartTime = table.Column<TimeSpan>(type: "time", nullable: false)
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
        }
    }
}
