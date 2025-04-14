using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Roshta.Data;
using Roshta.Models;

namespace Roshta.Pages_Medications
{
    public class DetailsModel : PageModel
    {
        private readonly Roshta.Data.ApplicationDbContext _context;

        public DetailsModel(Roshta.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Medication Medication { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medication = await _context.Medications.FirstOrDefaultAsync(m => m.Id == id);

            if (medication is not null)
            {
                Medication = medication;

                return Page();
            }

            return NotFound();
        }
    }
}
