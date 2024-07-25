using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSellers.Migrations
{
    public partial class AddAppUsersToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "288d3de9-eeff-4058-9b06-643e583f90bc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce1d7f8a-7611-420b-85a5-2cd4ec29cb32");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "27b63310-550b-401b-b101-95b9afdff9e8", "2b70e8c5-ccc5-432b-99ba-ba8173be531d", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4b1ce9df-fa3b-4a9b-b4fe-a62379a73941", "ae02ae2c-1571-4e7e-a8e6-3b6986d618bc", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27b63310-550b-401b-b101-95b9afdff9e8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b1ce9df-fa3b-4a9b-b4fe-a62379a73941");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "288d3de9-eeff-4058-9b06-643e583f90bc", "1d48986a-fee1-4a24-a1c0-8d18ef3a5dde", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ce1d7f8a-7611-420b-85a5-2cd4ec29cb32", "18120e81-a8c7-4519-a549-c515c07358b1", "Admin", "ADMIN" });
        }
    }
}
