# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

## [0.9.6] - YYYY-MM-DD

### Added

*   Implemented basic search functionality (form submission) for Patients, Medications, and Prescriptions lists.
*   Added `Dosage`, `Frequency`, and `Duration` fields to `PrescriptionItem` model and `PrescriptionItemCreateModel` view model.
*   Added corresponding input fields to `Pages/Prescriptions/Create.cshtml` and updated JavaScript logic.
*   Added EF Core migration `AddDosageFrequencyDurationToItem`.

### Changed

*   Updated `PatientService`, `MedicationService`, `PrescriptionService` and their interfaces to include `Search...Async` methods.
*   Updated `PatientRepository`, `MedicationRepository`, `PrescriptionRepository` and their interfaces to include `SearchAsync` methods.
*   Refactored method return types in `PatientRepository`/`IPatientRepository` and `MedicationRepository`/`IMedicationRepository` (Add/Update/Delete) for consistency.
*   Renamed methods in `PrescriptionRepository`/`IPrescriptionRepository` for consistency.
*   Updated `README.md` and `ROADMAP.md`.

### Fixed

*   Resolved build errors caused by mismatched method signatures between services, repositories, and interfaces after adding search functionality.
*   Resolved build errors caused by incorrect field names in search query for Patients (`ContactInfo` instead of `ContactEmail`/`ContactPhone`).
*   Resolved build errors caused by missing fields (`Dosage`, `Frequency`, `Duration`) in `PrescriptionItem` mapping within `PrescriptionService`.

## [0.9.5] - YYYY-MM-DD

### Added

*   Created `Pages/DoctorProfile/Setup.cshtml` and `.cs` for initial doctor profile setup after activation.
*   Created `Pages/DoctorProfile/Edit.cshtml` and `.cs` for editing the doctor profile after setup.
*   Added `GetDoctorProfileAsync(int)` and `UpdateDoctorProfileAsync(int, DoctorProfileInputModel)` methods to `IDoctorService`/`DoctorService` and `IDoctorRepository`/`DoctorRepository`.
*   Added "Edit Profile" link to the main navigation in `_Layout.cshtml`, visible only after activation and profile setup.

### Changed

*   Updated `ActivationCheckPageFilter` to redirect to `/DoctorProfile/Setup` if activated but profile is not set up.
*   Updated `ActivationCheckPageFilter` to redirect from `/Activate` page to `/Index` (or `/DoctorProfile/Setup`) if already activated.
*   Updated `README.md` and `ROADMAP.md` to reflect current features and progress.

### Fixed

*   Corrected `RedirectToPageResult` calls in `ActivationCheckPageFilter` to use correct page routes (e.g., `/DoctorProfile/Setup`) instead of file paths.

## [0.9.0] - YYYY-MM-DD

### Added

*   Added temporary license key storage in `appsettings.json` and `LicenseSettings` model.
*   Created `ILicenseService` and `LicenseService` for license validation and activation status (using simple file flag).
*   Created `Pages/Activate.cshtml` and `.cs` for user license key input.
*   Implemented `ActivationCheckPageFilter` (global Razor Page filter) to enforce activation.
*   Registered `LicenseService` and `ActivationCheckPageFilter`.

### Fixed

*   Resolved ambiguous route issue for the `/Activate` page.

## [0.8.0] - YYYY-MM-DD

### Added

*   Created `Pages/Prescriptions/Index.cshtml` and `.cs` for listing prescriptions.
*   Created `Pages/Prescriptions/Details.cshtml` and `.cs` for viewing prescription details (including items).
*   Updated `IPrescriptionService` and `PrescriptionService` with methods for retrieving prescriptions.
*   Added navigation link for Prescriptions list to `_Layout.cshtml`.

## [0.7.0] - YYYY-MM-DD

### Added

*   Defined `IMedicationService` and `IPatientService` interfaces.
*   Implemented `MedicationService` and `PatientService` using respective repositories.
*   Registered services for dependency injection.

### Changed

*   Refactored Medication CRUD pages (`Pages/Medications/*`) to use `IMedicationService`.
*   Refactored Patient CRUD pages (`Pages/Patients/*`) to use `IPatientService`.

## [0.6.0] - YYYY-MM-DD

### Added

*   Defined `IPrescriptionRepository` interface and `PrescriptionRepository` implementation.
*   Defined `IPrescriptionService` interface and `PrescriptionService` implementation.
*   Defined `PrescriptionCreateModel` ViewModel for prescription creation form.
*   Created `Pages/Prescriptions/Create.cshtml` and `.cs` for adding new prescriptions, including basic JavaScript for dynamic medication item handling.
*   Registered `IPrescriptionRepository` and `IPrescriptionService` for DI.

### Changed

*   Added navigation link for 'New Prescription' to `_Layout.cshtml`.

## [0.5.0] - 2025-04-14

### Added

*   Scaffolded basic CRUD Razor Pages for Patient Management (`Pages/Patients`).
*   Defined `IPatientRepository` interface.
*   Implemented `PatientRepository` using `ApplicationDbContext`.
*   Registered `IPatientRepository` for dependency injection.

### Changed

*   Refactored Patient CRUD pages (`Pages/Patients/*`) to use `IPatientRepository` instead of direct `DbContext` access.
*   Added navigation link for Patients to `_Layout.cshtml`.

## [0.4.0] - 2025-04-14

### Added

*   Defined `IMedicationRepository` interface.
*   Implemented `MedicationRepository` using `ApplicationDbContext`.
*   Registered `IMedicationRepository` for dependency injection.

### Changed

*   Refactored Medication CRUD pages (`Pages/Medications/*`) to use `IMedicationRepository` instead of direct `DbContext` access.

## [0.3.0] - 2025-04-13

### Added

*   Scaffolded basic CRUD Razor Pages for Medication Management (`Pages/Medications`).

### Changed

*   Updated `_Layout.cshtml` with 'Roshta' branding and navigation link for Medications.

## [0.2.0] - 2025-04-13

### Added

*   Added specific fields (e.g., `IsSubscribed`, `VisitCount`, `HasOutstandingBalance`, `Notes`, `ContactPhone`, `ContactEmail`) to core models based on requirements.
*   Added audit fields (`CreatedAt`, `UpdatedAt`) to all core models.
*   Added `PrescriptionStatus` enum.
*   Added `ApplicationDbContext.SaveChanges` override to auto-update audit fields.

### Changed

*   Refined core data models with suggestions (e.g., `