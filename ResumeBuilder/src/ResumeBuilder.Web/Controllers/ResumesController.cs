using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ResumeBuilder.Application.Contracts;
using ResumeBuilder.Domain.Entities;
using ResumeBuilder.Web.Models;

namespace ResumeBuilder.Web.Controllers;

public sealed class ResumesController(IResumeService resumeService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var resumes = await resumeService.GetAllAsync(cancellationToken);

        var model = resumes.Select(x => new ResumeListItemViewModel
        {
            Id = x.Id,
            Title = x.Title,
            FullName = $"{x.FirstName} {x.LastName}".Trim(),
            ProfessionalTitle = x.ProfessionalTitle,
            UpdatedUtc = x.UpdatedUtc
        }).ToList();

        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        PopulateTemplateOptions();
        return View(new ResumeFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ResumeFormViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            PopulateTemplateOptions();
            return View(model);
        }

        var entity = MapToEntity(model, new Resume());
        await resumeService.CreateAsync(entity, cancellationToken);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
    {
        var resume = await resumeService.GetByIdAsync(id, cancellationToken);
        if (resume is null)
        {
            return NotFound();
        }

        PopulateTemplateOptions();
        return View(MapToViewModel(resume));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ResumeFormViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            PopulateTemplateOptions();
            return View(model);
        }

        var updated = await resumeService.UpdateAsync(MapToEntity(model, new Resume { Id = model.Id }), cancellationToken);
        if (!updated)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Preview(Guid id, CancellationToken cancellationToken)
    {
        var resume = await resumeService.GetByIdAsync(id, cancellationToken);
        if (resume is null)
        {
            return NotFound();
        }

        return View(resume);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await resumeService.DeleteAsync(id, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    private void PopulateTemplateOptions()
    {
        ViewBag.TemplateOptions = Enum.GetValues<ResumeTemplateType>()
            .Select(x => new SelectListItem(x.ToString(), x.ToString()))
            .ToList();
    }

    private static ResumeFormViewModel MapToViewModel(Resume resume) => new()
    {
        Id = resume.Id,
        Title = resume.Title,
        FirstName = resume.FirstName,
        LastName = resume.LastName,
        ProfessionalTitle = resume.ProfessionalTitle,
        ProfessionalSummary = resume.ProfessionalSummary,
        Email = resume.Email,
        Phone = resume.Phone,
        Location = resume.Location,
        LinkedInUrl = resume.LinkedInUrl,
        GitHubUrl = resume.GitHubUrl,
        PortfolioUrl = resume.PortfolioUrl,
        ProfilePhotoPath = resume.ProfilePhotoPath,
        TemplateType = resume.TemplateType,
        IsDarkMode = resume.IsDarkMode
    };

    private static Resume MapToEntity(ResumeFormViewModel model, Resume resume)
    {
        resume.Title = model.Title;
        resume.FirstName = model.FirstName;
        resume.LastName = model.LastName;
        resume.ProfessionalTitle = model.ProfessionalTitle;
        resume.ProfessionalSummary = model.ProfessionalSummary;
        resume.Email = model.Email;
        resume.Phone = model.Phone;
        resume.Location = model.Location;
        resume.LinkedInUrl = model.LinkedInUrl;
        resume.GitHubUrl = model.GitHubUrl;
        resume.PortfolioUrl = model.PortfolioUrl;
        resume.ProfilePhotoPath = model.ProfilePhotoPath;
        resume.TemplateType = model.TemplateType;
        resume.IsDarkMode = model.IsDarkMode;
        return resume;
    }
}
