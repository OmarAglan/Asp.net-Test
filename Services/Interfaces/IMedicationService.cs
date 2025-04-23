using Roshta.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roshta.Services.Interfaces;

public interface IMedicationService
{
    Task<IEnumerable<Medication>> GetAllMedicationsAsync();
    Task<IEnumerable<Medication>> SearchMedicationsAsync(string searchTerm);
    Task<Medication?> GetMedicationByIdAsync(int id);
    Task<Medication> AddMedicationAsync(Medication medication);
    Task<Medication?> UpdateMedicationAsync(Medication medication);
    Task<bool> DeleteMedicationAsync(int id);
    Task<bool> MedicationExistsAsync(int id);
    Task<bool> IsNameUniqueAsync(string name, int? currentId = null);
} 