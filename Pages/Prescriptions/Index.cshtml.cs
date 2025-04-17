using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roshta.Models;
using Roshta.Services.Interfaces;

namespace Roshta.Pages.Prescriptions;

public class IndexModel : PageModel
{
    private readonly IPrescriptionService _prescriptionService;

    public IndexModel(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
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
} 