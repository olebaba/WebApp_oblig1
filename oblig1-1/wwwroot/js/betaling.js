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
    const navnOk = validerNavn("#kjøperNavn").val();
    const telefonOk = validerMobilnumme("#reisendeTelefon").val();
    const kortnavnOk = validerKortnavn("#navn").val();
    const kortnummerOk = validerKortnummer("#kortnummer").val();
    const utløpOk = validerUtlop("#utløpsdato").val();
    const cvcOk = validerCVC("#cvc").val();
    const vippsOk = validerVipps("#telefonnr").val();

    var valgt = $('#betaling').val();

    if (valgt == 'kort') {
        if (navnOk && telefonOk && kortnavnOk && kortnummerOk && utløpOk && cvcOk) {
            lagreBestilling();
        }
    }
    else if (valgt == 'vipps') {
        if (navnOk && telefonOk && vippsOk) {
            lagreBestilling();
        }
    }
}

function lagreBestilling() {
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);

    const tur = JSON.parse(urlParams.get('tur'));
    const retur = JSON.parse(urlParams.get('retur'));

    const kunde = {
        navn: $("#kjøperNavn").val(),
        telefon: $("#reisendeTelefon").val()
    }

    // må hente inn pris fra avganger-siden
    const pris = JSON.parse(urlParams.get('pris'));

    const bestilling = {
        pris: pris,
        kunde: kunde, 
        tur: tur,
        retur: retur
    }

    $.post("Bestilling/Lagre", bestilling, function () {
        window.location.href = 'godkjent.html';
    })
    .fail(function () {
        $("#feil").html("Feil på server - prøv igjen senere");
        console.log("feil med db");
    }); 
}
