using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roshta.Models;
using Roshta.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Roshta.Pages_Medications
{
    public class DeleteModel : PageModel
    {
        private readonly IMedicationRepository _medicationRepository;

        public DeleteModel(IMedicationRepository medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }

        [BindProperty]
        public Medication Medication { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medication = await _medicationRepository.GetByIdAsync(id.Value);

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

            await _medicationRepository.DeleteAsync(id.Value);

            return RedirectToPage("./Index");
        }
    }
}
