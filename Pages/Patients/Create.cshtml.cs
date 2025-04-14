using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Roshta.Data;
using Roshta.Models;
using Roshta.Repositories.Interfaces;

namespace Roshta.Pages_Patients
{
    public class CreateModel : PageModel
    {
        private readonly IPatientRepository _patientRepository;

        public CreateModel(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Patient Patient { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Patient == null)
            {
                return Page();
            }

            await _patientRepository.AddAsync(Patient);

            return RedirectToPage("./Index");
        }
    }
}
