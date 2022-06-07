using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FBT.WebAPI.Data.Migrations
{
    public partial class ModifiedOnColumnRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "RecurringIncomes");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "RecurringExpenses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "RecurringIncomes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "RecurringExpenses",
                type: "datetime2",
                nullable: true);
        }
    }
}
