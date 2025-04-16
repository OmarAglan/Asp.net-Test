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

## Phase 2: Core CRUD Functionality & UI

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

## Phase 3: Authentication & Authorization

1.  **Identity Setup:**
    *   Integrate ASP.NET Core Identity.
    *   Configure Identity services and `DbContext` (link Identity User to `Doctor` model).
    *   Scaffold Identity UI (Register, Login, etc. - Registration creates the Doctor record).
    *   Apply Identity migrations.
2.  **Authorization & Subscription:**
    *   Implement checks based on `Doctor.IsSubscribed` status to potentially limit features.
    *   Secure pages/actions (e.g., only logged-in doctors can access functionality).

## Phase 4: Enhancements & Advanced Features

1.  **Doctor Profile Management:**
    *   Create page(s) for the logged-in doctor to view/edit their own details (`Doctor` model).
2.  **Search & Filtering:**
    *   Implement search/filtering for Patients, Medications, Prescriptions.
3.  **Validation:**
    *   Add robust server-side and client-side validation.
4.  **User Experience:**
    *   Improve UI/UX.
    *   Add user feedback.
5.  **Desktop Application Experience (.NET MAUI Blazor Hybrid - Optional Future Step):**
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
6.  **Subscription Management (Admin/Payment):**
    *   (Future) Implement mechanism to manage `Doctor.IsSubscribed` flag (e.g., admin panel, payment gateway integration).
7.  **Reporting (Optional):**
    *   Generate basic reports.
8.  **API Endpoints (Optional):**
    *   Create APIs for external integration.
9.  **Testing:**
    *   Implement unit and integration tests.
10. **Deployment:**
    *   Prepare for deployment (Web App or MAUI App).

## Phase 5: Optimization & Refinement

1.  **Performance Tuning:**
    *   Analyze and optimize database queries.
    *   Implement caching.
2.  **Security Hardening:**
    *   Review and enhance security measures.
3.  **Code Refactoring:**
    *   Improve code quality and maintainability. 