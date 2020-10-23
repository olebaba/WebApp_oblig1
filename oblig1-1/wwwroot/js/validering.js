// metoder som validerer hver inputverdi for hver tabell i databasen
function validerNavn(navn) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,30}$/;
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
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,30}$/;
    const ok = regexp.test(fra);
    if (!ok) {
        $("#feilHoldeplassFra").html("Holdeplassen må bestå av 2 til 30 bokstaver");
        validerKnapp();
        return false;
    }
    else {
        $("#feilHoldeplassFra").html("");
        validerKnapp();
        return true; 
    }
}

function validerHoldeplassTil(til) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,30}$/;
    const ok = regexp.test(til);
    if (!ok) {
        $("#feilHoldeplassTil").html("Holdeplassen må bestå av 2 til 30 bokstaver");
        validerKnapp();
        return false;
    }
    else {
        $("#feilHoldeplassTil").html("");
        validerKnapp();
        return true;
    }
}

function validerKnapp() {
    let fraDato = $("input[name='turDato']").val();
    let tilDato = $("input[name='retDato']").val();
    let radio = $("input[name='tur/retur']:checked").val();
    var antall;
    antall = $('#textinput').val() * 1 +
        $('#honinput').val() * 1 +
        $('#barninput').val() * 1 +
        $('#studentinput').val() * 1 +
        $('#storbarninput').val() * 1 +
        $('#vernepliktiginput').val() * 1 +
        $('#ledsagerinput').val() * 1;
    if ($("#fra").val() && $("#til").val() && fraDato != "" && radio == "tur" && antall > 0) {
        $("#avgangerknapp").prop("disabled", false);
    } else if ($("#fra").val() && $("#til").val() && validerDato() && tilDato != "" && antall > 0) {
        $("#avgangerknapp").prop("disabled", false);
    } else {
        $("#avgangerknapp").prop("disabled", true);
    }

}

function validerDato() {
    let fraDato = new Date($("input[name='turDato']").val());
    let tilDato = new Date($("input[name='retDato']").val());
    let radio = $("input[name='tur/retur']:checked").val();
    if (radio == "retur") {
        if (fraDato.getTime() <= tilDato.getTime()) {
            $("#feilDato").html("");
            return true;
        } else {
            console.log("DAto feiul2)");
            $("#feilDato").html("Feil i valg av dato");
            return false;
        }
    }
}

function validerKortnavn(navn) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,30}$/;
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

function validerBrukernavn(brukernavn) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,20}$/;
    const ok = regexp.test(brukernavn);
    if (!ok) {
        $("#feilBrukernavn").html("Brukernavnet må bestå av 2 til 20 bokstaver");
        return false;
    }
    else {
        $("#feilBrukernavn").html("");
        return true;
    }
}

function validerPassord(passord) {
    const regexp = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/;
    const ok = regexp.test(passord);
    if (!ok) {
        $("#feilPassord").html("Passordet må bestå av minst 8 tegn, minst en bokstav og ett tall");
        return false;
    }
    else {
        $("#feilPassord").html("");
        return true;
    }
}

function validerRekkefolge(rekkefolge) {
    const regexp = /^[0-9]{1,3}$/;
    const ok = regexp.test(rekkefolge);
    if (!ok) {
        $("#feilRNr").html("RekkefølgeNr må bestå av 1 til 3 sammenhengende tall");
        return false;
    }
    else {
        $("#feilRNr").html("");
        return true;
    }
}

function validerTid(stoppTid) {
    const regexp = /^([0-1]?\d|2[0-3]):([0-5]?\d):([0-5]?\d)$/;
    const ok = regexp.test(stoppTid);
    if (!ok) {
        $("#feilTid").html("StoppTid må være på formen HH:MM:SS");
        return false;
    }
    else {
        $("#feilTid").html("");
        return true; 
    }
}

function validerSone(sone) {
    const regexp = /^[0-9]{1,3}$/;
    const ok = regexp.test(sone);
    if (!ok) {
        $("#feilSone").html("Sone må bestå av 1 til 3 sammenhengende tall");
        return false;
    }
    else {
        $("#feilSone").html("");
        return true;
    }
}

function validerSted(sted) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,30}$/;
    const ok = regexp.test(sted);
    if (!ok) {
        $("#feilSted").html("Holdeplassen må bestå av 2 til 30 bokstaver");
        return false;
    }
    else {
        $("#feilHoldeplassFra").html("");
        return true;
    }
}

function validerPris1(sone1) {
    const regexp = /^[0-9]{1,4}$/;
    const ok = regexp.test(sone1);
    if (!ok) {
        $("#feilSone1").html("Pris for sone 1 må bestå av et tall med 1 til 4 siffer");
        return false;
    }
    else {
        $("#feilSone1").html("");
        return true; 
    }
}
function validerPris2(sone2) {
    const regexp = /^[0-9]{1,4}$/;
    const ok = regexp.test(sone2);
    if (!ok) {
        $("#feilSone2").html("Pris for sone 2 må bestå av et tall med 1 til 4 siffer");
        return false;
    }
    else {
        $("#feilSone2").html("");
        return true;
    }
}
function validerPris3(sone3) {
    const regexp = /^[0-9]{1,4}$/;
    const ok = regexp.test(sone3);
    if (!ok) {
        $("#feilSone3").html("Pris for sone 3 må bestå av et tall med 1 til 4 siffer");
        return false;
    }
    else {
        $("#feilSone3").html("");
        return true;
    }
}
function validerPris4(sone4) {
    const regexp = /^[0-9]{1,4}$/;
    const ok = regexp.test(sone4);
    if (!ok) {
        $("#feilSone4").html("Pris for sone 4 må bestå av et tall med 1 til 4 siffer");
        return false;
    }
    else {
        $("#feilSone4").html("");
        return true;
    }
}
