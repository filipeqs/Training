using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDoctors.Data.Migrations
{
    public partial class AddConsultationFeeAndConsultationTimeAppointmentDayAndAppointmentTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "ConsultationFee",
                table: "Doctors",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ConsultationTime",
                table: "Availabilities",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "AppointmentTime",
                table: "Appointments",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDay",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsultationFee",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "ConsultationTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "AppointmentDay",
                table: "Appointments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AppointmentTime",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }
    }
}
