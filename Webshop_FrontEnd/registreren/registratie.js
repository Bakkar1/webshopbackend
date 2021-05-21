const fetchPosition = (position) => {
    const latitude = position.coords.latitude,
        longitude = position.coords.longitude;
    fetch(`https://api.opencagedata.com/geocode/v1/json?q=${latitude}+${longitude}&key=33624e6315c04a2f8e479a7c78b86339`)
    .then(response => response.json())
    .then(getResult);
}
function getResult(response){
    console.log(response.results);
    if(response.results.length > 0){
        // console.log(response.results[0].annotations.callingcode);
        // console.log(response.results[0].components.country)
        // console.log(response.results[0].components.postcode)
        // console.log(response.results[0].components.town)
        // console.log(response.results[0].components.road)
        // console.log(response.results[0].components.house_number)
        let resComponent = response.results[0].components;
        // setValue(resComponent.road,'#straatnaam');
        // setValue(resComponent.house_number,'#huisnummer');
        setValue(resComponent.postcode,'#postcode');
        setValue(resComponent.country,'#land');
        setValue(resComponent.town,'#plaats');
        document.querySelector('#telefoon').value = "+" + response.results[0].annotations.callingcode;
    }
}
function setValue(respnseValue , selector){
    if(respnseValue != "undefined" && respnseValue != null){
        document.querySelector(selector).value = respnseValue;
    }
}
function locationNotReceived(positionError){
    if(!navigator.onLine)
    {
        console.log("not conected")
    }
    else{
        console.log(positionError);
    }
}

if(navigator.geolocation){
    navigator.geolocation.watchPosition(fetchPosition,locationNotReceived,{timeout:10});
}


//go home on click on logo
document.querySelector('.logo').onclick = function(){
    location.pathname = '/index.html';
}



