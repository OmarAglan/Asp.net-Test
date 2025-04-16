using Microsoft.EntityFrameworkCore;
using Roshta.Data;
using Roshta.Models;
using Roshta.Repositories.Interfaces;
using static Roshta.Pages.DoctorProfile.EditModel;

namespace Roshta.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly ApplicationDbContext _context;

    public DoctorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Doctor?> GetDoctorProfileAsync()
    {
        // Since there should only be one, we can just take the first one found.
        return await _context.Doctors.FirstOrDefaultAsync();
    }

    // Implementation for the Get by ID overload
    public async Task<Doctor?> GetDoctorProfileAsync(int doctorId)
    {
        return await _context.Doctors.FindAsync(doctorId);
    }

    public async Task<Doctor> SaveDoctorProfileAsync(Doctor doctor)
    {
        var existingDoctor = await GetDoctorProfileAsync();

        if (existingDoctor == null)
        {
            // No doctor exists, add this one
            // Reset ID to 0 to ensure EF Core assigns a new one if it was set somehow
            doctor.Id = 0; 
            await _context.Doctors.AddAsync(doctor);
        }
        else
        {
            // Doctor exists, update it
            // Ensure we're updating the correct record by setting the ID
            doctor.Id = existingDoctor.Id;
            // Update properties (EF Core might need help tracking changes if the passed 'doctor' is not the tracked entity)
            _context.Entry(existingDoctor).CurrentValues.SetValues(doctor);
            _context.Entry(existingDoctor).State = EntityState.Modified; 
        }

        await _context.SaveChangesAsync();
        return doctor; // Return the saved entity (will have the correct ID)
    }

    // Implementation for UpdateDoctorProfileAsync
    public async Task<bool> UpdateDoctorProfileAsync(int doctorId, DoctorProfileInputModel profileInput)
    {
        var doctorToUpdate = await _context.Doctors.FindAsync(doctorId);

        if (doctorToUpdate == null)
        {
            return false; // Doctor not found
        }

        // Update properties from the input model
        doctorToUpdate.Name = profileInput.Name;
        doctorToUpdate.Specialization = profileInput.Specialization;
        doctorToUpdate.LicenseNumber = profileInput.LicenseNumber;
        doctorToUpdate.ContactPhone = profileInput.ContactPhone;
        doctorToUpdate.ContactEmail = profileInput.ContactEmail;
        // Note: Do not update IsActive or IsSubscribed here unless intended
        // Keep them as they were.

        _context.Entry(doctorToUpdate).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
            return true; // Update successful
        }
        catch (DbUpdateConcurrencyException)
        {
            // Handle potential concurrency issues if necessary
            // For now, just return false or re-throw
            return false;
        }
        catch (DbUpdateException)
        {
            // Handle other potential DB update errors
            return false;
        }
    }
} 