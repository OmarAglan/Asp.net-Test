using Roshta.Models;

namespace Roshta.Services.Interfaces;

public interface IPatientService
{
    Task<IEnumerable<Patient>> GetAllPatientsAsync();
    Task<Patient?> GetPatientByIdAsync(int id);
    Task AddPatientAsync(Patient patient);
    Task UpdatePatientAsync(Patient patient);
    Task DeletePatientAsync(int id);
    Task<bool> PatientExistsAsync(int id);
} 