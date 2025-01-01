// Open the modal when any bell icon is clicked
document.querySelectorAll('.bellIcon').forEach(function (button) {
    button.addEventListener('click', function () {
        var medicineId = this.getAttribute('data-medicine-id');
        // Optionally, you can pass the medicineId or other info to the modal as needed
        console.log('Medicine ID: ' + medicineId); // For demonstration, you can send this value to the server or use it in the form
        document.getElementById('notificationModal').style.display = 'block';
    });
});

// Close the modal
document.getElementById('closeModal').addEventListener('click', function () {
    document.getElementById('notificationModal').style.display = 'none';
});

// Close the modal if the user clicks outside of it
window.onclick = function (event) {
    if (event.target === document.getElementById('notificationModal')) {
        document.getElementById('notificationModal').style.display = 'none';
    }
};