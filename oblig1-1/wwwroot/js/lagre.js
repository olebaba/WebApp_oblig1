function validerOgLagreRS() {
    const tidOK = validerTid($("#tid").val());
    const ruteOK = validerRute($("#rute").val())
    const stedOK = validerHoldeplassFra($("#sted").val());
    if (tidOK && stedOK && ruteOK) {
        lagreRS();
    }
}

function lagreRS() {
    var sted = $("#sted").val();
    var rute = $("#rute").val();
    var tid = $("#tid").val();
    var innparameter = { argumenter: [sted, rute, tid] };
    const url = "Bestilling/NyttRuteStopp";
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