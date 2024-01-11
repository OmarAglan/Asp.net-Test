# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

## [0.1.0] - YYYY-MM-DD

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