using Roshta.Models;
using System.Threading.Tasks;
using static Roshta.Pages.DoctorProfile.EditModel;

namespace Roshta.Services.Interfaces;

public interface IDoctorService
{
    Task<Doctor?> GetDoctorProfileAsync();
    Task<Doctor?> GetDoctorProfileAsync(int doctorId);
    Task<Doctor> SaveDoctorProfileAsync(Doctor doctor);
    Task<bool> UpdateDoctorProfileAsync(int doctorId, DoctorProfileInputModel profileInput);
} 