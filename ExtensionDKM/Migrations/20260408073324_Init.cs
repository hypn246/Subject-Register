using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtensionDKM.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Majors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Majors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MajorId = table.Column<int>(type: "int", nullable: true),
                    Credit = table.Column<int>(type: "int", nullable: false),
                    Tuition = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Majors_MajorId",
                        column: x => x.MajorId,
                        principalTable: "Majors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    MajorId = table.Column<int>(type: "int", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    K = table.Column<int>(type: "int", nullable: true),
                    ScoreTableId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Majors_MajorId",
                        column: x => x.MajorId,
                        principalTable: "Majors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CoursePrevious",
                columns: table => new
                {
                    NextCoursesId = table.Column<int>(type: "int", nullable: false),
                    PreviousCoursesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursePrevious", x => new { x.NextCoursesId, x.PreviousCoursesId });
                    table.ForeignKey(
                        name: "FK_CoursePrevious_Courses_NextCoursesId",
                        column: x => x.NextCoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursePrevious_Courses_PreviousCoursesId",
                        column: x => x.PreviousCoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CourseRequirement",
                columns: table => new
                {
                    RequiredById = table.Column<int>(type: "int", nullable: false),
                    RequirementCoursesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseRequirement", x => new { x.RequiredById, x.RequirementCoursesId });
                    table.ForeignKey(
                        name: "FK_CourseRequirement_Courses_RequiredById",
                        column: x => x.RequiredById,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseRequirement_Courses_RequirementCoursesId",
                        column: x => x.RequirementCoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Assigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    ScoreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assigns_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Assigns_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<int>(type: "int", nullable: true),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    SchoolYear = table.Column<int>(type: "int", nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false),
                    SS = table.Column<int>(type: "int", nullable: false),
                    LecturerId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classrooms_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classrooms_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classrooms_Users_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoresTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GPA = table.Column<double>(type: "float", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoresTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScoresTables_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgressTerm = table.Column<int>(type: "int", nullable: false),
                    MidTerm = table.Column<int>(type: "int", nullable: false),
                    FinalTerm = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ScoreTableId = table.Column<int>(type: "int", nullable: false),
                    AssignId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scores_Assigns_AssignId",
                        column: x => x.AssignId,
                        principalTable: "Assigns",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Scores_ScoresTables_ScoreTableId",
                        column: x => x.ScoreTableId,
                        principalTable: "ScoresTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Discriminator", "MajorId", "Name", "Password", "Role", "Username" },
                values: new object[] { 1, "User", null, "Admin", "123", 0, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Assigns_CourseId",
                table: "Assigns",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Assigns_StudentId",
                table: "Assigns",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_CourseId",
                table: "Classrooms",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_LecturerId",
                table: "Classrooms",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_RoomId",
                table: "Classrooms",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursePrevious_PreviousCoursesId",
                table: "CoursePrevious",
                column: "PreviousCoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRequirement_RequirementCoursesId",
                table: "CourseRequirement",
                column: "RequirementCoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_MajorId",
                table: "Courses",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_AssignId",
                table: "Scores",
                column: "AssignId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scores_ScoreTableId",
                table: "Scores",
                column: "ScoreTableId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoresTables_StudentId",
                table: "ScoresTables",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_MajorId",
                table: "Users",
                column: "MajorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropTable(
                name: "CoursePrevious");

            migrationBuilder.DropTable(
                name: "CourseRequirement");

            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Assigns");

            migrationBuilder.DropTable(
                name: "ScoresTables");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Majors");
        }
    }
}
