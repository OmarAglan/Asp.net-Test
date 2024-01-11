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

    // Navigation property for related Prescriptions
    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
} 