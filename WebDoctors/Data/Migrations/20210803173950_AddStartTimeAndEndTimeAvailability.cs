using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDoctors.Data.Migrations
{
    public partial class AddStartTimeAndEndTimeAvailability : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FridayHours",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "MondayHours",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "SaturdayHours",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "SundayHours",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "ThursdayHours",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "TuesdayHours",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "WednesdayHours",
                table: "Availabilities");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "FridayEndTime",
                table: "Availabilities",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "FridayStartTime",
                table: "Availabilities",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "MondayEndTime",
                table: "Availabilities",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "MondayStartTime",
                table: "Availabilities",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SaturdayEndTime",
                table: "Availabilities",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SaturdayStartTime",
                table: "Availabilities",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SundayEndTime",
                table: "Availabilities",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SundayStartTime",
                table: "Availabilities",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ThursdayEndTime",
                table: "Availabilities",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ThursdayStartTime",
                table: "Availabilities",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TuesdayEndTime",
                table: "Availabilities",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TuesdayStartTime",
                table: "Availabilities",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "WednesdayEndTime",
                table: "Availabilities",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "WednesdayStartTime",
                table: "Availabilities",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FridayEndTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "FridayStartTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "MondayEndTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "MondayStartTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "SaturdayEndTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "SaturdayStartTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "SundayEndTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "SundayStartTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "ThursdayEndTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "ThursdayStartTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "TuesdayEndTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "TuesdayStartTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "WednesdayEndTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "WednesdayStartTime",
                table: "Availabilities");

            migrationBuilder.AddColumn<DateTime>(
                name: "FridayHours",
                table: "Availabilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "MondayHours",
                table: "Availabilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SaturdayHours",
                table: "Availabilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SundayHours",
                table: "Availabilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ThursdayHours",
                table: "Availabilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TuesdayHours",
                table: "Availabilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "WednesdayHours",
                table: "Availabilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
