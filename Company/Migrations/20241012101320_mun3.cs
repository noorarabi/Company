using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.Migrations
{
    /// <inheritdoc />
    public partial class mun3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeNumber",
                table: "Trains");

            migrationBuilder.RenameColumn(
                name: "TrainTime",
                table: "Trains",
                newName: "TrainingTime");

            migrationBuilder.RenameColumn(
                name: "TrainName",
                table: "Trains",
                newName: "TrainingName");

            migrationBuilder.RenameColumn(
                name: "TrainImg",
                table: "Trains",
                newName: "TrainingImg");

            migrationBuilder.RenameColumn(
                name: "TrainField",
                table: "Trains",
                newName: "TrainingField");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Trains",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Trains",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "TrainingDate",
                table: "Trains",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "TrainingDate",
                table: "Trains");

            migrationBuilder.RenameColumn(
                name: "TrainingTime",
                table: "Trains",
                newName: "TrainTime");

            migrationBuilder.RenameColumn(
                name: "TrainingName",
                table: "Trains",
                newName: "TrainName");

            migrationBuilder.RenameColumn(
                name: "TrainingImg",
                table: "Trains",
                newName: "TrainImg");

            migrationBuilder.RenameColumn(
                name: "TrainingField",
                table: "Trains",
                newName: "TrainField");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeNumber",
                table: "Trains",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
