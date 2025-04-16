using Microsoft.Extensions.Options; // Required for IOptions
using Roshta.Services.Interfaces;
using Roshta.Settings;
using System.IO; // Required for Path, File

namespace Roshta.Services;

public class LicenseService : ILicenseService
{
    private readonly LicenseSettings _licenseSettings;
    private readonly string _activationFlagPath; // Path to the activation marker file

    public LicenseService(IOptions<LicenseSettings> licenseSettingsOptions)
    {
        _licenseSettings = licenseSettingsOptions.Value; 

        // Define where the activation flag file will be stored.
        // Using AppContext.BaseDirectory places it alongside the application executable.
        // Consider Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) for a user-specific location later.
        _activationFlagPath = Path.Combine(AppContext.BaseDirectory, ".activated"); 
    }

    public bool ValidateLicense(string enteredRegistrationNumber, string enteredSerialNumber)
    {
        // Simple string comparison (case-insensitive for robustness)
        bool isValid = string.Equals(enteredRegistrationNumber, _licenseSettings.ExpectedRegistrationNumber, StringComparison.OrdinalIgnoreCase) &&
                       string.Equals(enteredSerialNumber, _licenseSettings.ExpectedSerialNumber, StringComparison.OrdinalIgnoreCase);
        
        // TODO: Implement more robust validation if needed (e.g., checking format, checksums, or calling an external server)

        return isValid;
    }

    public bool IsActivated()
    {
        // Check if the activation marker file exists.
        return File.Exists(_activationFlagPath);
    }

    public void MarkAsActivated()
    {
        try
        {
            // Create the empty marker file to indicate activation.
            // The content doesn't matter, only its existence.
            File.Create(_activationFlagPath).Dispose(); // Dispose releases the file handle immediately
        }
        catch (Exception ex)
        {
            // TODO: Log the exception (ex)
            // Handle potential file system errors (permissions, etc.)
            // For now, activation might fail silently if the file can't be created.
        }
    }
} 