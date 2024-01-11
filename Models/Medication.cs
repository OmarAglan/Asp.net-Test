using System.ComponentModel.DataAnnotations;

namespace Roshta.Models;

public class Medication
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty; // e.g., "Amoxicillin 500mg Capsules"

    [StringLength(100)]
    public string? Dosage { get; set; } // e.g., "500mg"

    [StringLength(50)]
    public string? Form { get; set; } // e.g., "Capsule", "Tablet", "Liquid"

    [StringLength(100)]
    public string? Manufacturer { get; set; }

    // Navigation property for related PrescriptionItems
    public virtual ICollection<PrescriptionItem> PrescriptionItems { get; set; } = new List<PrescriptionItem>();

} 