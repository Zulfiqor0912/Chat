using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.Api.Migrations
{
    /// <inheritdoc />
    public partial class Edit_ProfilePhotoDate_of_Users_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePhoto",
                table: "Users");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePhotoData",
                table: "Users",
                type: "bytea",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePhotoData",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhoto",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
