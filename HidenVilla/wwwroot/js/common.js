window.showToastr = (type, message) => {
    if (type === "success") {
        toastr.sucess(message,"Operation Successful")
    }
    if (type === "error") {
        toastr.error(message, "Operation Failed")
    }
}
window.showSwal = (type, message) => {
    if (type === "success") {
        Swal.fire(
            'Success Notification',
            message,
            'success'
        )
    }
    if (type === "error") {
        Swal.fire(
            'Error Notification',
            message,
            'error'
        )
    }
}
function ShowDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('show');
}
function HideDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('hide');
}