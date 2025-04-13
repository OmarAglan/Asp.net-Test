namespace Roshta.Models;

public enum PrescriptionStatus
{
    Active,    // Newly issued or still valid
    Expired,   // Past its expiry date
    Filled,    // Dispensed by pharmacy (potential future use)
    Cancelled  // Doctor cancelled the prescription
} 