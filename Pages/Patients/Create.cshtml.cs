using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Roshta.Data;
using Roshta.Models;
using Roshta.Services.Interfaces;

namespace Roshta.Pages_Patients
{
    public class CreateModel : PageModel
    {
        private readonly IPatientService _patientService;

        public CreateModel(IPatientService patientService)
        {
            _patientService = patientService;
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

            await _patientService.AddPatientAsync(Patient);

            return RedirectToPage("./Index");
        }
    }
}
