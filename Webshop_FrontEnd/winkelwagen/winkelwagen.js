let mainSec = document.getElementById("mainSec");
let totPrijs = document.getElementsByClassName("totPrijs");
let subTot = document.getElementsByClassName("subTot")[0];
let klantNr = JSON.parse(sessionStorage.getItem("loginInfo")).Klantnummer;
let prod = sessionStorage.getItem(`winkelmandje${klantNr}`).split("|");

let winkelmand = "";
let aantal = [];

getProductNumbersAndAmount();
console.log(winkelmand);
console.log(aantal);
postProduct();

function getProductNumbersAndAmount() {
    let teller = 0;
    for (let pair of prod) {
        let temp = pair.split(",");
        console.log(temp);
        if (teller == 0) {
            winkelmand += temp[0];
            aantal.push(temp[1]);
        } else {
            winkelmand += "|" + temp[0];
            aantal.push(temp[1]);
        }
        teller++;
    }
}

function postProduct() {
    const messageHeaders = new Headers({ "Content-Type": "application/json" });

    fetch("https://localhost:44351/Winkelwagen", {
        method: "POST",
        body: JSON.stringify(winkelmand),
        headers: messageHeaders,
    })
        .then(validateResponse)
        .then(readResponseAsJSON)
        .then(toonProducten)
        .catch(logError);
}

function validateResponse(response) {
    if (!response.ok) {
        throw Error(response.statusText);
    }
    return response;
}

function logError(error) {
    console.log("Looks like there was a problem:", error);
}

function readResponseAsJSON(response) {
    return response.json();
}

function toonProducten(result) {
    for (let i = 0; i < result.length; i++) {
        let sectie = document.createElement("section");
        let pic = getImage(result[i][0].images);
        let details = getDetails(
            result[i][0].producttype,
            result[i][0].pluspunten
        );
        let prijs = document.createElement("div");
        let aPrijs = getDivA(
            result[i][0].prijs,
            result[i][0].korting,
            aantal[i]
        );
        let bPrijs = getDivB(aantal[i]);

        sectie.setAttribute("data-id", result[i][0].Productnummer);
        prijs.setAttribute("class", "prijs");

        sectie.appendChild(pic);
        sectie.appendChild(details);
        prijs.appendChild(aPrijs);
        prijs.appendChild(bPrijs);
        sectie.appendChild(prijs);

        setListeners(
            sectie,
            result[i][0].prijs,
            result[i][0].korting,
            aantal[i]
        );

        mainSec.appendChild(sectie);
    }
    getPrice();
}

function getImage(images) {
    let pic = document.createElement("img");
    let fotos = images.split("|");
    pic.src = "../" + fotos[0];
    return pic;
}

function getDetails(type, punten) {
    let details = document.createElement("div");
    details.setAttribute("class", "details");
    let par = document.createElement("p");
    par.innerText = `Razer ${type}`;
    details.appendChild(par);
    let ulist = document.createElement("ul");
    let kenmerken = punten.split("|");
    for (let i = 0; i < kenmerken.length; i++) {
        let li = document.createElement("li");
        li.innerText = kenmerken[i];
        ulist.appendChild(li);
    }
    details.appendChild(ulist);
    return details;
}

function getDivA(prijs, korting, i) {
    let a = document.createElement("div");
    let iconEur = document.createElement("i");
    iconEur.setAttribute("class", "fas fa-euro-sign");
    a.appendChild(iconEur);
    let spanPrijs = document.createElement("span");
    spanPrijs.innerText = (
        (parseFloat(prijs) - parseFloat(korting)) *
        i
    ).toFixed(2);
    a.appendChild(spanPrijs);
    return a;
}

function getDivB(j) {
    let b = document.createElement("div");
    let label = document.createElement("label");
    label.htmlFor = "aantal-stuks";
    label.innerText = "Aantal";
    b.appendChild(label);
    let select = document.createElement("select");
    for (let i = 1; i <= 10; i++) {
        let opt = document.createElement("option");
        opt.value = i;
        opt.text = i;
        select.add(opt);
    }
    select.value = j;
    b.appendChild(select);
    let trash = document.createElement("i");
    trash.setAttribute("class", "fas fa-trash-alt");
    b.appendChild(trash);
    return b;
}

function setListeners(sectie, prijs, korting, i) {
    sectie.querySelector("select").addEventListener("change", function () {
        sectie.querySelector("span").innerHTML = (
            (parseFloat(prijs) - parseFloat(korting)) *
            this.value
        ).toFixed(2);
        i = this.value;
        updateStorage();
        getPrice();
    });

    sectie.querySelectorAll("i")[1].addEventListener("click", function () {
        sectie.remove();
        updateStorage();
        getPrice();
    });
}

function getPrice() {
    let aantal = 0;
    let prijs = 0;
    for (let element of document.querySelectorAll("section")) {
        prijs += parseFloat(element.querySelector("span").innerText);
        aantal += parseFloat(element.querySelector("select").value);
    }
    totPrijs[0].innerText = prijs.toFixed(2);
    totPrijs[1].innerText = prijs.toFixed(2);
    subTot.innerText = prijs.toFixed(2);
    document.getElementById("quick-check-out-amount").innerText = aantal;
}

function updateStorage() {
    let ids = "";
    let teller = 0;
    document.querySelectorAll("main section").forEach((sect) => {
        if (teller == 0) {
            ids +=
                sect.getAttribute("data-id") +
                "," +
                sect.querySelector("select").value;
        } else {
            ids +=
                "|" +
                sect.getAttribute("data-id") +
                "," +
                sect.querySelector("select").value;
        }
        teller++;
    });
    sessionStorage.setItem(`winkelmandje${klantNr}`, ids);
    console.log(`winkelmandje${klantNr}`, ids);
}
