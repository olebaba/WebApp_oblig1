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

    $('#add').click(function () {
        let number = $('#textinput').val();
        number++;
        $('#textinput').val(number);
    });

    $('#sub').click(function () {
        let number = $('#textinput').val();
        if (number <= 0) {
        } else {
            number--;
        }
        $('#textinput').val(number);
    });
});

function lagreBestilling() {

    const bestilling = {
        navn: $('#navn').val(),
        nummer: $("#nummer").val()
    }

    const url = "";
    $.post(url, bestilling, function (OK) {
        if (OK) {
            window.location.href = 'Rutetider.html';
        } else {
            $("#feil").html("Feil i db - bestilling");
        }
    });

}

