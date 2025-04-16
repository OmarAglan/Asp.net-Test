using Roshta.Models;
using Roshta.Repositories.Interfaces;
using Roshta.Services.Interfaces;
using Roshta.ViewModels;

namespace Roshta.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IPatientRepository _patientRepository; // To validate PatientId
    // private readonly IDoctorRepository _doctorRepository; // Needed later when we have Doctor repo
    // Potentially IMedicationRepository if validation is needed here

    public PrescriptionService(IPrescriptionRepository prescriptionRepository,
                               IPatientRepository patientRepository)
    {
        _prescriptionRepository = prescriptionRepository;
        _patientRepository = patientRepository;
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
        catch (Exception ex)
        {   
            // TODO: Add proper logging of the exception (ex)
            // Handle potential database errors
            return null;
        }
    }
} 