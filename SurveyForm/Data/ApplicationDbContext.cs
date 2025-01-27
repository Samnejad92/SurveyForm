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
        public DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }
        public DbSet<EssayQuestion> EssayQuestions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<MultipleAnswer> MultipleAnswers { get; set; }
        public DbSet<CodeUnit> CodeUnits { get; set; }
        public DbSet<EssayAnswer> EssayAnswers { get; set; }
        public DbSet<PersonnelInfo> PersonnelInfos { get; set; }

    }
}