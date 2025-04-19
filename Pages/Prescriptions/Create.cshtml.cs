using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering; // For SelectList
using Roshta.Models; // Potentially for error handling or direct model use if needed
using Roshta.Repositories.Interfaces;
using Roshta.Services.Interfaces;
using Roshta.ViewModels;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Roshta.Pages.Prescriptions
{
    public class CreateModel : PageModel
    {
        private readonly IPrescriptionService _prescriptionService;
        private readonly IPatientRepository _patientRepository;
        private readonly IMedicationRepository _medicationRepository;
        private readonly ILicenseService _licenseService;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(
            IPrescriptionService prescriptionService,
            IPatientRepository patientRepository,
            IMedicationRepository medicationRepository,
            ILicenseService licenseService,
            ILogger<CreateModel> logger)
        {
            _prescriptionService = prescriptionService;
            _patientRepository = patientRepository;
            _medicationRepository = medicationRepository;
            _licenseService = licenseService;
            _logger = logger;
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
            // --- Pre-check selected Patient ID --- 
            if (!await _patientRepository.ExistsAsync(PrescriptionCreate.PatientId))
            {
                ModelState.AddModelError("PrescriptionCreate.PatientId", "Selected patient does not exist.");
            }

            // --- Pre-check selected Medication IDs --- 
            if (PrescriptionCreate.Items != null)
            {
                // Ensure we have the select list available for looking up names
                if (MedicationSelectList == null) { await OnGetAsync(); }
                
                for(int i = 0; i < PrescriptionCreate.Items.Count; i++)
                {
                    var item = PrescriptionCreate.Items[i];
                    if (!await _medicationRepository.ExistsAsync(item.MedicationId))
                    {
                        // Try to find the text corresponding to the invalid ID from the select list
                        var medName = MedicationSelectList?.FirstOrDefault(m => m.Value == item.MedicationId.ToString())?.Text;
                        var errorMsg = medName != null 
                            ? $"Selected medication '{medName}' no longer exists or is invalid."
                            : $"Selected medication (ID: {item.MedicationId}) does not exist."; // Fallback if name not found
                        
                        // Add error specific to the item in the list
                        ModelState.AddModelError($"PrescriptionCreate.Items[{i}].MedicationId", errorMsg); 
                    }
                }
            }
            
            // --- Check Model State AFTER custom checks --- 
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model validation failed during prescription creation.");
                await OnGetAsync(); // Re-populate lists
                return Page();
            }

            // --- Get Doctor ID from License Service --- 
            int? currentDoctorId = _licenseService.GetCurrentDoctorId();
            if (currentDoctorId == null)
            {
                // This shouldn't happen if the ActivationCheckFilter is working correctly,
                // but handle it defensively.
                _logger.LogError("Could not retrieve Doctor ID for prescription creation. Profile might not be set up.");
                ModelState.AddModelError(string.Empty, "Cannot create prescription. Doctor profile not found.");
                await OnGetAsync(); // Re-populate lists
                return Page();
            }
            // ---------------------------------------------------

            var createdPrescription = await _prescriptionService.CreatePrescriptionAsync(PrescriptionCreate, currentDoctorId.Value);

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
            return RedirectToPage("./Index"); // Redirect to prescription list now
        }

        // AJAX Handler to check if a medication ID exists
        public async Task<IActionResult> OnGetCheckMedicationExistsAsync(int id)
        {
            bool exists = await _medicationRepository.ExistsAsync(id);
            return new JsonResult(new { exists });
        }
    }
} 