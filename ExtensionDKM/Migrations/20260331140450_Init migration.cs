using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtensionDKM.Migrations
{
    /// <inheritdoc />
    public partial class Initmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Majors",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Majors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgressTerm = table.Column<int>(type: "int", nullable: false),
                    MidTerm = table.Column<int>(type: "int", nullable: false),
                    FinalTerm = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ScoresTables",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GPA = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoresTables", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    majorid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_Majors_majorid",
                        column: x => x.majorid,
                        principalTable: "Majors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Credit = table.Column<int>(type: "int", nullable: false),
                    PreviousCourseId = table.Column<int>(type: "int", nullable: true),
                    RequirementCourseId = table.Column<int>(type: "int", nullable: true),
                    Scoreid = table.Column<int>(type: "int", nullable: false),
                    Classroomid = table.Column<int>(type: "int", nullable: false),
                    ScoreTableid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.id);
                    table.ForeignKey(
                        name: "FK_Courses_Classrooms_Classroomid",
                        column: x => x.Classroomid,
                        principalTable: "Classrooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_Courses_PreviousCourseId",
                        column: x => x.PreviousCourseId,
                        principalTable: "Courses",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Courses_Courses_RequirementCourseId",
                        column: x => x.RequirementCourseId,
                        principalTable: "Courses",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Courses_ScoresTables_ScoreTableid",
                        column: x => x.ScoreTableid,
                        principalTable: "ScoresTables",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_Scores_Scoreid",
                        column: x => x.Scoreid,
                        principalTable: "Scores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assigns",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    courseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assigns", x => x.id);
                    table.ForeignKey(
                        name: "FK_Assigns_Courses_courseId",
                        column: x => x.courseId,
                        principalTable: "Courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assigns_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assigns_courseId",
                table: "Assigns",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "IX_Assigns_userId",
                table: "Assigns",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Classroomid",
                table: "Courses",
                column: "Classroomid");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_PreviousCourseId",
                table: "Courses",
                column: "PreviousCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_RequirementCourseId",
                table: "Courses",
                column: "RequirementCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Scoreid",
                table: "Courses",
                column: "Scoreid");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ScoreTableid",
                table: "Courses",
                column: "ScoreTableid");

            migrationBuilder.CreateIndex(
                name: "IX_Users_majorid",
                table: "Users",
                column: "majorid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assigns");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropTable(
                name: "ScoresTables");

            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "Majors");
        }
    }
}
