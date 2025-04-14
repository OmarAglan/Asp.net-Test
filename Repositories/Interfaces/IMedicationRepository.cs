using Roshta.Models;

namespace Roshta.Repositories.Interfaces;

public interface IMedicationRepository
{
    Task<IEnumerable<Medication>> GetAllAsync();
    Task<Medication?> GetByIdAsync(int id);
    Task AddAsync(Medication medication);
    Task UpdateAsync(Medication medication);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id); // Useful helper method
} 