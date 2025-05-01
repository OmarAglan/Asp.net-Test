# Roshta Application Development Roadmap

This document outlines the planned development phases and features for the Roshta (Prescription Management) application.

## Phase 1: Project Setup & Core Data Foundation (Completed)

1. **Database Integration:**
    * Chose and configured SQLite provider.
    * Install EF Core NuGet packages.
    * Set up `ApplicationDbContext`.
    * Register `DbContext` in `Program.cs`.
2. **Core Data Models:**
    * Define `Patient`, `Doctor`, `Medication`, `Prescription`, `PrescriptionItem` classes.
    * Configure EF Core relationships.
3. **Database Migrations:**
    * Create and apply the initial EF Core migration.
4. **Basic Seeding (Optional):**
    * Add initial test data.

## Phase 2: Core CRUD Functionality & UI (Completed)

1. **Repository/Service Layer (Optional):** (Implemented Repository & Service layers for Medications & Patients)
    * Implement data access abstraction.
2. **Medication Management:** (Refactored to use Service Layer)
    * Create Razor Pages (List, Add, Edit, Delete).
    * Implement backend logic.
3. **Patient Management:** (Refactored to use Service Layer)
    * Create Razor Pages (List, Add, Edit, Delete).
    * Implement Repository pattern.
    * Implement backend logic.
4. **Prescription Creation:** (Implemented basic create page using Service Layer)
    * Create a Razor Page for issuing a new Prescription.
    * Include fields for selecting Patient, adding multiple Medications.
    * Implement backend logic (likely requires Service Layer).
5. **Prescription Viewing:** (Implemented Index and Details pages)
    * Create Razor Pages to List all prescriptions (for the logged-in doctor).
    * Create a Razor Page to view the Details of a specific prescription.

## Phase 3: Licensing & Activation (Initial Implementation - Completed)

1. **License Key Storage (Temporary):**
    * Store the expected unique Registration/Serial number pair **in `appsettings.json` for initial development**. **(Note: Must be replaced with a secure method like DPAPI or Online Activation before release)**.
2. **License Service:**
    * Create `ILicenseService` and `LicenseService` to read expected keys from `appsettings.json` and validate entered keys.
    * Implement logic to track activation status (e.g., simple local file flag).
3. **Activation UI:**
    * Create `Pages/Activate.cshtml` page for user to input license keys.
4. **Activation Check Middleware/Filter:**
    * Implement logic to check activation status on startup/request and redirect to Activation page if needed.

## Phase 4: Enhancements & Advanced Features

1. **Doctor Profile Management:** (Initial Implementation - Completed)
    * Implement mechanism to create/update the single licensed `Doctor` profile after successful activation. (Completed - via `/DoctorProfile/Setup`)
    * Store/retrieve the activated `DoctorId` for use in other features (e.g., Prescription creation). (Completed - via `ILicenseService`)
    * Create page(s) for the licensed doctor to view/edit their own details. (Completed - via `/DoctorProfile/Edit`)
2. **Search & Filtering:** (Basic Implementation - Completed)
    * Implement search/filtering for Patients, Medications, Prescriptions. (Completed basic text search)
3. **Validation:** (Completed)
    * Add robust server-side and client-side validation.
        * Implement server-side checks for foreign key validity (e.g., ensure selected PatientId, MedicationId exist before saving). (Completed)
        * Implement complex business rule validation (e.g., using `IValidatableObject` or custom `ValidationAttribute` for date comparisons, conditional requirements). (Completed)
        * Enhance client-side validation using JavaScript for immediate feedback. (Current Phase: B.5)
            * **Phase B.1: Refine Existing Checks (Consolidation):** Standardize triggers (blur/change/input), feedback (is-invalid class), refactor common JS logic to shared helpers. (Completed)
            * **Phase B.2: Input Formatting & Masking:** Implement client-side format checks (Regex/basic patterns) and input masking libraries for UX (e.g., Phone, License Number, Numeric fields). (Completed)
            * **Phase B.3: Client-Side Cross-Field Validation:** Mirror relevant server-side cross-field rules (like date comparisons) in JavaScript. (Completed)
            * **Phase B.4: Real-time Feedback Enhancements:** Explore debounced 'input' event validation and dynamic submit button state. (Completed)
            * **Phase B.5: Advanced Considerations:** Explore real-time uniqueness checks (AJAX) and potential integration with validation libraries if needed. (Completed AJAX checks for Medication Name, Patient ContactInfo, Patient Name) (Completed)
4. **User Experience:**
    * **Improve UI/UX:** (Expand this section)
        * **Consistency:**
            * Standardize button styles, sizes, and placements (e.g., Primary actions always left/right, consistent use of `btn-primary`, `btn-secondary`, `btn-danger`). (Completed for main CRUD actions and Dashboard)
            * Ensure uniform form layout, spacing, label alignment, and input field appearance across all Create/Edit pages. (Completed for Patient, Medication, Doctor Profile, Prescription Create)
            * Establish consistent typography (headings, body text, labels) and vertical rhythm.
        * **Visual Feedback & Interaction:**
            * Implement subtle loading indicators (e.g., spinners next to buttons, dimming sections) during AJAX operations (uniqueness checks, live search, dynamic form updates).
            * Use non-intrusive notifications (e.g., Bootstrap Toasts) for success messages (Save, Create, Delete) instead of full page reloads just for messages where possible. Use clear Alerts for critical errors. (Completed for main CRUD actions)
            * Provide immediate visual feedback for client-side validation errors (using existing `is-invalid` but ensure consistency).
        * **Layout & Readability:**
            * Review form structure on complex pages (e.g., Prescription Create). Consider using Bootstrap Cards (`<div class="card">`) to group related fields.
            * Optimize grid layout (`row`, `col-*`) for better alignment and spacing, especially on wider screens.
            * Test and refine responsiveness across common device sizes (mobile, tablet, desktop).
        * **Accessibility (a11y):**
            * Ensure all form inputs have correctly associated `<label>` tags (`asp-for` helps).
            * Review color contrast for text, buttons, and status indicators to meet WCAG guidelines.
            * Use ARIA attributes where necessary for dynamic content updates (e.g., live search results, dynamic form sections) to aid screen readers.
        * **Index Pages (Lists):**
            * Replace text links for row actions (Edit, Details, Delete) with clear icons (e.g., using Font Awesome or Bootstrap Icons) potentially with tooltips on hover. (Completed for Patients, Medications, Prescriptions)
            * Implement server-side pagination for potentially long lists (Patients, Medications, Prescriptions) to improve initial load time and performance. (Completed for Patients, Medications, Prescriptions)
            * Add client-side or server-side table sorting by clicking column headers. (Completed server-side for Patients, Medications, Prescriptions)
            * Use Bootstrap Badges for visually distinct status display (e.g., `PrescriptionStatus`, `Patient.IsActive`). (Completed for Patients, Prescriptions)
        * **Forms (Create/Edit):**
            * Enhance the dynamic Prescription Items list on `Prescriptions/Create.cshtml`: improve visual separation of items, make add/remove actions more intuitive (e.g., clear icons), ensure robust validation for dynamic rows. (Add/Remove working, validation improved, remove button uses icon, added visual separator)
            * For long dropdowns (`SelectList` - e.g., Patient/Medication selection), integrate a JavaScript library like `Select2` or `Tom Select` to provide type-ahead searching and filtering within the dropdown itself. (Completed for Prescription Create)
        * **Dashboard (`Index.cshtml`):**
            * Transform the default Index page into a useful dashboard: display key stats (e.g., Active Prescriptions count), quick action links (New Prescription, New Patient), maybe a list of recent prescriptions or upcoming appointments (if applicable). (Completed basic dashboard with counts and actions)
    * Add user feedback. // (This is largely covered by the detailed points above, perhaps remove or refine)
    * Implement live search/autocomplete with debouncing for list pages (Patients, Medications, Prescriptions). (In Progress)
        * Client-side JavaScript (e.g., using `fetch` and debouncing).
        * Server-side endpoint (e.g., named page handler or API controller returning JSON).
5. **Prescription Management Refinements:** (Completed)
    * Implement "Cancel Prescription" functionality (updates `Prescription.Status` to `Cancelled`). (Completed)
    * Avoid direct editing or physical deletion of issued prescriptions. (Policy Adopted)
    * (Optional Later) Add "Copy Prescription" feature to facilitate re-prescribing. (Completed)
6. **Desktop Application Experience (.NET MAUI Blazor Hybrid - Optional Future Step):**
    * **Goal:** Package the application as an installable desktop app (.exe, .dmg, etc.) for a native feel.
    * **Approach:** Utilize .NET MAUI Blazor Hybrid to host the existing web UI (Razor components) within a native app shell.
    * **Key Steps:**
        * **Shared UI Library:** Move Razor components (`.razor` files, potentially including some shared layout elements) from the web project (`Roshta`) into a new Razor Class Library (RCL) project (e.g., `Roshta.UI`).
        * **Shared Core Logic:** Ensure core logic (Models, Repositories, Interfaces, potentially Services, DbContext) is in separate Class Library projects referenced by both the web and MAUI apps.
        * **Create MAUI Project:** Add a new .NET MAUI Blazor App project to the solution.
        * **Reference Shared Libraries:** Reference the UI (RCL) and Core Logic libraries from the MAUI project.
        * **Configure Hosting:** Configure the MAUI Blazor project's `MauiProgram.cs` to register dependencies (like DbContext, Repositories) and set up the BlazorWebView to host the root component(s) from the shared UI library.
        * **Platform Adjustments:** Address any platform-specific styling or behavior differences if necessary.
        * **Build & Package:** Build the MAUI project to produce native desktop installers.
7. **Subscription Management (Admin/Payment):**
    * (Future) Implement mechanism to manage `Doctor.IsSubscribed` flag (e.g., admin panel, payment gateway integration).
8. **Reporting (Optional):**
    * Generate basic reports.
9. **API Endpoints (Optional):**
    * Create APIs for external integration.
10. **Testing:**
    * Implement unit and integration tests.
11. **Deployment:**
    * Prepare for deployment (Web App or MAUI App).

## Phase 5: Optimization & Refinement

1. **Performance Tuning:**
    * Analyze and optimize database queries.
    * Implement caching.
2. **Security Hardening:**
    * **Replace Temporary License Storage:** Implement a secure license validation and storage mechanism (e.g., using Windows DPAPI for local encrypted storage, or implementing an Online Activation server).
    * Review security practices (e.g., input validation, protection against common web vulnerabilities).
3. **Code Refactoring:**
    * Improve code quality and maintainability.
