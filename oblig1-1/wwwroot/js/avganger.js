$(function () { //Startfunksjon kaller på visAvganger()
    visAvganger();
});

function visAvganger() {    //Denne henter alle relevante avganger og sender dem til å bli skrevet ut
    hentTittel();
    hentDato();
    hentRuteFraDB();
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

var hentetRute;

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

    $.post("Bestilling/FinnEnRute", reise, function (rute) {
        formaterRute(rute);
        var fra = rute.holdeplasser[0];
        var til = rute.holdeplasser[rute.holdeplasser.length - 1];
        settTittel(fra.sted, til.sted);
        //settDato(rute.datoer);
        var holdeplasser = rute.holdeplasser;
        var avgangstider = fra.avgangstider.split(",");
        avreiser = [];
        for (i = 0; i < avgangstider.length; i++) {
            avreiser[i] = { start: avgangstider[i], totaltid: rute.totalTid, pris: (rute.holdeplasser.length * 66.6).toFixed(2), holdeplasser } //tullepris
        }
        visAvreiser(avreiser, retur);
    })

    // dersom det skjer en feil når man skal hente rute  
    .fail(function () {
        $("#feil").html("Feil på server - prøv igjen senere");
    }); 
}

function formaterRute(rute) {
    var tider = rute.holdeplasser[0].avgangstider.split(",");
    hentetRute = {
        avreiseTider: tider,
        totaltid: rute.totaltid,
        pris: (rute.holdeplasser.length * 66.6).toFixed(2), //bør være rute.pris
        holdeplasser: rute.holdeplasser
    };
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

function gaVidere() {
    var url = "betaling.html?tur=" + JSON.stringify(turJson) + "&retur=" + ((returJson != undefined) ? JSON.stringify(returJson) : null) +
        "&pris=" + ((returJson != undefined) ? (Number(turJson.pris) + Number(returJson.pris)).toFixed(2) : JSON.stringify(turJson.pris).toFixed(2));
    
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
        $("#tilbake").after("<br/><br/><h2>Retur:</h2>" + utretur);
        
    }
    
}

function setAvreise(avreiser, retur) { //Skriver ut avganger med data sendt til seg
    var timer = Math.floor(parseInt(avreiser[0].totaltid) / 60);
    var minutter = parseInt(avreiser[0].totaltid) % 60;
    var reisetid = timer + " timer og " + minutter + " minutter"
    var holdeplasser, holdeplasserReverse, pris;

    let ut = "<table" + ((retur) ? " id='returAvreiser'" : "") + " class='table table-striped'>" +
        "<tr>" +
        "<th>Avreise</th><th>Ankomst</th><th>Reisetid</th><th>Pris</th><th>Holdeplasser</th>" +
        "<th></th>" +
        "</tr>";
    for (let avreise of avreiser) {
        holdeplasser = avreise.holdeplasser;
        holdeplasserReverse = holdeplasser.slice().reverse();
        pris = avreise.pris;
        let avreiseTid = formaterTid(avreise.start);
        let ankomst = settAnkomst(avreiseTid, timer, minutter);
        ut += "<tr>" +
            "<td>" + avreiseTid + "</td>" +
            "<td>" + ankomst + "</td>" +
            "<td>" + reisetid + "</td>" +
            "<td>" + pris + "kr</td>" +
            "<td>";
        ut += ((retur) ? holdeplasserReverse[0].sted : holdeplasser[0].sted);
        let lengde = holdeplasser.length-1;
        let visHoldeplasser="";
        for (h = 1; h < lengde; h++) {
            visHoldeplasser += ((retur) ? holdeplasserReverse[h].sted : holdeplasser[h].sted) + ", ";
        }
        ut += "<a href='#' data-toggle='tooltip' title='" + visHoldeplasser + "'><br>" + lengde + " stopp <br></a>"
        ut += ((retur) ? holdeplasserReverse[lengde].sted : holdeplasser[lengde].sted);
        ut += "</td>" +
            '<td><div class="avgCheckBox"><label>' +
            '<input class="reisevalg" type="checkbox" hidden onChange="reisevalg($(this))"/><span>Velg reise</span>' +
            '</label></div></td>';
    }
    ut += "</tr></table>";

    /*
    if (retur) {
        returJson = {
            avreise: avreiser.start,
            reisetid: reisetid,
            pris: pris,
            holdeplasser: holdeplasserReverse
        }
    }
    turJson = {
        avreise: avreiser.start,
        reisetid: reisetid,
        pris: pris,
        holdeplasser: holdeplasser
    }*/

    return ut;
}

function setReise(tur, retur) {
    if (retur != null) {
        returJson = {

        }
    }
    turJson = {

    }
}

function reisevalg(element) {
    var valgtRad, table;
    if ($(element).prop('checked')) {
        valgtRad = element.closest('tr');
        table = element.closest('table').attr('id');
        
    } else {
        //console.log("not checked", element);
    }

    var i = 1;
    for (i; i < $(`#${table} tr`).length; i++) {
        if (i != valgtRad.index()) {
            $(`#${table} tr`).eq(i).find('input').prop('checked', false);
        }
    }

    if (table == "avreiser") {
        turJson = {
            avreise: hentetRute.avreiseTider[valgtRad-1],
            reisetid: hentetRute.totalTid,
            pris: hentetRute.pris,
            startsted: hentetRute.holdeplasser[0],
            reisemal: hentetRute.holdeplasser[hentetRute.holdeplasser.length-1]
        };
    }
    if (table == "returAvreiser") {
        returJson = {
            avreise: hentetRute.avreiseTider[valgtRad-1],
            reisetid: hentetRute.totalTid,
            pris: hentetRute.pris,
            startsted: hentetRute.holdeplasser[hentetRute.holdeplasser.length-1],
            reisemal: hentetRute.holdeplasser[0]
        };
    }

    
};