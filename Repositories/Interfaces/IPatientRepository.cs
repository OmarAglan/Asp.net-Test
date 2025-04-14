using Roshta.Models;

namespace Roshta.Repositories.Interfaces;

public interface IPatientRepository
{
    Task<IEnumerable<Patient>> GetAllAsync();
    Task<Patient?> GetByIdAsync(int id);
    Task AddAsync(Patient patient);
    Task UpdateAsync(Patient patient);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    // Add any patient-specific methods here later if needed (e.g., FindByNameAsync)
} 