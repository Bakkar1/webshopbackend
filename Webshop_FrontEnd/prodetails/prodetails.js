let upperBar = document.querySelector('.upper-bar'),
    lowerBar = document.querySelector('.lower-bar'),
    container = document.querySelector('.slider-container'),
    details = document.querySelector('.details'),
    currentSlide = 1,
    //current stroage from local storage
    currentSlideStorage = window.localStorage.getItem(`currentSlideStorage${location.search.substring(1)}`),
    //previous and next buttons
    nextButton = document.querySelector('.next'),
    prevButton = document.querySelector('.prev'),
    //get number of slides
    slidesCount = 0,
    i = 1,
    sliderItems = "",
    gegevens = {};

window.onload = function(){
    postRequest();

    sliderContent = document.querySelector('.slider-content');
    
    let SeeButton = document.querySelector('.tech-sepcs button')
    SeeButton.addEventListener('click',function(){
        if(this.classList.contains('active'))
        {
            this.classList.remove('active');
            this.parentElement.classList.add('heightBeperkt');
            this.textContent = "see more";
        }
        else{
            this.classList.add('active');
            this.parentElement.classList.remove('heightBeperkt');
            this.textContent = "see less";
        }
    });
}

function createSlider(images){
    //create the main ul element
    let paginationElement = document.createElement('ul');
    paginationElement.classList.add('visible-xl');
    //set id on ul
    paginationElement.setAttribute('id','pagination-ul');
    images.shift();

    images.forEach((img)=>{
        let slide = document.createElement('div');
        slide.className = "slider-product";
        slide.setAttribute('data-img', "../"+ escape(img));
        let myImage = document.createElement('img');
        myImage.setAttribute('src',"../"+ escape(img));
        slide.appendChild(myImage);
        container.appendChild(slide);  
        //Create list items based on array lenght
        let paginationItem = document.createElement('li');
        paginationItem.setAttribute('data-index',i);
        paginationItem.style.backgroundImage = "url('" + "../" + escape(img) +"')";
        paginationItem.addEventListener('click',function(){
            currentSlide = parseInt(this.getAttribute('data-index'));
            cheker();
        });
        //append item to the parent
        paginationElement.appendChild(paginationItem);
        i++;
    })

    //append ul to the page
    container.appendChild(paginationElement);
}






//handle click on previous and next buttons
//next slide function
nextButton.onclick = function(){
    currentSlide++;
    cheker();
}
//prev slide function
prevButton.onclick = function(){
    currentSlide--;
    cheker();
}

//create the chker funtion
function cheker(){
    if(currentSlide <= sliderItems.length && currentSlide >0){
        removeAllActive();
        addActiveToCurrentElement();

        window.localStorage.setItem(`currentSlideStorage${location.search.substring(1)}`,currentSlide);

        if(currentSlide == sliderItems.length){
            dasibleButton(nextButton);
        }
        else{
            enableButton(nextButton);
        }

        if(currentSlide == 1){
            dasibleButton(prevButton);
        }
        else{
            enableButton(prevButton);
        }
    }
}

function removeAllActive(){
    for(let i = 0; i < slidesCount; i++){
        sliderItems[i].classList.remove('active');
        myelmetli[i].classList.remove('active');
    }
}
function addActiveToCurrentElement(){
    sliderItems[currentSlide - 1].classList.add('active');
    myelmetli[currentSlide -1].classList.add('active');
}
function dasibleButton(button){
    button.style.pointerEvents = 'none';
    button.style.color = '#555';
}
function enableButton(button){
    button.style.pointerEvents = 'all';
    button.style.color = '#FFF';
}


// details 
function vulDetails(proId,titlePro,omschrijvingPro,prijsPro,oudPrijsPro,specificatie,vooraad,categorie){
    let h2 = document.createElement('h2'),
        omschrijving = document.createElement('div'),
        prijs = document.createElement('div'),
        ul = document.createElement('ul'),
        levering = document.createElement('div'),
        addToCart = document.createElement('div');
    
    h2.textContent = titlePro;
    details.appendChild(h2);

    omschrijving.textContent = omschrijvingPro
    details.appendChild(omschrijving);

    //als de oude prijs is not null
    if(oudPrijsPro != null)
    {
        let verschil = (parseInt(prijsPro) / parseInt(oudPrijsPro)) * 100;

        prijs.innerHTML = `$${prijsPro} <span>$${oudPrijsPro}</span><span>(${Math.ceil(verschil)}%off)</span>`;
    }
    else{
        prijs.innerHTML = `$${prijsPro}`;
    }
    details.appendChild(prijs);

    specificatie.forEach((spec)=>{
        let li = document.createElement('li');
        li.textContent = spec;
        ul.appendChild(li);
    })
    details.appendChild(ul);

    if(vooraad > 0)
    {
        levering.textContent = "Ships next busniess day";
    }
    else{
        levering.textContent = "not in stock";
        addToCart.classList.add('no-clicking');
    }
    details.appendChild(levering);
    addToCart.textContent = "Add To Cart";
    let klantGegevens = sessionStorage.getItem('loginInfo');
    if(klantGegevens != null)
    {
        let gebruikersNummer = JSON.parse(sessionStorage.getItem('loginInfo')).Klantnummer;
        addToCart.addEventListener('click',function(){
            //show animation cart
            let cart = document.querySelector('#login').nextElementSibling;
            cart.classList.add('cart-active');
            window.setTimeout(function(){
                cart.classList.remove('cart-active')
            },2000);

            let winkelMandje = sessionStorage.getItem(`winkelmandje${gebruikersNummer}`);
            
            if(!IsAllExistInStorage(winkelMandje,proId))
            {
                if(winkelMandje != "null" && winkelMandje != "" && winkelMandje != null)
                {
                    winkelMandje += "|" + proId + ",1";
                    sessionStorage.setItem(`winkelmandje${gebruikersNummer}`,winkelMandje);
                }
                else{
                    sessionStorage.setItem(`winkelmandje${gebruikersNummer}`,proId + ",1");
                }
            }
            console.log("after " +sessionStorage.getItem(`winkelmandje${gebruikersNummer}`))
        })
    } 
    else{
        addToCart.addEventListener('click',function(){
            location.href = location.protocol + "//" + location.host + "/" + "/login/login.html" + "?" + `prodetails|${proId}` + "#" +  categorie;
        });
    }
    details.appendChild(addToCart);
}

function IsAllExistInStorage(sotrage,value){

    let items = sotrage.split('|'),

        resultSeach = false;

    items.forEach((item)=>{

        let id = item.split(',')[0];

        if(id == value){

            resultSeach = true;

        }

    })

    return resultSeach;

}

//
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


function Result(result){
    console.log(result);
}

function getRequest() {
    fetch('https://localhost:44351/ProductDetails', {
        method: 'GET',
    })
    .then(validateResponse)
    .then(readResponseAsJSON)
    .then(Result)
    .catch(logError);
}

function postRequest() {
    const messageHeaders = new Headers({
        'Content-Type': 'application/json'
    })
    let newProduct = {Id : location.search.substring(1)}

    fetch(`https://localhost:44351/${location.hash.substring(1)}`, {
        method: 'POST',
        body: JSON.stringify(newProduct),
        headers: messageHeaders
    })
    .then(validateResponse)
    .then(readResponseAsJSON)
    .then(getResult)
    .catch(logError);
}
function getResult(result){
    gegevens = result[0];
    createSlider(result[0].images.split('|'));
    // console.log(result)
    let plusPunten = getPlusPunten(location.hash.substring(1),result[0]);
    console.log("plus punten" +plusPunten);

    vulDetails(
        result[0].productnummer
        ,result[0].producttype
        ,result[0].kleine_beschrijving
        ,result[0].prijs
        ,null
        ,plusPunten
        ,result[0].voorraad
        ,result[0].categorie);

    vulTechSpecs(location.hash.substring(1),result[0]);

    sliderItems = document.querySelectorAll('.slider-product');
    slidesCount = sliderItems.length;
    myelmetli = document.querySelectorAll('.slider-container ul li');
    checkLocalSlider();
    if(window.innerWidth > 1000)
    {
        sliderContent.style.height = (window.innerHeight - (upperBar.offsetHeight + lowerBar.offsetHeight)) + "px !important";
    }
    //slider content margin top bepalen
    sliderContent.style.marginTop = lowerBar.offsetHeight + "px";
}
function checkLocalSlider()
{
    if(currentSlideStorage != null)
    {
        currentSlide = currentSlideStorage;
        cheker();
    }
    else{
        currentSlide = 1;
        dasibleButton(prevButton);
        sliderItems[currentSlide-1].classList.add('active');
        myelmetli[currentSlide -1].classList.add('active');
    } 
}
function getPlusPunten(hashtag,result0)
{
    console.log('has')
    hashtag = hashtag.substring(0,1).toUpperCase() + hashtag.substring(1).toLocaleLowerCase();
    console.log(hashtag)
    console.log(result0);
    let pluspunten = [];
    switch(hashtag)
    {
        case "Laptop":
            pluspunten = [result0.Processor,result0.OS,result0.Display.split(';')[0],result0.Storage,result0.Memory];
            break;
        case "Muis":
            pluspunten = ["DPI : " + result0.DPI, "aantal knoppen  :" + result0.aantalknoppen,"vervinding : " + result0.verbinding];
            break;
        case "Headset":
            pluspunten = ["diameter drivers : " + result0.diameter_drivers_mm, "Vervinding : " + result0.verbinding];
            break;
        case "Toetsenbord":
            pluspunten = ["Model : "+ result0.modelazerty,"verbinding : " + result0.verbinding];
            break;
    }
    console.log(pluspunten);
    return pluspunten;
}
function vulTechSpecs(hash,techSpecs)
{
    console.log(techSpecs);
    document.querySelector(`#${hash.toLocaleLowerCase()}`).classList.add('active');
    for (const property in techSpecs) {
        let pr = document.querySelector(`#${hash.toLocaleLowerCase()} #${property.toLocaleLowerCase()}`)
        if(document.body.contains(pr))
        {
            if(techSpecs[property].toString().split('|').length > 1)
            {
                let ul = document.createElement('ul');
                techSpecs[property].split('|').forEach((tech)=>{
                    let li = document.createElement('li');
                    li.textContent = tech;
                    ul.appendChild(li);
                })
                pr.appendChild(ul);
                console.log(`ul ${property}: ${techSpecs[property]}`);
            }
            else{
                let p = document.createElement('p');
                let txt = "";
                switch(techSpecs[property])
                {
                    case "J":
                        txt = "Ja";
                        break;
                    case "N":
                        txt = "Nee";
                        break;
                    default:
                        txt = techSpecs[property];
                        break;
                }
                p.textContent = txt;
                pr.appendChild(p);
            }
            
        }
    }
}

// function postWinkelMandje(klantnummer,img,type,psPunten,prijs,aantalpro)
// {
//     const messageHeaders = new Headers({
//         'Content-Type': 'application/json'
//     })
//     let mandje = {
//         KlantNummer : klantnummer,
//         ImageUrl : img,
//         ProductType : type,
//         PlusPunten : psPunten,
//         ProductPrijs : prijs,
//         Aantal : aantalpro
//     }

//     fetch('https://localhost:44351/ProductDetails', {
//         method: 'POST',
//         body: JSON.stringify(mandje),
//         headers: messageHeaders
//     })
//     .then(validateResponse)
//     .then(geefSuccesTeken)
//     .catch(logError);
// }

// function geefSuccesTeken(){
//     console.log("succes");
// }