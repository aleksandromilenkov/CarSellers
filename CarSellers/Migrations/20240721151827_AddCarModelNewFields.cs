using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSellers.Migrations
{
    public partial class AddCarModelNewFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarColor",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CarType",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarColor",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarType",
                table: "Cars");
        }
    }
}
