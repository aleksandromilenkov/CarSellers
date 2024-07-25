using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSellers.Migrations
{
    public partial class NewCarColumnsAndNewJoinTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0de6bb37-85cb-4750-a9b5-a0fd5a6cbbc8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2934938-9c16-4ad5-8649-01b8fb32b461");

            migrationBuilder.AddColumn<int>(
                name: "CarOwner",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AppUserCars",
                columns: table => new
                {
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserCars", x => new { x.AppUserId, x.CarId });
                    table.ForeignKey(
                        name: "FK_AppUserCars_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserCars_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "288d3de9-eeff-4058-9b06-643e583f90bc", "1d48986a-fee1-4a24-a1c0-8d18ef3a5dde", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ce1d7f8a-7611-420b-85a5-2cd4ec29cb32", "18120e81-a8c7-4519-a549-c515c07358b1", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserCars_CarId",
                table: "AppUserCars",
                column: "CarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserCars");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "288d3de9-eeff-4058-9b06-643e583f90bc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce1d7f8a-7611-420b-85a5-2cd4ec29cb32");

            migrationBuilder.DropColumn(
                name: "CarOwner",
                table: "Cars");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0de6bb37-85cb-4750-a9b5-a0fd5a6cbbc8", "d1972e3b-c7ce-4bdd-86d9-ca1a542ebe0e", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b2934938-9c16-4ad5-8649-01b8fb32b461", "0a951bdd-d605-4205-bbf8-b2135fc9eaa1", "Admin", "ADMIN" });
        }
    }
}
