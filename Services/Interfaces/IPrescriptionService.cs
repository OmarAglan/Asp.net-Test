using Roshta.Models;
using Roshta.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roshta.Services.Interfaces;

public interface IPrescriptionService
{
    // Takes the view model and the ID of the doctor creating the prescription
    Task<Prescription?> CreatePrescriptionAsync(PrescriptionCreateModel model, int doctorId);
    Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync();
    Task<IEnumerable<Prescription>> SearchPrescriptionsAsync(string searchTerm);
    Task<Prescription?> GetPrescriptionByIdAsync(int id);
} 