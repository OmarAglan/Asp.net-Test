using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roshta.Models;
using Roshta.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Roshta.Pages.DoctorProfile;

public class SetupModel : PageModel
{
    private readonly IDoctorService _doctorService;
    private readonly ILicenseService _licenseService;
    private readonly ILogger<SetupModel> _logger;

    public SetupModel(IDoctorService doctorService, ILicenseService licenseService, ILogger<SetupModel> logger)
    {
        _doctorService = doctorService;
        _licenseService = licenseService;
        _logger = logger;
    }

    [BindProperty]
    public DoctorProfileInputModel DoctorProfile { get; set; } = new DoctorProfileInputModel();

    // ViewModel for the form to avoid overposting issues and unwanted fields
    public class DoctorProfileInputModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Specialization { get; set; }

        [StringLength(50)]
        [Display(Name = "License Number")]
        public string? LicenseNumber { get; set; }

        [Phone]
        [StringLength(20)]
        [Display(Name = "Contact Phone")]
        public string? ContactPhone { get; set; }

        [EmailAddress]
        [StringLength(100)]
        [Display(Name = "Contact Email")]
        public string? ContactEmail { get; set; }
    }


    public async Task<IActionResult> OnGetAsync()
    {
        // Ensure activated but profile not set up
        if (!_licenseService.IsActivated())
        {
            _logger.LogWarning("Attempted to access profile setup without activation.");
            return RedirectToPage("/Activate");
        }
        if (_licenseService.IsProfileSetup())
        {
             _logger.LogInformation("Profile already set up. Redirecting from Setup page.");
             // Maybe redirect to an Edit page or dashboard later
             return RedirectToPage("/Index"); 
        }

        // Pre-fill form if a profile somehow exists but isn't marked as setup (edge case)
        var existingProfile = await _doctorService.GetDoctorProfileAsync();
        if(existingProfile != null)
        {
             DoctorProfile.Name = existingProfile.Name;
             DoctorProfile.Specialization = existingProfile.Specialization;
             DoctorProfile.LicenseNumber = existingProfile.LicenseNumber;
             DoctorProfile.ContactPhone = existingProfile.ContactPhone;
             DoctorProfile.ContactEmail = existingProfile.ContactEmail;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // Re-check activation/setup status on post
         if (!_licenseService.IsActivated())
        {
            return RedirectToPage("/Activate");
        }
        if (_licenseService.IsProfileSetup())
        {
             return RedirectToPage("/Index"); 
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Map ViewModel to the actual Doctor model
        var doctorToSave = await _doctorService.GetDoctorProfileAsync() ?? new Doctor();
        
        doctorToSave.Name = DoctorProfile.Name;
        doctorToSave.Specialization = DoctorProfile.Specialization;
        doctorToSave.LicenseNumber = DoctorProfile.LicenseNumber;
        doctorToSave.ContactPhone = DoctorProfile.ContactPhone;
        doctorToSave.ContactEmail = DoctorProfile.ContactEmail;
        // Keep existing IsSubscribed/IsActive flags or set defaults if new
        doctorToSave.IsActive = true; 
        // IsSubscribed would typically be managed elsewhere/later
        // doctorToSave.IsSubscribed = true; // Don't set here unless license implies subscription

        try
        {
            var savedDoctor = await _doctorService.SaveDoctorProfileAsync(doctorToSave);
            
            // Mark profile as setup with the saved doctor's ID
            _licenseService.MarkProfileAsSetup(savedDoctor.Id);
            
            _logger.LogInformation("Doctor profile setup completed for Doctor ID: {DoctorId}", savedDoctor.Id);
            TempData["SuccessMessage"] = "Doctor profile saved successfully!";
            return RedirectToPage("/Index"); // Redirect to main app page
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving doctor profile.");
            ModelState.AddModelError(string.Empty, "An error occurred while saving the profile. Please try again.");
            return Page();
        }
    }
} 