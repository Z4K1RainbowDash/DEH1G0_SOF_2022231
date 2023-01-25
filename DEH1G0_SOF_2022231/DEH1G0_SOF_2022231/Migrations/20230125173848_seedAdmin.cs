using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DEH1G0_SOF_2022231.Migrations
{
    public partial class seedAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2", null, "Normal User", "NORMAL USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoContentType", "PhotoData", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4d401792-f405-4802-bb39-4ed0841414ee", 0, "1f6a5ea8-ff03-4be1-907b-3f8245959ee2", "AppUser", "admin@admin.com", true, "Admin", "Admin", false, null, null, "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEET8KF+M90Ahbl1di/KZmjZ+q0v6+Q8+btt+zvAWx0745Y2UoOGAyM0BkJjpf66rng==", null, false, null, null, "b48acaaf-16e6-4f2b-92b8-684b4637d7ac", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "4d401792-f405-4802-bb39-4ed0841414ee" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "4d401792-f405-4802-bb39-4ed0841414ee" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d401792-f405-4802-bb39-4ed0841414ee");
        }
    }
}
