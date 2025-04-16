using Roshta.Models;
using Roshta.Repositories.Interfaces;
using Roshta.Services.Interfaces;

namespace Roshta.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
    {
        // Business logic like filtering only active patients could go here later
        return await _patientRepository.GetAllAsync();
    }

    public async Task<Patient?> GetPatientByIdAsync(int id)
    {
        return await _patientRepository.GetByIdAsync(id);
    }

    public async Task AddPatientAsync(Patient patient)
    {
        // Business logic/validation before saving
        await _patientRepository.AddAsync(patient);
    }

    public async Task UpdatePatientAsync(Patient patient)
    {
        // Business logic/validation before saving
        await _patientRepository.UpdateAsync(patient);
    }

    public async Task DeletePatientAsync(int id)
    {
        // Business logic (e.g., can't delete patient with outstanding balance?)
        await _patientRepository.DeleteAsync(id);
    }

    public async Task<bool> PatientExistsAsync(int id)
    {
        return await _patientRepository.ExistsAsync(id);
    }
} 