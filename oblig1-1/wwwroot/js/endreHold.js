$(function () {
    const id = window.location.search.substring(1);
    const url = "Bestilling/HentHoldeplass?" + id;
    $.get(url, function (holdeplass) {
        $("#id").val(holdeplass.id);
        $("#sted").val(holdeplass.sted);
        $("#sone").val(holdeplass.sone);
    });
});

function validerOgEndreHold() {
    const sjekkSted = validerSted($("#sted").val());
    const sjekkSone = validerSone($("#sone").val());
    if (sjekkSted && sjekkSone) {
        endreHoldeplass();
    }
}

function endreHoldeplass() {
    const holdeplass = {
        id: $("#id").val(),
        sted: $("#sted").val(),
        sone: $("#sone").val()
    };
    $.post("Admin/EndreHoldeplass", holdeplass, function () {
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