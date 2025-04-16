using System.ComponentModel.DataAnnotations;

namespace Roshta.ViewModels;

public class PrescriptionCreateModel
{
    [Required(ErrorMessage = "Please select a patient.")]
    [Display(Name = "Patient")]
    public int PatientId { get; set; }

    // We will need to get the DoctorId from the logged-in user later.
    // For now, we might omit it or hardcode it in the service for testing.
    // public int DoctorId { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Expiry Date")]
    public DateTime? ExpiryDate { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Next Appointment")]
    public DateTime? NextAppointmentDate { get; set; }

    // List to hold the items added via the UI
    public List<PrescriptionItemCreateModel> Items { get; set; } = new List<PrescriptionItemCreateModel>();

    // Nested class for individual items
    public class PrescriptionItemCreateModel
    {
        [Required(ErrorMessage = "Please select a medication.")]
        public int MedicationId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [StringLength(50)]
        public string Quantity { get; set; } = string.Empty;

        [Required(ErrorMessage = "Instructions are required.")]
        [StringLength(300)]
        public string Instructions { get; set; } = string.Empty;

        public int? Refills { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }
} 