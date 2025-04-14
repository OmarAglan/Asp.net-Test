using Microsoft.EntityFrameworkCore;
using Roshta.Data;
using Roshta.Models;
using Roshta.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roshta.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly ApplicationDbContext _context;

    public PatientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Patient>> GetAllAsync()
    {
        // Consider ordering for consistency, e.g., by Name
        return await _context.Patients.OrderBy(p => p.Name).ToListAsync();
    }

    public async Task<Patient?> GetByIdAsync(int id)
    {
        // Include related data if needed often, e.g., Prescriptions
        // return await _context.Patients.Include(p => p.Prescriptions).FirstOrDefaultAsync(p => p.Id == id);
        return await _context.Patients.FindAsync(id);
    }

    public async Task AddAsync(Patient patient)
    {
        // Could add validation or default values here if not handled elsewhere
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Patient patient)
    {
        // The DbContext is already tracking the original entity if it was loaded
        // Setting the state ensures it gets marked as modified if changes occurred.
        _context.Entry(patient).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var patient = await GetByIdAsync(id);
        if (patient != null)
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }
        // Consider handling cases where deletion might be restricted (e.g., if patient has active prescriptions)
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Patients.AnyAsync(e => e.Id == id);
    }
} 