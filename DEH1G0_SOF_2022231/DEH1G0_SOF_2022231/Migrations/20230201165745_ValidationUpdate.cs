using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DEH1G0_SOF_2022231.Migrations
{
    public partial class ValidationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "ca47f611-7ede-46f5-a777-ab332c28a126" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca47f611-7ede-46f5-a777-ab332c28a126");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoContentType", "PhotoData", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c66cf706-ab2b-4a87-befa-9c0aa5cc034c", 0, "aba62781-2f01-4b29-91ac-5592967a9a37", "AppUser", "admin@admin.com", true, "Admin", "Admin", false, null, null, "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEHT0ZQaAScBebgd5d1vOvhfEkkej+yJd98UQaI1BlH/P0SuvCmFCW0vioc5q1na7Fg==", null, false, null, null, "32603868-3586-4d29-ad52-7999bb855fb2", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "c66cf706-ab2b-4a87-befa-9c0aa5cc034c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "c66cf706-ab2b-4a87-befa-9c0aa5cc034c" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c66cf706-ab2b-4a87-befa-9c0aa5cc034c");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoContentType", "PhotoData", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ca47f611-7ede-46f5-a777-ab332c28a126", 0, "334941d9-c94b-481e-aee1-130388f1e732", "AppUser", "admin@admin.com", true, "Admin", "Admin", false, null, null, "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEPf/EZahl0z4dP9rrHtAtOAIqLBgBYyibmBSxZALbO0BZ6Fyl90tCQmt1vBeg65eUw==", null, false, null, null, "a5a697f7-c8d5-4cd5-8e8f-f2f497699f6b", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "ca47f611-7ede-46f5-a777-ab332c28a126" });
        }
    }
}
