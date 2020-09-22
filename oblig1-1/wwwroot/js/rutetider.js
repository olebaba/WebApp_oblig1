$(function () {
    $.get("Bestilling/VisAlleRuter", function (ruter) {
        formaterRuter(ruter);
    });
});

function formaterRuter(ruter) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Sted</th><th>Dato</th>" +
        "</tr>";
    for (let rute of ruter) {
        ut += "<tr>" +
            "<td>" + rute.sted + "</td>" +
            "<td>" + rute.dato + "</td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#ruter").html(ut);
}