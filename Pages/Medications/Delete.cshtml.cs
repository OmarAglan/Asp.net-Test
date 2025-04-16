using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roshta.Models;
using Roshta.Services.Interfaces;
using System.Threading.Tasks;

namespace Roshta.Pages_Medications
{
    public class DeleteModel : PageModel
    {
        private readonly IMedicationService _medicationService;

        public DeleteModel(IMedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        [BindProperty]
        public Medication Medication { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medication = await _medicationService.GetMedicationByIdAsync(id.Value);

            if (medication == null)
            {
                return NotFound();
            }
            else
            {
                Medication = medication;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _medicationService.DeleteMedicationAsync(id.Value);

            return RedirectToPage("./Index");
        }
    }
}
