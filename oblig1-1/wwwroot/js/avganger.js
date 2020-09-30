$(function () { //Startfunksjon kaller på visAvganger()
    visAvganger();
});

function newFunction() {
    //Dette er for å teste hvordan skrivUt() skal funke:
    var table = document.getElementById("myTable");
    var row = table.insertRow(1);
    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);
    var cell3 = row.insertCell(2);
    var cell4 = row.insertCell(3);
    var cell5 = row.insertCell(4);
    var cell6 = row.insertCell(5);
    cell1.innerHTML = "14:10";
    cell2.innerHTML = "18:30";
    cell3.innerHTML = "4t 20min";
    cell4.innerHTML = "468kr";
    cell5.innerHTML = '<input type="button" value="13 Stopp V" name="vis" class="btn btn - primary" />';
    cell6.innerHTML = '<input type="button" value="Velg" name="velg" class="btn btn - primary" />';
}

function visAvganger() {    //Denne henter alle relevante avganger og sender dem til å bli skrevet ut
    //Denne må fullføres
    //Hent fra og til fra db?
    hentRuteFraDB();

    //Hent billetter fra db?
    settBilletter();

    //Hent avreiser fra db?
    //skrivUt(avreiser);
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
        settTittel(rute.holdeplasser[0].sted, rute.holdeplasser[rute.holdeplasser.length-1].sted);
        settDato(rute.datoer);
    });
}

function settTittel(fra, til) {
    let tittel = fra + " -> " + til;
    $("#fraOgTil").html(tittel);
}

function settDato(dato) {
    $("#datoTittel").html(dato);
}

function settBilletter() {
    let bill = "1 Voksen, 2 Barn";
    $("#billetter").html(bill);
}

function skrivUt(avreiser) {    //Funksjon som skriver ut avganger
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Avreise</th><th>Reisetid</th><th>Pris</th><th>Holdeplasser</th>" +
        "<th>Knapp</th>" +
        "</tr>";
    for (let avreise of avreiser) {
        ut += "<tr>" +
            "<td>" + avreise.start + "</td>" +
            "<td>" + avreise.reisetid + "</td>" +
            "<td>" + avreise.pris + "</td>" +
            "<td>" + avreise.holdeplasser + "</td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#bestillinger").html(ut);
}

