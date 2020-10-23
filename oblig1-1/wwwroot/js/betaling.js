$(function () { //Startfunksjon kaller på visAvganger()
    visReiser();
});

function getUrlParam(param) { //Henter ut parametere fra url. Kode tatt fra nett.
    var queryString = window.location.search;
    var urlParams = new URLSearchParams(queryString);
    return urlParams.get(param);
}

function hentDato(tur) { //Henter reisedato fra url. Kode tatt fra nett.
    let url_dato = new Date(tur.dato); //getUrlParam(dato));
    var options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
    var formatert_dato = url_dato.toLocaleDateString("no-NO", options);
    var reise_dato = formatert_dato.charAt(0).toUpperCase() + formatert_dato.slice(1);
    return "<tr class='tr2'><th><br/>" + reise_dato; + "</th></tr>";
}

function settDetaljer(reise) {
    var startSted = (reise.rute).substr(0, (reise.rute).indexOf('-'));
    var sluttSted = (reise.rute).split('-')[1];

    var detaljer = "<br/><br/><tr class='tr2'><th class='notbold'>Avreise:</th></tr><tr class='tr2'><th>" + /*Legg inn for å hente reisetid her!! + " " + */
        startSted + "</th></tr class='tr2'>" + "<tr class='tr2'><th class='notbold'><br/>Ankomst:</th></tr><tr class='tr2'><th>" +
        /*Legg inn for å hente ankomsttid her!! + " " + */ sluttSted + "</th></tr>";
    return detaljer;
}

function visReiser() {

    var utReise = JSON.parse(getUrlParam("tur"));
    
    var tilbakeReise = JSON.parse(getUrlParam("retur"));
    var tur = hentDato(utReise) + settDetaljer(utReise);
    var retur = "";// ((tilbakeReise != null) ? "" : settDetaljer(tilbakeReise));

    if (tilbakeReise != null) {
        retur = hentDato(tilbakeReise) + settDetaljer(tilbakeReise);
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
    /*
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);

    const tur = JSON.parse(getUrlParam('tur'));
    const retur = JSON.parse(getUrlParam('retur'));
    const goDate = new Date(tur.dato);
    
    const bestilltTur = {
        //totalTid: tur.totalTid, //fiks her
        holdeplasser: tur.holdeplasser,
        datoer: goDate.toISOString().substr(0,10)
    }
    
    const bestilltRetur = null;
    
    if(retur != null){
        const backDate = new Date(retur.dato);
        
        bestilltRetur = {
            //totalTid: retur.totalTid,
            holdeplasser: retur.holdeplasser,
            datoer: backDate.toISOString().substr(0, 10)
        }
    }else{
        bestilltRetur = bestilltTur;   
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
    }); */
    location.href = 'godkjent.html' + window.location.search;
}
