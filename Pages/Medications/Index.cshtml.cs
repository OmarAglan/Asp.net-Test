using Microsoft.AspNetCore.Mvc;
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

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            IEnumerable<Medication> medications;
            if (!string.IsNullOrEmpty(SearchString))
            {
                // TODO: Implement SearchMedicationsAsync in service/repo
                medications = await _medicationService.SearchMedicationsAsync(SearchString);
            }
            else
            {
                medications = await _medicationService.GetAllMedicationsAsync();
            }
            Medication = medications.ToList();
        }
    }
}
