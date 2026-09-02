namespace ResumeBuilder.Domain.Entities;

public enum ResumeTemplateType
{
    Ats = 1,
    Modern = 2,
    Executive = 3
}

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedUtc { get; set; } = DateTime.UtcNow;
}

public sealed class Resume : BaseEntity
{
    public string Title { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ProfessionalTitle { get; set; } = string.Empty;
    public string ProfessionalSummary { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string? LinkedInUrl { get; set; }
    public string? GitHubUrl { get; set; }
    public string? PortfolioUrl { get; set; }
    public string? ProfilePhotoPath { get; set; }

    public ResumeTemplateType TemplateType { get; set; } = ResumeTemplateType.Modern;
    public bool IsDarkMode { get; set; }

    public ICollection<ResumeSkill> Skills { get; set; } = new List<ResumeSkill>();
    public ICollection<ResumeLanguage> Languages { get; set; } = new List<ResumeLanguage>();
    public ICollection<ResumeExperience> Experiences { get; set; } = new List<ResumeExperience>();
    public ICollection<ResumeProject> Projects { get; set; } = new List<ResumeProject>();
    public ICollection<ResumeEducation> EducationEntries { get; set; } = new List<ResumeEducation>();
    public ICollection<ResumeCertification> Certifications { get; set; } = new List<ResumeCertification>();
    public ICollection<ResumeActivity> Activities { get; set; } = new List<ResumeActivity>();
    public ICollection<ResumeAward> Awards { get; set; } = new List<ResumeAward>();
    public ICollection<ResumePublication> Publications { get; set; } = new List<ResumePublication>();
    public ICollection<ResumeResearch> ResearchItems { get; set; } = new List<ResumeResearch>();
    public ICollection<ResumeReference> References { get; set; } = new List<ResumeReference>();
}

public sealed class ResumeSkill : BaseEntity
{
    public Guid ResumeId { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

public sealed class ResumeLanguage : BaseEntity
{
    public Guid ResumeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Proficiency { get; set; } = string.Empty;
}

public sealed class ResumeExperience : BaseEntity
{
    public Guid ResumeId { get; set; }
    public string Company { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string EmploymentType { get; set; } = string.Empty;
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public bool IsCurrentJob { get; set; }
    public string Responsibilities { get; set; } = string.Empty;
    public string Achievements { get; set; } = string.Empty;
    public string TechnologiesUsed { get; set; } = string.Empty;
    public string KeyLearnings { get; set; } = string.Empty;
}

public sealed class ResumeProject : BaseEntity
{
    public Guid ResumeId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string BusinessProblem { get; set; } = string.Empty;
    public string Responsibilities { get; set; } = string.Empty;
    public string Architecture { get; set; } = string.Empty;
    public string Technologies { get; set; } = string.Empty;
    public string Challenges { get; set; } = string.Empty;
    public string Solution { get; set; } = string.Empty;
    public string Outcome { get; set; } = string.Empty;
    public string KeyLearnings { get; set; } = string.Empty;
    public string? GitHubLink { get; set; }
    public string? LiveDemoLink { get; set; }
    public string? ImagePath { get; set; }
}

public sealed class ResumeEducation : BaseEntity
{
    public Guid ResumeId { get; set; }
    public string School { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public string Major { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Gpa { get; set; }
    public string Coursework { get; set; } = string.Empty;
    public string Achievements { get; set; } = string.Empty;
}

public sealed class ResumeCertification : BaseEntity
{
    public Guid ResumeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Organization { get; set; } = string.Empty;
    public DateOnly? IssueDate { get; set; }
    public string? CredentialUrl { get; set; }
}

public sealed class ResumeActivity : BaseEntity
{
    public Guid ResumeId { get; set; }
    public string Organization { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public sealed class ResumeAward : BaseEntity
{
    public Guid ResumeId { get; set; }
    public string AwardName { get; set; } = string.Empty;
    public string Organization { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Year { get; set; }
}

public sealed class ResumePublication : BaseEntity
{
    public Guid ResumeId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Conference { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Link { get; set; }
}

public sealed class ResumeResearch : BaseEntity
{
    public Guid ResumeId { get; set; }
    public string Topic { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Technologies { get; set; } = string.Empty;
}

public sealed class ResumeReference : BaseEntity
{
    public Guid ResumeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ContactInformation { get; set; } = string.Empty;
    public string Relationship { get; set; } = string.Empty;
}
