using System.ComponentModel.DataAnnotations;

namespace ExtensionDKM.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name {  get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Major Major { get; set; }
        public List<Assign>? Assignments { get; set; } = new List<Assign>();

    }
    public class Lecturer : User
    {
        public string Level{ get; set; }
    }
    public class Student : User
    {
        public int K { get; set; }
    }
}
