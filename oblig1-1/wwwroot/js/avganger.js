$(function () { //Startfunksjon kaller på visAvganger()
    visAvganger();
});

var avreiser = {};
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
        v1 = JSON.parse(getUrlParam('from'));
        v2 = JSON.parse(getUrlParam('to'));
    } else {
        v1 = JSON.parse(getUrlParam('to'));
        v2 = JSON.parse(getUrlParam("from"));
    }
    settTittel(v1.sted, v2.sted);
}

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

function hentRuteFraDB() { //henter rute fra databasen og formaterer + viser tider i en tabell
    var date = new Date(getUrlParam('goDate'));
    var day = date.getDate();
    var month = date.getMonth() + 1;
    var year = date.getFullYear();

    // After this construct a string with the above results as below
    var time = day + "/" + month + "/" + year + " 00:00:00";
    var onsketReise = {
        holdeplasserOgDato: [
            getUrlParam('from'),
            getUrlParam('to'),
            time
        ]
    };

    $.post("Bestilling/FinnEnRuteAvgang", onsketReise, function (ruteavganger) {
        if (ruteavganger == null || ruteavganger.length <= 0) {
            visFeilmelding("Ingen ruteravganger for denne reisen kunne bli funnet.");
        } else {
            console.log(ruteavganger);
            avreiser = ruteavganger;
            visAvreiser(ruteavganger, sjekkRetur()); 
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

function gaVidere() { //setter url til betalingssiden med korrekte verdier
    console.log("gå videre");
    var url = "betaling.html?tur=" + JSON.stringify(turJson) + "&retur=" + ((returJson != undefined) ? JSON.stringify(returJson) : null) +
        "&pris=" + ((returJson != undefined) ? (Number(turJson.pris) + Number(returJson.pris)).toFixed(2) : JSON.stringify(turJson.pris).toFixed(2));
    //"&goDate=" + avreiser.goDate + "&backDate=" + avreiser.backDate;
    location.href = url;
    /*if ($(".avgCheckBox").length == 4 && $(".avgCheckBox input:checkbox:checked").length > 1) {
        location.href = url;
    } else if ($(".avgCheckBox").length == 2) {
        location.href = url;
    }*/
        
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

function finnAnkomst(avreise, reisetid) { //Setter ankomsttid
    var avreiseArr = avreise.split("");
    var reisetidArr = reisetid.split("");
    var ankomst = [];

    for (i = 0; i < avreiseArr.length; i++) {
        ankomst[i] = Number(avreiseArr[i]) + Number(reisetidArr[i]);
    }
    var time = ankomst[0];
    var minutter = "" + ankomst[2] + ankomst[3];
    var sekunder = ankomst[5] + ankomst[6];
    if (minutter > 60) {
        time++;
        minutter = minutter - 60;
    }

    return time + ":" + minutter + ":" + sekunder;
}

function regnReisetid(start, stopp, motsattreise) {//finner reisetid
    var stopptid = stopp.split("");
    var starttid = start.split("");
    var totaltid = [];
    if (motsattreise) {
        for (i = 0; i < stopptid.length; i++) {
            totaltid[i] = Number(starttid[i]) - Number(stopptid[i]);
        }
    } else {
        for (i = 0; i < stopptid.length; i++) {
            totaltid[i] = Number(stopptid[i]) - Number(starttid[i]);
        }
    }
    return totaltid[0] + totaltid[1] + ":" + totaltid[3] + totaltid[4] + ":" +
        totaltid[6] + totaltid[7];
}

function finnAvgangTid(rutestart, forstestopptid) {//finner avgangstid
    var rutestartArr = rutestart.split("");
    var forstestopptidArr = forstestopptid.split("");
    var avgangtid = [];

    for (i = 0; i < rutestartArr.length; i++) {
        avgangtid[i] = Number(forstestopptidArr[i]) + Number(rutestartArr[i]);
    }

    return avgangtid[0] + avgangtid[1] + ":" + avgangtid[3] + avgangtid[4] +
        ":" + avgangtid[6] + avgangtid[7];
}

function visAvreiser(ruteavganger, retur) {    //Funksjon som skriver ut avganger
    var uttur = setAvreise(ruteavganger, false);
    $("#avreiser").html(uttur);
    
    if (retur) {
        console.log("Reisen er retur!");
        var utretur = setAvreise(ruteavganger, true);
        $("#tilbake").after("<br/><br/><h2>Retur:</h2>" + utretur);
    }    
}

var holdeplasserIReise = [];

function setAvreise(ruteavganger, retur) { //Skriver ut avganger med data sendt til seg

    let ut = "<table" + ((retur) ? " id='returAvreiser'" : "") + " class='table table-striped'>" +
        "<tr>" +
        "<th>Avreise</th><th>Ankomst</th><th>Reisetid</th><th>Pris</th><th>Holdeplasser</th>" +
        "<th></th>" +
        "</tr>";

    for (let avgang of ruteavganger) {
        var holdeplasser = [];
        var sisteRutestopp = JSON.parse(getUrlParam('to'));
        var forsteStopp = JSON.parse(getUrlParam('from'));
        var sisteStoppTid, forsteStoppTid;
        var alleRutestopp = avgang.rute.ruteStopp;
        alleRutestopp.sort(function (a, b) {
            if (a.stoppTid > b.stoppTid) return 1;
            if (a.stoppTid < b.stoppTid) return -1;
            return 0;
        });
        var reiserute = [];
        var leggtil = false;
        for (i in alleRutestopp) { //filtrer til riktige rutestopp for reise
            
            if (alleRutestopp[i].holdeplass.id == forsteStopp.id) {
                forsteStoppTid = alleRutestopp[i].stoppTid;
                leggtil = true;
            }
            if (alleRutestopp[i].holdeplass.id == sisteRutestopp.id) {
                sisteStoppTid = alleRutestopp[i].stoppTid;
                reiserute.push(alleRutestopp[i]);
                break;
            }
            if (leggtil) {
                reiserute.push(alleRutestopp[i]);
            }
        }
        var motsattreise = false;
        if (reiserute.length == 1) {//om reisen går motsatt vei av ruten
            reiserute = [];
            motsattreise = true;
            for (i in alleRutestopp) { //filtrer til riktige rutestopp for reise

                if (alleRutestopp[i].holdeplass.id == sisteRutestopp.id) {
                    sisteStoppTid = alleRutestopp[i].stoppTid;
                    leggtil = true;
                }
                if (alleRutestopp[i].holdeplass.id == forsteStopp.id) {
                    forsteStoppTid = alleRutestopp[i].stoppTid;
                    reiserute.push(alleRutestopp[i]);
                    break;
                }
                if (leggtil) {
                    reiserute.push(alleRutestopp[i]);
                }
            }
            reiserute.reverse();
        }
        //console.log(reiserute);
        reiserute.forEach(rs => { holdeplasser.push(rs.holdeplass) });
        holdeplasser.forEach(h => { holdeplasserIReise.push(h) });
        if (holdeplasser.length > 0) {
            holdeplasserReverse = holdeplasser.slice().reverse();
            pris = "en rimelig pris ";
            let avreise = avgang.dato.substr(0, 10) + ", ";
            let avreiseTid = finnAvgangTid(avgang.dato.substr(11, 8), forsteStoppTid);
            let reisetid = regnReisetid(forsteStoppTid, sisteStoppTid, motsattreise);
            let ankomst = finnAnkomst(avreiseTid, reisetid);
            ut += "<tr>" +
                "<td>" + avreise + avreiseTid + "</td>" +
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
            ut += "<a href='#' data-toggle='tooltip' title='" + visHoldeplasser + "'><br>" + (lengde-1) + " stopp <br></a>"
            ut += ((retur) ? holdeplasserReverse[lengde].sted : holdeplasser[lengde].sted);
            ut += "</td>" +
                '<td><div class="avgCheckBox"><label>' +
                '<input class="reisevalg" type="checkbox" hidden onChange="reisevalg($(this))"/><span>Velg reise</span>' +
                '</label></div></td>';
        }
        
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
    console.log(valgtRad.index());

    var i = 1;
    for (i; i < $(`#${table} tr`).length; i++) {
        if (i != valgtRad.index()) {
            $(`#${table} tr`).eq(i).find('input').prop('checked', false);
        }
    }

    if (table == "avreiser") {
        turJson = {
            rute: avreiser[valgtRad.index() - 1].rute.navn,
            pris: 50,
            dato: avreiser[valgtRad.index() - 1].dato
        };
    }
    if (table == "returAvreiser") {
        returJson = {
            rute: avreiser[valgtRad.index() - 1].rute.navn,
            pris: 50,
            dato: avreiser[valgtRad.index() - 1].dato
        };
    }

    
};
