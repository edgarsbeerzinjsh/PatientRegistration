using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NETApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class DoctorAndPatientPropsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "License",
                table: "Doctors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Patients",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "License",
                table: "Doctors",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
