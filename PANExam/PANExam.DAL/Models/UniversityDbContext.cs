using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PANExam.DAL.Migrations;

namespace PANExam.DAL.Models
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext() : base("name=UniversityDbContext")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<UniversityDbContext, Configuration>());
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
