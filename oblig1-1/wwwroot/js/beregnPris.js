

// må hente inn antall fra forside og tid fra avganger
function beregnpris() {
    const antVoksen = $('#textinput').val();
    const antBarn = $('#barninput').val();

    // må endres på når knapp er laget 
    const antSBarn = $('#textinput').val();

    const antStudent = $('#studentinput').val(); 
    const antHonnor = $('#honinput').val();

    // må endres på når knapp er laget 
    const antVern = $('#verninput').val();
    // ---- || -------
    const antLedsager = $('#ledinput').val(); 

    // må regne ut tid for strekningen 
    const tid = 128;
    
    // startpris 
    const prisPer;
    const prisPerMin = 1.2;

    // grunnpris for billetter er 50 dersom man reiser under 20 min 
    // tok ca 1.2 kr per min etter 20 min
    if (tid < 20) {
        const nyTid = parseDouble(tid) - 20;
        prisPer = 50 + nyTid * parseDouble(prisPerMin); 
    }
    else {
        prisPer = 50; 
    }

    let prisVoksen = 0; 
    let prisBarn = 0; 
    let prisSBarn = 0; 
    let prisStudent = 0; 
    let prisHonnor = 0; 
    let prisVern = 0; 
    let prisLedsager = 0; 

    let ut = "<br/>";

    if (antVoksen != 0) {
        // pris skal være i heltall
        prisVoksen = parseInt(prisPer * antVoksen);
        ut += "Voksen: " + prisVoksen + " kr<br/>";
    }
    if (antBarn != 0) {
        prisBarn = parseInt(prisPer * antBarn / 2);
        ut += "Barn (6-17 år): " + prisBarn + " kr<br/>";
    }
    if (antSBarn != 0) {
        // barn under 6 år gratis
        ut += "Spedbarn (0-5 år): " + #prisSBarn + " kr<br/>";
    }
    if (antStudent != 0) {
        prisStudent = parseInt(prisPer * antStudent / 1.5);
        ut += "Student: " + prisStudent + " kr<br/>";
    }
    if (antHonnor != 0) {
        prisHonnor = parseInt(prisPer * antHonnor / 2);
        ut += "Honnør: " + prisHonnor + " kr<br/>";
    }
    if (antVern != 0) {
        prisVern = parseInt(prisPer * antVern / 2);
        ut += "Vernepliktig: " + prisVern + " kr<br/>";
    }
    if (antLedsager != 0) {
        // ledsager gratis
        ut += "Ledsager: " + prisLedsager + " kr<br/>";
    }

    const total = prisVoksen + prisBarn + prisSBarn + prisStudent + prisHonnor + prisVern + prisLedsager; 
    ut += "Totalpris: " + total + " kr<br/>";

    $("#resultat").val(ut);
}