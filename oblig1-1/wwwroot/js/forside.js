$(function () {
    console.log("Test");
});

$(document).ready(function () {
    $("input:radio[name='tur/retur']").change(function () {
        if ($(this).val() == "retur") {
            $("#retDato").show();
            $("#retLabel").show();
        } else {
            $("#retDato").hide();
            $("#retLabel").hide();
        }
    });
});


function lagreKunde() {

    const kunde = {
        navn: $('#fra').val(),
        telefon: $("#til").val()
    }

    const url = "Buss/LagreKunde";
    $.post(url, kunde, function (OK) {
        if (OK) {
            window.location.href = 'forside.html';
        } else {
            $("#feil").html("Feil i db - bestilling");
        }
    });

}

