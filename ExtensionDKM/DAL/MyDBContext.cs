using Microsoft.EntityFrameworkCore;
using ExtensionDKM.Models;

namespace ExtensionDKM.DAL
{
    public class MyDBContext:DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CoursePrevious> CoursePrevious { get; set; }
        public DbSet<CourseRequirement> CourseRequirement { get; set; }
        public DbSet<Assign> Assigns { get; set; }
        public DbSet<Score> Scores{ get; set; }
        public DbSet<ScoreTable> ScoresTables{ get; set; }
        public DbSet<Classroom> Classrooms{ get; set; }
        public DbSet<Room> Room { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
                 new User
                 {
                     Id = 1,
                     Name = "Admin",
                     Username = "admin",
                     Password = "123",
                     Role = UserRole.Admin
                 });

            //Course self-ref Many-to-Many relationships
            //previous
            modelBuilder.Entity<CoursePrevious>()
                .HasKey(x => new { x.CourseId, x.PreviousCourseId });

            modelBuilder.Entity<CoursePrevious>()
                .HasOne(x => x.Course)
                .WithMany(x => x.PreviousCourses)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CoursePrevious>()
                .HasOne(x => x.PreviousCourse)
                .WithMany()
                .HasForeignKey(x => x.PreviousCourseId)
                .OnDelete(DeleteBehavior.Restrict);

            //requirement
            modelBuilder.Entity<CourseRequirement>()
                .HasKey(x => new { x.CourseId, x.RequirementCourseId });

            modelBuilder.Entity<CourseRequirement>()
                .HasOne(x => x.Course)
                .WithMany(x => x.RequirementCourses)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseRequirement>()
                .HasOne(x => x.RequirementCourse)
                .WithMany()
                .HasForeignKey(x => x.RequirementCourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Assign relation
            modelBuilder.Entity<Assign>()
                .HasOne(a => a.Student)
                .WithMany(s => s.Assignments)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.NoAction); //3

            modelBuilder.Entity<Assign>()
                .HasOne(a => a.Classroom)
                .WithMany(c => c.Assignments)
                .HasForeignKey(a => a.ClassroomId);

            // Assign - Score / 1-1
            modelBuilder.Entity<Assign>()
                .HasOne(a => a.Score)
                .WithOne(s => s.Assign)
                .HasForeignKey<Score>(s => s.AssignId)
                .OnDelete(DeleteBehavior.NoAction); //5

            // Student - ScoreTable / 1-1
            modelBuilder.Entity<Student>()
                .HasOne(s => s.ScoreTable)
                .WithOne(st => st.Student)
                .HasForeignKey<ScoreTable>(st => st.StudentId) //6
                .OnDelete(DeleteBehavior.NoAction);

            // ScoreTable - Score / 1-many
            modelBuilder.Entity<Score>()
                .HasOne(s => s.ScoreTable)
                .WithMany(st => st.Scores)
                .HasForeignKey(s => s.ScoreTableId)
                .OnDelete(DeleteBehavior.Cascade); //7

        }

    }

}
