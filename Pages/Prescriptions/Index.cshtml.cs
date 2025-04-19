using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roshta.Models;
using Roshta.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Roshta.Pages.Prescriptions;

public class IndexModel : PageModel
{
    private readonly IPrescriptionService _prescriptionService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(IPrescriptionService prescriptionService, ILogger<IndexModel> logger)
    {
        _prescriptionService = prescriptionService;
        _logger = logger;
    }

    public IList<Prescription> PrescriptionList { get;set; } = new List<Prescription>();

    [BindProperty(SupportsGet = true)]
    public string? SearchString { get; set; }

    public async Task OnGetAsync()
    {
        IEnumerable<Prescription> prescriptions;
        if (!string.IsNullOrEmpty(SearchString))
        {
            // TODO: Implement SearchPrescriptionsAsync in service/repo
            prescriptions = await _prescriptionService.SearchPrescriptionsAsync(SearchString);
        }
        else
        {
            prescriptions = await _prescriptionService.GetAllPrescriptionsAsync();
        }
        PrescriptionList = prescriptions.ToList();
    }

    public async Task<IActionResult> OnPostCancelAsync(int id)
    {
        _logger.LogInformation("Received request to cancel prescription ID {PrescriptionId}", id);
        bool success = await _prescriptionService.CancelPrescriptionAsync(id);

        if (success)
        {
            TempData["SuccessMessage"] = $"Prescription ID {id} cancelled successfully.";
        }
        else
        {
            // Log error? Service/Repo should log specific DB errors.
             _logger.LogWarning("Failed to cancel prescription ID {PrescriptionId}. It might not exist or was already cancelled.", id);
            TempData["ErrorMessage"] = $"Could not cancel prescription ID {id}. It might have already been cancelled or an error occurred.";
        }

        // Redirect back to the index page (will refresh the list)
        return RedirectToPage();
    }
} 