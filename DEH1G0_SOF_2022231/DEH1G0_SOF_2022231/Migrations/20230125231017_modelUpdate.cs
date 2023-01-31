using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DEH1G0_SOF_2022231.Migrations
{
    public partial class modelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "4d401792-f405-4802-bb39-4ed0841414ee" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d401792-f405-4802-bb39-4ed0841414ee");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Torrents");

            migrationBuilder.DropColumn(
                name: "Downloads",
                table: "Torrents");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Torrents");

            migrationBuilder.DropColumn(
                name: "Leechers",
                table: "Torrents");

            migrationBuilder.DropColumn(
                name: "Seeders",
                table: "Torrents");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Torrents");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoContentType", "PhotoData", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ca47f611-7ede-46f5-a777-ab332c28a126", 0, "334941d9-c94b-481e-aee1-130388f1e732", "AppUser", "admin@admin.com", true, "Admin", "Admin", false, null, null, "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEPf/EZahl0z4dP9rrHtAtOAIqLBgBYyibmBSxZALbO0BZ6Fyl90tCQmt1vBeg65eUw==", null, false, null, null, "a5a697f7-c8d5-4cd5-8e8f-f2f497699f6b", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "ca47f611-7ede-46f5-a777-ab332c28a126" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "ca47f611-7ede-46f5-a777-ab332c28a126" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca47f611-7ede-46f5-a777-ab332c28a126");

            migrationBuilder.AddColumn<string>(
                name: "CreatedDateTime",
                table: "Torrents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Downloads",
                table: "Torrents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Torrents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Leechers",
                table: "Torrents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Seeders",
                table: "Torrents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Torrents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoContentType", "PhotoData", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4d401792-f405-4802-bb39-4ed0841414ee", 0, "1f6a5ea8-ff03-4be1-907b-3f8245959ee2", "AppUser", "admin@admin.com", true, "Admin", "Admin", false, null, null, "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEET8KF+M90Ahbl1di/KZmjZ+q0v6+Q8+btt+zvAWx0745Y2UoOGAyM0BkJjpf66rng==", null, false, null, null, "b48acaaf-16e6-4f2b-92b8-684b4637d7ac", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "4d401792-f405-4802-bb39-4ed0841414ee" });
        }
    }
}
