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
    public class IndexModel : PageModel
    {
        private readonly Roshta.Data.ApplicationDbContext _context;

        public IndexModel(Roshta.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Medication> Medication { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Medication = await _context.Medications.ToListAsync();
        }
    }
}
