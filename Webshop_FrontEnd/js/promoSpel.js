// ==============================================================================
let promoKeuze = localStorage.getItem('promoKeuze'),
    spelIntervale;

    let logininfo = sessionStorage.getItem('loginInfo');

//als de gebruiker is ingelogd
if(logininfo != null){
    if(promoKeuze != null){
        document.querySelectorAll('.promo-spel-set span').forEach((elm)=>{
            if(elm.getAttribute('data-keuze') == promoKeuze){
                handleActiveClass(elm);
                if(elm.getAttribute('data-keuze') == 'ja'){
                    window.setTimeout(vrragTeSpelen(),2000);

                    spelIntervale = setInterval(function(){
                        vrragTeSpelen();
                    },600000);
                    console.log('ja');
                }
            }
        })
    }
    else{
            window.setTimeout(vrragTeSpelen,2000);

            spelIntervale = setInterval(function(){
                vrragTeSpelen();
            },600000);
    }
}
else{
    document.querySelectorAll('.settings-box .promo-spel-set span').forEach((span)=>{
        span.classList.add('no-clicking');
        span.classList.remove('active');
    })
}

// create promo spel

function vrragTeSpelen(){
    let promSpel = document.createElement('div');
    promSpel.className = "promo-spel";

    //eerste icon
    let icon1 = document.createElement('i');
    icon1.className = "far fa-times-circle";
    icon1.setAttribute('data-spel','no');
    icon1.addEventListener('click',function(){
        checkKeuze(icon1)
    });

    //tweede icon
    let icon2 = document.createElement('i');
    icon2.className = "far fa-check-circle";
    icon2.setAttribute('data-spel','yes');
    icon2.addEventListener('click',function(){
        checkKeuze(icon2)
    });

    //title
    let title = document.createElement('span');
    title.className = "title";
    title.textContent = "Speel je mee";

    //boodschap
    let boodschap = document.createElement('p');
    boodschap.className = 'boodschap';

    boodschap.textContent = "Maak kans tot 90% korting";
    
    promSpel.appendChild(title);
    promSpel.appendChild(boodschap);
    promSpel.appendChild(icon1);
    promSpel.appendChild(icon2);
    

    document.body.appendChild(promSpel);
    //de functie wordt uitgevoerd na de duration zodat de gebruiken de transition kan zien
    window.setTimeout(function(){
        promSpel.style.bottom  = "0";
    }, (200));
    
}
function checkKeuze(icon){
    if(icon.getAttribute('data-spel') === "yes"){
        //stop click on icon1
        icon.style.pointerEvents = "none";
        //reloding items laten draien
        let span1 = document.createElement('span');
        span1.className = "circle";
        let span2 = document.createElement('span');

        span1.appendChild(span2);
        
        let boodschap = document.querySelector('.boodschap');
        boodschap.textContent = "";
        boodschap.appendChild(span1);
        //remove span1 and span2 na de duration
        window.setTimeout(function(){
            boodschap.removeChild(span1);
        },2000);

        window.setTimeout(function(){
            let proRandom = Math.floor(Math.random() * 15) + 2;
            let mupar = document.querySelector('.promo-spel p');
            mupar.textContent = "proficiat je hebt een korting van " + proRandom
                + "%" +
                "\n korting code : " + generatePromo()
                + " \n je kunt weer spelen na 10min";
        },2100);
    }
    else{
        removePromoSepl(icon.parentElement);
    }
}

function generatePromo(){
    let alpha = "AZERTYUIPQSDFGHJKLMWXCVBN",
        teken = "$#@&",
        numbers = "1234567890";
    
    let indexAlpha = Math.floor(Math.random() * alpha.length),
        indexTeken = Math.floor(Math.random() * teken.length),
        indexNumbers = Math.floor(Math.random() * numbers.length);
    let promo = alpha.substr(indexAlpha,2) + 
        teken.substr(indexTeken,1) + 
        alpha.substr(indexTeken,1).toLowerCase() +
        numbers.substr(indexNumbers,1) +
        alpha.substr(indexNumbers,1).toLowerCase()
        ; 

    return promo;
}

function removePromoSepl(promoSpel){
    promoSpel.style.bottom  = "-100%";
    window.setTimeout(()=>{
        promoSpel.remove();
    },1000)
}

//+==========================================
//promo spel settings

let spelJa = document.querySelector('.promo-spel-set .ja'),
    spelNee = document.querySelector('.promo-spel-set .nee');

spelJa.onclick = function(){
    handleActiveClass(this);
    spelIntervale = setInterval(function(){
        vrragTeSpelen();
    },600000)
    localStorage.setItem('promoKeuze',this.getAttribute('data-keuze'));
}
spelNee.onclick = function(){
    handleActiveClass(this);
    clearInterval(spelIntervale);
    if(document.body.contains(document.querySelector('.promo-spel')))
    {
        removePromoSepl(document.querySelector('.promo-spel'));
    }
    localStorage.setItem('promoKeuze',this.getAttribute('data-keuze'));
}


// handle active claas funtion
function handleActiveClass(elment){
    let elements = Array.from(elment.parentElement.children);
    elements.forEach((el)=>{el.classList.remove('active')});
    elment.classList.add('active');
}