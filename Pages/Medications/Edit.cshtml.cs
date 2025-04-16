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

namespace Roshta.Pages_Medications
{
    public class EditModel : PageModel
    {
        private readonly IMedicationService _medicationService;

        public EditModel(IMedicationService medicationService)
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
            Medication = medication;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _medicationService.UpdateMedicationAsync(Medication);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MedicationExistsAsync(Medication.Id))
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

        private async Task<bool> MedicationExistsAsync(int id)
        {
            return await _medicationService.MedicationExistsAsync(id);
        }
    }
}
