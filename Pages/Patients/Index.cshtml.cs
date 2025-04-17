using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Roshta.Data;
using Roshta.Models;
using Roshta.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Roshta.Pages_Patients
{
    public class IndexModel : PageModel
    {
        private readonly IPatientService _patientService;

        public IndexModel(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public IList<Patient> Patient { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            IEnumerable<Patient> patients;
            if (!string.IsNullOrEmpty(SearchString))
            {
                // TODO: Implement SearchPatientsAsync in service/repo
                patients = await _patientService.SearchPatientsAsync(SearchString);
            }
            else
            {
                patients = await _patientService.GetAllPatientsAsync();
            }
            Patient = patients.ToList();
        }
    }
}
