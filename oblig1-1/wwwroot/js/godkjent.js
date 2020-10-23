$(function () { //Startfunksjon kaller på visBilletter()
    visBilletter();
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

function visBilletter() {

    var utReise = JSON.parse(getUrlParam("tur"));
    var tilbakeReise = JSON.parse(getUrlParam("retur"));
    var tur = hentDato('goDate') + settDetaljer(utReise);
    var retur = "";// ((tilbakeReise != null) ? "" : settDetaljer(tilbakeReise));

    if (tilbakeReise != null) {
        retur = hentDato('backDate') + settDetaljer(tilbakeReise);
    }

    var prisen = "<tr class='tr2'><th><br/>Pris: " + JSON.parse(getUrlParam("pris")) + " kr</th></tr>";

    $("#billett-table").html(tur + retur + prisen);
}
