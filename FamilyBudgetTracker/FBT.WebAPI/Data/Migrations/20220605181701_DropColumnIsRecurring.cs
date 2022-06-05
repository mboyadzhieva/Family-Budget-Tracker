using Microsoft.EntityFrameworkCore.Migrations;

namespace FBT.WebAPI.Data.Migrations
{
    public partial class DropColumnIsRecurring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRecurring",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "IsRecurring",
                table: "Expenses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRecurring",
                table: "Incomes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRecurring",
                table: "Expenses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
