using System.ComponentModel.DataAnnotations;

namespace Roshta.Models;

public class Doctor
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Specialization { get; set; }

    [StringLength(50)]
    public string? LicenseNumber { get; set; } // Should be validated

    // Navigation property for related Prescriptions
    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
} 