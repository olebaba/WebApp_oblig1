$(function () {
    //hentHoldeplasser();
    //hentRuter();
    hentRS();
});

function hentRS() {
    $.get("Bestilling/HentRuteStopp", function (rutestopp) {
        formaterRS(rutestopp);
    })
    .fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'innlogging.html';
        }
        else {
            $("#feil").html("Feil på server - prøv igjen senere");
        }
    });
}

function formaterRS(rutestopp) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>RekkefølgeNr</th><th>StoppTid</th><th>Sted</th><th>Sone</th><th></th><th></th>" +
        "</tr>";
    for (let rs of rutestopp) {
        ut += "<tr>" +
            "<td>" + rs.rekkefølgeNr + "</td>" +
            "<td>" + rs.stopptid + "</td>" +
            "<td>" + rs.holdeplass.sted + "</td>" +
            "<td>" + rs.holdeplass.sone + "</td>" +
            "<td> <a class='btn btn-primary' href='endre.html?id=" + rs.id + "'>Endre</a></td>" +
            "<td> <button class='btn btn-danger' onclick='slettRS(" + rs.id + ")'>Slett</button></td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#rutestopp").html(ut);
}

function slettRS(id) {
    const url = "Bestilling/SlettRS?id=" + id;

    $.get(url, function () {
        window.location.href = 'admin.html';
    })
    .fail(function (feil) {
        if (feil.status == 401) { // sjekker om vi er pålogget
            window.location.href = 'innlogging.html';
        }
        else {
            $("#feil").html("Feil på server - prøv igjen senere");
        }
    });
}

/*function hentHoldeplasser() {
    $.get("Bestilling/HentHoldeplasser", function (holdeplasser) {
        formaterHoldeplasser(holdeplasser);
    })
    .fail(function (feil) {
        $("#feilH").html("Feil på server - prøv igjen senere");
    });
}

function formaterHoldeplasser(holdeplasser) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Sted</th><th>Sone</th><th></th><th></th>" +
        "</tr>";
    for (let holdeplass of holdeplasser) {
        ut += "<tr>" +
            "<td>" + holdeplass.sted + "</td>" +
            "<td>" + holdeplass.sone + "</td>" +
            "<td> <a class='btn btn-primary' href='endreHold.html?id=" + holdeplass.id + "'>Endre</a></td>" +
            "<td> <button class='btn btn-danger' onclick='slettHold(" + holdeplass.id + ")'>Slett</button></td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#holdeplasser").html(ut);
}

function hentRuter() {
    $.get("Bestilling/VisAlleRuter", function (ruter) {
        formaterRuter(rute);
    })
    .fail(function (feil) {
        $("#feilR").html("Feil på server - prøv igjen senere");
    });
}

function formaterRuter(ruter) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Datoer</th><th>Holdeplasser</th><th>Totaltid</th><th></th><th></th>" +
        "</tr>";
    for (let rute of ruter) {
        ut += "<tr>" +
            "<td>" + rute.dato + "</td>" +
            "<td>" + rute.holdeplasser + "</td>" +
            "<td>" + rute.totaltid + "</td>" +
            "<td> <a class='btn btn-primary' href='endreRute.html?id=" + rute.id + "'>Endre</a></td>" +
            "<td> <button class='btn btn-danger' onclick='slettRute(" + rute.id + ")'>Slett</button></td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#holdeplasser").html(ut);
}*/
