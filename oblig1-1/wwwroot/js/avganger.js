$(function () { //Startfunksjon kaller på visAvganger()
    visAvganger();
});

function visAvganger() {    //Denne henter alle relevante avganger og sender dem til å bli skrevet ut
    //Denne må fullføres
    //Hent fra og til fra db?
    hentRuteFraDB();

    //Hent billetter fra db?
    settBilletter();
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

    console.log("Reise fra " + fra.sted + " til " + til.sted);
    $.post("Bestilling/FinnEnRute", reise, function (rute) {
        console.log(rute);
        //console.log(rute.holdeplasser);
        var fra = rute.holdeplasser[0];
        var til = rute.holdeplasser[rute.holdeplasser.length - 1];
        settTittel(fra.sted, til.sted);
        settDato(rute.datoer);
        var holdeplasser = rute.holdeplasser;
        avreiser = [
            { start: fra.avgangstider, totaltid: rute.totalTid, pris: (rute.holdeplasser.length * 66.6).toFixed(2), holdeplasser}
        ]
        console.log(avreiser);
        skrivUt(avreiser);
    });
}

function settTittel(fra, til) {
    let tittel = fra + " -> " + til;
    $("#fraOgTil").html(tittel);
}

function hentDato() {
    let url_dato = new Date(getUrlParam('goDate'));
    var options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
    var formatert_dato = url_dato.toLocaleDateString("no-NO", options);
    var reise_dato = formatert_dato.charAt(0).toUpperCase() + formatert_dato.slice(1);
    settDato(reise_dato);
}

function settDato(dato) {
    $("#datoTittel").html(dato);
}

function settBilletter() {
    let bill = "1 Voksen, 2 Barn";
    $("#billetter").html(bill);
}

function gaTilbake() {
    location.href = "forside.html";
} 

function skrivUt(avreiser) {    //Funksjon som skriver ut avganger
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Avreise</th><th>Reisetid</th><th>Pris</th><th>Holdeplasser</th>" +
        "<th>Valg</th>" +
        "</tr>";
    for (let avreise of avreiser) {
        ut += "<tr>" +
            "<td>" + avreise.start + "</td>" +
            "<td>" + avreise.totaltid + "</td>" +
            "<td>" + avreise.pris + "</td>" +
            "<td>";
        for (h in avreise.holdeplasser) {
            ut += avreise.holdeplasser[h].sted + ", ";
        }
        ut += "</td>";
            
    }
    ut += "</tr></table>";
    $("#avreiser").html(ut);
}

