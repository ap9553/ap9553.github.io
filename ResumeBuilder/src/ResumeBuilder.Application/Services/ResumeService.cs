using ResumeBuilder.Application.Contracts;
using ResumeBuilder.Application.Models;
using ResumeBuilder.Domain.Entities;

namespace ResumeBuilder.Application.Services;

public sealed class ResumeService(IResumeRepository repository) : IResumeService
{
    public async Task<DashboardSnapshot> GetDashboardSnapshotAsync(CancellationToken cancellationToken = default)
    {
        var recent = await repository.GetRecentAsync(5, cancellationToken);
        var total = await repository.GetTotalCountAsync(cancellationToken);

        return new DashboardSnapshot
        {
            TotalResumes = total,
            LastModifiedUtc = recent.OrderByDescending(x => x.UpdatedUtc).Select(x => x.UpdatedUtc).FirstOrDefault(),
            RecentResumes = recent
        };
    }

    public Task<IReadOnlyList<Resume>> GetAllAsync(CancellationToken cancellationToken = default)
        => repository.GetAllAsync(cancellationToken);

    public Task<Resume?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => repository.GetByIdAsync(id, cancellationToken);

    public async Task<Resume> CreateAsync(Resume resume, CancellationToken cancellationToken = default)
    {
        resume.CreatedUtc = DateTime.UtcNow;
        resume.UpdatedUtc = DateTime.UtcNow;
        await repository.AddAsync(resume, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return resume;
    }

    public async Task<bool> UpdateAsync(Resume resume, CancellationToken cancellationToken = default)
    {
        var existing = await repository.GetByIdAsync(resume.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        existing.Title = resume.Title;
        existing.FirstName = resume.FirstName;
        existing.LastName = resume.LastName;
        existing.ProfessionalTitle = resume.ProfessionalTitle;
        existing.ProfessionalSummary = resume.ProfessionalSummary;
        existing.Email = resume.Email;
        existing.Phone = resume.Phone;
        existing.Location = resume.Location;
        existing.LinkedInUrl = resume.LinkedInUrl;
        existing.GitHubUrl = resume.GitHubUrl;
        existing.PortfolioUrl = resume.PortfolioUrl;
        existing.ProfilePhotoPath = resume.ProfilePhotoPath;
        existing.TemplateType = resume.TemplateType;
        existing.IsDarkMode = resume.IsDarkMode;
        existing.UpdatedUtc = DateTime.UtcNow;

        await repository.UpdateAsync(existing, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var existing = await repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        await repository.DeleteAsync(id, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return true;
    }
}
