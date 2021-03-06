﻿$(function () {
    hentHoldeplasser();
    hentRuteAvgang();
    hentRuter();
    hentRS();
    hentPriser();
});

function hentRuteAvgang() {
    $.get("Bestilling/VisAlleRuteAvganger", function (ruteavgang) {
        formaterRA(ruteavgang);
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

function formaterRA(ruteavgang) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Dato</th><th>Rute</th>" +
        "</tr>";
    for (let ra of ruteavgang) {
        ut += "<tr>" +
            "<td>" + ra.dato + "</td>" +
            "<td>" + ra.rute.navn + "</td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#ruteavgang").html(ut);
}

function hentRS() {
    $.get("Admin/HentRuteStopp", function (rutestopp) {
        formaterRS(rutestopp);
    })
    .fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'innlogging.html';
        }
        else {
            $("#feilRuteStopp").html("Feil på server - prøv igjen senere");
        }
    });
}

function formaterRS(rutestopp) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Rute</th><th>Sted</th><th>Tid etter ruteavgang</th><th></th><th></th>" +
        "</tr>";
    for (let rs of rutestopp) {
        ut += "<tr>" +
            "<td>" + rs.rute.navn + "</td>" +
            "<td>" + rs.holdeplass.sted + "</td>" +
            "<td>" + rs.stoppTid + "</td>" +
            "<td> <a class='btn btn-primary' href='endre.html?id=" + rs.id + "'>Endre</a></td>" +
            "<td> <button class='btn btn-danger' onclick='slettRS(" + rs.id + ")'>Slett</button></td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#rutestopp").html(ut);
}

function slettRS(id) {
    const url = "Admin/SlettRS?id=" + id;

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

function hentHoldeplasser() {
    $.get("Bestilling/HentAlleHoldeplasser", function (holdeplasser) {
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
            "<td> <button class='btn btn-danger' onclick='slettHoldeplass(" + holdeplass.id + ")'>Slett</button></td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#holdeplasser").html(ut);
}

function hentRuter() {
    $.get("Bestilling/AlleRuter", function (rute) {
        formaterRuter(rute);
    })
    .fail(function (feil) {
        $("#feilR").html("Feil på server - prøv igjen senere");
    });
}

function formaterRuter(ruter) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Navn</th>" +
        "</tr>";
    for (let rute of ruter) {
        ut += "<tr>" +
            "<td>" + rute.navn + "</td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#ruter").html(ut);
}


function hentPriser() {
    $.post("Bestilling/HentPriser", function (priser) {
        formaterPriser(priser);
    })
    .fail(function (feil) {
        $("#feilPris").html("Feil på server - prøv igjen senere");
    });
}

function formaterPriser(priser) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Prisklasse</th><th>Pris for 1 sone</th><th>Pris for 2 soner</th><th>Pris for 3 soner</th><th>Pris for 4 soner</th><th></th>" +
        "</tr>";
    for (let i = 0; i < priser.length; i++) {
        ut += "<tr>" +
            "<td>" + priser[i].prisklasse + "</td>" +
            "<td>" + priser[i].pris1Sone + "</td>" +
            "<td>" + priser[i].pris2Sone + "</td>" +
            "<td>" + priser[i].pris3Sone + "</td>" +
            "<td>" + priser[i].pris4Sone + "</td>" +
            "<td> <button class='btn btn-primary' onclick='endrePriser(" + priser[i].prisID + ")'>Oppdater priser</button></td>" +

            "</tr>";
    }
    ut += "</table>";
    $('#output').html(ut);
}


function endrePriser(objekt) {
    const sjekkPris1 = validerPris1($("#1sone").val());
    const sjekkPris2 = validerPris2($("#2sone").val());
    const sjekkPris3 = validerPris3($("#3sone").val());
    const sjekkPris4 = validerPris4($("#4sone").val());
    if (sjekkPris1 && sjekkPris2 && sjekkPris3 && sjekkPris4) {
        const priser = {
            prisID: objekt,
            pris1Sone: $("#1sone").val(),
            pris2Sone: $("#2sone").val(),
            pris3Sone: $("#3sone").val(),
            pris4Sone: $("#4sone").val(),
        };

        $.post("Admin/EndrePriser", priser, function () {
            window.location.href = 'admin.html';
            
        })
            .fail(function (feil) {
                if (feil.status === 401) {
                    window.location.href = 'innlogging.html';
                } else {
                    $("#feilPris").html("Feil på server - prøv igjen senere");
                }

            });
    }
    else {
        ("#feilPris").html("Feil i inputvalidering");
    }
}

function slettHoldeplass(id) {
    const url = "Admin/SlettHoldeplass?id=" + id;
    $.get("Admin/HentRuteStopp", function (rutestopp) {
        for (let i = 0; i < rutestopp.length; i++) {
            if (rutestopp[i].holdeplass.id === id) {
                const url2 = "Admin/SlettRS?id=" + rutestopp[i].id;
                $.post(url2, function () {
                    $.post(url, function () {
                        window.location.href = 'admin.html';
                    })
                    .fail(function (feil) {
                        if (feil.status === 401) {
                            window.location.href = 'innlogging.html';
                        } else {
                            $("#feil").html("Feil på server - prøv igjen senere");
                        }
                    });
                });
                break;
            } else {
            }
        }
        $.post(url, function () {
            window.location.href = 'admin.html';
        });
    });      
}