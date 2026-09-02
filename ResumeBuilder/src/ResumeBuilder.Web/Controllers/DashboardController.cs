using Microsoft.AspNetCore.Mvc;
using ResumeBuilder.Application.Contracts;
using ResumeBuilder.Web.Models;

namespace ResumeBuilder.Web.Controllers;

public sealed class DashboardController(IResumeService resumeService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var snapshot = await resumeService.GetDashboardSnapshotAsync(cancellationToken);

        var model = new DashboardViewModel
        {
            TotalResumes = snapshot.TotalResumes,
            LastModifiedUtc = snapshot.LastModifiedUtc,
            RecentResumes = snapshot.RecentResumes
                .Select(x => new ResumeListItemViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    FullName = $"{x.FirstName} {x.LastName}".Trim(),
                    ProfessionalTitle = x.ProfessionalTitle,
                    UpdatedUtc = x.UpdatedUtc
                })
                .ToList()
        };

        return View(model);
    }
}
