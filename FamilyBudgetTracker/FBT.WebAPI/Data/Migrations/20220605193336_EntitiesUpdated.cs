using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FBT.WebAPI.Data.Migrations
{
    public partial class EntitiesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Incomes",
                newName: "PaymentDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Expenses",
                newName: "PaymentDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Incomes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Incomes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Incomes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Incomes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Incomes",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Expenses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Expenses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Expenses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Expenses",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "Incomes",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "Expenses",
                newName: "Date");
        }
    }
}
