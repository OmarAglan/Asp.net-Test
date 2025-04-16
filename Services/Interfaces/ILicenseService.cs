namespace Roshta.Services.Interfaces;

public interface ILicenseService
{
    /// <summary>
    /// Validates the entered registration and serial numbers against the expected values.
    /// </summary>
    /// <param name="enteredRegistrationNumber">The registration number entered by the user.</param>
    /// <param name="enteredSerialNumber">The serial number entered by the user.</param>
    /// <returns>True if the keys are valid, false otherwise.</returns>
    bool ValidateLicense(string enteredRegistrationNumber, string enteredSerialNumber);

    /// <summary>
    /// Checks if the application has been successfully activated.
    /// </summary>
    /// <returns>True if activated, false otherwise.</returns>
    bool IsActivated();

    /// <summary>
    /// Marks the application as activated (e.g., after successful validation).
    /// </summary>
    void MarkAsActivated();
} 