using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSellers.Migrations
{
    public partial class AddCarCompanyImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e9d1889-cabc-4e7f-97b0-f5317c15dd0e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59b2d8a6-0a29-4ea3-a7b7-8dec84a4a9f2");

            migrationBuilder.AddColumn<string>(
                name: "CompanyImage",
                table: "CarSellerCompanies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1e8799d1-75cd-4b02-b701-9431ec16bbd5", "7c152cb5-7018-4fe4-ab47-27a0a7b141b3", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b390957e-8b00-4d72-a37c-674561795182", "a7ae6e4b-cc2c-4e16-879e-b0e8475df304", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e8799d1-75cd-4b02-b701-9431ec16bbd5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b390957e-8b00-4d72-a37c-674561795182");

            migrationBuilder.DropColumn(
                name: "CompanyImage",
                table: "CarSellerCompanies");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2e9d1889-cabc-4e7f-97b0-f5317c15dd0e", "64d8db3e-1672-4677-87a4-f3617c04c74a", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "59b2d8a6-0a29-4ea3-a7b7-8dec84a4a9f2", "65cf7952-e291-40d9-bdaa-9699990189aa", "Admin", "ADMIN" });
        }
    }
}
