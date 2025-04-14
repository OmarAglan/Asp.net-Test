using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Roshta.Data;
using Roshta.Models;
using Roshta.Repositories.Interfaces;

namespace Roshta.Pages_Patients
{
    public class DeleteModel : PageModel
    {
        private readonly IPatientRepository _patientRepository;

        public DeleteModel(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [BindProperty]
        public Patient Patient { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _patientRepository.GetByIdAsync(id.Value);

            if (patient == null)
            {
                return NotFound();
            }
            else 
            {
                Patient = patient;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            await _patientRepository.DeleteAsync(id.Value);

            return RedirectToPage("./Index");
        }
    }
}
