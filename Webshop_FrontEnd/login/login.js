//+==============================================================
/*
    een functie die de placeholder weg doet on focus
    op all de input binnen de pagina
*/

let allInputVelden = document.querySelectorAll('input');

allInputVelden.forEach((input)=>{

    let textPlaceholder = "";

    input.addEventListener('focus',function(){
        textPlaceholder = this.getAttribute('placeholder');
        this.setAttribute('placeholder','');
    });
    
    input.addEventListener('blur',function(){
        this.setAttribute('placeholder',textPlaceholder);
    })
});

//=========================================


let loginSwitcher = document.querySelector('.login-form .switcher'),
    regSwitcher = document.querySelector('.register-form .switcher'),
    emailLogin = document.querySelector('.login-form  .email'),
    wachtwoordLogin = document.querySelector('.login-form  .password input'),
    emailR = document.querySelector('.register-form .email'), 
    wachtwoordR = document.querySelector('.register-form .password input');;


loginSwitcher.onclick = function(){
    this.parentElement.classList.remove('active');
    regSwitcher.parentElement.classList.add('active');
}
regSwitcher.onclick = function(){
    this.parentElement.classList.remove('active');
    loginSwitcher.parentElement.classList.add('active');
}

//password zien controleren
let logPaswordInput = document.querySelector('.login-form .password input'),
    logPaswordControl = document.querySelector('.login-form  .password i');

logPaswordControl.addEventListener('click',function(){
    hideShowPassword(logPaswordInput,this);
});

let regPaswordInput = document.querySelector('.register-form .password input'),
    regPaswordControl = document.querySelector('.register-form  .password i');

regPaswordControl.addEventListener('click',function(){
    hideShowPassword(regPaswordInput,this);
});


function hideShowPassword(input,icon){
    if(icon.classList == 'fas fa-low-vision'){
        icon.classList = 'fas fa-eye';
        icon.style.color = 'var(--main-color)';
        input.setAttribute('type','text');
    }
    else{
        icon.classList = 'fas fa-low-vision';
        input.setAttribute('type','password');
        icon.style.color = '#777';
    }
}

//go home on click on logo
document.querySelector('.logo').onclick = function(){
    location.pathname = '/index.html';
}


//aanmelden =================================================================================================

emailLogin.addEventListener('blur',function(){
    if(!IsEmail(this))
    {
        this.style.border = "1px solid #F00";
    }
    else{
        this.style.border = "1px solid transparent";
    }
})
emailLogin.addEventListener('input',function(){
    CanEnableButton(document.querySelector('.login-form .submit'),this,wachtwoordLogin)
})
wachtwoordLogin.addEventListener('blur',function(){
    if(!IsWachtwoord(this)){
        this.style.border = "1px solid #F00";
    }
    else{
        this.style.border = "1px solid transparent";
    }
})

wachtwoordLogin.addEventListener('input',function(){
    CanEnableButton(document.querySelector('.login-form .submit'),emailLogin,this)
})

// 
window.onload = function (){
    //aanmelden
    document.querySelector('.login-form .submit').addEventListener('click',function (){
        let formUserdata_2 = new FormData();
        formUserdata_2.append('Emailadres', emailLogin.value);
        formUserdata_2.append('Wachtwoord',wachtwoordLogin.value);
        psotLoginForm(formUserdata_2);
    })
    //registreren
    document.querySelector('.register-form .submit').addEventListener('click',function (){
        let formUserdata_1 = new FormData();
        formUserdata_1.append('Emailadres', emailR.value);
        formUserdata_1.append('Wachtwoord',wachtwoordR.value);
        postFormData(formUserdata_1);
    })

}
let counter = 0;
function psotLoginForm(formUserdata){
    fetch("https://localhost:44351/login",
        {
            body: formUserdata,
            method: "post"
        }).then(validateResponse)
        .then(response => response.json())
        .then(result);
}

function validateResponse(response) {
        if (!response.ok) {
            throw Error(response.statusText);
        }
        return response;
    }

function result(response){
    if(response.length > 0)
    {
        sessionStorage.setItem(`winkelmandje${response[0].Klantnummer}`,"");
        sessionStorage.setItem('loginInfo',JSON.stringify(response[0]));
        if(location.search.split('|')[0] == "?prodetails")
        {
            location.href = location.protocol + "//" + location.host + "/prodetails/prodetails.html" + "?" + `${location.search.split('|')[1]}` + location.hash;
        }
        else{
            location.href = location.protocol + "//" + location.host  + "/index.html";
        }
    }
    else{
        // alert("Email of wachtwoord is onjuist");
        emailLogin.style.border = "1px solid red";
        wachtwoordLogin.style.border = "1px solid red"; 
    }
}


//registratie

emailR.addEventListener('blur',function(){
    if(!IsEmail(this))
    {
        this.style.border = "1px solid #F00";
    }
    else{
        this.style.border = "1px solid transparent";
    }
})
emailR.addEventListener('input',function(){
    CanEnableButton(document.querySelector('.register-form .submit'),this,wachtwoordR)
})
wachtwoordR.addEventListener('blur',function(){
    if(!IsWachtwoord(this)){
        this.style.border = "1px solid #F00";
    }
    else{
        this.style.border = "1px solid transparent";
    }
})
wachtwoordR.addEventListener('input',function(){
    CanEnableButton(document.querySelector('.register-form .submit'),emailR,this)
})

function postFormData(formUserdata_1){
    fetch("https://localhost:44351/Registratie",
        {
            body: formUserdata_1,
            method: "post"
        }).then(validateResponse)
        .then(response => response.json())
        .then(after);
}
function after(response){
    console.log(response);
    console.log(response[0]);
    if(response[0] == undefined)
    {
        alert('alerdy exit');
        emailR.style.border = "1px solid #F00";
    }
    else{
        alert('ok');
        sessionStorage.setItem('loginInfo',JSON.stringify(response[0]));
        location.pathname = "/index.html";
    }
}

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

//enter click
window.addEventListener('keypress', function (e) {
    if (e.key === 'Enter') {
        if(document.querySelector('.content > div.active').classList.contains('login-form'))
        {
            if(IsEmail(emailLogin) && wachtwoordLogin != null)
            {
                let formUserdata_2 = new FormData();
                formUserdata_2.append('Emailadres', emailLogin.value);
                formUserdata_2.append('Wachtwoord',wachtwoordLogin.value);
                psotLoginForm(formUserdata_2);
                console.log('login')
            }
        }
        else{
            if(IsEmail(emailR) && IsWachtwoord(wachtwoordR)){
                console.log("register");
                let formUserdata_1 = new FormData();
                formUserdata_1.append('Emailadres', emailR.value);
                formUserdata_1.append('Wachtwoord',wachtwoordR.value);
                postFormData(formUserdata_1);
            }
        }
    }
});
