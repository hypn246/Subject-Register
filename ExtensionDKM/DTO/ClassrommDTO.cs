
using ExtensionDKM.Models;

namespace ExtensionDKM.DTO
{
    public class ClassrommDTO
    {
        public int Id { get; set; }
        public int? Time { get; set; }
        public int SchoolYear { get; set; }
        public int Semester { get; set; }
        public int? RoomId { get; set; }
        public string? RoomName { get; set; }

        public int Capacity { get; set; }
        public int SS { get; set; }

        public string? CourseName { get; set; }
        public string? LecturerName { get; set; }
        public int Remain => Capacity-SS;

        public List<string>? PreviousCourses { get; set; } 
        public List<string>? RequirementCourses { get; set; } 
        public bool IsAssigned { get; set; }
        public int AssignedCount { get; set; }
        public int CourseId { get; set; }
    }
}
