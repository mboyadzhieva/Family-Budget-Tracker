using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FBT.WebAPI.Data.Migrations
{
    public partial class FixAuditInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Expenses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Incomes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Expenses",
                type: "datetime2",
                nullable: true);
        }
    }
}
