$(function () {
    var url = "Bestilling/HentEn?" + 0;
    $.get(url, function (bestilling) {
        console.log(bestilling);
        //formaterBestilling(bestilling);
    });
});

function formaterBestilling(bestilling) {
    var rute = bestilling.rute;
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Dato</th><th>Holdeplass1</th><th>Holdeplass2</th><th>Pris</th>" +
        "</tr><tr>" +
        "<td>" + rute.dato + "</td>" +
        "<td>" + rute.holdeplasser[0].sted + "</td>" +
        "<td>" + rute.holdeplasser[1].sted + "</td>" +
        "<td>" + bestilling.pris + "</td>" +
        "</tr>" +
        "</table>";
    $("#ruter").html(ut);
}

function hentEnBestilling() {
    var url = "Bestilling/HentEn?" + 0;
    var ut = $.get(url);
    console.log(ut)
}