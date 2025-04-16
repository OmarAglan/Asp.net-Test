using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roshta.Models;
using Roshta.Services.Interfaces;
using System.Threading.Tasks;

namespace Roshta.Pages_Medications
{
    public class CreateModel : PageModel
    {
        private readonly IMedicationService _medicationService;

        public CreateModel(IMedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Medication Medication { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _medicationService.AddMedicationAsync(Medication);

            return RedirectToPage("./Index");
        }
    }
}
