using System.ComponentModel.DataAnnotations;

namespace Roshta.Models;

public class Patient
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [StringLength(200)]
    public string? ContactInfo { get; set; } // e.g., phone, email

    public int VisitCount { get; set; } = 0; // Added: Number of visits

    [DataType(DataType.Date)]
    public DateTime? LastVisitDate { get; set; } // Added: Last time visited (nullable)

    public bool HasOutstandingBalance { get; set; } = false; // Added: Payed all or not

    public bool IsActive { get; set; } = true; // Suggested: Active status

    // Audit Fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property for related Prescriptions
    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
} 