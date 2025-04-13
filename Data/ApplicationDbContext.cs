using Microsoft.EntityFrameworkCore;
using Roshta.Models; // Update namespace
using System.Reflection.Emit;

namespace Roshta.Data; // Update namespace

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Define DbSet properties for each of your entities
    public DbSet<Patient> Patients { get; set; } = default!;
    public DbSet<Doctor> Doctors { get; set; } = default!;
    public DbSet<Medication> Medications { get; set; } = default!;
    public DbSet<Prescription> Prescriptions { get; set; } = default!;
    public DbSet<PrescriptionItem> PrescriptionItems { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships and constraints here if not using Data Annotations
        // or if you need more complex configurations.

        // Example: If you wanted a unique constraint on Doctor LicenseNumber
        // modelBuilder.Entity<Doctor>()
        //     .HasIndex(d => d.LicenseNumber)
        //     .IsUnique();

        // Relationships are mostly inferred by EF Core based on navigation properties
        // and foreign key attributes used in the models, but you can be explicit:

        modelBuilder.Entity<Prescription>()
            .HasOne(p => p.Patient)
            .WithMany(pt => pt.Prescriptions)
            .HasForeignKey(p => p.PatientId);

        modelBuilder.Entity<Prescription>()
            .HasOne(p => p.Doctor)
            .WithMany(d => d.Prescriptions)
            .HasForeignKey(p => p.DoctorId);

        modelBuilder.Entity<PrescriptionItem>()
            .HasOne(pi => pi.Prescription)
            .WithMany(p => p.PrescriptionItems)
            .HasForeignKey(pi => pi.PrescriptionId);

        modelBuilder.Entity<PrescriptionItem>()
            .HasOne(pi => pi.Medication)
            .WithMany(m => m.PrescriptionItems)
            .HasForeignKey(pi => pi.MedicationId);

    }

    // Override SaveChanges to update audit fields
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void OnBeforeSaving()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        var utcNow = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            // Check if the entity has 'CreatedAt' and 'UpdatedAt' properties
            if (entry.Entity is Patient || entry.Entity is Doctor || entry.Entity is Medication || entry.Entity is Prescription || entry.Entity is PrescriptionItem)
            {
                var updatedAtProperty = entry.Property("UpdatedAt");
                if (updatedAtProperty != null)
                {
                    updatedAtProperty.CurrentValue = utcNow;
                }

                if (entry.State == EntityState.Added)
                {
                    var createdAtProperty = entry.Property("CreatedAt");
                    if (createdAtProperty != null)
                    {
                        createdAtProperty.CurrentValue = utcNow;
                    }
                }
            }
            // If you create a base class or interface for auditable entities,
            // you can make this logic more generic and cleaner.
            // Example with an interface IAuditable:
            // if (entry.Entity is IAuditable auditableEntity)
            // {
            //     auditableEntity.UpdatedAt = utcNow;
            //     if (entry.State == EntityState.Added)
            //     {
            //         auditableEntity.CreatedAt = utcNow;
            //     }
            // }
        }
    }
} 