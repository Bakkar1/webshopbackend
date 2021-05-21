// 
let header = document.querySelector('header'),
    upperBar = document.querySelector('.upper-bar'),
    prevButton = document.querySelector('.prev'),
    nextButton = document.querySelector('.next'),
    slider = document.querySelector('.slider'),
    sliderItems = document.querySelectorAll('header .slider-item'),
    currentSlide,
    currentSlideStorage = localStorage.getItem('cuurentSlideStorage'),
    hoverEnter = 250;

window.addEventListener('load', function(){
    if(currentSlideStorage != null){
        currentSlide = currentSlideStorage;
        //move slider
        scrollToItems();
    }
    else{
        currentSlide = 1;
    }
    cehckDisable();
    }
);


//de height van de header bepalen
header.style.height = (window.innerHeight - upperBar.offsetHeight) + "px";
//bullets maken
//bullet container maken
let bullets = document.createElement('div');
scrollData = 0;
bullets.classList.add('bullets');

for(let i = 1;i <= sliderItems.length; i++){
    //bullet maken
    let bullet = document.createElement('div');
    //bullet scroll in data zitten
    bullet.setAttribute('data-scroll',scrollData);

    bullet.setAttribute('data-index',i);
    //bullet class tovoegen
    bullet.classList.add('bullet');
    //click event tovoegen
    bullet.addEventListener('click',function(){
        currentSlide = bullet.getAttribute('data-index');
        cehckDisable();
        slider.scrollLeft = bullet.getAttribute('data-scroll');
        //save currentSlide in de local storge
        saveCurrentSlide();
    });
    //de bullet in de container bullets zitten
    bullets.appendChild(bullet);
    scrollData += window.innerWidth;
}
//de bullets container in de header zitten
header.appendChild(bullets);

// click events
nextButton.onclick = function(){
    currentSlide++;
    //check of een button moet disable worden
    cehckDisable();
    // scroll to the next of prev slider item
    scrollToItems();
    //save currentSlide in de local storge
    saveCurrentSlide();
    //add hover na de duration
    // window.setTimeout(()=>{
    //     slider.scrollLeft += hoverEnter;
    // },1000)
}

prevButton.onclick = function(){
    currentSlide--;
    //check of een button moet disable worden
    cehckDisable();
    //scrol to item
    scrollToItems();
    //save currentSlide in de local storge
    saveCurrentSlide();
    //add hover na de duration
    // window.setTimeout(()=>{
    //     slider.scrollLeft -= hoverEnter;
    // },1000)
}
let bulletsItems = document.querySelectorAll('.bullets .bullet');
//check of een button moet disabled worden
function cehckDisable(){
    handleActiveClass(bulletsItems[currentSlide - 1]);
    handleActiveClass(sliderItems[currentSlide - 1]);
    if(currentSlide == 1){
        prevButton.classList.add('disable');
    }
    else{
        prevButton.classList.remove('disable');
    }
    if(currentSlide == sliderItems.length){
        nextButton.classList.add('disable');
    }
    else{
        nextButton.classList.remove('disable');
    }
} 

//function to scroll to the next of pre slider item
function scrollToItems(){
    slider.scrollLeft = bulletsSlider[currentSlide-1].getAttribute('data-scroll');
}

//save currentSlide in de local storge
function saveCurrentSlide(){
    localStorage.setItem('cuurentSlideStorage',currentSlide);
}

// handle active claas funtion
function handleActiveClass(elment){
    let elements = Array.from(elment.parentElement.children);
    elements.forEach((el)=>{el.classList.remove('active')});
    elment.classList.add('active');
}

//control touch on screen
let touch,startingX;

slider.addEventListener('touchstart',(e)=>{
    startingX = e.touches[0].clientX;
});
slider.addEventListener('touchmove',(e)=>{
    touch = e.touches[0].clientX;
    e.preventDefault();
});
slider.addEventListener('touchend',function(){
    slideShow();
});

let bulletsSlider = Array.from(document.querySelectorAll('.bullets .bullet'));
function slideShow(){
    //swipe left
    if(startingX > touch){
        if(currentSlide != sliderItems.length)
        {
            currentSlide++;
            cehckDisable();
            scrollToItems();
        }
    }
    //swipe right
    else{
        if(currentSlide != 1)
        {
            currentSlide--;
            cehckDisable();
            scrollToItems();
        }
    }
    //save currentSlide in de local storge
    saveCurrentSlide();
}

//===============================================================================

// let scrolColors = ["#44d62c","#3497DB","#E91E63"];

// let scrollIntervale = setInterval(function(){
//     let indexScroll = Math.floor(Math.random() * scrolColors.length);
//     document.documentElement.style.setProperty('--scroll-color',scrolColors[indexScroll]);
//     console.log(scrolColors[indexScroll]);
// },2000);

//=========================================================================
//box seting
let mySettings = document.querySelector('.settings-box'),
myLiColors = document.querySelectorAll('.colors-list li'),
mainColors = localStorage.getItem("color_option"),
myIconSnt = document.querySelector('.fa-sun');

// chek if ther is local storage color option
if(mainColors !== null){
    // set the localstorage value on root
    document.documentElement.style.setProperty('--main-color', mainColors);
    myLiColors.forEach(elm => { 
        if(elm.getAttribute('data-color') === mainColors){
            handleActiveClass(elm);
        }
    });
}

/*open menu setting*/
myIconSnt.onclick = function(){
    // lndPage.classList.toggle('filterB');
    mySettings.classList.toggle('open-settings');
    this.classList.toggle('turn-icon');
}

/* switch main color*/
// loop on all li
for(let c = 0; c < myLiColors.length; c++){
    myLiColors[c].onclick = function(){
        handleActiveClass(this);
        //get data-color
        let myColor = this.getAttribute('data-color');
        //set colors on root
        document.documentElement.style.setProperty('--main-color', myColor);
        //stoor color on local Storage
        localStorage.setItem("color_option", myColor);
    }
}


// ovely backgroundcolor setting
let ovelyBackgroundColor = document.querySelector('.settings-box .overly-background-color input');
ovelyBackgroundColor.onchange = function()
{
    document.documentElement.style.setProperty('--overly-back',`rgba(0,0,0,.${this.value})`)
    localStorage.setItem('overlyBackColor',this.value);
}

//range value pebalen
let overlyBackColor = localStorage.getItem('overlyBackColor')
if(overlyBackColor != null){
    let inputRange = document.querySelector('.settings-box .overly-background-color input');
    if(document.body.contains(inputRange)){
        inputRange.value = overlyBackColor;
    }
}


//font family
let fonts = document.querySelector('div.settings-box .option-box select');
fonts.addEventListener('change',function(){
    console.log(fonts.value)
    document.documentElement.style.setProperty('--main-font-family', fonts.value);
    localStorage.setItem('fontFamily',this.value);
})

//fonts value pebalen
let fontFamilySet = localStorage.getItem('fontFamily')
if(fontFamilySet != null){
    let selectFont = document.querySelector('div.settings-box .option-box select');
    if(document.body.contains(selectFont)){
        selectFont.value = fontFamilySet;
    }
}