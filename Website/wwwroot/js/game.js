//url of the card api
let cardAPIURL = "http://localhost:49340/api/cards";

//card handling
let cards; //all cards in game
let currentCard; //current card displayed
let cardIDs = new Array(); //ids of all cards, incase we mess up and dont have them in order or skip an id
let cardIDsUsed = new Array(); //ids of cards that have been used, can never be longer than reuse factor
let reuseFactor = 2; //how many turns before a card can be used again

//game variables
let status = { //status of empire, range from 0 to 100
    military: 50,
    happiness: 50,
    relations: 50,
    economy: 50
}
let turn = -1; //turns since start, starts at -1 because  1 is added at game start

//Start the game by loading the card data and saving it
loadData();

//get card data from json
async function loadData() {
    const response = await fetch(cardAPIURL);
    const data = await response.json();
    //after data is loaded, execute startup function and pass in json data
    startup(await Promise.all(data));
}

//called after all data is loaded, saves data from loadData function
function startup(data) {
    //save card data from json to cards variable
    cards = data;
    generateCardIDs();
    //start first turn of game
    nextTurn();
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
    //~/images/
    currentCard = getRandomUnusedCard();
    document.getElementById("gameImage").src = "../images/" + currentCard.imageRef;
}

//takes the result you have agreed to and adds the consequences to status
function agree() {
    status.military += parseInt(currentCard.military1);
    status.relations += parseInt(currentCard.relations1);
    status.happiness += parseInt(currentCard.happiness1);
    status.economy += parseInt(currentCard.economy1);
    nextTurn();
}

function disagree() {
    status.military += parseInt(currentCard.military2);
    status.relations += parseInt(currentCard.relations2);
    status.happiness += parseInt(currentCard.happiness2);
    status.economy += parseInt(currentCard.economy2);
    nextTurn();
}

//rare cardS??


function nextTurn() {
    turn++;
    newCard();
    console.log(currentCard);
    console.log(status);
}