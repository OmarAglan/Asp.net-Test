using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roshta.Models;
using Roshta.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Roshta.Pages_Medications
{
    public class CreateModel : PageModel
    {
        private readonly IMedicationRepository _medicationRepository;

        public CreateModel(IMedicationRepository medicationRepository)
        {
            _medicationRepository = medicationRepository;
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

            await _medicationRepository.AddAsync(Medication);

            return RedirectToPage("./Index");
        }
    }
}
