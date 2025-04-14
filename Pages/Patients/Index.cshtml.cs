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
    public class IndexModel : PageModel
    {
        private readonly IPatientRepository _patientRepository;

        public IndexModel(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public IList<Patient> Patient { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Patient = (await _patientRepository.GetAllAsync()).ToList();
        }
    }
}
