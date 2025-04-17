using Roshta.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roshta.Repositories.Interfaces;

public interface IPatientRepository
{
    Task<IEnumerable<Patient>> GetAllAsync();
    Task<IEnumerable<Patient>> SearchAsync(string searchTerm);
    Task<Patient?> GetByIdAsync(int id);
    Task<Patient> AddAsync(Patient patient);
    Task<bool> UpdateAsync(Patient patient);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    // Add any patient-specific methods here later if needed (e.g., FindByNameAsync)
} 