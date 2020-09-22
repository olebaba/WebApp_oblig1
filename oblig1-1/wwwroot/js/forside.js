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

    $('#add3').click(function () {
        $("#sub3").prop("disabled", false);
        let number = $('#studentinput').val();

        if (number >= 9) {
            $("#add3").prop("disabled", true);
        } else {
            number++;
            $('#studentinput').val(number);
        }
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
    });

    $.get("bestilling/HentHoldeplasser", function (holdeplasser) {
        formaterHoldeplass(holdeplasser);
    });

    function formaterHoldeplass(holdeplass) {
        let avTags = [];
        for (let i = 0; i < holdeplass.length; i++) {
            avTags.push(holdeplass[i].sted);
            console.log(holdeplass[i].sted);
        }
            $("#fra").autocomplete({
                source: avTags,
                minLength: 1
            });
            $("#til").autocomplete({
                source: avTags,
                minLength: 1
            });
            }
    

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