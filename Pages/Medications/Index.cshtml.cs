using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roshta.Models;
using Roshta.Services.Interfaces;
using System.Collections.Generic;
using System; // Added for Math.Ceiling
using System.Threading.Tasks;

namespace Roshta.Pages_Medications
{
    public class IndexModel : PageModel
    {
        private readonly IMedicationService _medicationService;
        private const int PageSize = 10; // Define page size

        public IndexModel(IMedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        public List<Medication> Medication { get;set; } = new List<Medication>(); // Initialize

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1; // Default to page 1

        public int TotalPages { get; set; }
        public int Count { get; set; }

        // --- Sorting Properties ---
        [BindProperty(SupportsGet = true)]
        public string? CurrentSort { get; set; }
        public string? NameSort { get; set; }
        // Add properties for other sortable columns (Dosage, Form, Manufacturer) if needed later
        // -------------------------

        public async Task OnGetAsync(string? sortOrder) // Add sortOrder parameter
        {
            CurrentSort = sortOrder;

            // --- Determine next sort order for links ---
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : ""; // Default sort is Name Asc
            // Set other sort properties here if implementing sorting for them
            // -------------------------------------------

            // Ensure CurrentPage is at least 1
            if (CurrentPage < 1)
            {
                CurrentPage = 1;
            }

            // Get total count for pagination calculation
            Count = await _medicationService.GetMedicationsCountAsync(SearchString);
            TotalPages = (int)Math.Ceiling(Count / (double)PageSize);

            // Ensure CurrentPage is not beyond the last page
            if (CurrentPage > TotalPages && TotalPages > 0)
            {
                CurrentPage = TotalPages;
            }

            // Get the paged data, passing the current sort order
            Medication = await _medicationService.GetMedicationsPagedAsync(CurrentPage, PageSize, SearchString, CurrentSort);

            // Note: The old logic using SearchMedicationsAsync/GetAllMedicationsAsync is replaced.
        }
    }
}
