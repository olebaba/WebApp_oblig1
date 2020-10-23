function validerOgLagreRuteAvgang() {
    const datoOK = validerDato($("#dato").val());
    const ruteOK = validerRute($("#rute").val())
    if (datoOK && ruteOK) {
        lagreRuteAvgang();
    }
}
function lagreRuteAvgang() {
    var rutenavn = $("#rute").val();
    var dato = $("#dato").val();
    var innparameter = { argumenter: [rutenavn, dato] };
    const url = "Bestilling/NyRuteAvgang";
    $.post(url, innparameter, function () {
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