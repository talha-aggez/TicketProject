using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketProject.DataAccess.Migrations
{
    public partial class AddSeed1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d24d87eb-7153-4cf6-9d49-cf5e11478836");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "CompanyId", "FirstName", "LastName" },
                values: new object[] { "698d3bd9-7fd8-42dd-be71-863bdfa05a8c", 0, "612c0d1e-c87b-4d21-91d1-c8e14f835af4", "ApplicationUser", "admin@x.com", true, false, null, "ADMİN@X.COM", "ADMİN@X.COM", "AQAAAAEAACcQAAAAECwLGuAzd6MGULLQWyVHtQye/FOyZPFwZAopj3sybYxjxf2NJQS9+MlESntIDsGwKA==", null, false, "", false, "admin@x.com", null, "Admin", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "698d3bd9-7fd8-42dd-be71-863bdfa05a8c");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "CompanyId", "FirstName", "LastName" },
                values: new object[] { "d24d87eb-7153-4cf6-9d49-cf5e11478836", 0, "7d071281-ae25-45c7-bbc1-0ddd5af7e19c", "ApplicationUser", "root@gmail.com", false, false, null, null, null, "AQAAAAEAACcQAAAAEJtkpdDdevVO0Nn5ht4aLZ3uTsEOUsqaGrA2r4i28AXQDOVNwzfXcL/cnrom3d0w9A==", null, false, "5ee61568-c09b-4918-805d-dd71982c8160", false, "root@gmail.com", null, "Admin", "Root" });
        }
    }
}
