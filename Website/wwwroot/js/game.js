//url of the card api
let cardAPIURL = window.location.href.replace("/game", "/api/cards");

//cookie
let cookieName = "saveData";
let cookieData = {};
let isValidCookie;
try {
    isValidCookie = JSON.parse(getCookie(cookieName)).isValid;
} catch {
    isValidCookie = 0;
}

//card handling
let cards; //all cards in game
let currentCard; //current card displayed
let cardIDs = new Array(); //ids of all cards, incase we mess up and dont have them in order or skip an id
let cardIDsUsed = new Array(); //ids of cards that have been used, can never be longer than reuse factor
let reuseFactor = 2; //how many turns before a card can be used again

//icon variables
let moneyCanvas = document.getElementById("game-money-canvas");
let moneyImage = document.getElementById("money-icon");
let militaryCanvas = document.getElementById("game-military-canvas");
let militaryImage = document.getElementById("military-icon");
let happinessCanvas = document.getElementById("game-happiness-canvas");
let happinesImage = document.getElementById("happiness-icon");
let relationsCanvas = document.getElementById("game-relations-canvas");
let relationsImage = document.getElementById("relations-icon");
let colors = {
    military: "#0066cc",
    happiness: "#ff0000",
    relations: "#ff9900",
    economy: "#33cc33"
}



//game variables
let hasLost = false;
let status = { //status of empire, range from 0 to 100
    military: 50,
    happiness: 50,
    relations: 50,
    economy: 50
}
let turn = -1; //turns since start, starts at -1 because  1 is added at game start

// Trying to keep the session going
var refreshSession = function () {
    var time = 600000; //10 mins
    setTimeout(
        function () {
            $.ajax({
                url: 'refresh_session.php',
                cache: false,
                complete: function () { refreshSession(); }
            });
        },
        time
    );
};

//Start the game by loading the card data and saving it
loadData();

//get card data from json
async function loadData() {
    const response = await fetch(cardAPIURL);
    const data = await response.json();

    //load cookie data
    if (isValidCookie) {
        continueGame(await Promise.all(data));
    } else {
        startGame(await Promise.all(data));
    }
}

//called after all data is loaded, saves data from loadData function
function startGame(data) {
    cards = data;
    generateCardIDs();
    hasLost = false;
    //start first turn of game
    nextTurn();
}

function continueGame(data) {
    cards = data;
    generateCardIDs();
    updateGameData();
    updatePageData();
}

//saves cookie data to game
function updateGameData() {
    cookieData = JSON.parse(getCookie(cookieName));
    status.military = cookieData.military;
    status.happiness = cookieData.happiness;
    status.relations = cookieData.relations;
    status.economy = cookieData.economy;
    turn = cookieData.turn;
    currentCard = getCardByID(cookieData.currentCardID);
}

//Canvas and icon stuff
function updatePageData() {
    //page elements
    document.getElementById("gameImage").src = "../images/" + currentCard.imageRef;
    document.getElementById("years_office").innerHTML = "Years in office: " + turn;
    document.getElementById("Cardtext").innerHTML = currentCard.text;

    drawIcon(moneyCanvas, status.economy, moneyImage, colors.economy);
    drawIcon(militaryCanvas, status.military, militaryImage, colors.military);
    drawIcon(happinessCanvas, status.happiness, happinesImage, colors.happiness);
    drawIcon(relationsCanvas, status.relations, relationsImage, colors.relations);
}

function drawIcon(canvas, status, image, fillColor) {
    canvas.width = 100;
    canvas.height = 100;
    let imageWidth = 50;
    let imageHeight = 50;
    let startRadian = Math.PI / 2;
    let statusRadians = status / 50 * Math.PI + startRadian;
    let ctx = canvas.getContext("2d");
    let centerX = canvas.width / 2;
    let centerY = canvas.height / 2;
    let radius = 46;

    ctx.beginPath();
    ctx.fillStyle = pSBC(status / 100, fillColor, false, true);
    ctx.strokeStyle = "000000";
    ctx.lineWidth = 3;
    ctx.shadowBlur = 5;
    ctx.shadowColor = "black";
    ctx.moveTo(centerX, centerY);
    ctx.lineTo(centerX, centerY + radius);


    ctx.arc(centerX, centerY, radius, startRadian, startRadian + 2 * Math.PI);
    ctx.lineTo(centerX, centerY);
    //ctx.stroke();
    ctx.fill();
    ctx.drawImage(image, imageWidth / 2, imageHeight / 2, imageWidth, imageHeight);
}

//saves game data to cookie
function updateCookie() {
    if (hasLost) {
        cookieData.isValid = 0;
    } else {
        cookieData.isValid = 1;
    }

    cookieData.turn = turn;
    cookieData.military = status.military;
    cookieData.happiness = status.happiness;
    cookieData.relations = status.relations;
    cookieData.economy = status.economy;
    cookieData.currentCardID = currentCard.cardID;
    document.cookie = cookieName + "=" + JSON.stringify(cookieData);
}

//from stack overflow
function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}

//creates list of ids for all cards
function generateCardIDs() {
    //for each card add its id to cardids list
    cards.forEach(card => {
        cardIDs.push(card.cardID);
    });
}

//returns card with same id or null if not found
function getCardByID(id) {
    //return variable, assigned as null to return null if no card found
    let r = null;
    //for each card
    cards.forEach(card => {
        //if card id is equal to id input
        if (card.cardID === id)
            //assign card to return variable
            r = card;
    });
    return r;
}

//returns list of unused cards (cards)
function getUnusedCardIDs() {
    //return list
    let r = new Array();
    //copy card ids and add them to r 
    cardIDs.forEach(id => {
        r.push(id);
    });
    //for each used cards id subtract it from r
    cardIDsUsed.forEach(id => {
        if (r.includes(id))
            r.splice(r.indexOf(id), 1);
    });
    return r;
}

//returns a random card that has not been used recently
function getRandomUnusedCard() {
    //if length of usedcards us equal to reuse factor
    if (cardIDsUsed.length == reuseFactor) {
        //remove last member of card ids used
        cardIDsUsed.pop();
    }
    //get unused cards list
    let unusedCards = getUnusedCardIDs();
    //select random card from unused cards
    let newCard = unusedCards[Math.floor(Math.random() * unusedCards.length)];
    //add new cards id to beginning of used cards list
    cardIDsUsed.unshift(newCard);
    //return card
    return getCardByID(newCard);
}

//selects new card and assigns it to current card, also updates previouscards
function newCard() {
    currentCard = getRandomUnusedCard();
}

//takes the result you have agreed to and adds the consequences to status
function agree() {
    status.military += parseInt(currentCard.military1);
    status.relations += parseInt(currentCard.relations1);
    status.happiness += parseInt(currentCard.happiness1);
    status.economy += parseInt(currentCard.economy1);
    nextTurn();
}

function showAgreeResult() {
    drawIcon(moneyCanvas, status.economy + parseInt(currentCard.economy1), moneyImage, colors.economy);
    drawIcon(militaryCanvas, status.military + parseInt(currentCard.military1), militaryImage, colors.military);
    drawIcon(happinessCanvas, status.happiness + parseInt(currentCard.happiness1), happinesImage, colors.happiness);
    drawIcon(relationsCanvas, status.relations + parseInt(currentCard.relations1), relationsImage, colors.relations);
}

function disagree() {
    status.military += parseInt(currentCard.military2);
    status.relations += parseInt(currentCard.relations2);
    status.happiness += parseInt(currentCard.happiness2);
    status.economy += parseInt(currentCard.economy2);
    nextTurn();
}

function showDisagreeResult() {
    drawIcon(moneyCanvas, status.economy + parseInt(currentCard.economy2), moneyImage, colors.economy);
    drawIcon(militaryCanvas, status.military + parseInt(currentCard.military2), militaryImage, colors.military);
    drawIcon(happinessCanvas, status.happiness + parseInt(currentCard.happiness2), happinesImage, colors.happiness);
    drawIcon(relationsCanvas, status.relations + parseInt(currentCard.relations2), relationsImage, colors.relations);
}

function lost() {
    alert("You have lost");
    hasLost = true;
    reset();
}

//resets game
function reset() {
    hasLost = false;
    status.economy = 50;
    status.relations = 50;
    status.happiness = 50;
    status.military = 50;
    turn = -1;
    nextTurn();
}

function checkIfLost() {
    if (status.military <= 0 || status.military >= 100)
        return true;
    if (status.relations <= 0 || status.relations >= 100)
        return true;
    if (status.happiness <= 0 || status.happiness >= 100)
        return true;
    if (status.economy <= 0 || status.economy >= 100)
        return true;
    return false;
}

function nextTurn() {
    if (checkIfLost()) {
        lost();
    } else {
        turn++;
        newCard();
    }
    updateCookie();
    updatePageData();
}

//https://stackoverflow.com/questions/5560248/programmatically-lighten-or-darken-a-hex-color-or-rgb-and-blend-colors
// Version 4.0
const pSBC = (p, c0, c1, l) => {
    let r, g, b, P, f, t, h, i = parseInt, m = Math.round, a = typeof (c1) == "string";
    if (typeof (p) != "number" || p < -1 || p > 1 || typeof (c0) != "string" || (c0[0] != 'r' && c0[0] != '#') || (c1 && !a)) return null;
    if (!this.pSBCr) this.pSBCr = (d) => {
        let n = d.length, x = {};
        if (n > 9) {
            [r, g, b, a] = d = d.split(","), n = d.length;
            if (n < 3 || n > 4) return null;
            x.r = i(r[3] == "a" ? r.slice(5) : r.slice(4)), x.g = i(g), x.b = i(b), x.a = a ? parseFloat(a) : -1
        } else {
            if (n == 8 || n == 6 || n < 4) return null;
            if (n < 6) d = "#" + d[1] + d[1] + d[2] + d[2] + d[3] + d[3] + (n > 4 ? d[4] + d[4] : "");
            d = i(d.slice(1), 16);
            if (n == 9 || n == 5) x.r = d >> 24 & 255, x.g = d >> 16 & 255, x.b = d >> 8 & 255, x.a = m((d & 255) / 0.255) / 1000;
            else x.r = d >> 16, x.g = d >> 8 & 255, x.b = d & 255, x.a = -1
        } return x
    };
    h = c0.length > 9, h = a ? c1.length > 9 ? true : c1 == "c" ? !h : false : h, f = this.pSBCr(c0), P = p < 0, t = c1 && c1 != "c" ? this.pSBCr(c1) : P ? { r: 0, g: 0, b: 0, a: -1 } : { r: 255, g: 255, b: 255, a: -1 }, p = P ? p * -1 : p, P = 1 - p;
    if (!f || !t) return null;
    if (l) r = m(P * f.r + p * t.r), g = m(P * f.g + p * t.g), b = m(P * f.b + p * t.b);
    else r = m((P * f.r ** 2 + p * t.r ** 2) ** 0.5), g = m((P * f.g ** 2 + p * t.g ** 2) ** 0.5), b = m((P * f.b ** 2 + p * t.b ** 2) ** 0.5);
    a = f.a, t = t.a, f = a >= 0 || t >= 0, a = f ? a < 0 ? t : t < 0 ? a : a * P + t * p : 0;
    if (h) return "rgb" + (f ? "a(" : "(") + r + "," + g + "," + b + (f ? "," + m(a * 1000) / 1000 : "") + ")";
    else return "#" + (4294967296 + r * 16777216 + g * 65536 + b * 256 + (f ? m(a * 255) : 0)).toString(16).slice(1, f ? undefined : -2)
}