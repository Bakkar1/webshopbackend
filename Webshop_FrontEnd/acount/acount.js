//go home on click on logo
document.querySelector('.logo').onclick = function(){
    location.pathname = '/index.html';
}


//check of de user is ongelogd
let loginInfo = JSON.parse(sessionStorage.getItem('loginInfo')),
    content = document.querySelector('.content'),
    wijzigin = document.querySelector('.wijzigin'),
    loginKlantnummer = loginInfo.Klantnummer,
    loginAangemaakt = loginInfo.Aangemaakt;

console.log(loginInfo);
//delete aagemaakt datum from de object
delete loginInfo.Aangemaakt;
delete loginInfo.Klantnummer;

for(let info in loginInfo){
    if(info == "Land"){
        document.querySelector('#land').value = loginInfo[info];
        console.log(loginInfo[info]);
    }
    else{
        let infobox = document.createElement('div');
        infobox.classList.add('info');
    
        let infokey = document.createElement('div');
        infokey.className = "info-name";
        infokey.textContent = info;
        
        let infoValue = document.createElement('input');
        infoValue.className = "info-value";
        infoValue.setAttribute('required','');
    
        infoValue.addEventListener('input',function(){
            //enable save button
            document.querySelector('.wijzigin .save').style.pointerEvents = "all";
            document.querySelector('.wijzigin .save').style.opacity = "1";
        });
        if(loginInfo[info] == null){
            infoValue.value = "";
        }
        else{
            infoValue.value = loginInfo[info];
        }
    
        infobox.appendChild(infokey);
        infobox.appendChild(infoValue);

            //als de input wachtwoord is
        if(info == "Wachtwoord"){
            infoValue.placeholder = "********";
            infoValue.type = "password";
            infobox.style.position = "relative";

            let icon = document.createElement('i');
            icon.className = "fas fa-low-vision";

            icon.addEventListener('click',function(){
                if(icon.classList == 'fas fa-low-vision'){
                    icon.classList = 'fas fa-eye';
                    icon.style.color = 'var(--main-color)';
                    infoValue.setAttribute('type','text');
                }
                else{
                    icon.classList = 'fas fa-low-vision';
                    infoValue.setAttribute('type','password');
                    icon.style.color = '#777';
                }
            })

            infobox.appendChild(icon);
        }

        content.insertBefore(infobox,wijzigin);
    }

}


if(window.innerHeight > 800)
{
    document.querySelector('footer').classList.add('absolute-footer');
}








//===================================================================
//save click event
let confirm = document.querySelector('.save');

confirm.onclick = function(){
    let isLeeg = false;
    let formData = new FormData();
    let allData = document.querySelectorAll('.info');
    formData.append("Klantnummer" , loginKlantnummer);
    allData.forEach((data) =>{
        if(data.children[1].value == null || data.children[1].value == "")
        {
            isLeeg = true;
        }
        else{
            formData.append(data.firstElementChild.textContent,data.children[1].value);
        }
        //formData.append(data.firstElementChild.textContent,data.children[1].value);
    })
    formData.append("Aangemaakt", loginAangemaakt);

    if(isLeeg == false)
    {
        postFormData(formData);
    }
    else{
         allData.forEach((data) =>{
            let input = data.children[1];
            
            if(input.value == "")
            {
                input.style.border = "1px solid #F00";
            }

            input.addEventListener('focus',function(){
                input.style.border = "1px solid var(--main-color)";
            });
            input.addEventListener('blur',function(){
                if(input.value != "")
                {
                    input.style.border = "1px solid var(--gray-color-3)";
                }
                else{
                    input.style.border = "1px solid #F00";
                }
            });
        })
    }
}

function after(response){
    sessionStorage.setItem('loginInfo',JSON.stringify(response[0]));
    location.reload();
}
function validateResponse(response) {
    if (!response.ok) {
        throw Error(response.statusText);
    }
    return response;
}
function postFormData(formUserdata_1){
    fetch("https://localhost:44351/UpdateUserData",
        {
            body: formUserdata_1,
            method: "post"
        }).then(validateResponse)
        .then(response => response.json())
        .then(after);
}

//===================================================================
//back click evenet
let back = document.querySelector('.back');
back.onclick = function(){
    //go to home page
    location.pathname = "/index.html";
}