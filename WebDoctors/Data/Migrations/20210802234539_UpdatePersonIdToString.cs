using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDoctors.Data.Migrations
{
    public partial class UpdatePersonIdToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_AspNetUsers_PersonId1",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_PersonId1",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "PersonId1",
                table: "Doctors");

            migrationBuilder.AlterColumn<string>(
                name: "PersonId",
                table: "Doctors",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "CreateDoctorVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreateDoctorVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoctorVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorVM", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_PersonId",
                table: "Doctors",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_AspNetUsers_PersonId",
                table: "Doctors",
                column: "PersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_AspNetUsers_PersonId",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "CreateDoctorVM");

            migrationBuilder.DropTable(
                name: "DoctorVM");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_PersonId",
                table: "Doctors");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonId1",
                table: "Doctors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_PersonId1",
                table: "Doctors",
                column: "PersonId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_AspNetUsers_PersonId1",
                table: "Doctors",
                column: "PersonId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
