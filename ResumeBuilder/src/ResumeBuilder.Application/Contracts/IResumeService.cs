using ResumeBuilder.Application.Models;
using ResumeBuilder.Domain.Entities;

namespace ResumeBuilder.Application.Contracts;

public interface IResumeService
{
    Task<DashboardSnapshot> GetDashboardSnapshotAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Resume>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Resume?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Resume> CreateAsync(Resume resume, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Resume resume, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
