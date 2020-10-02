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

function getUrlParam(param) { //Henter ut parametere fra url
    var queryString = window.location.search;
    var urlParams = new URLSearchParams(queryString);
    return urlParams.get(param);
}

function hentTittel() { //Henter hvor reisen starter og slutter fra url, og sender videre
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

function hentDato() { //Henter dato fra url og sender videre
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

function hentBilletter() { //Henter billetter fra url og sender videre
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
        settDato(rute.datoer);
        var holdeplasser = rute.holdeplasser;
        var avgangstider = fra.avgangstider.split(",");
        avreiser = [];
        for (i = 0; i < avgangstider.length; i++) {
            avreiser[i] = { start: avgangstider[i], totaltid: rute.totalTid, pris: (rute.holdeplasser.length * 66.6).toFixed(2), holdeplasser }
        }
        console.log(avreiser);
        skrivUt(avreiser, retur);
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

function hentDato() { //Henter reisedato fra url
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

function gaVidere() { //Gå til retur-side hvis reisen er tur-retur
    let steg = "";
    if (sjekkRetur()) {
        steg = "avganger.html" + window.location.search + "&steg=2";
    } else {
        steg = "betaling.html";
    }
    location.href = steg;
}

function skrivUt(avreiser, retur) {    //Funksjon som skriver ut avganger
    var timer = Math.floor(parseInt(avreiser[0].totaltid) / 60);
    var minutter = parseInt(avreiser[0].totaltid) % 60;
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Avreise</th><th>Reisetid</th><th>Pris</th><th>Holdeplasser</th>" +
        "<th></th>" +
        "</tr>";
    for (let avreise of avreiser) {
        ut += "<tr>" +
            "<td>" + avreise.start + "</td>" +
            "<td>" + timer + " timer og " + minutter + " minutter</td>" +
            "<td>" + avreise.pris + "kr</td>" +
            "<td>";
        for (h in avreise.holdeplasser) {
            ut += avreise.holdeplasser[h].sted + ", ";
        }
        ut += "</td>" +
            '<td><input type="button" value="Velg reise"/></td>';

    }
    ut += "</tr></table>";
    $("#avreiser").html(ut);
    if (retur) {
        //console.log("reisen er med retur");
        utretur = "<table class='table table-striped'>" +
            "<tr>" +
            "<th>Avreise</th><th>Reisetid</th><th>Pris</th><th>Holdeplasser</th>" +
            "<th></th>" +
            "</tr>";
        for (let avreise of avreiser) {
            utretur += "<tr>" +
                "<td>" + avreise.start + "</td>" +
                "<td>" + timer + " timer og " + minutter + " minutter</td>" +
                "<td>" + avreise.pris + "kr</td>" +
                "<td>";
            for (h in avreise.holdeplasser) {
                utretur += avreise.holdeplasser[h].sted + ", ";
            }
            utretur += "</td>" +
                '<td><input type="button" value="Velg reise"/></td>';

        }
        utretur += "</tr></table>";
        $("#tilbake").after("<br/><br/>" + utretur);
    }
    
}

