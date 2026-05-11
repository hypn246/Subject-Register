using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtensionDKM.Migrations
{
    /// <inheritdoc />
    public partial class Updaterelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassroomId",
                table: "Assigns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assigns_ClassroomId",
                table: "Assigns",
                column: "ClassroomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assigns_Classrooms_ClassroomId",
                table: "Assigns",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assigns_Classrooms_ClassroomId",
                table: "Assigns");

            migrationBuilder.DropIndex(
                name: "IX_Assigns_ClassroomId",
                table: "Assigns");

            migrationBuilder.DropColumn(
                name: "ClassroomId",
                table: "Assigns");
        }
    }
}
