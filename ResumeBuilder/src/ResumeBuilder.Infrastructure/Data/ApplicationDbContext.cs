using Microsoft.EntityFrameworkCore;
using ResumeBuilder.Domain.Entities;

namespace ResumeBuilder.Infrastructure.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Resume> Resumes => Set<Resume>();
    public DbSet<ResumeSkill> ResumeSkills => Set<ResumeSkill>();
    public DbSet<ResumeLanguage> ResumeLanguages => Set<ResumeLanguage>();
    public DbSet<ResumeExperience> ResumeExperiences => Set<ResumeExperience>();
    public DbSet<ResumeProject> ResumeProjects => Set<ResumeProject>();
    public DbSet<ResumeEducation> ResumeEducations => Set<ResumeEducation>();
    public DbSet<ResumeCertification> ResumeCertifications => Set<ResumeCertification>();
    public DbSet<ResumeActivity> ResumeActivities => Set<ResumeActivity>();
    public DbSet<ResumeAward> ResumeAwards => Set<ResumeAward>();
    public DbSet<ResumePublication> ResumePublications => Set<ResumePublication>();
    public DbSet<ResumeResearch> ResumeResearches => Set<ResumeResearch>();
    public DbSet<ResumeReference> ResumeReferences => Set<ResumeReference>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Resume>(entity =>
        {
            entity.Property(x => x.Title).HasMaxLength(120).IsRequired();
            entity.Property(x => x.FirstName).HasMaxLength(80).IsRequired();
            entity.Property(x => x.LastName).HasMaxLength(80).IsRequired();
            entity.Property(x => x.ProfessionalTitle).HasMaxLength(140).IsRequired();
            entity.Property(x => x.Email).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Phone).HasMaxLength(30);
            entity.Property(x => x.Location).HasMaxLength(120);
            entity.Property(x => x.LinkedInUrl).HasMaxLength(500);
            entity.Property(x => x.GitHubUrl).HasMaxLength(500);
            entity.Property(x => x.PortfolioUrl).HasMaxLength(500);
            entity.Property(x => x.ProfilePhotoPath).HasMaxLength(500);

            entity.HasMany(x => x.Skills).WithOne().HasForeignKey(x => x.ResumeId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(x => x.Languages).WithOne().HasForeignKey(x => x.ResumeId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(x => x.Experiences).WithOne().HasForeignKey(x => x.ResumeId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(x => x.Projects).WithOne().HasForeignKey(x => x.ResumeId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(x => x.EducationEntries).WithOne().HasForeignKey(x => x.ResumeId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(x => x.Certifications).WithOne().HasForeignKey(x => x.ResumeId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(x => x.Activities).WithOne().HasForeignKey(x => x.ResumeId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(x => x.Awards).WithOne().HasForeignKey(x => x.ResumeId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(x => x.Publications).WithOne().HasForeignKey(x => x.ResumeId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(x => x.ResearchItems).WithOne().HasForeignKey(x => x.ResumeId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(x => x.References).WithOne().HasForeignKey(x => x.ResumeId).OnDelete(DeleteBehavior.Cascade);
        });

        ConfigureChild<ResumeSkill>(modelBuilder);
        ConfigureChild<ResumeLanguage>(modelBuilder);
        ConfigureChild<ResumeExperience>(modelBuilder);
        ConfigureChild<ResumeProject>(modelBuilder);
        ConfigureChild<ResumeEducation>(modelBuilder);
        ConfigureChild<ResumeCertification>(modelBuilder);
        ConfigureChild<ResumeActivity>(modelBuilder);
        ConfigureChild<ResumeAward>(modelBuilder);
        ConfigureChild<ResumePublication>(modelBuilder);
        ConfigureChild<ResumeResearch>(modelBuilder);
        ConfigureChild<ResumeReference>(modelBuilder);
    }

    private static void ConfigureChild<T>(ModelBuilder modelBuilder) where T : BaseEntity
    {
        modelBuilder.Entity<T>().Property(x => x.CreatedUtc).IsRequired();
        modelBuilder.Entity<T>().Property(x => x.UpdatedUtc).IsRequired();
    }
}
