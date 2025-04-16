using Roshta.Models;

namespace Roshta.Services.Interfaces;

public interface IMedicationService
{
    Task<IEnumerable<Medication>> GetAllMedicationsAsync();
    Task<Medication?> GetMedicationByIdAsync(int id);
    Task AddMedicationAsync(Medication medication);
    Task UpdateMedicationAsync(Medication medication);
    Task DeleteMedicationAsync(int id);
    Task<bool> MedicationExistsAsync(int id);
} 