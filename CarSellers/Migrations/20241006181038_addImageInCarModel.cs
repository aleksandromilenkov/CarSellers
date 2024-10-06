using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSellers.Migrations
{
    public partial class addImageInCarModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36092d0d-4ce9-4f61-89a0-a164ca83f502");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58b819ea-c95f-4b25-8648-e0617854bb12");

            migrationBuilder.AddColumn<string>(
                name: "CarImage",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "765a9086-040e-4e21-b741-0f44e52b98dc", "0cdcffde-0a62-4e4e-8845-9a681fd740fd", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c2f920e7-f360-4240-b0a1-9558a837c79d", "411c07a9-c5b8-4b77-87dd-e10b2fddc904", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "765a9086-040e-4e21-b741-0f44e52b98dc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c2f920e7-f360-4240-b0a1-9558a837c79d");

            migrationBuilder.DropColumn(
                name: "CarImage",
                table: "Cars");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "36092d0d-4ce9-4f61-89a0-a164ca83f502", "f8fa83c6-cb4c-410e-bf64-9a87ca3a6226", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "58b819ea-c95f-4b25-8648-e0617854bb12", "3d6fdcee-819d-4479-b2fa-bc6455b020df", "Admin", "ADMIN" });
        }
    }
}
