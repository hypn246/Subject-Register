using Microsoft.EntityFrameworkCore;
using ExtensionDKM.Models;

namespace ExtensionDKM.Data
{
    public class MyDBContext:DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Assign> Assigns { get; set; }
        public DbSet<Score> Scores{ get; set; }
        public DbSet<ScoreTable> ScoresTables{ get; set; }
        public DbSet<Classroom> Classrooms{ get; set; }
    }
}
