using Roshta.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roshta.Repositories.Interfaces;

public interface IPrescriptionRepository
{
    // We'll likely add Get methods later for viewing prescriptions
    Task<IEnumerable<Prescription>> GetAllAsync(); // Renamed from GetAllPrescriptionsAsync
    Task<IEnumerable<Prescription>> SearchAsync(string searchTerm); // Add this
    Task<Prescription?> GetByIdAsync(int id); // Renamed from GetPrescriptionByIdAsync
    Task<Prescription> AddAsync(Prescription prescription); // Renamed from CreatePrescriptionAsync
    Task<bool> CancelAsync(int prescriptionId); // Add this
    // Consider adding Update/Delete if needed
} 