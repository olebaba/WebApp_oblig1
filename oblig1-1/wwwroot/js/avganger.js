$(function () { //Startfunksjon kaller på visAvganger()
    visAvganger();
});

var hentetRute = {};
var totalpris;
var turJson, returJson, pris;

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

/*
function hentDato() { //Henter dato fra url og sender videre. Kode tatt fra nett.
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
}*/

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

function beregnPris(array) {
    for (i = 0; i < array.length; i++) {
        console.log("YES " + array[i]);
    }
    let billetter = hentBilletter();

    let num = 1;
    let j = 0;
    for (let i = 1; i < array.length; i++) {
        for (j = 0; j < i; j++) {
            if (array[i] === array[j]) {
                break;
            }
        }
        if (i === j) {
            num++;
        }
    }
    console.log("Num" + num);

    $.post("bestilling/HentPriser", function (priser) {
        var bill = billetter.split(',');
        let totPris = 0;
        let sonePris = 0;
        let antall = 0;
        for (let i = 0; i < array.length; i++) {
            var name = bill[i];
            console.log("BILETE "+name)
            if (i > 0) {
                var sName = name.substr(3, 20);
                console.log("SNAME "+sName);
            } else {
                var sName = name.substr(2, 20);
                console.log("SNAME2 " + sName);
            }
            if (sName === "Voksen") {
                if (num === 1) {
                    sonePris = priser[0].pris1Sone;
                } else if (num === 2) {
                    sonePris = priser[0].pris2Sone;
                } else if (num === 3) {
                    sonePris = priser[0].pris3Sone;
                } else if (num === 4) {
                    sonePris = priser[0].pris4Sone;
                }
            } else if (sName === "Barn") {
                if (num === 1) {
                    sonePris = priser[1].pris1Sone;
                } else if (num === 2) {
                    sonePris = priser[1].pris2Sone;
                } else if (num === 3) {
                    sonePris = priser[1].pris3Sone;
                } else if (num === 4) {
                    sonePris = priser[1].pris4Sone;
                }
            } else if (sName === "Honnør") {
                if (num === 1) {
                    sonePris = priser[2].pris1Sone;
                } else if (num === 2) {
                    sonePris = priser[2].pris2Sone;
                } else if (num === 3) {
                    sonePris = priser[2].pris3Sone;
                } else if (num === 4) {
                    sonePris = priser[2].pris4Sone;
                }
            }
            if (i > 0) {
                antall = name.substr(1, 2);
            } else {
                antall = name.substr(0, 2);
            }
            console.log("Runde: " + i);
            totPris += sonePris * antall;

        }
        console.log("PIRSERN12: " + totPris);
        for (let i = 0; i < priser.length; i++) {
            console.log("P: " + priser[i].prisklasse);
            
        }
        
    });
}


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
        console.log(rute);
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
        }
        visAvreiser(avreiser, retur);
        var array = [];
        for (i = 0; i < rute.holdeplasser.length; i++) {
            array.push(rute.holdeplasser[i].sone);
            
        }
        beregnPris(array);
    })

    // dersom det skjer en feil når man skal hente rute  
    .fail(function () {
        $("#feil").html("Feil på server - prøv igjen senere");
    }); 
}

function formaterRute(rute) { //formaterer rute til en JSON, hentetRute
    var tider = rute.holdeplasser[0].avgangstider.split(",");
    hentetRute = {
        avreiseTider: tider,
        totalTid: rute.totalTid,
        pris: totalpris.toFixed(2), 
        holdeplasser: rute.holdeplasser,
        goDate: getUrlParam('goDate'),
        backDate: getUrlParam('backDate')
    };
    console.log(hentetRute);
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

function gaVidere() { //setter url til betalingssiden med korrekte verdier
    var url = "betaling.html?tur=" + JSON.stringify(turJson) + "&retur=" + ((returJson != undefined) ? JSON.stringify(returJson) : null) +
        "&pris=" + ((returJson != undefined) ? (Number(turJson.pris) + Number(returJson.pris)).toFixed(2) : JSON.stringify(turJson.pris)) +
        "&goDate=" + hentetRute.goDate + "&backDate=" + hentetRute.backDate;
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
            "<td id='textHoldeplass'>";
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
            totalTid: hentetRute.totalTid,
            pris: hentetRute.pris,
            startsted: hentetRute.holdeplasser[0],
            reisemal: hentetRute.holdeplasser[hentetRute.holdeplasser.length - 1],
            holdeplasser: hentetRute.holdeplasser
        };
    }
    if (table == "returAvreiser") {
        returJson = {
            avreise: hentetRute.avreiseTider[valgtRad-1],
            totalTid: hentetRute.totalTid,
            pris: hentetRute.pris,
            startsted: hentetRute.holdeplasser[hentetRute.holdeplasser.length-1],
            reisemal: hentetRute.holdeplasser[0],
            holdeplasser: hentetRute.holdeplasser
        };
    }

    
};

