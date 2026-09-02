using Microsoft.EntityFrameworkCore;
using ResumeBuilder.Application.Contracts;
using ResumeBuilder.Domain.Entities;
using ResumeBuilder.Infrastructure.Data;

namespace ResumeBuilder.Infrastructure.Repositories;

public sealed class ResumeRepository(ApplicationDbContext dbContext) : IResumeRepository
{
    private readonly IQueryable<Resume> _query = dbContext.Resumes
        .Include(x => x.Skills)
        .Include(x => x.Languages)
        .Include(x => x.Experiences)
        .Include(x => x.Projects)
        .Include(x => x.EducationEntries)
        .Include(x => x.Certifications)
        .Include(x => x.Activities)
        .Include(x => x.Awards)
        .Include(x => x.Publications)
        .Include(x => x.ResearchItems)
        .Include(x => x.References)
        .AsQueryable();

    public async Task<IReadOnlyList<Resume>> GetRecentAsync(int take, CancellationToken cancellationToken = default)
        => await _query.OrderByDescending(x => x.UpdatedUtc).Take(take).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Resume>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _query.OrderByDescending(x => x.UpdatedUtc).ToListAsync(cancellationToken);

    public Task<Resume?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => _query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task AddAsync(Resume resume, CancellationToken cancellationToken = default)
        => await dbContext.Resumes.AddAsync(resume, cancellationToken);

    public Task UpdateAsync(Resume resume, CancellationToken cancellationToken = default)
    {
        dbContext.Resumes.Update(resume);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var resume = await dbContext.Resumes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (resume is not null)
        {
            dbContext.Resumes.Remove(resume);
        }
    }

    public Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default)
        => dbContext.Resumes.CountAsync(cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => dbContext.SaveChangesAsync(cancellationToken);
}
