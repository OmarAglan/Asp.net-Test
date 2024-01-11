using Microsoft.EntityFrameworkCore;
using Roshta.Models; // Update namespace

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
} 