$(function () {
    console.log("Kjører");
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
        $("#sub").prop("disabled", false);
        let number = $('#textinput').val();
        
        if (number >= 9) {
            $("#add").prop("disabled", true);
        } else {
            number++;
            $('#textinput').val(number);
        }
        
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
    });
});

function beregnPris() {
    
    let pris;
    pris = $('#textinput').val() * 50 +
        $('#honinput').val() * 25 +
        $('#barninput').val() * 20;
    console.log(pris);
    $('#pristest').html("Prisen blir " + pris);
    if (pris == 0) {
        $('#pristest').html("Du har ikke valgt noen reisende");
    }
}

function visTider() {
    var url = "Rutetider.html";

    location.href = url;
}