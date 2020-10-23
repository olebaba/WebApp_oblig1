function validerOgLagreRS() {
    const tidOK = validerTid($("#tid").val());
    const ruteOK = validerRute($("#rute").val())
    const stedOK = validerHoldeplassFra($("#sted").val());
    if (tidOK && stedOK && ruteOK) {
        lagreRS();
    }
}

function lagreRS() {
    const rutestopp = {
        sted: $("#sted").val(),
        sone: $("#rute").val(),
        stopptid: $("#tid").val()
    };
    const url = "Bestilling/NyttRuteStopp";
    $.post(url, rutestopp, function () {
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