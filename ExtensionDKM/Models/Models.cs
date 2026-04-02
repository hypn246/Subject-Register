using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtensionDKM.Models
{
    public enum UserRole
    {
        Admin,
        Lecturer,
        Student
    }

    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        public UserRole Role { get; set; }

        // Made nullable because Admins/Lecturers might not have a Major
        public int? MajorId { get; set; }
        public Major? Major { get; set; }
    }

    public class Lecturer : User
    {
        public string? Level { get; set; }

        // A Lecturer can open/teach many Classrooms
        public List<Classroom> Classrooms { get; set; } = new();
    }

    public class Student : User
    {
        public int K { get; set; } // Cohort/Year

        // A Student has many enrollments
        public List<Assign> Assignments { get; set; } = new();

        // A Student has one ScoreTable (Transcript)
        public int? ScoreTableId { get; set; }
        public ScoreTable? ScoreTable { get; set; }
    }

    // Acts as the Enrollment junction between a Student and a Course
    public class Assign
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }

        // Each enrollment results in a Score
        public int? ScoreId { get; set; }
        public Score? Score { get; set; }
    }

    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; } // Added from CourseInfo
        public int Credit { get; set; }
        public int Tuition { get; set; } // Added from CourseInfo

        // Prerequisites and dependencies
        public List<Course> PreviousCourses { get; set; } = new();
        public List<Course> NextCourses { get; set; } = new();
        public List<Course> RequirementCourses { get; set; } = new();
        public List<Course> RequiredBy { get; set; } = new();

        // A course can have many student enrollments
        public List<Assign> Assignments { get; set; } = new();

        // A course can be taught in multiple classrooms over time
        public List<Classroom> Classrooms { get; set; } = new();
    }

    public class Classroom
    {
        [Key]
        public int Id { get; set; }
        public TimeSpan Time { get; set; }

        // Added missing fields from UML diagram
        public int SS { get; set; }
        public int Semester { get; set; }

        // Taught by 1 Lecturer
        public int LecturerId { get; set; }
        public Lecturer? Lecturer { get; set; }

        // Teaches 1 Course
        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }

    public class ScoreTable
    {
        [Key]
        public int Id { get; set; }
        public double GPA { get; set; }

        // Belongs to 1 Student
        public int StudentId { get; set; }
        public Student? Student { get; set; }

        // Contains many Scores
        public List<Score> Scores { get; set; } = new();
    }

    public class Score
    {
        [Key]
        public int Id { get; set; }
        public int ProgressTerm { get; set; }
        public int MidTerm { get; set; }
        public int FinalTerm { get; set; }
        public int Status { get; set; }

        // Tied back to the specific Student's ScoreTable
        public int ScoreTableId { get; set; }
        public ScoreTable? ScoreTable { get; set; }

        // Tied to the specific Course Enrollment
        public int AssignId { get; set; }
        public Assign? Assign { get; set; }
    }

    public class Major
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<User> Users { get; set; } = new();
    }
}