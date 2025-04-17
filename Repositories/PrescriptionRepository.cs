using Microsoft.EntityFrameworkCore;
using Roshta.Data;
using Roshta.Models;
using Roshta.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roshta.Repositories;

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly ApplicationDbContext _context;

    public PrescriptionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Prescription>> GetAllAsync()
    {
        // Include Patient info for display in the list
        return await _context.Prescriptions
                             .Include(p => p.Patient)
                             .OrderByDescending(p => p.DateIssued) // Show newest first
                             .ToListAsync();
    }

    public async Task<IEnumerable<Prescription>> SearchAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return await GetAllAsync();
        }

        var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

        // Search by Patient Name
        return await _context.Prescriptions
                             .Include(p => p.Patient) // Need patient info for searching and display
                             .Where(p => p.Patient != null && p.Patient.Name != null && p.Patient.Name.ToLower().Contains(lowerCaseSearchTerm))
                             .OrderByDescending(p => p.DateIssued) // Keep consistent ordering
                             .ToListAsync();
    }

    public async Task<Prescription?> GetByIdAsync(int id)
    {
        // Include related data needed for the Details view
        return await _context.Prescriptions
                             .Include(p => p.Patient)
                             .Include(p => p.Doctor)
                             .Include(p => p.PrescriptionItems)
                                .ThenInclude(pi => pi.Medication)
                             .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Prescription> AddAsync(Prescription prescription)
    {
        await _context.Prescriptions.AddAsync(prescription);
        // Note: EF Core should handle adding related PrescriptionItems automatically
        // if they are part of the 'prescription' object graph being added.
        await _context.SaveChangesAsync();
        return prescription;
    }

    // Implement Update/Delete later if needed
} 