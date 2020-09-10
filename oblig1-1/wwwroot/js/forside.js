$(function () {
    console.log("Test");
});

function lagreKunde() {

    const kunde = {
        navn: $('#fra').val(),
        telefon: $("#til").val()
    }


    const url = "Buss/LagreKunde";
    $.post(url, kunde, function (OK) {
        console.log(kunde.navn);
        if (OK) {
            window.location.href = 'forside.html';
        } else {
            $("#feil").html("Feil i db - bestilling");
        }
    });

}

