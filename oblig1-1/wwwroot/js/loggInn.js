// funksjon som logger inn for å komme til holdeplass siden 

function loggInn() {
    const sjekkBrukernavn = validerBrukernavn($("#brukernavn").val());
    const sjekkPassord = validerPassord($("#passord").val());

    if (sjekkBrukernavn && sjekkPassord) {
        const bruker = {
            brukernavn: $("#brukernavn").val(),
            passord: $("#passord").val()
        }
        $.post("Bestilling/LoggInn",bruker,function (OK) {
            if (OK) {
                window.location.href = 'admin.html';
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