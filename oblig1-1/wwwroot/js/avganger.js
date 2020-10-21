$(function () { //Startfunksjon kaller på visAvganger()
    visAvganger();
});

function visAvganger() {    //Denne henter alle relevante avganger og sender dem til å bli skrevet ut
    hentTittel();
    hentDato();
    settBilletter();
    hentRuteFraDB();
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

var totalpris;

function hentBilletter() { //Henter billetter fra url og sender videre.
    var billettNavn = [" Voksen", " Barn", " Småbarn", " Student", " Honnør", " Vernepliktig", " Ledsager"];
    var billettPriser = [60, 28, 0, 32, 28, 14, 25]; //må nok justeres
    let billetter = "";
    let counter = 0;
    var pris = 0;
    for (let i = 0; i < billettNavn.length; i++) {
        let billett = getUrlParam("pass_" + i);
        if (billett > 0) {
            if (counter > 0) {
                billetter += ", ";
            }
            billetter += billett + billettNavn[i];
            pris += billettPriser[i];
        }
        counter++;

    }
    totalpris = pris;
    return billetter;
}

var hentetRute;

function hentRuteFraDB() { //henter rute fra databasen og formaterer + viser tider i en tabell
    var fra = {
        sted: getUrlParam('from')
    }
    var til = {
        sted: getUrlParam('to')
    }

    var reise = {
        datoer: getUrlParam('goDate'),
        holdeplasser: [
            fra, til
        ]
    }
    var retur = (getUrlParam('tur') == 'tovei') ? true : false; 

    $.post("Bestilling/FinnEnRute", reise, function (rute) {        
        if (rute == null) {
            visFeilmelding("Ingen ruter for denne reisen kunne bli funnet.");
        } else {
            formaterRute(rute); //setter verdier i hentetRute
            var fra = rute.holdeplasser[0];
            var til = rute.holdeplasser[rute.holdeplasser.length - 1];
            settTittel(fra.sted, til.sted);
            avreiser = [];
          
            for (i = 0; i < hentetRute.avreiseTider.length; i++) {
                console.log(i);
                avreiser[i] = {
                    start: hentetRute.avreiseTider[i],
                    totaltid: hentetRute.avreiseTider,
                    pris: hentetRute.pris,
                    holdeplasser: hentetRute.holdeplasser
            }
          
            visAvreiser(avreiser, retur);

        }
    })

    // dersom det skjer en feil når man skal hente rute  
    .fail(function () {
        $("#feil").html("Feil på server - prøv igjen senere");
    }); 
}
           
function visFeilmelding(melding) {
    $("#avreiser").after('<p style="color:red">' + melding + '</p>');
}

function formaterRute(rute) { //formaterer rute til en JSON, hentetRute
    var tider = rute.holdeplasser[0].avgangstider.split(",");
    hentetRute = {
        avreiseTider: tider,
        totaltid: rute.totaltid,
        pris: totalpris.toFixed(2), 
        holdeplasser: rute.holdeplasser
    };
    console.log(hentetRute);
}

function sjekkRetur() { //Sjekker om reisen er tur-retur
    /*if (getUrlParam("tur") == "tovei") {
        return true;
    } else {
        return false;
    }*/
    return (getUrlParam("tur") == "tovei")
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

function gaVidere() { //setter url til betalingssiden med korrekte verdier
    var url = "betaling.html?tur=" + JSON.stringify(turJson) + "&retur=" + ((returJson != undefined) ? JSON.stringify(returJson) : null) +
        "&pris=" + ((returJson != undefined) ? (Number(turJson.pris) + Number(returJson.pris)).toFixed(2) : JSON.stringify(turJson.pris).toFixed(2));
    
    location.href = url;
}

function formaterTid(tid) { //Formaterer tid til 00:00-format. Noe av kode tatt fra nett.
    console.log(tid);
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
    for (let avreise of avreiser) { //avreise = rute med én tid
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
    return ut;
}

function reisevalg(element) { //gjør det mulig å huke av hvilke reiser man vil bestille, og setter verdiene som brukes i url-en
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
