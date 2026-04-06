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
        public DbSet<ExtensionDKM.Models.Room> Room { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seedin
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<User>().HasData(
            //     new User
            //     {
            //         Id = 1,
            //         Name = "Admin",
            //         Username = "admin",
            //         Password = "123",
            //         Role = UserRole.Admin
            //     });

            // Course self-ref Many-to-Many relationships
            modelBuilder.Entity<Course>() 
                .HasMany(c => c.PreviousCourses)
                .WithMany(c => c.NextCourses)
                .UsingEntity(j => j.ToTable("CoursePrevious")); //1

            modelBuilder.Entity<Course>()
                .HasMany(c => c.RequirementCourses)
                .WithMany(c => c.RequiredBy)
                .UsingEntity(j => j.ToTable("CourseRequirement")); //2

            // Assign relation
            modelBuilder.Entity<Assign>()
                .HasOne(a => a.Student)
                .WithMany(s => s.Assignments)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.NoAction); //3

            modelBuilder.Entity<Assign>()
                .HasOne(a => a.Course)
                .WithMany(c => c.Assignments)
                .HasForeignKey(a => a.CourseId)
                .OnDelete(DeleteBehavior.NoAction); //4

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
