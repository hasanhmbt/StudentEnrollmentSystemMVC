using Microsoft.EntityFrameworkCore;
using StudentEnrollmentSystemMVC.Models;

namespace StudentEnrollmentSystemMVC.Data;

public class SchoolContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }

    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().ToTable("Student");
        modelBuilder.Entity<Course>().ToTable("Course");
        modelBuilder.Entity<Enrollment>().ToTable("Enrollment");

        modelBuilder.Entity<Enrollment>()
           .HasOne(e => e.Student)
           .WithMany(s => s.Enrollments)
           .HasForeignKey(e => e.StudentId);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseId);

        modelBuilder.Entity<Course>().HasData(
            new Course { Id = 1, Title = "Mathematics", Credits = 4 },
            new Course { Id = 2, Title = "History", Credits = 3 }
        );

        modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, Name = "John Doe", Email = "john@example.com", EnrollmentDate = DateTime.Now },
            new Student { Id = 2, Name = "Jane Smith", Email = "jane@example.com", EnrollmentDate = DateTime.Now }
        );

        modelBuilder.Entity<Enrollment>().HasData(
            new Enrollment { Id = 1, StudentId = 1, CourseId = 1, Grade = Grade.A },
            new Enrollment { Id = 2, StudentId = 2, CourseId = 2, Grade = Grade.B }
        );
    }


}

