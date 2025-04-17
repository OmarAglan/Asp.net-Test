using Roshta.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roshta.Services.Interfaces;

public interface IPatientService
{
    Task<IEnumerable<Patient>> GetAllPatientsAsync();
    Task<IEnumerable<Patient>> SearchPatientsAsync(string searchTerm);
    Task<Patient?> GetPatientByIdAsync(int id);
    Task<Patient> AddPatientAsync(Patient patient);
    Task<Patient?> UpdatePatientAsync(Patient patient);
    Task<bool> DeletePatientAsync(int id);
    Task<bool> PatientExistsAsync(int id);
} 