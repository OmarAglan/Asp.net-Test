using Roshta.Models;
using Roshta.ViewModels;

namespace Roshta.Services.Interfaces;

public interface IPrescriptionService
{
    // Takes the view model and the ID of the doctor creating the prescription
    Task<Prescription?> CreatePrescriptionAsync(PrescriptionCreateModel model, int doctorId);
} 