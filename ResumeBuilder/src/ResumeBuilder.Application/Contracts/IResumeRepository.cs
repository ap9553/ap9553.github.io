using ResumeBuilder.Domain.Entities;

namespace ResumeBuilder.Application.Contracts;

public interface IResumeRepository
{
    Task<IReadOnlyList<Resume>> GetRecentAsync(int take, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Resume>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Resume?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Resume resume, CancellationToken cancellationToken = default);
    Task UpdateAsync(Resume resume, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
