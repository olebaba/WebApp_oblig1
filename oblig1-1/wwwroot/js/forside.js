$(document).ready(function () {
    $("input:radio[name='tur/retur']").change(function () {
        if ($(this).val() == "retur") {
            $("#retDato").show();
            $("#retLabel").show();
            validerKnapp();
        } else {
            $("#retDato").hide();
            $("#retLabel").hide();
            validerKnapp();
        }
    });

    $('#add').click(function () {
        $("#sub").prop("disabled", false);
        let number = $('#textinput').val();

        if (number >= 9) {
            $("#add").prop("disabled", true);
        } else {
            number++;
            $('#textinput').val(number);
        }
        validerKnapp();
    });

    $('#sub').click(function () {
        $("#add").prop("disabled", false);
        let number = $('#textinput').val();
        if (number <= 0) {
            $("#sub").prop("disabled", true);
        } else {
            number--;

        }
        $('#textinput').val(number);
        validerKnapp();
    });

    $('#add1').click(function () {
        $("#sub1").prop("disabled", false);
        let number = $('#honinput').val();

        if (number >= 9) {
            $("#add1").prop("disabled", true);
        } else {
            number++;
            $('#honinput').val(number);
        }
        validerKnapp();
    });

    $('#sub1').click(function () {
        $("#add1").prop("disabled", false);
        let number = $('#honinput').val();
        if (number <= 0) {
            $("#sub1").prop("disabled", true);
        } else {
            number--;

        }
        $('#honinput').val(number);
        validerKnapp();
    });

    $('#add2').click(function () {
        $("#sub2").prop("disabled", false);
        let number = $('#barninput').val();

        if (number >= 9) {
            $("#add2").prop("disabled", true);
        } else {
            number++;
            $('#barninput').val(number);
        }
        validerKnapp();
    });

    $('#sub2').click(function () {
        $("#add2").prop("disabled", false);
        let number = $('#barninput').val();
        if (number <= 0) {
            $("#sub2").prop("disabled", true);
        } else {
            number--;

        }
        $('#barninput').val(number);
        validerKnapp();
    });

    $('#add3').click(function () {
        $("#sub3").prop("disabled", false);
        let number = $('#studentinput').val();

        if (number >= 9) {
            $("#add3").prop("disabled", true);
        } else {
            number++;
            $('#studentinput').val(number);
        }
        validerKnapp();
    });

    $('#sub3').click(function () {
        $("#add3").prop("disabled", false);
        let number = $('#studentinput').val();
        if (number <= 0) {
            $("#sub3").prop("disabled", true);
        } else {
            number--;

        }
        $('#studentinput').val(number);
        validerKnapp();
    });

    $('#add4').click(function () {
        $("#sub4").prop("disabled", false);
        let number = $('#storbarninput').val();

        if (number >= 9) {
            $("#add4").prop("disabled", true);
        } else {
            number++;
            $('#storbarninput').val(number);
        }
        validerKnapp();
    });

    $('#sub4').click(function () {
        $("#add4").prop("disabled", false);
        let number = $('#storbarninput').val();
        if (number <= 0) {
            $("#sub4").prop("disabled", true);
        } else {
            number--;

        }
        $('#storbarninput').val(number);
        validerKnapp();
    });

    $('#add5').click(function () {
        $("#sub5").prop("disabled", false);
        let number = $('#vernepliktiginput').val();

        if (number >= 9) {
            $("#add5").prop("disabled", true);
        } else {
            number++;
            $('#vernepliktiginput').val(number);
        }
        validerKnapp();
    });

    $('#sub5').click(function () {
        $("#add5").prop("disabled", false);
        let number = $('#vernepliktiginput').val();
        if (number <= 0) {
            $("#sub5").prop("disabled", true);
        } else {
            number--;

        }
        $('#vernepliktiginput').val(number);
        validerKnapp();
    });

    $('#add6').click(function () {
        $("#sub6").prop("disabled", false);
        let number = $('#ledsagerinput').val();

        if (number >= 9) {
            $("#add6").prop("disabled", true);
        } else {
            number++;
            $('#ledsagerinput').val(number);
        }
        validerKnapp();
    });

    $('#sub6').click(function () {
        $("#add6").prop("disabled", false);
        let number = $('#ledsagerinput').val();
        if (number <= 0) {
            $("#sub6").prop("disabled", true);
        } else {
            number--;

        }
        $('#ledsagerinput').val(number);
        validerKnapp();
    });

    $.get("Bestilling/HentAlleHoldeplasser", function (holdeplasser) {
        formaterFraHoldeplass(holdeplasser);
    });

    hentPriser();
    
});

var startHoldeplass, sluttHoldeplass;

function formaterFraHoldeplass(holdeplasser) {
    let steder = [];
    
    for (i in holdeplasser) {
        steder.push(holdeplasser[i].sted);
    }
    $("#fra").autocomplete( {
        source: steder,
        minLength: 1,
        change: function (event, ui) {
            if (!ui.item) {
                //Hvis ikke en holdeplass fra listen blir valgt gjøres inputen tom
                $("#fra").val("");
                $("#feilHoldeplassFra").html("Vennligst velg en holdeplass fra listen")
                validerKnapp();
            }
            for (h in holdeplasser) {
                if (holdeplasser[h].sted == $("#fra").val()) startHoldeplass = holdeplasser[h];
            }
        }
    });
    $("#til").autocomplete({
        source: steder,
        minLength: 1,
        change: function (event, ui) {
            if (!ui.item) {
                $("#til").val("");
                $("#feilHoldeplassTil").html("Vennligst velg en holdeplass fra listen")
                validerKnapp();
            }
            for (h in holdeplasser) {
                if (holdeplasser[h].sted == $("#til").val()) sluttHoldeplass = holdeplasser[h];
            }
        }
    });
}


function validerOgVisAvganger() {
    const holdeplassFraOk = validerHoldeplassFra(hentVerdi("#fra"));
    const holdeplassTilOk = validerHoldeplassTil(hentVerdi("#til"));
    
    if (holdeplassFraOk && holdeplassTilOk) {
        tilAvganger();
    }
}

function hentVerdi(id) {
    return document.getElementById(id).value;
}

function hentBilletter() {
    let url_billetter = "&pass_0=" + hentVerdi("textinput") + "&pass_1=" + hentVerdi("storbarninput") + "&pass_2=" +
        hentVerdi("barninput") + "&pass_3=" + hentVerdi("studentinput") + "&pass_4=" + hentVerdi("honinput") + "&pass_5="
        + hentVerdi("vernepliktiginput") + "&pass_6=" + hentVerdi("ledsagerinput");
    //Denne kan brukes i stedet hvis gjort om litt:
    /*for (let i = 0; i < 7; i++) {
        let antall_billett = document.getElementsByClassName('field')[i].value;
        //if (antall_billett > 0) {
            url_billetter += "&pass_" + i + "=" + antall_billett;
        //}
    }*/
    return url_billetter;
}

function tilAvganger() {
    var from = JSON.stringify(startHoldeplass);
    var to = JSON.stringify(sluttHoldeplass);
    var datt = hentVerdi("turDato");
    let vindu = "avganger.html?from=" + from + "&to=" + to + "&goDate=" + datt + "&tur=";
    if (document.getElementById("retur").checked == true) {
        vindu += "tovei&backDate=" + hentVerdi("retDato");
    } else {
        vindu += "envei";
    }
    vindu += hentBilletter();

    location.href = vindu;
}

