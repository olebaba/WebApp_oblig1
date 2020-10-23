function validerOgLagreRute() {
    const stedOK = validerSted($("#navn").val());
    if (stedOK) {
        lagreRute();
    }
}

function lagreRute() {
    const navn = $("#navn").val();
    const url = "Admin/LagreRute";
    $.post(url, navn, function () {
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