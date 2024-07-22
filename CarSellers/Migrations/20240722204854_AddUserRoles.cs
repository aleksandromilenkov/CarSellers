using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSellers.Migrations
{
    public partial class AddUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3367ac93-e081-49f0-a314-adc2e5905301", "c9a3af51-c8cd-40be-9faf-ece34447b1a1", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cc3e42ca-9f6a-4775-a842-c6a67f4aaff5", "79f772c4-fe88-4be4-8215-bd2ef87a2936", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3367ac93-e081-49f0-a314-adc2e5905301");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc3e42ca-9f6a-4775-a842-c6a67f4aaff5");
        }
    }
}
