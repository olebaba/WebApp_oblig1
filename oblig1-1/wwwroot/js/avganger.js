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
        v1 = JSON.parse(getUrlParam('from'));
        v2 = JSON.parse(getUrlParam('to'));
    } else {
        v1 = JSON.parse(getUrlParam('to'));
        v2 = JSON.parse(getUrlParam("from"));
    }
    settTittel(v1.sted, v2.sted);
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



function hentRuteFraDB() { //henter rute fra databasen og formaterer + viser tider i en tabell
    var date = new Date(getUrlParam('goDate'));
    var day = date.getDate();       // yields date
    var month = date.getMonth() + 1;    // yields month (add one as '.getMonth()' is zero indexed)
    var year = date.getFullYear();  // yields year

    // After this construct a string with the above results as below
    var time = day + "/" + month + "/" + year + " 00:00:00";
    var onsketReise = {
        holdeplasserOgDato: [
            getUrlParam('from'),
            getUrlParam('to'),
            time
        ]
    };
    var retur = (getUrlParam('tur') == 'tovei') ? true : false; 

    $.post("Bestilling/FinnEnRuteAvgang", onsketReise, function (ruteavganger) {
        if (ruteavganger == null) {
            visFeilmelding("Ingen ruteravganger for denne reisen kunne bli funnet.");
        } else {
            console.log(ruteavganger);
            visAvreiser(ruteavganger, sjekkRetur); 
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
    var url = "betaling.html?tur=" + JSON.stringify(turJson) + "&retur=" + ((returJson != undefined) ? JSON.stringify(returJson) : null) +
        "&pris=" + ((returJson != undefined) ? (Number(turJson.pris) + Number(returJson.pris)).toFixed(2) : JSON.stringify(turJson.pris).toFixed(2)) +
        "&goDate=" + hentetRute.goDate + "&backDate=" + hentetRute.backDate;

    if ($(".avgCheckBox").length == 4 && $(".avgCheckBox input:checkbox:checked").length > 1) {
        location.href = url;
    } else if ($(".avgCheckBox").length == 2) {
        location.href = url;
    }
        
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

function finnAnkomst(start, stopp) { //Setter ankomsttid. Noe av kode tatt fra nett.
    /*let reise_1 = avreise.substr(0, 2);
    let reise_2 = avreise.substr(3, 2);
    let time = parseInt(timer);
    let min = parseInt(minutter);
    let reiseDato = new Date(2020, 01, 01, reise_1, reise_2, 0);
    reiseDato.setHours(reiseDato.getHours() + time);
    reiseDato.setMinutes(reiseDato.getMinutes() + min);
    reiseDato = reiseDato.toString();
    return reiseDato.substr(16, 2) + ":" + reiseDato.substr(19, 2);*/

    return start + stopp;
}

function visAvreiser(ruteavganger, retur) {    //Funksjon som skriver ut avganger
    var uttur = setAvreise(ruteavganger, false);
    $("#avreiser").html(uttur);
    
    if (retur) {
        var utretur = setAvreise(ruteavganger, true);
        $("#tilbake").after("<br/><br/><h2>Retur:</h2>" + utretur);
    }    
}

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
        var stoppTid;
        var alleRutestopp = avgang.rute.ruteStopp;
        alleRutestopp.sort(function (a, b) {
            if (a.stoppTid > b.stoppTid) return 1;
            if (a.stoppTid < b.stoppTid) return -1;
            return 0;
        });
        console.log(alleRutestopp);
        for (i in alleRutestopp) { //filtrer til riktige holdeplasser for reise

            console.log(forsteStopp.id);
            if (alleRutestopp[i].holdeplass.id == forsteStopp.id) {
                console.log("hei");

            }
            if (alleRutestopp[i].holdeplass.id == sisteRutestopp.id) break;

            holdeplasser.push(alleRutestopp[i].holdeplass);

            if (alleRutestopp[i].holdeplass.id == sisteRutestopp.id) {
                stoppTid = alleRutestopp[i].stoppTid;
            }
            
        }
        if (holdeplasser.length > 0) {
            console.log(holdeplasser);
            holdeplasserReverse = holdeplasser.slice().reverse();
            pris = "en rimelig pris ";
            let avreiseTid = avgang.dato;
            let ankomst = finnAnkomst(avgang.dato, stoppTid);
            let reisetid = ankomst;
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
