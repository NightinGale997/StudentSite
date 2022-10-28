using Microsoft.EntityFrameworkCore;
using StudentSite.Models;
using StudentSite.Entities;

namespace StudentSite.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<StudyMaterial> StudyMaterials { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PointOperation> PointOperations { get; set; }
        public DbSet<ChangeOperation> ChangeOperations { get; set; }
        public DbSet<FileModel> Files { get; set; }
        public DbSet<DayNote> Notes { get; set; }
    }
}
