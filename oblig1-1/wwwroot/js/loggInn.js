// funksjon som logger inn for å komme til endre-siden 

function loggInn() {
    const sjekkBrukernavn = validerBrukernavn($("#bruker").val());
    const sjekkPassord = validerPassord($("#pass").val());

    if (sjekkBrukernavn && sjekkPassord) {
        const bruker = {
            brukernavn: $("#bruker").val(),
            passord: $("#pass").val()
        }

        $.post("Bestilling/LoggInn", bruker, function (OK) {
            if (OK) {
                window.location.href = 'endre.html';
            }
            else {
                $("#feil").html("Feil brukernavn eller passord");
            }
        })
        .fail(function () {
            $("#feil").html("Feil på server - prøv igjen senere");
        });
    }
}