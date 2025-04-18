// wwwroot/js/validation-helpers.js

// --- Reusable Validation Helper Functions ---

/**
 * Validates a date input to ensure it is in the future.
 * @param {HTMLInputElement} inputElement The date input element.
 * @param {HTMLElement} validationSpan The span element to display validation messages.
 * @param {string} [errorMessagePrefix='Date'] Prefix for the error message (e.g., 'Expiry Date').
 */
function validateDateIsFuture(inputElement, validationSpan, errorMessagePrefix = 'Date') {
    if (!inputElement || !validationSpan) return; 

    const dateValue = inputElement.value;
    validationSpan.textContent = '';
    inputElement.classList.remove('is-invalid');

    if (dateValue) {
        try {
            const today = new Date();
            today.setUTCHours(0, 0, 0, 0);
            const parts = dateValue.split('-');
            const inputDate = new Date(Date.UTC(parts[0], parts[1] - 1, parts[2]));

            if (inputDate <= today) {
                validationSpan.textContent = `${errorMessagePrefix} must be in the future.`;
                inputElement.classList.add('is-invalid');
            }
        } catch (e) {
            console.error("Error parsing date:", e);
            validationSpan.textContent = 'Invalid date format.';
            inputElement.classList.add('is-invalid');
        }
    }
}

/**
 * Validates a date input to ensure it is in the past.
 * @param {HTMLInputElement} inputElement The date input element.
 * @param {HTMLElement} validationSpan The span element to display validation messages.
 * @param {string} [errorMessagePrefix='Date'] Prefix for the error message (e.g., 'Date of Birth').
 */
function validateDateIsPast(inputElement, validationSpan, errorMessagePrefix = 'Date') {
    if (!inputElement || !validationSpan) return;
    const dateValue = inputElement.value;
    validationSpan.textContent = ''; 
    inputElement.classList.remove('is-invalid');

    if (dateValue) {
        try {
            const today = new Date();
            today.setUTCHours(0, 0, 0, 0);
            const parts = dateValue.split('-');
            const inputDate = new Date(Date.UTC(parts[0], parts[1] - 1, parts[2]));

            if (inputDate >= today) {
                validationSpan.textContent = `${errorMessagePrefix} must be in the past.`;
                inputElement.classList.add('is-invalid');
            }
        } catch (e) {
            validationSpan.textContent = 'Invalid date format.';
            inputElement.classList.add('is-invalid');
        }
    }
}

/**
 * Validates that at least one of two contact fields (phone, email) is filled.
 * @param {HTMLInputElement} phoneInputElement The phone input element.
 * @param {HTMLInputElement} emailInputElement The email input element.
 * @param {HTMLElement} phoneValidationSpan The phone validation message span.
 * @param {HTMLElement} emailValidationSpan The email validation message span.
 */
function validateContactMethodRequired(phoneInputElement, emailInputElement, phoneValidationSpan, emailValidationSpan) {
    const validationMessage = "Please provide at least one contact method (Phone or Email).";
    const phoneValue = phoneInputElement ? phoneInputElement.value.trim() : '';
    const emailValue = emailInputElement ? emailInputElement.value.trim() : '';

    if (!phoneValue && !emailValue) {
        if (phoneValidationSpan) phoneValidationSpan.textContent = validationMessage;
        if (emailValidationSpan) emailValidationSpan.textContent = validationMessage;
        if (phoneInputElement) phoneInputElement.classList.add('is-invalid');
        if (emailInputElement) emailInputElement.classList.add('is-invalid');
    } else {
        if (phoneValidationSpan) phoneValidationSpan.textContent = '';
        if (emailValidationSpan) emailValidationSpan.textContent = '';
        if (phoneInputElement) phoneInputElement.classList.remove('is-invalid');
        if (emailInputElement) emailInputElement.classList.remove('is-invalid');
    }
}

/**
 * Validates the format of an Egyptian phone number (e.g., +201XXXXXXXXX).
 * @param {HTMLInputElement} inputElement The phone input element.
 * @param {HTMLElement} validationSpan The span element to display validation messages.
 */
function validatePhoneNumberFormat(inputElement, validationSpan) {
    if (!inputElement || !validationSpan) return;
    const phoneValue = inputElement.value.trim();
    // Regex for Egyptian format like +201012345678 or +201112345678 etc. (+201 followed by 9 digits)
    const phoneRegex = /^\+201[0-9]{9}$/;

    // Clear previous format error, but respect errors from other validators (like required contact)
    // Only add/remove format error message and is-invalid if the field has content.
    if (phoneValue) { 
        if (!phoneRegex.test(phoneValue)) {
            // Updated error message for specific format
            validationSpan.textContent = 'Invalid format (e.g., +201XXXXXXXXX).';
            inputElement.classList.add('is-invalid');
        } else {
            // If format is valid, clear ONLY the format error message 
            // ONLY IF the current message IS the format error message.
            // Otherwise, another validator (like required contact) might be active.
            if (validationSpan.textContent.startsWith('Invalid format')) {
                 validationSpan.textContent = '';
            }
            // We only remove is-invalid if no other validation message exists for this span
            if (!validationSpan.textContent) { 
                 inputElement.classList.remove('is-invalid');
            }
        }
    } else {
         // If the field is empty, clear a format-specific error, but leave others.
         if (validationSpan.textContent.startsWith('Invalid format')) {
            validationSpan.textContent = '';
         }
         // Only remove is-invalid if no other validation message exists
         if (!validationSpan.textContent) { 
            inputElement.classList.remove('is-invalid');
         }         
    }
}

/**
 * Validates that an input field is not empty.
 * @param {HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement} inputElement The input element.
 * @param {HTMLElement} validationSpan The span element to display validation messages.
 * @param {string} [fieldName='Field'] Name of the field for the error message.
 */
function validateRequiredField(inputElement, validationSpan, fieldName = 'Field') {
    if (!inputElement || !validationSpan) return;
    const value = inputElement.value.trim();
    const requiredMessage = `${fieldName} is required.`;

    if (!value) {
        validationSpan.textContent = requiredMessage;
        inputElement.classList.add('is-invalid');
    } else {
        // Only clear the error if it's the required field message
        if (validationSpan.textContent === requiredMessage) {
            validationSpan.textContent = '';
        }
         // Only remove is-invalid if no other validation message exists
        if (!validationSpan.textContent) {
            inputElement.classList.remove('is-invalid');
        }
    }
}

/**
 * Validates that date B is after date A, if both are provided.
 * @param {HTMLInputElement} dateAInputElement The first date input (e.g., Next Appointment).
 * @param {HTMLInputElement} dateBInputElement The second date input (e.g., Expiry Date).
 * @param {HTMLElement} dateAValidationSpan Validation span for date A.
 * @param {HTMLElement} dateBValidationSpan Validation span for date B.
 * @param {string} dateAName Name for date A in error messages.
 * @param {string} dateBName Name for date B in error messages.
 */
function validateDateBAfterDateA(dateAInputElement, dateBInputElement, dateAValidationSpan, dateBValidationSpan, dateAName, dateBName) {
    if (!dateAInputElement || !dateBInputElement || !dateAValidationSpan || !dateBValidationSpan) return;

    const dateAValue = dateAInputElement.value;
    const dateBValue = dateBInputElement.value;
    const errorMessage = `${dateBName} must be after ${dateAName}.`;

    // Clear previous cross-field error messages first
    if (dateAValidationSpan.textContent === errorMessage) dateAValidationSpan.textContent = '';
    if (dateBValidationSpan.textContent === errorMessage) dateBValidationSpan.textContent = '';
    // Only remove is-invalid if no other validation error exists for the spans
    if (!dateAValidationSpan.textContent) dateAInputElement.classList.remove('is-invalid');
    if (!dateBValidationSpan.textContent) dateBInputElement.classList.remove('is-invalid');

    // Only perform check if both dates have values
    if (dateAValue && dateBValue) {
        try {
            const partsA = dateAValue.split('-');
            const dateA = new Date(Date.UTC(partsA[0], partsA[1] - 1, partsA[2]));

            const partsB = dateBValue.split('-');
            const dateB = new Date(Date.UTC(partsB[0], partsB[1] - 1, partsB[2]));

            if (dateB <= dateA) {
                // Show error on both fields involved in the cross-validation
                dateAValidationSpan.textContent = errorMessage;
                dateBValidationSpan.textContent = errorMessage;
                dateAInputElement.classList.add('is-invalid');
                dateBInputElement.classList.add('is-invalid');
            }
        } catch (e) {
            console.error("Error parsing dates for cross-validation:", e);
            // Don't add a new error here, let the individual field format validation handle it
        }
    }
}

// --- Utility Functions ---

/**
 * Debounce function: Limits the rate at which a function can fire.
 * @param {Function} func The function to debounce.
 * @param {number} delay Delay in milliseconds.
 * @returns {Function} The debounced function.
 */
function debounce(func, delay) {
    let debounceTimer;
    return function() {
        const context = this;
        const args = arguments;
        clearTimeout(debounceTimer);
        debounceTimer = setTimeout(() => func.apply(context, args), delay);
    }
} 