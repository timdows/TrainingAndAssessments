using System.Data.Entity;
using SnapChallenge.Data.DatabaseSeeder;
using SnapChallenge.Data.Models;

namespace SnapChallenge.Data
{
    public class SnapDataContext : DbContext
    {
        public SnapDataContext() : base("SnapConnection")
        {
        }

        public DbSet<SeederProgress> SeederProgress { get; set; }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<LearningObjective> LearningObjectives { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}