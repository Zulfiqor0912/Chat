using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.Api.Migrations
{
    /// <inheritdoc />
    public partial class role_e : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Bio", "CreatedDateTime", "FirsName", "Gender", "LastName", "PasswrodHash", "ProfilePhotoData", "Role", "Status", "Username" },
                values: new object[] { new Guid("32858c6c-652b-4389-97d2-27598cb259da"), (byte)0, "", new DateTime(2026, 9, 16, 18, 46, 42, 911, DateTimeKind.Utc).AddTicks(3988), "Admin", "MALE", "Admin", "AQAAAAIAAYagAAAAEJ52KJsWcHNY/9051jB56y/pQV3UtmX7X1Q5rX8Zm47Zop+b1qgp9ZzsGtdz7Ush2Q==", null, 0, 1, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("32858c6c-652b-4389-97d2-27598cb259da"));
        }
    }
}
