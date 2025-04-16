using Roshta.Models;
using Roshta.Repositories.Interfaces;
using Roshta.Services.Interfaces;
using System.Threading.Tasks;
using static Roshta.Pages.DoctorProfile.EditModel;

namespace Roshta.Services;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _doctorRepository;

    public DoctorService(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<Doctor?> GetDoctorProfileAsync()
    {
        return await _doctorRepository.GetDoctorProfileAsync();
    }

    public async Task<Doctor?> GetDoctorProfileAsync(int doctorId)
    {
        return await _doctorRepository.GetDoctorProfileAsync(doctorId);
    }

    public async Task<Doctor> SaveDoctorProfileAsync(Doctor doctor)
    {
        // Add validation logic here if needed (e.g., check required fields)
        // Ensure IsActive/IsSubscribed aren't accidentally changed by user input
        // (though they shouldn't be on the setup/edit form directly)
        return await _doctorRepository.SaveDoctorProfileAsync(doctor);
    }

    public async Task<bool> UpdateDoctorProfileAsync(int doctorId, DoctorProfileInputModel profileInput)
    {
        // Add any service-level validation or logic here if needed
        return await _doctorRepository.UpdateDoctorProfileAsync(doctorId, profileInput);
    }
} 