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
        let number = $('#honinput').val();
        number++;
        $('#honinput').val(number);
    });

    $('#sub1').click(function () {
        let number = $('#honinput').val();
        if (number <= 0) {
        } else {
            number--;
        }
        $('#honinput').val(number);
    });

    $('#add2').click(function () {
        let number = $('#barninput').val();
        number++;
        $('#barninput').val(number);
    });

    $('#sub2').click(function () {
        let number = $('#barninput').val();
        if (number <= 0) {
        } else {
            number--;
        }
        $('#barninput').val(number);
    });
});


