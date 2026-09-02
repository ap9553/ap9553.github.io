using ResumeBuilder.Domain.Entities;

namespace ResumeBuilder.Application.Models;

public sealed class DashboardSnapshot
{
    public int TotalResumes { get; init; }
    public DateTime? LastModifiedUtc { get; init; }
    public IReadOnlyList<Resume> RecentResumes { get; init; } = [];
}
