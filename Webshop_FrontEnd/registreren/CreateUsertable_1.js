

window.onload = function () {
    document.getElementById('submitButton').addEventListener('click', function () {


        let voornaam, naam, emailadres, telefoon, straatnaam, huisnummer, postcode, plaats, land, wachtwoord , bevestig;

        voornaam = document.getElementById('voornaam').value;
        naam = document.getElementById('achternaam').value;
        emailadres = document.getElementById('emailadres').value;
        telefoon = document.getElementById('telefoon').value;
        straatnaam = document.getElementById('straatnaam').value;
        huisnummer = document.getElementById('huisnummer').value;
        postcode = document.getElementById('postcode').value;
        plaats = document.getElementById('plaats').value;
        land = document.getElementById('land').value;
        wachtwoord = document.getElementById('wachtwoord').value;
        bevestig = document.getElementById('bevestig').value;


        if (wachtwoord === bevestig && checkInputs()) {
            let formUserdata_1 = new FormData();
            formUserdata_1.append('Voornaam', voornaam);
            formUserdata_1.append('Naam', naam);
            formUserdata_1.append('Emailadres', emailadres);
            formUserdata_1.append('Telefoon', telefoon);
            formUserdata_1.append('Straatnaam', straatnaam);
            formUserdata_1.append('Huisnummer', huisnummer);
            formUserdata_1.append('Postcode', postcode);
            formUserdata_1.append('Plaats', plaats);
            formUserdata_1.append('Land', land);
            formUserdata_1.append('Wachtwoord', wachtwoord);

            postFormData(formUserdata_1);
        } 
        else if(wachtwoord != bevestig){
            alert('wachtwoorden komen niet overeen')
        }
    })
}


//44340*/

function postFormData(formUserdata) {
    fetch("https://localhost:44351/Userdata",
        {
            body: formUserdata,
            method: "post"
        }).then(validateResponse)
        .then(response => response.json())
        .then(after);
}

function after(response){
    if(response[0] == undefined)
    {
        alert('alerdy exit');
        document.querySelector('input[type="email"]').style.border = "1px solid #F00";
    }
    else{
        alert('ok');
        sessionStorage.setItem('loginInfo',JSON.stringify(response[0]));
        location.pathname = "/index.html";
    }
}

function validateResponse(response) {
    if (!response.ok) {
        throw Error(response.statusText);
    }
    return response;
}



//+==================================================
function IsEmail(emailVeld)
{
    if(emailVeld.value.includes('@') && emailVeld.value.includes('.'))
    {
        return true;
    }
    else{
        return false;
    }
}

function IsWachtwoord(wachtwoordVeld)
{
    if(wachtwoordVeld.value.length >= 2)
    {
        return true;
    }
    else{
        return false;
    }
}

function CanEnableButton(button,emailVeld,wachtwoordveld)
{
    if(emailVeld != null && wachtwoordveld != null && emailVeld != "" && wachtwoordveld != "")
    {
        if(IsEmail(emailVeld) && IsWachtwoord(wachtwoordveld))
        {
            button.classList.remove('no-clicking');
        }
        else{
            button.classList.add('no-clicking');
        }
        return true;
    }
    else{
        button.classList.add('no-clicking');
        return false;
    }
}


function checkInputs(){
    let isLeeg = false;
    let allData = document.querySelectorAll('.invoerlang input');
    allData.forEach((data) =>{
        if(data.value == null || data.value == "" || data.value.length < 2)
        {
            isLeeg = true;
            data.style.border = "1px solid #F00";
        }
        data.addEventListener('focus',function(){
            this.style.border = "1px solid transparent";
        })
    })

    if(isLeeg == false)
    {
        return true;
    }
    else{
        return false;
    }
}