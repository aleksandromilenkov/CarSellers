using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSellers.Migrations
{
    public partial class NewCarColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3367ac93-e081-49f0-a314-adc2e5905301");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc3e42ca-9f6a-4775-a842-c6a67f4aaff5");

            migrationBuilder.AddColumn<int>(
                name: "CarRegistration",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0de6bb37-85cb-4750-a9b5-a0fd5a6cbbc8", "d1972e3b-c7ce-4bdd-86d9-ca1a542ebe0e", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b2934938-9c16-4ad5-8649-01b8fb32b461", "0a951bdd-d605-4205-bbf8-b2135fc9eaa1", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0de6bb37-85cb-4750-a9b5-a0fd5a6cbbc8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2934938-9c16-4ad5-8649-01b8fb32b461");

            migrationBuilder.DropColumn(
                name: "CarRegistration",
                table: "Cars");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3367ac93-e081-49f0-a314-adc2e5905301", "c9a3af51-c8cd-40be-9faf-ece34447b1a1", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cc3e42ca-9f6a-4775-a842-c6a67f4aaff5", "79f772c4-fe88-4be4-8215-bd2ef87a2936", "User", "USER" });
        }
    }
}
