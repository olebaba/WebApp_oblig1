function lagreRuteAvgang() {
    const ruteavgang = {
        dato: $("#dato").val(),

    }
    const url = "Admin/LagreRuteAvgang";
    $.post(url, ruteavgang, function () {
        window.location.href = 'admin.html';
    })
        .fail(function (feil) {
            if (feil.status == 401) {
                window.location.href = 'innlogging.html';
            }
            else {
                $("#feil").html("Feil på server - prøv igjen senere");
            }
        });
}