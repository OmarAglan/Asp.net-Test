using Roshta.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roshta.Repositories.Interfaces;

public interface IMedicationRepository
{
    Task<IEnumerable<Medication>> GetAllAsync();
    Task<IEnumerable<Medication>> SearchAsync(string searchTerm);
    Task<Medication?> GetByIdAsync(int id);
    Task<Medication> AddAsync(Medication medication);
    Task<bool> UpdateAsync(Medication medication);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id); // Useful helper method
} 