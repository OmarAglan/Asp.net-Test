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

    public async Task<IEnumerable<Patient>> SearchAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return await GetAllAsync(); // Return all if search term is empty
        }

        var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

        return await _context.Patients
                             .Where(p => (p.Name != null && p.Name.ToLower().Contains(lowerCaseSearchTerm)) || 
                                         (p.ContactInfo != null && p.ContactInfo.ToLower().Contains(lowerCaseSearchTerm)))
                             .OrderBy(p => p.Name) // Keep consistent ordering
                             .ToListAsync();
    }

    public async Task<Patient?> GetByIdAsync(int id)
    {
        // Include related data if needed often, e.g., Prescriptions
        // return await _context.Patients.Include(p => p.Prescriptions).FirstOrDefaultAsync(p => p.Id == id);
        return await _context.Patients.FindAsync(id);
    }

    public async Task<Patient> AddAsync(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
        return patient;
    }

    public async Task<bool> UpdateAsync(Patient patient)
    {
        _context.Entry(patient).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
        catch (DbUpdateException)
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null)
        {
            return false;
        }
        
        _context.Patients.Remove(patient);
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException)
        {
            return false;
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Patients.AnyAsync(e => e.Id == id);
    }

    // Implementation for the new interface method
    public async Task<bool> IsContactInfoUniqueAsync(string contactInfo, int? currentId = null)
    {
        if (string.IsNullOrWhiteSpace(contactInfo))
        {
            // Consider empty/whitespace contact info as non-unique or handle based on requirements
            // If ContactInfo is nullable or not required, empty might be considered "unique"
            return true; 
        }

        // Normalize for case-insensitive comparison
        var normalizedContact = contactInfo.Trim().ToLower();

        bool exists = await _context.Patients
            .Where(p => p.ContactInfo != null && p.ContactInfo.ToLower() == normalizedContact)
            .Where(p => currentId == null || p.Id != currentId.Value) // Exclude current item if ID provided
            .AnyAsync();

        return !exists; // True if no conflicting record exists
    }

    // Implementation for Name uniqueness
    public async Task<bool> IsNameUniqueAsync(string name, int? currentId = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return true; // Consider empty name unique or handle based on requirements
        }

        var normalizedName = name.Trim().ToLower();

        bool exists = await _context.Patients
            .Where(p => p.Name != null && p.Name.ToLower() == normalizedName)
            .Where(p => currentId == null || p.Id != currentId.Value) 
            .AnyAsync();

        return !exists; 
    }
} 