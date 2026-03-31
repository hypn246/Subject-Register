using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtensionDKM.Models
{
    public class Assign
    {
        [Key]
        public int id { get; set; }

        public int userId {  get; set; }
        public User User { get; set; }

        public int courseId { get; set; }
        public Course Course { get; set; }
    }

    public class Course
    {
        public int id { get; set; }
        public int Credit { get; set; }

        public Course PreviousCourse { get; set; }
        public Course RequirementCourse { get; set; }
        public List<Assign>? Assignments { get; set; } = new List<Assign>();

        public Score Score { get; set; }
        public Classroom Classroom { get; set; }
        public ScoreTable ScoreTable { get; set; }
    }

    public class ScoreTable
    {
        public int id {  get; set; }
        public double GPA { get; set; }
    }

    public class Score
    {
        public int id { get; set; }
        public int ProgressTerm { get; set; }
        public int MidTerm { get; set; }
        public int FinalTerm { get; set; }
        public int Status { get; set; }
    }

    public class Classroom
    {
        public int id { get; set; }
        public TimeSpan Time { get; set; }
    }
}
