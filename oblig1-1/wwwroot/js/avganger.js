$(function () { //Startfunksjon kaller på visAvganger()
    visAvganger();
});

function visAvganger() {    //Denne henter alle relevante avganger og sender dem til å bli skrevet ut
    //Denne må fullføres
    hentTittel();
    hentDato();
    //Hent fra og til fra db?
    hentRuteFraDB();

    //Hent billetter fra db?
    settBilletter();
}

function getUrlParam(param) { //Henter ut parametere fra url. Kode tatt fra nett.
    var queryString = window.location.search;
    var urlParams = new URLSearchParams(queryString);
    return urlParams.get(param);
}

function hentTittel() { //Henter hvor reisen starter og slutter fra url, og sender videre. Kode tatt fra nett.
    let v1, v2;
    if (getUrlParam("steg") == null) {
        v1 = getUrlParam('from');
        v2 = getUrlParam('to');
    } else {
        v1 = getUrlParam('to');
        v2 = getUrlParam("from");
    }
    settTittel(v1, v2);
}

function hentDato() { //Henter dato fra url og sender videre. Kode tatt fra nett.
    sjekkRetur();
    let url_dato;
    if (getUrlParam("steg") == null) {
        url_dato = new Date(getUrlParam('goDate'));
    } else {
        url_dato = new Date(getUrlParam('backDate'));
    }
    
    var options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
    var formatert_dato = url_dato.toLocaleDateString("no-NO", options);
    var reise_dato = formatert_dato.charAt(0).toUpperCase() + formatert_dato.slice(1);
    settDato(reise_dato);
}

function hentBilletter() { //Henter billetter fra url og sender videre. Noe av kode tatt fra nett.
    var billettNavn = [" Voksen", " Barn", " Småbarn", " Student", " Honnør", " Vernepliktig", " Ledsager"];
    let billetter = "";
    let counter = 0;
    for (let i = 0; i < 7; i++) {
        let billett = getUrlParam("pass_" + i);
        if (billett > 0) {
            if (counter > 0) {
                billetter += ", ";
            }
            billetter += billett + billettNavn[i]; 
        }
        counter++;
    }
    return billetter;
}

function hentRuteFraDB() {
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    var fra = {
        sted: urlParams.get('from')
    }
    var til = {
        sted: urlParams.get('to')
    }

    var reise = {
        datoer: urlParams.get('goDate'),
        holdeplasser: [
            fra, til
        ]
    }
    var retur = false;
    if (urlParams.get('tur') == 'tovei') retur = true; 

    console.log("Reise fra " + fra.sted + " til " + til.sted);
    $.post("Bestilling/FinnEnRute", reise, function (rute) {
        console.log(rute);
        //console.log(rute.holdeplasser);
        var fra = rute.holdeplasser[0];
        var til = rute.holdeplasser[rute.holdeplasser.length - 1];
        settTittel(fra.sted, til.sted);
        //settDato(rute.datoer);
        var holdeplasser = rute.holdeplasser;
        var avgangstider = fra.avgangstider.split(",");
        avreiser = [];
        for (i = 0; i < avgangstider.length; i++) {
            avreiser[i] = { start: avgangstider[i], totaltid: rute.totalTid, pris: (rute.holdeplasser.length * 66.6).toFixed(2), holdeplasser }
        }
        console.log(avreiser);
        visAvreiser(avreiser, retur);
    })

    // dersom det skjer en feil når man skal hente rute  
    .fail(function () {
        $("#feil").html("Feil på server - prøv igjen senere");
        console.log("feil med db");
    }); 

    
}

function sjekkRetur() { //Sjekker om reisen er tur-retur
    if (getUrlParam("tur") == "tovei") {
        return true;
    } else {
        return false;
    }
}

function settTittel(fra, til) { //Setter hvor reisen starter og slutter
    let tittel = fra + " til " + til;
    $("#fraOgTil").html(tittel);
}

function hentDato() { //Henter reisedato fra url. Kode tatt fra nett.
    let url_dato = new Date(getUrlParam('goDate'));
    var options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
    var formatert_dato = url_dato.toLocaleDateString("no-NO", options);
    var reise_dato = formatert_dato.charAt(0).toUpperCase() + formatert_dato.slice(1);
    settDato(reise_dato);
}

function settDato(dato) { //Setter reisedato
    $("#datoTittel").html(dato);
}

function settBilletter() { //Setter valgte billetter
    let bill = hentBilletter();
    $("#billetter").html(bill);
}

function gaTilbake() {
    location.href = "forside.html";
}

var turJson, returJson, pris;

function gaVidere() { //Gå til retur-side hvis reisen er tur-retur
    var url = "betaling.html?tur=" + JSON.stringify(turJson) + "retur=" + JSON.stringify(returJson) +
        "pris=" + ((returJson != undefined) ? (turJson.pris + returJson.pris) : turJson.pris);
    location.href = url;
}

function formaterTid(tid) { //Formaterer tid til 00:00-format. Noe av kode tatt fra nett.
    let time, min;
    if (tid.indexOf(" ") == 0) {
        tid = tid.substr(1, 4);
    }
    time = tid.substr(0, 2);
    min = tid.substr(2, 2);

    let nyFormat = time + ":" + min;
    return nyFormat;
}

function settAnkomst(avreise, timer, minutter) { //Setter ankomsttid. Noe av kode tatt fra nett.
    let reise_1 = avreise.substr(0, 2);
    let reise_2 = avreise.substr(3, 2);
    let time = parseInt(timer);
    let min = parseInt(minutter);
    let reiseDato = new Date(2020, 01, 01, reise_1, reise_2, 0);
    reiseDato.setHours(reiseDato.getHours() + time);
    reiseDato.setMinutes(reiseDato.getMinutes() + min);
    reiseDato = reiseDato.toString();
    return reiseDato.substr(16, 2) + ":" + reiseDato.substr(19, 2);
}

function visAvreiser(avreiser, retur) {    //Funksjon som skriver ut avganger

    var uttur = setAvreise(avreiser, false);
    $("#avreiser").html(uttur);
    
    if (retur) {
        var utretur = setAvreise(avreiser, true);
        $("#tilbake").after("<br/><br/>" + utretur);
        
    }
    
}

function setAvreise(avreiser, retur) {
    var timer = Math.floor(parseInt(avreiser[0].totaltid) / 60);
    var minutter = parseInt(avreiser[0].totaltid) % 60;
    var reisetid = timer + " timer og " + minutter + " minutter"
    var holdeplasser, pris;

    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Avreise</th><th>Ankomst</th><th>Reisetid</th><th>Pris</th><th>Holdeplasser</th>" +
        "<th></th>" +
        "</tr>";
    for (let avreise of avreiser) {
        holdeplasser = avreise.holdeplasser;
        pris = avreise.pris;
        let avreiseTid = formaterTid(avreise.start);
        let ankomst = settAnkomst(avreiseTid, timer, minutter);
        ut += "<tr>" +
            "<td>" + avreiseTid + "</td>" +
            "<td>" + ankomst + "</td>" +
            "<td>" + reisetid + "</td>" +
            "<td>" + pris + "kr</td>" +
            "<td>";
        ut += holdeplasser[0].sted;
        let lengde = holdeplasser.length-1;
        let visHoldeplasser="";
        for (h = 1; h < lengde; h++) {
            visHoldeplasser += ((retur) ? holdeplasser.reverse()[h].sted : holdeplasser[h].sted) + ", ";
        }
        ut += "<a href='#' data-toggle='tooltip' title='" + visHoldeplasser + "'><br>" + lengde+" stopp <br></a>"
        ut += holdeplasser[holdeplasser.length - 1].sted;
        ut += "</td>" +
            '<td><input type="button" value="Velg reise" onclick="gaVidere()"/></td>';
    }
    ut += "</tr></table>";

    if (retur) {
        returJson = {
            avreise: avreiser.start,
            reisetid: reisetid,
            pris: pris,
            holdeplasser: holdeplasser.reverse()
        }
    } else {
        turJson = {
            avreise: avreiser.start,
            reisetid: reisetid,
            pris: pris,
            holdeplasser: holdeplasser
        }
    }

    return ut;
}

