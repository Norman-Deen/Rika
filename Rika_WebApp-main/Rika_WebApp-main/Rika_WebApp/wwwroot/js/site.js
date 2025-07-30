
function toggleProfileImageInput() {
    const inputField = document.querySelector('.profile-image-url');

    if (inputField.style.display === 'none' || inputField.style.display === '') {
        inputField.style.display = 'flex';
    } else {
        inputField.style.display = 'none';
    }
}
