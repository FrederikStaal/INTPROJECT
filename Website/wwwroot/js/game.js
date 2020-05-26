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
    ctx.fillStyle = perc2color(status);
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

//https://gist.github.com/mlocati/7210513
function perc2color(perc) {
    var r, g, b = 0;
    if (perc < 50) {
        r = 255;
        g = Math.round(5.1 * perc);
    }
    else {
        g = 255;
        r = Math.round(510 - 5.10 * perc);
    }
    var h = r * 0x10000 + g * 0x100 + b * 0x1;
    return '#' + ('000000' + h.toString(16)).slice(-6);
}