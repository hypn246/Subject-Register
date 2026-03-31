namespace ExtensionDKM.Models
{
    public class User
    {
        public int id { get; set; }
        public string name {  get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
    public class Lecturer : User
    {
        public string level{ get; set; }
    }
    public class Student : User
    {
        public int K { get; set; }
    }
}
