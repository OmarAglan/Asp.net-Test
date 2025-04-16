using Microsoft.Extensions.Options; // Required for IOptions
using Roshta.Services.Interfaces;
using Roshta.Settings;
using System.IO; // Required for Path, File

namespace Roshta.Services;

public class LicenseService : ILicenseService
{
    private readonly LicenseSettings _licenseSettings;
    private readonly string _activationFlagPath; // Path to the activation marker file
    private readonly string _doctorIdFlagPath;   // Path to store the configured doctor ID

    // Cache the Doctor ID to avoid reading the file repeatedly
    private int? _cachedDoctorId = null;
    private bool _doctorIdChecked = false;

    public LicenseService(IOptions<LicenseSettings> licenseSettingsOptions)
    {
        _licenseSettings = licenseSettingsOptions.Value;
        _activationFlagPath = Path.Combine(AppContext.BaseDirectory, ".activated");
        _doctorIdFlagPath = Path.Combine(AppContext.BaseDirectory, ".doctorid");
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
        catch (Exception)
        {
            // TODO: Log the exception (ex)
            // Handle potential file system errors (permissions, etc.)
            // For now, activation might fail silently if the file can't be created.
        }
    }

    // --- New Methods --- 

    public bool IsProfileSetup()
    {
        // Profile is setup if the Doctor ID file exists and contains a valid integer
        return GetCurrentDoctorId() != null;
    }

    public void MarkProfileAsSetup(int doctorId)
    {
        try
        {
            // Store the Doctor ID in the flag file.
            File.WriteAllText(_doctorIdFlagPath, doctorId.ToString());
            // Reset cache
            _cachedDoctorId = doctorId;
            _doctorIdChecked = true;
        }
        catch (Exception)
        {
            // TODO: Log the exception (ex)
            // Handle potential file system errors
        }
    }

    public int? GetCurrentDoctorId()
    {
        // Return cached value if already checked
        if (_doctorIdChecked)
        {
            return _cachedDoctorId;
        }

        _doctorIdChecked = true; // Mark as checked even if file doesn't exist or is invalid
        if (File.Exists(_doctorIdFlagPath))
        {
            try
            {
                string content = File.ReadAllText(_doctorIdFlagPath);
                if (int.TryParse(content, out int id))
                {
                    _cachedDoctorId = id;
                    return id;
                }
            }
            catch (Exception)
            {
                 // TODO: Log the exception (ex)
            }
        }
        
        _cachedDoctorId = null;
        return null;
    }
} 