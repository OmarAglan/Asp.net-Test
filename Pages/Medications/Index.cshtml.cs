using Microsoft.AspNetCore.Mvc.RazorPages;
using Roshta.Models;
using Roshta.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roshta.Pages_Medications
{
    public class IndexModel : PageModel
    {
        private readonly IMedicationService _medicationService;

        public IndexModel(IMedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        public IList<Medication> Medication { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Medication = (await _medicationService.GetAllMedicationsAsync()).ToList();
        }
    }
}
