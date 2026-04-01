using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtensionDKM.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCaptitalAttr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assigns_Courses_courseId",
                table: "Assigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Assigns_Users_userId",
                table: "Assigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Classrooms_Classroomid",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_ScoresTables_ScoreTableid",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Scores_Scoreid",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Majors_majorid",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "majorid",
                table: "Users",
                newName: "MajorId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Users_majorid",
                table: "Users",
                newName: "IX_Users_MajorId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ScoresTables",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Scores",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Majors",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Majors",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Scoreid",
                table: "Courses",
                newName: "ScoreId");

            migrationBuilder.RenameColumn(
                name: "ScoreTableid",
                table: "Courses",
                newName: "ScoreTableId");

            migrationBuilder.RenameColumn(
                name: "Classroomid",
                table: "Courses",
                newName: "ClassroomId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Courses",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_ScoreTableid",
                table: "Courses",
                newName: "IX_Courses_ScoreTableId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_Scoreid",
                table: "Courses",
                newName: "IX_Courses_ScoreId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_Classroomid",
                table: "Courses",
                newName: "IX_Courses_ClassroomId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Classrooms",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Assigns",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "courseId",
                table: "Assigns",
                newName: "CourseId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Assigns",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Assigns_userId",
                table: "Assigns",
                newName: "IX_Assigns_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Assigns_courseId",
                table: "Assigns",
                newName: "IX_Assigns_CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assigns_Courses_CourseId",
                table: "Assigns",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assigns_Users_UserId",
                table: "Assigns",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Classrooms_ClassroomId",
                table: "Courses",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_ScoresTables_ScoreTableId",
                table: "Courses",
                column: "ScoreTableId",
                principalTable: "ScoresTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Scores_ScoreId",
                table: "Courses",
                column: "ScoreId",
                principalTable: "Scores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Majors_MajorId",
                table: "Users",
                column: "MajorId",
                principalTable: "Majors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assigns_Courses_CourseId",
                table: "Assigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Assigns_Users_UserId",
                table: "Assigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Classrooms_ClassroomId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_ScoresTables_ScoreTableId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Scores_ScoreId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Majors_MajorId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "MajorId",
                table: "Users",
                newName: "majorid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Users_MajorId",
                table: "Users",
                newName: "IX_Users_majorid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ScoresTables",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Scores",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Majors",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Majors",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ScoreTableId",
                table: "Courses",
                newName: "ScoreTableid");

            migrationBuilder.RenameColumn(
                name: "ScoreId",
                table: "Courses",
                newName: "Scoreid");

            migrationBuilder.RenameColumn(
                name: "ClassroomId",
                table: "Courses",
                newName: "Classroomid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Courses",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_ScoreTableId",
                table: "Courses",
                newName: "IX_Courses_ScoreTableid");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_ScoreId",
                table: "Courses",
                newName: "IX_Courses_Scoreid");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_ClassroomId",
                table: "Courses",
                newName: "IX_Courses_Classroomid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Classrooms",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Assigns",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Assigns",
                newName: "courseId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Assigns",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Assigns_UserId",
                table: "Assigns",
                newName: "IX_Assigns_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Assigns_CourseId",
                table: "Assigns",
                newName: "IX_Assigns_courseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assigns_Courses_courseId",
                table: "Assigns",
                column: "courseId",
                principalTable: "Courses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assigns_Users_userId",
                table: "Assigns",
                column: "userId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Classrooms_Classroomid",
                table: "Courses",
                column: "Classroomid",
                principalTable: "Classrooms",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_ScoresTables_ScoreTableid",
                table: "Courses",
                column: "ScoreTableid",
                principalTable: "ScoresTables",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Scores_Scoreid",
                table: "Courses",
                column: "Scoreid",
                principalTable: "Scores",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Majors_majorid",
                table: "Users",
                column: "majorid",
                principalTable: "Majors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
