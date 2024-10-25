function fShowToast(x) {
    $.toast({
        heading: 'Success',
        text: x,
        position: 'top-right',
        loaderBg: '#ff6849',
        icon: 'success',
        hideAfter: 7000,
        stack: 6
    });
}
function fShowToasterror(x) {
    $.toast({
        heading: 'Error',
        text: x,
        position: 'top-right',
        loaderBg: '#ff6849',
        icon: 'error',
        hideAfter: 7000

    });
} function fShowToastalert(x) {
    $.toast({
        heading: 'Warning',
        text: x,
        position: 'top-right',
        loaderBg: '#ff6849',
        icon: 'warning',
        hideAfter: 7000,
        stack: 6
    });
}          
