using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Roshta.Data;
using Roshta.Models;
using Roshta.Services.Interfaces;

namespace Roshta.Pages_Patients
{
    public class EditModel : PageModel
    {
        private readonly IPatientService _patientService;

        public EditModel(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [BindProperty]
        public Patient Patient { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient =  await _patientService.GetPatientByIdAsync(id.Value);
            if (patient == null)
            {
                return NotFound();
            }
            Patient = patient;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _patientService.UpdatePatientAsync(Patient);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PatientExistsAsync(Patient.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> PatientExistsAsync(int id)
        {
             return await _patientService.PatientExistsAsync(id);
        }
    }
}
