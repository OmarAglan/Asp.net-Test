# Roshta - Prescription Management Application

This project is an ASP.NET Core 9.0 Razor Pages application designed for managing medical prescriptions.

## Project Goal

To provide a system for doctors to issue and manage prescriptions, patients to view their prescriptions, and potentially for pharmacists to track dispensing.

## Technology Stack

*   **Framework:** ASP.NET Core 9.0
*   **UI:** Razor Pages
*   **Data Access:** Entity Framework Core
*   **Database:** SQLite (currently using `roshta.db`)
*   **Authentication:** ASP.NET Core Identity (Planned)
*   **Core Models:** Includes Patients, Doctors, Prescriptions, Medications with relationships, audit fields (`CreatedAt`, `UpdatedAt`), status tracking (e.g., `PrescriptionStatus` enum), and specific fields for visits, payments, etc.

## Getting Started

1.  Ensure you have the .NET 9 SDK installed.
2.  Install EF Core tools globally: `dotnet tool install --global dotnet-ef` (if not already installed).
3.  Clone the repository.
4.  The application uses a SQLite database (`roshta.db`) located in the project root. The connection string is configured in `appsettings.json`.
5.  Apply database migrations: `dotnet ef database update` (this will create the `roshta.db` file if it doesn't exist).
6.  Run the application: `dotnet run`

## Development Roadmap

See [ROADMAP.md](ROADMAP.md) for the detailed development plan. 