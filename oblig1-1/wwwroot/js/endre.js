$(function () {
    const id = window.location.search.substring(1);
    const url = "Bestilling/EtRuteStopp?" + id;
    $.get(url, function (rutestopp) {
        $("#id").val(rutestopp.id);
        $("#rekkefolge").val(rutestopp.rekkefølgeNr);
        $("#tid").val(rutestopp.stopptid);
        $("#sted").val(rutestopp.sted);
        $("#sone").val(rutestopp.sone);
    });
});

function validerOgEndreRS() {
    const rekkefOK = validerRekkefolge($("#rekkefolge").val());
    const tidOK = validerTid($("#tid").val());
    const stedOK = validerHoldeplassFra($("#sted"));
    const soneOK = validerSone($("#sone"));
    if (rekkefOK && tidOK && stedOK && soneOK) {
        endreRS();
    }
}

function endreRS() {
    const rutestopp = {
        id: $("#id").val(),
        rekkefolgeNr: $("#rekkefolge").val(),
        stopptid: $("#tid").val(),
        sted: $("#sted").val(),
        sone: $("#sone").val()
    };
    $.post("Bestilling/EndreRS", rutestopp, function () {
        window.location.href = 'admin.html';
    })
    .fail(function (feil) {
        if (feil.status == 401) {  // ikke logget inn, redirect til innlogging.html
            window.location.href = 'innlogging.html';
        }
        else {
            $("#feil").html("Feil på server - prøv igjen senere");
        }
    });
}