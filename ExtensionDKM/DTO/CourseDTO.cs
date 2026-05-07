namespace ExtensionDKM.DTOs
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int MajorId { get; set; }
        public string? MajorName { get; set; }
        public int Credit { get; set; }
        public int Tuition { get; set; }
    }
}
