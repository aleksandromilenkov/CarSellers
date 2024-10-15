using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSellers.Migrations
{
    public partial class AddingUserProfilePhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "765a9086-040e-4e21-b741-0f44e52b98dc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c2f920e7-f360-4240-b0a1-9558a837c79d");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2e9d1889-cabc-4e7f-97b0-f5317c15dd0e", "64d8db3e-1672-4677-87a4-f3617c04c74a", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "59b2d8a6-0a29-4ea3-a7b7-8dec84a4a9f2", "65cf7952-e291-40d9-bdaa-9699990189aa", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e9d1889-cabc-4e7f-97b0-f5317c15dd0e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59b2d8a6-0a29-4ea3-a7b7-8dec84a4a9f2");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "765a9086-040e-4e21-b741-0f44e52b98dc", "0cdcffde-0a62-4e4e-8845-9a681fd740fd", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c2f920e7-f360-4240-b0a1-9558a837c79d", "411c07a9-c5b8-4b77-87dd-e10b2fddc904", "User", "USER" });
        }
    }
}
