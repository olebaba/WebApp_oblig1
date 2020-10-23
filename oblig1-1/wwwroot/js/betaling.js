$(function () { //Startfunksjon kaller på visAvganger()
    visReiser();
});

function getUrlParam(param) { //Henter ut parametere fra url. Kode tatt fra nett.
    var queryString = window.location.search;
    var urlParams = new URLSearchParams(queryString);
    return urlParams.get(param);
}

function hentDato(dato) { //Henter reisedato fra url. Kode tatt fra nett.
    let url_dato = new Date(getUrlParam(dato));
    var options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
    var formatert_dato = url_dato.toLocaleDateString("no-NO", options);
    var reise_dato = formatert_dato.charAt(0).toUpperCase() + formatert_dato.slice(1);
    return "<tr class='tr2'><th><br/>" + reise_dato; + "</th></tr>";
}

function settDetaljer(reise) {
    var detaljer = "<br/><br/><tr class='tr2'><th class='notbold'>Avreise:</th></tr><tr class='tr2'><th>" + /*Legg inn for å hente reisetid her!! + " " + */
        reise.startsted.sted + "</th></tr class='tr2'>" + "<tr class='tr2'><th class='notbold'><br/>Ankomst:</th></tr><tr class='tr2'><th>" +
        /*Legg inn for å hente ankomsttid her!! + " " + */ reise.reisemal.sted + "</th></tr>";
    return detaljer;
}

function visReiser() {

    var utReise = JSON.parse(getUrlParam("tur"));
    var tilbakeReise = JSON.parse(getUrlParam("retur"));
    var tur = hentDato('goDate') + settDetaljer(utReise);
    var retur = "";// ((tilbakeReise != null) ? "" : settDetaljer(tilbakeReise));

    if (tilbakeReise != null) {
        retur = hentDato('backDate') + settDetaljer(tilbakeReise);
    }

    var prisen = "<tr class='tr2'><th><br/>Pris: " + JSON.parse(getUrlParam("pris")) + " kr</th></tr>";

    $("#reise-table").html(tur + retur + prisen);
}

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
            lagreBestilling();
            //location.href = 'godkjent.html';
        }
    }
    else if (valgt == 'vipps') {
        if (navnOk && telefonOk && vippsOk) {
            lagreBestilling();
            //location.href='godkjent.html';
        }
    }
}

function lagreBestilling() {
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);

    const tur = JSON.parse(urlParams.get('tur'));
    const retur = JSON.parse(urlParams.get('retur'));
    const goDate = new Date((urlParams.get('goDate')));
    const backDate = new Date((urlParams.get('backDate')));

    const bestilltTur = {
        totalTid: tur.totalTid, //fiks her
        holdeplasser: tur.holdeplasser,
        datoer: goDate.toISOString().substr(0,10)
    }

    const bestilltRetur = {
        totalTid: retur.totalTid,
        holdeplasser: retur.holdeplasser,
        datoer: backDate.toISOString().substr(0, 10)
    }

    const kunde = {
        navn: $("#kjøperNavn").val(),
        mobilnummer: $("#reisendeTelefon").val()
    }

    const pris = JSON.parse(urlParams.get('pris'));

    const bestilling = {
        pris: pris,
        kunde: kunde,
        tur: bestilltTur,
        retur: bestilltRetur
    }

    $.post("Bestilling/Lagre", bestilling, function () {
        location.href = 'godkjent.html' + window.location.search;
    })
    .fail(function (error) {
        $("#feil").html("Feil på server - prøv igjen senere. (" + error.responseText + ")");
    }); 
}

function visEnBestilling() {
    $.get("Bestilling/HentEn?id=1", function (bestilling) {
    })
        .fail((message) => {
        });
}
