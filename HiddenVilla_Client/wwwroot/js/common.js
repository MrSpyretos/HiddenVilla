window.showToastr = (type, message) => {
    if (type === "success") {
        toastr.sucess(message, "Operation Successful")
    }
    if (type === "error") {
        toastr.error(message, "Operation Failed")
    }
}