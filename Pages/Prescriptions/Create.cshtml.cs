using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering; // For SelectList
using Roshta.Models; // Potentially for error handling or direct model use if needed
using Roshta.Repositories.Interfaces;
using Roshta.Services.Interfaces;
using Roshta.ViewModels;
using System.Threading.Tasks;
using System.Linq;

namespace Roshta.Pages.Prescriptions
{
    public class CreateModel : PageModel
    {
        private readonly IPrescriptionService _prescriptionService;
        private readonly IPatientRepository _patientRepository;
        private readonly IMedicationRepository _medicationRepository;

        public CreateModel(
            IPrescriptionService prescriptionService,
            IPatientRepository patientRepository,
            IMedicationRepository medicationRepository)
        {
            _prescriptionService = prescriptionService;
            _patientRepository = patientRepository;
            _medicationRepository = medicationRepository;
        }

        // Properties to hold data for the form
        [BindProperty]
        public PrescriptionCreateModel PrescriptionCreate { get; set; } = new PrescriptionCreateModel();

        public SelectList? PatientSelectList { get; set; }
        public SelectList? MedicationSelectList { get; set; }

        public async Task OnGetAsync()
        {
            // Populate SelectLists for dropdowns
            var patients = await _patientRepository.GetAllAsync();
            PatientSelectList = new SelectList(patients.OrderBy(p => p.Name), nameof(Patient.Id), nameof(Patient.Name));

            var medications = await _medicationRepository.GetAllAsync();
            MedicationSelectList = new SelectList(medications.OrderBy(m => m.Name), nameof(Medication.Id), nameof(Medication.Name));
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Re-populate SelectLists if returning to the page due to errors
                await OnGetAsync(); // Re-run OnGet to repopulate lists
                return Page();
            }

            // --- TODO: Get Doctor ID from Authenticated User --- 
            // Hardcoding DoctorId = 1 for now. Replace this when authentication is implemented.
            // You might fetch the Doctor based on the logged-in user's identity.
            int currentDoctorId = 1; 
            // ---------------------------------------------------

            var createdPrescription = await _prescriptionService.CreatePrescriptionAsync(PrescriptionCreate, currentDoctorId);

            if (createdPrescription == null)
            {
                // Add a general error message if creation failed in the service
                // (The service should ideally log specific errors)
                ModelState.AddModelError(string.Empty, "Unable to create prescription. Please check inputs or contact support.");
                await OnGetAsync(); // Re-populate lists
                return Page();
            }

            // Redirect to a details page (or index for now) upon successful creation
            // TODO: Create a Details page later and redirect there: return RedirectToPage("./Details", new { id = createdPrescription.Id });
            TempData["SuccessMessage"] = "Prescription created successfully!"; // Add success message
            return RedirectToPage("/Index"); // Redirect to site home for now
        }
    }
} 