function validerOgLagreHold() {
    const stedOK = validerSted($("#sted").val());
    const soneOK = validerSone($("#sone").val());
    if (stedOK && soneOK) {
        lagreHold();
    }
}

function lagreHold() {
    const holdeplass = {
        sted: $("#sted").val(),
        sone: $("#sone").val()
    };
    const url = "Admin/LagreHoldeplass";
    $.post(url, holdeplass, function () {
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