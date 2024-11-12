document.addEventListener("DOMContentLoaded", function () {
    const userForm = document.getElementById('UserForm');
    const checkbox = document.getElementById('CreateUser');
    const emailField = document.getElementById('UserEmail');
    const loginField = document.getElementById('UserLogin');
    const passwordField = document.getElementById('UserPassword');


    function toggleUser(checkbox) {
        if (checkbox.checked) {
            userForm.classList.remove('hidden');
            emailField.required = true;
            loginField.required = true;
            passwordField.required = true;
        } else {
            userForm.classList.add('hidden');
            emailField.required = false;
            loginField.required = false;
            passwordField.required = false;
        }
    }


    toggleUser(checkbox);


    checkbox.addEventListener('change', function () {
        toggleUser(checkbox);
    });
});
