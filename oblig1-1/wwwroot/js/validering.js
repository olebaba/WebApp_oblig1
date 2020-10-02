// metoder som validerer hver verdi for hver tabell i databasen
function validerNavn(navn) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2-20}$/;
    const ok = regexp.test(navn);
    if (!ok) {
        $("#feilNavn").html("Navnet må bestå av 2 til 30 bokstaver");
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

function validerHoldeplassFra(fra) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,20}$/;
    const ok = regexp.test(fra);
    if (!ok) {
        $("#feilHoldeplassFra").html("Holdeplassen må bestå av 2 til 20 bokstaver");
        return false;
    }
    else {
        $("#feilHoldeplassFra").html("");
        return true; 
    }
}

function validerHoldeplassTil(til) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,20}$/;
    const ok = regexp.test(til);
    if (!ok) {
        $("#feilHoldeplassTil").html("Holdeplassen må bestå av 2 til 20 bokstaver");
        return false;
    }
    else {
        $("#feilHoldeplassTil").html("");
        return true;
    }
}

function validerKortnavn(navn) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2-20}$/;
    const ok = regexp.test(navn);
    if (!ok) {
        $("#feilKortnavn").html("Navnet må bestå av 2 til 30 bokstaver");
        return false;
    }
    else {
        $("#feilKortnavn").html("");
        return true;
    }
}

function validerKortnummer(kortnummer) {
    const regexp = /^[0-9]{16}$/;
    const ok = regexp.test(kortnummer);
    if (!ok) {
        $("#feilKortnummer").html("Kortnummeret må bestå av 16 tall");
        return false;
    }
    else {
        $("#feilKortnummer").html("");
        return true;
    }
}

function validerUtlop(utlop) {
    const regexp = /^[0-9]{6}$/;
    const ok = regexp.test(utlop);
    if (!ok) {
        $("#feilUtlop").html("Utløpsdato må bestå av 6 tall");
        return false;
    }
    else {
        $("#feilUtlop").html("");
        return true;
    }
}

function validerCVC(cvc) {
    const regexp = /^[0-9]{3}$/;
    const ok = regexp.test(cvc);
    if (!ok) {
        $("#feilCVC").html("CVC må bestå av 3 tall");
        return false;
    }
    else {
        $("#feilCVC").html("");
        return true;
    }
}

function validerVipps(mobilnummer) {
    const regexp = /^[0-9]{8}$/;
    const ok = regexp.test(mobilnummer);
    if (!ok) {
        $("#feilVipps").html("Mobilnummer må bestå av 8 tall");
        return false;
    }
    else {
        $("#feilVipps").html("");
        return true;
    }
}