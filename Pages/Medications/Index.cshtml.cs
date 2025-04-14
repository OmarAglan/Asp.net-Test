using Microsoft.AspNetCore.Mvc.RazorPages;
using Roshta.Models;
using Roshta.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roshta.Pages_Medications
{
    public class IndexModel : PageModel
    {
        private readonly IMedicationRepository _medicationRepository;

        public IndexModel(IMedicationRepository medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }

        public IList<Medication> Medication { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Medication = (await _medicationRepository.GetAllAsync()).ToList();
        }
    }
}
