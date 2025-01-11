using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SurveyForm.Models;

public partial class SurveyFormContext : DbContext
{
    public SurveyFormContext()
    {
    }

    public SurveyFormContext(DbContextOptions<SurveyFormContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ViewResults> ViewResults { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=192.168.4.59;database=SurveyForm;uid=sa;pwd=Aa123456;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ViewResults>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_Results");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
