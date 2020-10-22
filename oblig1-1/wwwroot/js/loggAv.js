function loggAv() {
    $.get("Bestilling/LoggUt", function () {
        window.location.href = 'innlogging.html';
    });
}