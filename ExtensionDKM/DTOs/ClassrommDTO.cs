
namespace ExtensionDKM.DTOs
{
    public class ClassrommDTO
    {
        public int Id { get; set; }
        public int? Time { get; set; }
        public int SchoolYear { get; set; }
        public int Semester { get; set; }
        public int SS { get; set; }

        public string CourseName { get; set; }
        public string LecturerName { get; set; }
        public string RoomName { get; set; }

        public List<string> PreviousCourses { get; set; } = new();
        public List<string> RequirementCourses { get; set; } = new();
    }
}
