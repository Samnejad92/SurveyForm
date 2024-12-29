// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using SurveyForm.Models;

namespace SurveyForm.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Sex> Sexes { get; set; }
        public DbSet<Age> Ages { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<OrganizationalPosition> OrganizationalPositions { get; set; }

        public DbSet<MatrixTable> MatrixTable { get; set; }
        public DbSet<MultipleChoiseQuestion> MultipleChoiseQuestions { get; set; }

    }
}