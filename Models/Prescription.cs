using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Roshta.Models;

public class Prescription
{
    public int Id { get; set; }

    [Required]
    public int PatientId { get; set; }
    [ForeignKey("PatientId")]
    public virtual Patient Patient { get; set; } = null!;

    [Required]
    public int DoctorId { get; set; }
    [ForeignKey("DoctorId")]
    public virtual Doctor Doctor { get; set; } = null!;

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime DateIssued { get; set; } = DateTime.UtcNow;

    [DataType(DataType.Date)]
    public DateTime? ExpiryDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime? NextAppointmentDate { get; set; }

    public PrescriptionStatus Status { get; set; } = PrescriptionStatus.Active;

    // Audit Fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property for related PrescriptionItems
    public virtual ICollection<PrescriptionItem> PrescriptionItems { get; set; } = new List<PrescriptionItem>();
} 