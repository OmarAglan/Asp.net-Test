using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roshta.Models;
using Roshta.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Roshta.Pages_Medications
{
    public class DetailsModel : PageModel
    {
        private readonly IMedicationRepository _medicationRepository;

        public DetailsModel(IMedicationRepository medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }

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
    }
}
