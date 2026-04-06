using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtensionDKM.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Discriminator", "MajorId", "Name", "Password", "Role", "Username" },
                values: new object[] { 1, "User", null, "Admin", "123", 0, "admin" });
        }
    }
}
