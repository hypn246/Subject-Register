namespace ExtensionDKM.DTO
{
    public class ScheduleDTO
    {
        public int CourseId { get; set; }

        public string? CourseName { get; set; }

        public string? LecturerName { get; set; }

        public string? RoomName { get; set; }

        public int? Time { get; set; }

        public int SchoolYear { get; set; }

        public int Semester { get; set; }
    }
}