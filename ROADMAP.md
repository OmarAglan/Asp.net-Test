# Roshta Application Development Roadmap

This document outlines the planned development phases and features for the Roshta (Prescription Management) application.

## Phase 1: Project Setup & Core Data Foundation (Completed)

1.  **Database Integration:**
    *   Chose and configured SQLite provider.
    *   Install EF Core NuGet packages.
    *   Set up `ApplicationDbContext`.
    *   Register `DbContext` in `Program.cs`.
2.  **Core Data Models:**
    *   Define `Patient`, `Doctor`, `Medication`, `Prescription`, `PrescriptionItem` classes.
    *   Configure EF Core relationships.
3.  **Database Migrations:**
    *   Create and apply the initial EF Core migration.
4.  **Basic Seeding (Optional):**
    *   Add initial test data.

## Phase 2: Core CRUD Functionality & UI (Completed)

1.  **Repository/Service Layer (Optional):** (Implemented Repository & Service layers for Medications & Patients)
    *   Implement data access abstraction.
2.  **Medication Management:** (Refactored to use Service Layer)
    *   Create Razor Pages (List, Add, Edit, Delete).
    *   Implement backend logic.
3.  **Patient Management:** (Refactored to use Service Layer)
    *   Create Razor Pages (List, Add, Edit, Delete).
    *   Implement Repository pattern.
    *   Implement backend logic.
4.  **Prescription Creation:** (Implemented basic create page using Service Layer)
    *   Create a Razor Page for issuing a new Prescription.
    *   Include fields for selecting Patient, adding multiple Medications.
    *   Implement backend logic (likely requires Service Layer).
5.  **Prescription Viewing:** (Implemented Index and Details pages)
    *   Create Razor Pages to List all prescriptions (for the logged-in doctor).
    *   Create a Razor Page to view the Details of a specific prescription.

## Phase 3: Licensing & Activation (Initial Implementation - Completed)

1.  **License Key Storage (Temporary):**
    *   Store the expected unique Registration/Serial number pair **in `appsettings.json` for initial development**. **(Note: Must be replaced with a secure method like DPAPI or Online Activation before release)**.
2.  **License Service:**
    *   Create `ILicenseService` and `LicenseService` to read expected keys from `appsettings.json` and validate entered keys.
    *   Implement logic to track activation status (e.g., simple local file flag).
3.  **Activation UI:**
    *   Create `Pages/Activate.cshtml` page for user to input license keys.
4.  **Activation Check Middleware/Filter:**
    *   Implement logic to check activation status on startup/request and redirect to Activation page if needed.

## Phase 4: Enhancements & Advanced Features

1.  **Doctor Profile Management:** (Initial Implementation - Completed)
    *   Implement mechanism to create/update the single licensed `Doctor` profile after successful activation. (Completed - via `/DoctorProfile/Setup`)
    *   Store/retrieve the activated `DoctorId` for use in other features (e.g., Prescription creation). (Completed - via `ILicenseService`)
    *   Create page(s) for the licensed doctor to view/edit their own details. (Completed - via `/DoctorProfile/Edit`)
2.  **Search & Filtering:** (Basic Implementation - Completed)
    *   Implement search/filtering for Patients, Medications, Prescriptions. (Completed basic text search)
3.  **Validation:** (In Progress)
    *   Add robust server-side and client-side validation.
        *   Implement server-side checks for foreign key validity (e.g., ensure selected PatientId, MedicationId exist before saving). (Completed)
        *   Implement complex business rule validation (e.g., using `IValidatableObject` or custom `ValidationAttribute` for date comparisons, conditional requirements). (Completed)
        *   Enhance client-side validation using JavaScript for immediate feedback. (Current Phase: B.1)
            *   **Phase B.1: Refine Existing Checks (Consolidation):** Standardize triggers (blur/change), feedback (is-invalid class), refactor common JS logic to shared helpers. (Current Step)
            *   **Phase B.2: Input Formatting & Masking:** Implement client-side format checks (Regex) and consider input masking libraries for UX.
            *   **Phase B.3: Client-Side Cross-Field Validation:** Mirror relevant server-side cross-field rules (like date comparisons) in JavaScript.
            *   **Phase B.4: Real-time Feedback Enhancements:** Explore debounced 'input' event validation and dynamic submit button state.
            *   **Phase B.5: Advanced Considerations:** Explore real-time uniqueness checks (AJAX) and potential integration with validation libraries if needed.
4.  **User Experience:**
    *   Improve UI/UX.
    *   Add user feedback.
    *   Implement live search/autocomplete with debouncing for list pages (Patients, Medications, Prescriptions).
        *   Client-side JavaScript (e.g., using `fetch` and debouncing).
        *   Server-side endpoint (e.g., named page handler or API controller returning JSON).
5.  **Prescription Management Refinements:** (Completed)
    *   Implement "Cancel Prescription" functionality (updates `Prescription.Status` to `Cancelled`). (Completed)
    *   Avoid direct editing or physical deletion of issued prescriptions. (Policy Adopted)
    *   (Optional Later) Add "Copy Prescription" feature to facilitate re-prescribing. (Completed)
6.  **Desktop Application Experience (.NET MAUI Blazor Hybrid - Optional Future Step):**
    *   **Goal:** Package the application as an installable desktop app (.exe, .dmg, etc.) for a native feel.
    *   **Approach:** Utilize .NET MAUI Blazor Hybrid to host the existing web UI (Razor components) within a native app shell.
    *   **Key Steps:**
        *   **Shared UI Library:** Move Razor components (`.razor` files, potentially including some shared layout elements) from the web project (`Roshta`) into a new Razor Class Library (RCL) project (e.g., `Roshta.UI`).
        *   **Shared Core Logic:** Ensure core logic (Models, Repositories, Interfaces, potentially Services, DbContext) is in separate Class Library projects referenced by both the web and MAUI apps.
        *   **Create MAUI Project:** Add a new .NET MAUI Blazor App project to the solution.
        *   **Reference Shared Libraries:** Reference the UI (RCL) and Core Logic libraries from the MAUI project.
        *   **Configure Hosting:** Configure the MAUI Blazor project's `MauiProgram.cs` to register dependencies (like DbContext, Repositories) and set up the BlazorWebView to host the root component(s) from the shared UI library.
        *   **Platform Adjustments:** Address any platform-specific styling or behavior differences if necessary.
        *   **Build & Package:** Build the MAUI project to produce native desktop installers.
7.  **Subscription Management (Admin/Payment):**
    *   (Future) Implement mechanism to manage `Doctor.IsSubscribed` flag (e.g., admin panel, payment gateway integration).
8.  **Reporting (Optional):**
    *   Generate basic reports.
9.  **API Endpoints (Optional):**
    *   Create APIs for external integration.
10. **Testing:**
    *   Implement unit and integration tests.
11. **Deployment:**
    *   Prepare for deployment (Web App or MAUI App).

## Phase 5: Optimization & Refinement

1.  **Performance Tuning:**
    *   Analyze and optimize database queries.
    *   Implement caching.
2.  **Security Hardening:**
    *   **Replace Temporary License Storage:** Implement a secure license validation and storage mechanism (e.g., using Windows DPAPI for local encrypted storage, or implementing an Online Activation server).
    *   Review security practices (e.g., input validation, protection against common web vulnerabilities).
3.  **Code Refactoring:**
    *   Improve code quality and maintainability. 