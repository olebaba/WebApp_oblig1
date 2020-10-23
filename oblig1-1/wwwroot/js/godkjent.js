$(function () { //Startfunksjon kaller på visBilletter()
    visReiser();
});

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

    $("#billett-table").html(tur + retur + prisen);
}
