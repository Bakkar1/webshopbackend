//check of main kleur is veranderd
let mainColor = localStorage.getItem("color_option");
if(mainColor != null){
    document.documentElement.style.setProperty('--main-color', mainColor);
}

//check of background overly kleur is veranderd
let overlyBackgroundColor = localStorage.getItem('overlyBackColor')
if(overlyBackgroundColor != null)
{
    document.documentElement.style.setProperty('--overly-back',`rgba(0,0,0,.${overlyBackgroundColor})`)
}

//font family setting
let fontFamily = localStorage.getItem('fontFamily');
if(fontFamily != null)
{
    document.documentElement.style.setProperty('--main-font-family', fontFamily);
}