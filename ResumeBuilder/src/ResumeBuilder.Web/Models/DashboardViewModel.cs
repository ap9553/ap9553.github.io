namespace ResumeBuilder.Web.Models;

public sealed class DashboardViewModel
{
    public int TotalResumes { get; init; }
    public DateTime? LastModifiedUtc { get; init; }
    public IReadOnlyList<ResumeListItemViewModel> RecentResumes { get; init; } = [];
}

public sealed class ResumeListItemViewModel
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string FullName { get; init; } = string.Empty;
    public string ProfessionalTitle { get; init; } = string.Empty;
    public DateTime UpdatedUtc { get; init; }
}
