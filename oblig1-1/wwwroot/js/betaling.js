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
    const navn = $("#kjøperNavn").val();
    const telefon = $("#reisendeTelefon").val();
    const kortnummer = $("#kortnummer").val();
    const kortnavn = $("#navn").val();
    const utløpsdato = $("#utløpsdato").val();
    const cvc = $("#cvc").val();
    const kjøperTelefon = $("#telefonnr").val();

    const navnOk = validerNavn(navn);
    const telefonOk = validerMobilnummer(telefon);
    const kortnavnOk = validerKortnavn(kortnavn);
    const kortnummerOk = validerKortnummer(kortnummer);
    const utløpOk = validerUtlop(utløpsdato);
    const cvcOk = validerCVC(cvc);
    const vippsOk = validerVipps(kjøperTelefon);

    var valgt = $('#betaling').val();

    if (valgt == 'kort') {
        if (navnOk && telefonOk && kortnavnOk && kortnummerOk && utløpOk && cvcOk) {
            //lagreBestilling();
            location.href = 'godkjent.html';
        }
    }
    else if (valgt == 'vipps') {
        if (navnOk && telefonOk && vippsOk) {
            //lagreBestilling();
            location.href='godkjent.html';
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

    const pris = JSON.parse(urlParams.get('pris'));

    const bestilling = {
        pris: pris,
        kunde: kunde, 
        tur: tur,
        retur: retur
    }

    $.post("Bestilling/Lagre", bestilling, function () {
        location.href = 'godkjent.html';
    })
    .fail(function () {
        $("#feil").html("Feil på server - prøv igjen senere");
        console.log("feil med db");
    }); 
}
