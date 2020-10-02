// funksjon som gjør at vi kun viser input-bokser som er relevant til valgt 
// betalingsmåte 
function valgtBetaling() {
    var valgt = $('#betaling').val();
    if (valgt == 'kort') {
        $('#kort').show();
        $('#vipps').hide();
    }
    else if (valgt == 'vipps') {
        $('#vipps').show();
        $('#kort').hide();
    }
    else {
        $('#vipps').hide();
        $('#kort').hide();
    }
}

function validerOgBetal() {
    const navnOk = validerNavn("#kjøpernavn").val();
    const telefonOk = validerMobilnumme("#reisendeTelefon").val();
    const kortnavnOk = validerKortnavn("#navn").val();
    const kortnummerOk = validerKortnummer("#kortnummer").val();
    const utløpOk = validerUtlop("#utløpsdato").val();
    const cvcOk = validerCVC("#cvc").val();
    const vippsOk = validerVipps("#telefonnr").val();

    var valgt = $('#betaling').val();

    if (valgt == 'kort') {
        if (navnOk && telefonOk && kortnavnOk && kortnummerOk && utløpOk && cvcOk) {
            location.href = 'godkjent.html';
        }
    }
    else if (valgt == 'vipps') {
        if (navnOk && telefonOk && vippsOk) {
            location.href = 'godkjent.html';
        }
    }
}
