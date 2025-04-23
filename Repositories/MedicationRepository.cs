using Microsoft.EntityFrameworkCore;
using Roshta.Data;
using Roshta.Models;
using Roshta.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roshta.Repositories;

public class MedicationRepository : IMedicationRepository
{
    private readonly ApplicationDbContext _context;

    public MedicationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Medication>> GetAllAsync()
    {
        return await _context.Medications.ToListAsync();
    }

    public async Task<IEnumerable<Medication>> SearchAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return await GetAllAsync(); // Return all if search term is empty
        }

        var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

        return await _context.Medications
                             .Where(m => m.Name != null && m.Name.ToLower().Contains(lowerCaseSearchTerm))
                             .OrderBy(m => m.Name) // Keep consistent ordering
                             .ToListAsync();
    }

    public async Task<Medication?> GetByIdAsync(int id)
    {
        return await _context.Medications.FindAsync(id);
    }

    public async Task<Medication> AddAsync(Medication medication)
    {
        await _context.Medications.AddAsync(medication);
        await _context.SaveChangesAsync();
        return medication;
    }

    public async Task<bool> UpdateAsync(Medication medication)
    {
        _context.Entry(medication).State = EntityState.Modified;
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
        var medication = await _context.Medications.FindAsync(id);
        if (medication == null)
        {
            return false;
        }
        
        _context.Medications.Remove(medication);
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
        return await _context.Medications.AnyAsync(e => e.Id == id);
    }

    // Implementation for the new interface method
    public async Task<bool> IsNameUniqueAsync(string name, int? currentId = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return true; // Or false, depending on whether an empty name is allowed/unique
        }

        var normalizedName = name.Trim().ToLower();

        bool exists = await _context.Medications
            .Where(m => m.Name != null && m.Name.ToLower() == normalizedName)
            .Where(m => currentId == null || m.Id != currentId.Value) // Exclude current item if ID is provided
            .AnyAsync();

        return !exists; // True if no conflicting record exists
    }
} 