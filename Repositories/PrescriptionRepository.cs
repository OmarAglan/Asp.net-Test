using Microsoft.EntityFrameworkCore;
using Roshta.Data;
using Roshta.Models;
using Roshta.Repositories.Interfaces;

namespace Roshta.Repositories;

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly ApplicationDbContext _context;

    public PrescriptionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddPrescriptionAsync(Prescription prescription)
    {
        // EF Core tracks related entities added to navigation properties.
        // Adding the Prescription will also add its associated PrescriptionItems.
        await _context.Prescriptions.AddAsync(prescription);
        await _context.SaveChangesAsync(); // Commit the transaction
    }

    public async Task<Prescription?> GetPrescriptionByIdAsync(int id)
    {
        // Include related data needed for viewing details
        return await _context.Prescriptions
            .Include(p => p.Patient)
            .Include(p => p.Doctor)
            .Include(p => p.PrescriptionItems) // Include the items
                .ThenInclude(pi => pi.Medication) // Include the Medication for each item
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync()
    {
        // Include basic related data for list view, maybe not all items initially for performance
        return await _context.Prescriptions
            .Include(p => p.Patient)
            .Include(p => p.Doctor)
            .OrderByDescending(p => p.DateIssued) // Example ordering
            .ToListAsync();
    }
} 