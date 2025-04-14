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

    public async Task<Medication?> GetByIdAsync(int id)
    {
        return await _context.Medications.FindAsync(id);
    }

    public async Task AddAsync(Medication medication)
    {
        await _context.Medications.AddAsync(medication);
        await _context.SaveChangesAsync(); // Save changes here
    }

    public async Task UpdateAsync(Medication medication)
    {
        _context.Entry(medication).State = EntityState.Modified;
        await _context.SaveChangesAsync(); // Save changes here
    }

    public async Task DeleteAsync(int id)
    {
        var medication = await GetByIdAsync(id);
        if (medication != null)
        {
            _context.Medications.Remove(medication);
            await _context.SaveChangesAsync(); // Save changes here
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Medications.AnyAsync(e => e.Id == id);
    }
} 