let upperBar = document.querySelector('.upper-bar'),
    footer = document.querySelector('footer'),
    i = 0,
    isEerstProduct = true,
    aantlSliders;

window.addEventListener('load', function(){
    window.scrollTo(0,0);
    document.title = location.search.substring(1);
    //handel active class on categorie links
    document.querySelectorAll('.lower-bar ul li a').forEach((linkCategorie)=>{
        if(linkCategorie.getAttribute('href') == "categorie.html"+ location.search)
        {
            linkCategorie.parentElement.classList.add('active');
            
        }
    })
    getRequest();
})
window.onresize = function()
{
    eerstProducHeightBepalen();
}

window.addEventListener('scroll',function(){
    document.querySelectorAll('.product-image ').forEach((product)=>{
        showProduct(product)
    })
});

function showProduct(target){
    let targetOffsetTop = target.offsetTop;

    let windowScrollTop = window.pageYOffset;

    if(windowScrollTop > targetOffsetTop && windowScrollTop < targetOffsetTop + 300){
        target.classList.remove('hide');
        console.log('hide')
    }
    else{
        target.classList.add('hide');
    }
}
window.addEventListener('scroll',function(){
    document.querySelectorAll('.product-image ').forEach((product)=>{
        showProduct(product)
    })
});

function showProduct(target){
    let targetOffsetTop = target.offsetTop;

    let targetOuterHeight = target.offsetHeight;

    let windowHeight = window.innerHeight;


    let windowScrollTop = window.pageYOffset;
    console.log(windowScrollTop);
    console.log("tr" + targetOffsetTop)

    if(windowScrollTop > targetOffsetTop && windowScrollTop < targetOffsetTop + 300){
        target.classList.remove('hide');
        console.log('hide')
    }
    else{
        target.classList.add('hide');
    }
}

function eerstProducHeightBepalen(){
    let eerstProducImage = document.querySelector('#product-image');
    if(window.innerWidth > 576){
        eerstProducImage.style.height = (window.innerHeight - upperBar.offsetHeight) + "px";
    }
    else{
        eerstProducImage.style.height = "60vh";
    }
}

function createProduct(Title,Omschrijving,productId,imgUrl){
    let productImage = document.createElement('div'),
        product = document.createElement('div'),
        title = document.createElement('span'),
        omschrijving = document.createElement('span'),
        details = document.createElement('span');

    productImage.className = "product-image";
    productImage.classList.add('hide');
    productImage.style.backgroundImage = `url('${imgUrl}')`;
    if(isEerstProduct){
        productImage.setAttribute('id',"product-image");
        isEerstProduct = false;
    }
    
    product.className = "product";

    productImage.appendChild(product);

    title.textContent = Title;
    product.appendChild(title);

    omschrijving.textContent = Omschrijving;
    product.appendChild(omschrijving);

    details.textContent = "Details";
    details.className = "details-show";
    details.setAttribute('data-id',productId);

    details.addEventListener('click',function(){
        location.href = location.protocol + "//" + location.host + "/" + "prodetails/prodetails.html" + "?" + this.getAttribute('data-id') + "#" + location.search.substring(1);
    });
    product.appendChild(details);

    document.querySelector('main').appendChild(productImage);
}

//wheel event handlen
window.addEventListener('wheel',scroll,true);

function scroll(evenet){
    if(window.innerWidth > 576){
        if(evenet.deltaY > 0){
            scrollNaarOnder();
        }
        else{
            scrollNaarBoven();
        }
        window.removeEventListener("wheel", scroll, true); 
        window.setTimeout(function(){
            window.addEventListener('wheel',scroll,true)
        },500)
    }
}

//click naar boven button
document.querySelector('.naar-boven').onclick = function(){
    scrollNaarBoven();
    handelClickEvent(this);
};
//click naar onder button
document.querySelector('.naar-onder').onclick = function(){
    scrollNaarOnder();
    handelClickEvent(this);
};

function scrollNaarOnder(){
    console.log(aantlSliders);
    if(i != aantlSliders)
    {
        if(i >= aantlSliders -1){
            window.scrollBy(0,footer.clientHeight);
        }
        else{
            window.scrollBy(0,window.innerHeight);
        }
        i++;
    }
}
function scrollNaarBoven(){
    if(i <= 0)
    {
        i = 1;
    }
    if(i == aantlSliders){
        window.scrollBy(0,-footer.clientHeight);
    }
    else{
        window.scrollBy(0,-window.innerHeight);
    }
    i--;
}

function handelClickEvent(element){
    element.classList.add('no-clicking');
    //remove the class after the duration 
    window.setTimeout(function(){
        element.classList.remove('no-clicking');
    },500);
}

//control touch on screen event
let touch,startingY;

window.addEventListener('touchstart',(e)=>{
    e.preventDefault();
    startingY = e.touches[0].clientY;
    
});
window.addEventListener('touchmove',(e)=>{
    touch = e.touches[0].clientY;
});
window.addEventListener('touchend',function(){
    slideShow();
});

function slideShow(){
    //swipe naar onder
    if(startingY > touch){
        scrollNaarOnder();
    }
    //swipe naar boven
    else{
        scrollNaarBoven();
    }
}

//get products from de database
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
    result.forEach(element => {
        createProduct(element.producttype,
            element.kleine_beschrijving,
            element.productnummer,
            "../" + escape(element.images.split('|')[1])
            );
    })

    eerstProducHeightBepalen();
    aantlSliders = document.querySelectorAll('main .product-image').length;
}

function getRequest() {
    let product = location.search.substring(1);
    fetch(`https://localhost:44351/${product}`, {
        method: 'GET',
    })
    .then(validateResponse)
    .then(readResponseAsJSON)
    .then(Result)
    .catch(logError);
}
//controllers names :  Laptop Muis Headset Toetsenbord