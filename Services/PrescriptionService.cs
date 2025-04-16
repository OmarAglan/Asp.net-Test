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
        // --- Validation --- 
        // Ensure the selected patient exists
        if (!await _patientRepository.ExistsAsync(model.PatientId))
        {
            // Handle error - perhaps return null or throw specific exception
            // Consider adding logging here
            return null; 
        }

        // TODO: Validate DoctorId when authentication/doctor repository is implemented
        // if (!await _doctorRepository.ExistsAsync(doctorId)) return null;

        // Ensure there are items to add
        if (model.Items == null || !model.Items.Any())
        {
            // Handle error - no items added
            return null;
        }

        // TODO: Validate that each MedicationId in model.Items exists?
        // Might require IMedicationRepository dependency

        // --- Mapping --- 
        var prescription = new Prescription
        {
            PatientId = model.PatientId,
            DoctorId = doctorId, // Use the passed-in doctorId
            DateIssued = DateTime.UtcNow, // Set by the service
            ExpiryDate = model.ExpiryDate,
            NextAppointmentDate = model.NextAppointmentDate,
            Status = PrescriptionStatus.Active, // Default status
            // CreatedAt/UpdatedAt handled by DbContext override

            PrescriptionItems = model.Items.Select(itemModel => new PrescriptionItem
            {
                MedicationId = itemModel.MedicationId,
                Quantity = itemModel.Quantity,
                Instructions = itemModel.Instructions,
                Refills = itemModel.Refills,
                Notes = itemModel.Notes
                // CreatedAt/UpdatedAt handled by DbContext override
            }).ToList()
        };

        // --- Persistence --- 
        try
        {
            await _prescriptionRepository.AddPrescriptionAsync(prescription);
            return prescription; // Return the created prescription (with its new Id)
        }
        catch (Exception)
        {
            // TODO: Add proper logging of the exception (ex)
            // Handle potential database errors
            return null;
        }
    }

    // Add implementation for GetAllPrescriptionsAsync
    public async Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync()
    {
        // TODO: Add filtering logic here later (e.g., by logged-in doctorId)
        try
        {
            return await _prescriptionRepository.GetAllPrescriptionsAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting prescriptions: {ex.Message}");
            return new List<Prescription>();
        }
    }

    // Add implementation for GetPrescriptionByIdAsync
    public async Task<Prescription?> GetPrescriptionByIdAsync(int id)
    {
        return await _prescriptionRepository.GetPrescriptionByIdAsync(id);
    }
} 