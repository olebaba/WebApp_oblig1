function validerOgLagreRS() {
    const rNrOK = validerRekkefolge($("#rekkefolge").val());
    const tidOK = validerTid($("#tid").val());
    const stedOK = validerHoldeplassFra($("#sted").val());
    const soneOK = validerSone($("#sone").val());
    if (rNrOK && tidOK && stedOK && soneOK) {
        lagreRS();
    }
}

function lagreRS() {
    const rutestopp = {
        rekkefolgeNr: $("#rekkefolge").val(),
        stopptid: $("#tid").val(),
        sted: $("#sted").val(),
        sone: $("#sone").val()
    };
    const url = "Admin/LagreRS";
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