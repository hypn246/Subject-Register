using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtensionDKM.Models
{
    public class Assign
    {
        [Key]
        public int Id { get; set; }

        public int UserId {  get; set; }
        public User User { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }

    public class Course
    {
        [Key]
        public int Id { get; set; }
        public int Credit { get; set; }

        // FK for PreviousCourse
        //public int? PreviousCourseId { get; set; }
        //[ForeignKey("PreviousCourseId")]
        public List<Course>? PreviousCourse { get; set; }= new List<Course>();

        // FK for RequirementCourse
        //public int? RequirementCourseId { get; set; }
        //[ForeignKey("RequirementCourseId")]
        public List<Course>? RequirementCourse { get; set; }=new List<Course>();
        public List<Assign>? Assignments { get; set; } = new List<Assign>();

        public Score Score { get; set; }
        public Classroom Classroom { get; set; }
        public ScoreTable ScoreTable { get; set; }
    }

    public class ScoreTable
    {
        [Key]
        public int Id {  get; set; }
        public double GPA { get; set; }
        public List<Course> Courses { get; set; }=new List<Course>();

    }

    public class Score
    {
        public int Id { get; set; }
        public int ProgressTerm { get; set; }
        public int MidTerm { get; set; }
        public int FinalTerm { get; set; }
        public int Status { get; set; }
    }

    public class Classroom
    {
        [Key]
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
    }
}
