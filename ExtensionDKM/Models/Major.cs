using System.ComponentModel.DataAnnotations;

namespace ExtensionDKM.Models
{
    public class Major
    {
        [Key]
        public int Id { get; set; }
        public string Name {  get; set; }
        public List<User> Users { get; set; }=new List<User>();
   }
}
