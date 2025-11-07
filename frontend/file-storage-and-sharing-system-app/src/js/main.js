document.addEventListener('DOMContentLoaded', () => {
    // Elements for download functionality
    const downloadLinkInput = document.getElementById('downloadLink');
    const downloadButton = document.getElementById('downloadButton');

    // Elements for login dialog
    const loginButton = document.querySelector('.btn-login');
    const loginDialogOverlay = document.getElementById('loginDialogOverlay');
    const closeLoginDialogButton = loginDialogOverlay.querySelector('.close-button');

    // Elements for register dialog
    const registerButton = document.querySelector('.btn-register');
    const registerDialogOverlay = document.getElementById('registerDialogOverlay');
    const closeRegisterDialogButton = registerDialogOverlay.querySelector('.close-button');

    /**
     * Updates the download button's state (enabled/disabled) based on the input field's value.
     */
    function updateButtonState() {
        if (downloadLinkInput.value.trim() === '') {
            downloadButton.classList.add('disabled');
        } else {
            downloadButton.classList.remove('disabled');
        }
    }

    // Set the initial state of the button when the page loads.
    updateButtonState();

    // Add an event listener to the input field to check for changes.
    downloadLinkInput.addEventListener('input', updateButtonState);

    // --- Login Dialog Logic ---

    // Show the dialog when the login button is clicked
    loginButton.addEventListener('click', () => {
        loginDialogOverlay.classList.add('show');
    });

    // Hide the dialog when the close button is clicked
    closeLoginDialogButton.addEventListener('click', () => {
        loginDialogOverlay.classList.remove('show');
    });

    // Hide the dialog when clicking on the overlay (outside the dialog box)
    loginDialogOverlay.addEventListener('click', (event) => {
        if (event.target === loginDialogOverlay) {
            loginDialogOverlay.classList.remove('show');
        }
    });

    // --- Register Dialog Logic ---

    // Show the dialog when the register button is clicked
    registerButton.addEventListener('click', () => {
        registerDialogOverlay.classList.add('show');
    });

    // Hide the dialog when the close button is clicked
    closeRegisterDialogButton.addEventListener('click', () => {
        registerDialogOverlay.classList.remove('show');
    });

    // Hide the dialog when clicking on the overlay (outside the dialog box)
    registerDialogOverlay.addEventListener('click', (event) => {
        if (event.target === registerDialogOverlay) {
            registerDialogOverlay.classList.remove('show');
        }
    });

});