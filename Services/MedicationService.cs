using Roshta.Models;
using Roshta.Repositories.Interfaces;
using Roshta.Services.Interfaces;

namespace Roshta.Services;

public class MedicationService : IMedicationService
{
    private readonly IMedicationRepository _medicationRepository;

    public MedicationService(IMedicationRepository medicationRepository)
    {
        _medicationRepository = medicationRepository;
    }

    public async Task<IEnumerable<Medication>> GetAllMedicationsAsync()
    {
        return await _medicationRepository.GetAllAsync();
    }

    public async Task<Medication?> GetMedicationByIdAsync(int id)
    {
        return await _medicationRepository.GetByIdAsync(id);
    }

    public async Task AddMedicationAsync(Medication medication)
    {
        // Add any medication-specific business logic/validation here in the future
        await _medicationRepository.AddAsync(medication);
    }

    public async Task UpdateMedicationAsync(Medication medication)
    {
        // Add any medication-specific business logic/validation here in the future
        await _medicationRepository.UpdateAsync(medication);
    }

    public async Task DeleteMedicationAsync(int id)
    {
        // Add any medication-specific business logic/validation here (e.g., check if medication is in use)
        await _medicationRepository.DeleteAsync(id);
    }

    public async Task<bool> MedicationExistsAsync(int id)
    {
        return await _medicationRepository.ExistsAsync(id);
    }
} 