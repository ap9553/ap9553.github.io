using System.ComponentModel.DataAnnotations;
using ResumeBuilder.Domain.Entities;

namespace ResumeBuilder.Web.Models;

public sealed class ResumeFormViewModel
{
    public Guid Id { get; set; }

    [Required, StringLength(120)]
    public string Title { get; set; } = string.Empty;

    [Required, Display(Name = "First Name"), StringLength(80)]
    public string FirstName { get; set; } = string.Empty;

    [Required, Display(Name = "Last Name"), StringLength(80)]
    public string LastName { get; set; } = string.Empty;

    [Required, Display(Name = "Professional Title"), StringLength(140)]
    public string ProfessionalTitle { get; set; } = string.Empty;

    [Display(Name = "Professional Summary")]
    public string ProfessionalSummary { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Phone]
    public string Phone { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    [Display(Name = "LinkedIn URL"), Url]
    public string? LinkedInUrl { get; set; }

    [Display(Name = "GitHub URL"), Url]
    public string? GitHubUrl { get; set; }

    [Display(Name = "Portfolio URL"), Url]
    public string? PortfolioUrl { get; set; }

    [Display(Name = "Profile Photo Path")]
    public string? ProfilePhotoPath { get; set; }

    [Display(Name = "Template")]
    public ResumeTemplateType TemplateType { get; set; } = ResumeTemplateType.Modern;

    [Display(Name = "Dark Mode")]
    public bool IsDarkMode { get; set; }
}
