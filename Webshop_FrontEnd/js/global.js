//ask for location information
navigator.geolocation.getCurrentPosition(console.log,console.log);

//open the home page on click op logo
let blLogo = document.querySelector('.logo');

blLogo.onclick = function(){
    window.location.pathname = '/index.html';
}


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


//toggle class active on bar
let burger = document.querySelector('.burger');
if(document.body.contains(burger))
{
    burger.onclick = function(){
        Array.from(this.children).forEach((bar)=>{bar.classList.toggle('active')});
    }
}

window.addEventListener('click',function(e){
    console.log(e.target.classList.contains('burger'));
    console.log(e.target.tagName);
})
//check of de user is ongelogd
let loginInfo = sessionStorage.getItem('loginInfo');


window.addEventListener('load',function(){
    if(loginInfo != null){
        let Info = JSON.parse(loginInfo);
        console.log(Info);
        // add active class on user icon
        document.querySelector('.upper-bar ul li#login').classList.add('active');
    }
})

//clcik event on acount
let acount = document.querySelector('#acount');
// disible a click event
acount.firstElementChild.addEventListener('click',function(evenet){
    evenet.preventDefault();
})
// click event on acount
acount.onclick = function() {
    
    if(loginInfo != null){
        location.pathname = "/acount/acount.html";
    }
    else{
        location.pathname = "/login/login.html";
    }
}

//afmelden click
let afmelden = document.querySelector('#afmelden');

afmelden.onclick = function(){
    //clear data
    sessionStorage.removeItem('loginInfo');
    //remove active class
    document.querySelector('.upper-bar ul li#login').classList.remove('active');
    location.reload();
}
//====
// Copy TelNr to clipboard
let copyTel = document.querySelector('.fa-copy');
copyTel.onclick = function(){
    let copyText = this.previousSibling.textContent;
    let textArea = document.createElement('input');
    textArea.value = copyText;
    document.body.appendChild(textArea);
    textArea.select();
    textArea.setSelectionRange(0, 99999); 
    document.execCommand("copy");
    alert("Copied to clipboard: " + textArea.value);
    textArea.remove();
}



//check of element exist op pagina

function exist(element){
    if(document.body.contains(element))
    {
        return true;
    }
    else{
        return false;
    }
}



//handel seach input via veld
let seachInput = document.querySelector('.searchInput');
seachInput.addEventListener('keyup',function()
{
    if(this.value != null && this.value != "")
    {
        postSearchRequest(this.value);
    }
    else{
        let myOldUl = document.querySelector('.search-ul');
        if(document.body.contains(myOldUl))
        {
            myOldUl.remove();
        }
    }
})


function logError(error) {
    console.log('Looks like there was a problem:', error);
}

function validateResponse(response) {
    if (!response.ok) {
        throw Error(response.statusText);
    }
    return response;
}

function readResponseAsJSON(response) {
    return response.json();
}

function readResponseAsText(response) {
    return response.text();
}

function postSearchRequest(searchVeld) {
    const messageHeaders = new Headers({
        'Content-Type': 'application/json'
    })
    let getSearch = {SeatchVeld : searchVeld}

    fetch('https://localhost:44351/Search', {
        method: 'POST',
        body: JSON.stringify(getSearch),
        headers: messageHeaders
    })
    .then(validateResponse)
    .then(readResponseAsJSON)
    .then(ResultSearch)
    .catch(logError);
}

function ResultSearch(result){
    let myOldUl = document.querySelector('.search-ul');
    if(document.body.contains(myOldUl))
    {
        myOldUl.remove();
    }
    let ul = document.createElement('ul');
    ul.className = "search-ul";
    result.forEach((search)=>{
        console.log(result)
        let li = document.createElement('li');
        li.textContent = search.producttype;
        li.addEventListener('click',function(){
            location.href = location.protocol + "//" + location.host + "/" + "prodetails/prodetails.html" + "?" + search.Productnummer + "#" + search.Categorie;
        });
        ul.appendChild(li);
    })
    document.body.children[1].appendChild(ul);
}
