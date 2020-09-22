// metoder som validerer hver verdi for hver tabell i databasen
function validerNavn(navn) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2-20}$/;
    const ok = regexp.test(navn);
    if (!ok) {
        $("#feilNavn").html("Navnet må bestå av 2 til 20 bokstaver");
        return false;
    }
    else {
        $("#feilNavn").html("");
        return true;
    }
}

function validerMobilnummer(mobilnummer) {
    const regexp = /^[0-9]{8}$/;
    const ok = regexp.test(mobilnummer);
    if (!ok) {
        $("#feilMobilnummer").html("Mobilnummeret må bestå av 8 tall");
        return false;
    }
    else {
        $("#feilMobilnummer").html("");
        return true; 
    }
}

function validerHoldeplass(holdeplass) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2-20}$/;
    const ok = regexp.test(holdeplass);
    if (!ok) {
        $("#feilHoldeplass").html("Holdeplassen må bestå av 2 til 20 bokstaver");
        return false;
    }
    else {
        $("#feilHoldeplass").html("");
        return true; 
    }
}