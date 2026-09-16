using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.Api.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e2b56228-a38b-46d9-a82d-a982e3ed0402"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Bio", "CreatedDateTime", "FirsName", "Gender", "LastName", "PasswrodHash", "ProfilePhotoData", "Role", "Status", "Username" },
                values: new object[] { new Guid("e2b56228-a38b-46d9-a82d-a982e3ed0402"), (byte)0, "", new DateTime(2026, 9, 16, 18, 49, 46, 688, DateTimeKind.Utc).AddTicks(9962), "Admin", "MALE", "Admin", "AQAAAAIAAYagAAAAEPYKiOHZSHatQQoKmaj+mMtHwXtYcl0tRR4wxHvbckxH2ogkDG5ZChnYRjuAh0H29g==", null, 0, 1, "admin" });
        }
    }
}
