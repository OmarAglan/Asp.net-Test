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

    [Phone]
    [StringLength(20)]
    public string? ContactPhone { get; set; }

    [EmailAddress]
    [StringLength(100)]
    public string? ContactEmail { get; set; }

    public bool IsSubscribed { get; set; } = false; // Added: Payed for program or not

    public bool IsActive { get; set; } = true; // Suggested: Active status

    // Audit Fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property for related Prescriptions
    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
} 