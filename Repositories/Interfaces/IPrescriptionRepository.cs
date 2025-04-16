using Roshta.Models;

namespace Roshta.Repositories.Interfaces;

public interface IPrescriptionRepository
{
    // We'll likely add Get methods later for viewing prescriptions
    Task AddPrescriptionAsync(Prescription prescription); // Handles adding Prescription and its Items
    Task<Prescription?> GetPrescriptionByIdAsync(int id); // Added for potential details view later
    Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync(); // Added for potential list view later

} 