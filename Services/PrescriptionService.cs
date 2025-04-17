using Roshta.Models;
using Roshta.Repositories.Interfaces;
using Roshta.Services.Interfaces;
using Roshta.ViewModels;
using Microsoft.Extensions.Logging;

namespace Roshta.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IPatientRepository _patientRepository; // To validate PatientId
    // private readonly IDoctorRepository _doctorRepository; // Needed later when we have Doctor repo
    // Potentially IMedicationRepository if validation is needed here
    private readonly ILogger<PrescriptionService> _logger;

    public PrescriptionService(IPrescriptionRepository prescriptionRepository,
                               IPatientRepository patientRepository,
                               ILogger<PrescriptionService> logger)
    {
        _prescriptionRepository = prescriptionRepository;
        _patientRepository = patientRepository;
        _logger = logger;
    }

    public async Task<Prescription?> CreatePrescriptionAsync(PrescriptionCreateModel model, int doctorId)
    {
        // Map ViewModel to Model
        var prescription = new Prescription
        {
            PatientId = model.PatientId,
            DoctorId = doctorId, // Use the ID passed from the license service
            DateIssued = DateTime.UtcNow, // Set issue date on creation
            ExpiryDate = model.ExpiryDate,
            NextAppointmentDate = model.NextAppointmentDate,
            Status = PrescriptionStatus.Active, // Default status
            PrescriptionItems = new List<PrescriptionItem>()
        };

        // Add PrescriptionItems
        foreach (var itemModel in model.Items)
        {
            if (itemModel.MedicationId > 0 && !string.IsNullOrWhiteSpace(itemModel.Instructions))
            {
                prescription.PrescriptionItems.Add(new PrescriptionItem
                {
                    MedicationId = itemModel.MedicationId,
                    Dosage = itemModel.Dosage,
                    Frequency = itemModel.Frequency,
                    Duration = itemModel.Duration,
                    Instructions = itemModel.Instructions,
                    Prescription = prescription // Link back to parent
                });
            }
        }

        // Validate (basic example - add more robust validation)
        if (!prescription.PrescriptionItems.Any())
        {
            throw new ArgumentException("Prescription must have at least one item.");
        }

        // Save using the repository
        return await _prescriptionRepository.AddAsync(prescription); // Use renamed method
    }

    // Add implementation for GetAllPrescriptionsAsync
    public async Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync()
    {
        // Enhance later to include Patient/Doctor info if needed for display
        return await _prescriptionRepository.GetAllAsync(); // Use renamed method
    }

    public async Task<IEnumerable<Prescription>> SearchPrescriptionsAsync(string searchTerm)
    {
        // Implement search logic, potentially involving related entities
        return await _prescriptionRepository.SearchAsync(searchTerm);
    }

    // Add implementation for GetPrescriptionByIdAsync
    public async Task<Prescription?> GetPrescriptionByIdAsync(int id)
    {
        // Enhance later to include related items/patient/doctor
        return await _prescriptionRepository.GetByIdAsync(id); // Use renamed method
    }

    // Add Update/Delete service methods later if needed
} 