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

    public async Task OnGetAsync()
    {
        PrescriptionList = (await _prescriptionService.GetAllPrescriptionsAsync()).ToList();
    }
} 