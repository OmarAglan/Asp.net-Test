# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

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

*   Refined core data models with suggestions (e.g., `IsActive` flags, `Prescription.Status` changed to enum).

## [0.1.0] - 2025-04-13

### Added

*   Initial project setup using ASP.NET Core 9 Razor Pages template.
*   Integrated Entity Framework Core with SQLite (`roshta.db`).
*   Defined core data models: `Patient`, `Doctor`, `Medication`, `Prescription`, `PrescriptionItem`.
*   Created initial database migration (`InitialCreate`).
*   Added `README.md`, `ROADMAP.md`, and `.gitignore`.
*   Created this `CHANGELOG.md`.

### Changed

*   Updated target framework from .NET 7 to .NET 9.
*   Renamed project and namespaces from `Test1` to `Roshta`.
*   Updated default logging in `Program.cs` (reverted incorrect attempt).

### Fixed

*   Resolved issues with `dotnet ef` tool installation and usage.
*   Fixed build errors after namespace renaming. 