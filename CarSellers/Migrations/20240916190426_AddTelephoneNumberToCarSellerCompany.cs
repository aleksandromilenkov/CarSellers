using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSellers.Migrations
{
    public partial class AddTelephoneNumberToCarSellerCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01f27fa1-d89e-4ba6-b553-c170d08277b4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9c355a2-e98e-4167-a38a-09b60266b03c");

            migrationBuilder.AddColumn<string>(
                name: "TelephoneNumber",
                table: "CarSellerCompanies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "36092d0d-4ce9-4f61-89a0-a164ca83f502", "f8fa83c6-cb4c-410e-bf64-9a87ca3a6226", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "58b819ea-c95f-4b25-8648-e0617854bb12", "3d6fdcee-819d-4479-b2fa-bc6455b020df", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36092d0d-4ce9-4f61-89a0-a164ca83f502");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58b819ea-c95f-4b25-8648-e0617854bb12");

            migrationBuilder.DropColumn(
                name: "TelephoneNumber",
                table: "CarSellerCompanies");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "01f27fa1-d89e-4ba6-b553-c170d08277b4", "732ff868-2747-4927-bf72-59ccd68c0ed0", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e9c355a2-e98e-4167-a38a-09b60266b03c", "a964709e-fcc6-4978-b39e-dcae955431cd", "Admin", "ADMIN" });
        }
    }
}
