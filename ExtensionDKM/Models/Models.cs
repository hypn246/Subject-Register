using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtensionDKM.Models
{
    public enum UserRole
    {
        Admin =0,
        Lecturer=1,
        Student=2
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

        public int? MajorId { get; set; }
        public Major? Major { get; set; }
    }

    public class Lecturer : User
    {
        public string? Level { get; set; }

        // A Lecturer can open many Classrooms
        public List<Classroom> Classrooms { get; set; } = new();
    }

    public class Student : User
    {
        public int K { get; set; }

        // A Student has many assign
        public List<Assign> Assignments { get; set; } = new();

        // Student has one ScoreTable
        public int? ScoreTableId { get; set; }
        public ScoreTable? ScoreTable { get; set; }
    }

    public class Assign
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }

        // results in a Score
        public int? ScoreId { get; set; }
        public Score? Score { get; set; }
    }

    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        //
        public int? MajorId { get; set; }
        public Major? Major { get; set; }
        public int Credit { get; set; }
        public int Tuition { get; set; }

        // Prerequisites and dependencies
        public List<Course> PreviousCourses { get; set; } = new();
        public List<Course> NextCourses { get; set; } = new();
        public List<Course> RequirementCourses { get; set; } = new();
        public List<Course> RequiredBy { get; set; } = new();

        // A course can have many student
        public List<Assign> Assignments { get; set; } = new();

        // A course can be multiple classrooms over time
        public List<Classroom> Classrooms { get; set; } = new();
    }
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Capacity { get; set; }
        public List<Classroom> Classrooms { get; set; } = new();
    }
    public class Classroom
    {
        [Key]
        public int Id { get; set; }
        public int? Time { get; set; }
        public int RoomId{ get; set; }
        public Room? Room { get; set; }
        public int SchoolYear { get; set; }
        public int Semester { get; set; }
        public int SS { get; set; }
        public int LecturerId { get; set; }
        public User? Lecturer { get; set; }

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

        // Tied back to ScoreTable
        public int ScoreTableId { get; set; }
        public ScoreTable? ScoreTable { get; set; }

        // Tied to the specific Course
        public int AssignId { get; set; }
        public Assign? Assign { get; set; }
    }

    public class Major
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<User> Users { get; set; } = new();
        public List<Course> Courses { get; set; } =new();
    }
}