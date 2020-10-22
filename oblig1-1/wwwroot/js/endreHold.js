$(function () {
    const id = window.location.search.substring(1);
    const url = "Bestilling/HentHoldeplass" + id;
    $.get(url, function (holdeplass) {
        $("#id").val(holdeplass.id);
        $("#sted").val(holdeplass.sted);
        $("#avgangstider").val(holdeplass.avgangstider);
    });
});

function validerOgEndreHold() {
    const sjekkSted = validerHoldeplassFra($("sted").val());
    const sjekkTid = validerAvgangstid($("avgangstid").val());
    if (sjekkSted && sjekkTid) {
        endreHoldeplass();
    }
}

function endreHoldeplass() {
    const holdeplass = {
        id: $("#id").val(),
        sted: $("#sted").val(),
        avgangstider: $("#avgangstider").val()
    };
    $.post("Bestilling/EndreHoldeplass", holdeplass, function () {
        window.location.href = 'admin.html';
    })
    .fail(function (feil) {
        if (feil.status == 401) { // sjekker om vi er logget inn 
            window.location.href = 'innlogging.html';
        }
        else {
            $("#feil").html("Feil på server - prøv igjen senere");
        }
    });
}