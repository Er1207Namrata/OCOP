function validateEmail(email) {
    const re = /\S+@\S+\.\S+/;
    return re.test(email);
}

function validateIndianPhoneNumber(phoneNumber) {
    const re = /^[6789]\d{9}$/;
    return re.test(phoneNumber);
}