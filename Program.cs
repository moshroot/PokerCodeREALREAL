using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;


// PLAYER CLASS

public class Player
{
    // PROPERTIES
    private int id;

    // The player's hole cards:
    private Card card1 = new Card();
    private Card card2 = new Card();

    // Properties relating to betting / money:
    private int money;
    private int totalBet;
    private int lastBet;

    // Properties relating to determining winner:
    private bool folded;
    private int rank;
    private int rankRank;

    // GETTERS

    public int getId() { return this.id; }

    // Get cards:
    public Card getCard1() { return this.card1; }
    public Card getCard2() { return this.card2; }

    // Get properties:
    public int getMoney() { return this.money; }
    public int getTotalBet() { return this.totalBet; }
    public int getLastBet() { return this.lastBet; }
    public int getRank() { return this.rank; }
    public int getRankRank() { return this.rankRank; }
    public bool isFolded() { return this.folded; }

    // SETTERS

    public void setId(int id) { this.id = id; }

    // Set cards:
    public void setCard1(Card card) { this.card1 = card; }
    public void setCard2(Card card) { this.card2 = card; }

    // Set properties:
    public void setMoney(int money) { this.money = money; }
    public void setTotalBet(int bet) { this.totalBet = bet; }
    public void setLastBet(int bet) { this.lastBet = bet; }
    public void setRank(int rank) { this.rank = rank; }
    public void setRankRank(int rankRank) { this.rankRank = rankRank; }
    public void setFolded(bool folded) { this.folded = folded; }


    // METHODS

    // CALL
    public void call(int lastPlayerBet)
    {
        
    }

    // RAISE
    public void raise(int lastPlayerBet)
    {

    }

    // CHECK
    public void check()
    {

    }

    // FOLD
    public void fold()
    {


    }
}

// BOT CLASS
// Inherits from Player class:
public class Bot : Player
{
    // PROPERTIES
    private int bluffChance;

    // METHODS
    public void raise(int lastPlayerBet)
    {

    }
}

// CARD CLASS
public class Card
{
    // PROPERTIES
    private int value;
    private int suit;
    private int cardNum;

    // GETTERS
    public int getValue() { return this.value; }
    public int getSuit() { return this.suit; }
    public int getCardNum() { return this.cardNum; }

    // Setters are unnecessary due to constructor generating ints

    // METHODS

    // Constructor generates value and suit, and calculates cardNum:
    public Card()
    {
        // Generates value and suit:
        this.value = new Random().Next(0, 13); // Between 0-12 inclusive
        this.suit = new Random().Next(0, 4); // Between 0-3 inclusive

        // Calculates cardNum:
        this.cardNum = (this.suit * 13) + this.value;
    }
}

// CHECK CARDS CLASS
// Used when checking for duplicate cards:
public class CheckCards
{
    //PROPERTIES 
    private int[] cardNums = new int[17];
    private int count = 0;

    // GETTERS
    public int[] getCardNums()
    {
        return this.cardNums;
    }
    public int getCount() { 
        return this.count; 
    }

    // SETTERS
    public void setCardNums(int cardNum)
    {
        this.cardNums[this.count] = cardNum;
        this.count++;
    }
}

// DETERMINE HAND CLASS
// Used while determining winning hand:
public class DetermineHand
{
    // PROPERTIES
    private int[] suits = new int[4];
    private int[] values = new int[13];
    
    // METHODS

    // Increments a suit count by one:
    public void addSuit(int suitNum) { this.suits[suitNum]++; }
    // Increments a value count by one:
    public void addValue(int valueNum) { this.values[valueNum]++; }

    // SUITS CHECK

    // Checks for five of the same suit (variant of a flush)
    // Returns true if there is an instance of five cards of the same suit in a hand:
    public bool checkFiveSuit()
    {
        bool check = false;
        for (int i = 0; i < this.suits.Length; i++)
        {
            if (this.suits[i] == 5)
            {
                check = true;
            }
        }
        return check;
    }

    // VALUES CHECK

    // Checks for royal straight (flush)
    public bool checkRoyalFlush()
    {
        bool check = false;
        int confirmCount = 0;
        for (int i = 8; i < this.values.Length; i++)
        {
            if (this.values[i] >= 1)
            {
                confirmCount++;
            }
        }
        if (confirmCount == 5)
        {
            check = true;
        }
        return check;
    }

    // Checks for four of a kind
    public bool checkFourOfAKind()
    {
        bool check = false;
        for (int i = 0; i < this.values.Length; i++)
        {
            if (this.values[i] == 4)
            {
                check = true;
            }
        }
        return check;
    }

    // Checks for full house
    public bool checkFullHouse()
    {
        bool check = false;
        int threeValueIndex = -1;
        for (int i = 0; i < this.values.Length; i++)
        {
            if (this.values[i] == 3)
            {
                threeValueIndex = i;
            }
        }
        if (threeValueIndex != -1)
        {
            for (int i = 0; i < threeValueIndex; i++)
            {
                if (this.values[i] == 2 || this.values[i] == 3)
                {
                    check = true;
                }
            }
            for (int i = threeValueIndex + 1; i < this.values.Length; i++)
            {
                if (this.values[i] == 2 || this.values[i] == 3)
                {
                    check = true;
                }
            }
        }
        return check;
    }

    // Checks for straight
    public bool checkStraight()
    {
        bool check = false;
        int confirmCount = 0;
        for (int i = 0; i < this.values.Length - 4; i++ )
        {
            for (int j = i; j < i+4; i++)
            {
                if (this.values[j] >= 1)
                {
                    confirmCount++;
                }    
            }
            if (confirmCount == 5)
            {
                check = true;
            }
            confirmCount = 0;
        }

        // Checks for straight beginning with ace
        for (int i = 12; i < 17; i++)
        {
            i = i % 13;
            if (this.values[i] >= 1)
            {
                confirmCount++;
            }
        }
        if (confirmCount == 5)
        {
            check = true;
        }
        confirmCount = 0;
        return check;
    }

    // Checks for three of a kind
    public bool checkThreeOfAKind()
    {
        bool check = false;
        for (int i = 0; i < this.values.Length; i++)
        {
            if (this.values[i] == 3)
            {
                check = true;
            }
        }
        return check;
    }

    // Checks for two pair
    public bool checkTwoPair()
    {
        bool check = false;
        int twoValueIndex = -1;
        for (int i = 0; i < this.values.Length; i++ )
        {
            if (this.values[i] == 2)
            {
                twoValueIndex = i;
            }
        }
        if (twoValueIndex != -1)
        {
            for (int i = 0; i < this.values[twoValueIndex]; i++)
            {
                if (this.values[i] == 2)
                {
                    check = true;
                }
            }
            for (int i = this.values[twoValueIndex] + 1; i < this.values.Length; i++)
            {
                if (this.values[i] == 2)
                {
                    check = true;
                }
            }
        }
        return check;
    }

    // Checks for pair
    public bool checkPair()
    {
        bool check = false;
        for (int i = 0; i < this.values.Length; i++) 
        { 
        if (this.values[i] == 2)
            {
                check = true;
            }
        }
        return check;
    }
}

// DETERMINE RANKRANK CLASS
// If two or more players with the same rank hand, determine rank inside of rank:
public class DetermineRankRank(Player[] players, int count, Card[] communityCards)
{
    // PROPERTIES
    private bool check = false;
    private int[] suits = new int[4];
    private int[] values = new int[13];

    // METHODS

    // Increments a suit count by one:
    public void addSuit(int suitNum) { this.suits[suitNum]++; }
    // Increments a value count by one:
    public void addValue(int valueNum) { this.values[valueNum]++; }

    // Gets the royal flush rank
    public void getRoyalFlushRank()
    {
        for (int i = 0; i < count; i++)
        {
            players[i].setRankRank(1);
        }
    }
    public void getFourOfAKindRank()
    {
        // WORK ON THIS PART!!!!
    }
}


// COMMUNITY CARDS CLASS
public class CommunityCards()
{
    // PROPERTIES
    private Card[] communityCards = new Card[5];
    private int count = 0;

    // GETTER
    public Card[] getCommunityCards() { return this.communityCards; }

    // SETTER
    public void setCommunityCard(Card card) 
    { 
        this.communityCards[this.count] = card;
        this.count++;
    }
}

// GAME CLASS
public class Game
{
    // PROPERTIES

    private int dealerId; // The Id of the player who is currently the dealer // MAKE INTO SEPARATE CLASS
    private int pot;                                                                 // MAKE INTO SEPARATE CLASS
    private CommunityCards communityCards = new CommunityCards();
    private TurnsQueue queue = new TurnsQueue();

    // METHODS
    // Constructor automatically generates cards:
    public Game()
    {
        // Used for checking while generating cards:
        CheckCards checkCards = new CheckCards();
        Player temp = new Player();
        int check = 0;
        bool check2 = false;

        // Generating community cards:

        for (int i = 0; i < 5; i++)
        {
            check2 = false;
            while (check2 == false)
            {
                Card card = new Card();
                for (int j = 0; j < checkCards.getCount(); j++)
                { 
                    if (card.getCardNum() == checkCards.getCardNums()[j])
                    {
                        check++;
                    }
                }
                if (check != 0)
                {
                    check2 = false;
                    check = 0;
                }
                else
                {
                    check2 = true;
                    check = 0;
                    communityCards.setCommunityCard(card);
                    checkCards.setCardNums(card.getCardNum());
                }

            }

        }

        // Generating player / bot cards:

        // Generating each player's card1:
        for (int i = 0; i < 6; i++) 
        {
            check2 = false;
            while (check2 == false)
            {
                Card card = new Card();
                for (int j = 0; j < checkCards.getCount(); j++)
                {
                    if (card.getCardNum() == checkCards.getCardNums()[j])
                    {
                        check++;
                    }
                }
                if (check != 0)
                {
                    check2 = false;
                    check = 0;
                }
                else
                {
                    check2 = true;
                    check = 0;
                    temp = queue.dequeue();
                    temp.setCard1(card);
                    queue.enqueue(temp);
                    checkCards.setCardNums(card.getCardNum());
                }

            }
        }

        // Generating each player's card2:
        for (int i = 0; i < 6; i++)
        {
            check2 = false;
            while (check2 == false)
            {
                Card card = new Card();
                for (int j = 0; j < checkCards.getCount(); j++)
                {
                    if (card.getCardNum() == checkCards.getCardNums()[j])
                    {
                        check++;
                    }
                }
                if (check != 0)
                {
                    check2 = false;
                    check = 0;
                }
                else
                {
                    check2 = true;
                    check = 0;
                    temp = queue.dequeue();
                    temp.setCard2(card);
                    queue.enqueue(temp);
                    checkCards.setCardNums(card.getCardNum());
                }

            }

        }

    }

    // METHODS
    // Assigns hand rankings to players and checks whether further ranking inside ranks is needed:

    public void assignHandRankings(Player[] players, Card[] communityCards)
    {

        // Iterates through players / bots and assigns them a rank:
        for (int i = 0; i < players.Length; i++)
        {
            DetermineHand determineHand = new DetermineHand();
            // Iterates through community cards to add all values and suits to arrays:
            for (int j = 0; j < communityCards.Length; j++)
            {
                determineHand.addValue(communityCards[j].getValue());
                determineHand.addSuit(communityCards[j].getSuit());
            }
            // Adds player's two card values:
            determineHand.addValue(players[i].getCard1().getValue());
            determineHand.addValue(players[i].getCard2().getValue());

            // Adds player's two card suits:
            determineHand.addSuit(players[i].getCard1().getSuit());
            determineHand.addSuit(players[i].getCard2().getSuit());

            // Determines hand:
            // Checks for same five suit (flush) first:
            if (determineHand.checkFiveSuit() == true)
            {
                if (determineHand.checkRoyalFlush() == true)
                {
                    players[i].setRank(9);
                }
                else if (determineHand.checkStraight() == true)
                {
                    players[i].setRank(8);
                }
                else
                {
                    players[i].setRank(5);
                }
            }
            // If not flush type, checks for any other hand type depending on values:
            else if (determineHand.checkFourOfAKind() == true)
            {
                players[i].setRank(7);
            }
            else if (determineHand.checkFullHouse() == true) 
            {
                players[i].setRank(6);
            }
            else if (determineHand.checkStraight() == true)
            {
                players[i].setRank(4);
            }
            else if (determineHand.checkThreeOfAKind() == true)
            {
                players[i].setRank(3);
            }
            else if (determineHand.checkTwoPair() == true)
            {
                players[i].setRank(2);
            }
            else if (determineHand.checkPair() == true)
            {
                players[i].setRank(1);
            }
            else
            {
                players[i].setRank(0);
            }
        }

        // To test whether more specific ranking is needed:
        int[] rankingsCount = new int[10];
        Player[] sameRankPlayers = new Player[6];
        int sameRankPlayersCount = 0;



        for (int i = 0; i < players.Length; i++)
        {
            rankingsCount[players[i].getRank()]++;
        }
        for (int i = 0; i < rankingsCount.Length; i++)
        {
            if (rankingsCount[i] > 1)
            {
                for (int j = 0; j < players.Length; j++)
                {
                    if (players[j].getRank() == rankingsCount[i])
                    {
                        sameRankPlayers[sameRankPlayersCount] = players[j];
                        sameRankPlayersCount++;
                    }
                }
                if (sameRankPlayersCount > 0)
                {
                    DetermineRankRank determineRankRank = new DetermineRankRank(sameRankPlayers, sameRankPlayersCount, communityCards);
                    // WORK ON THIS PART!!!!!



                    // Iterates through community cards to add all values and suits to arrays:
                    for (int j = 0; j < communityCards.Length; j++)
                    {
                        determineRankRank.addValue(communityCards[j].getValue());
                        determineRankRank.addSuit(communityCards[j].getSuit());
                    }
                    // Adds player's two card values:
                    determineRankRank.addValue(players[i].getCard1().getValue());
                    determineRankRank.addValue(players[i].getCard2().getValue());

                    // Adds player's two card suits:
                    determineRankRank.addSuit(players[i].getCard1().getSuit());
                    determineRankRank.addSuit(players[i].getCard2().getSuit());


                    switch (i)
                    {
                        case 0:  // HIGH CARD
                            break;
                        case 1:  // PAIR
                            break;
                        case 2:  // TWO PAIR
                            break;
                        case 3:  // THREE OF A KIND
                            break;
                        case 4:  // STRAIGHT
                            break;
                        case 5:  // FLUSH
                            break;
                        case 6:  // FULL HOUSE
                            break;
                        case 7:  // FOUR OF A KIND
                            break;
                        case 8:  // STRAIGHT FLUSH
                            break;
                        case 9:  // ROYAL FLUSH
                            determineRankRank.getRoyalFlushRank();
                            break;
                    }
                }
            }
        }
    }
}


// PLAYER TURNS QUEUE CLASS
// Also fills queue with Player / Bot objects:
public class TurnsQueue
{
    // Creates the array that functions as a queue:
    private Player[] players = new Player[6];

    // Properties of the queue:
    private int capacity = 6;
    private int count = 0;
    private int head = 0;
    private int tail = 5;


    // Constructor fills queue with one player and five bots on creation:
    public TurnsQueue()
    {
        // One player (Id 0):
        Player player = new Player();
        player.setId(0);
        this.players[0] = player;

        // Five bots (Ids 1-5):
        for (int i = 1; i < 6; i++)
        {
            Bot bot = new Bot();
            bot.setId(i);
            this.players[i] = bot;
        }
    }

    // Used to return whole queue (used only when determining hand rankings):
    public Player[] getPlayers() { return players; }

    // Used to set the whole queue:
    public void setPlayers(Player[] playersQueue) { players = playersQueue; }

    // Used to add player back into the queue:
    public void enqueue(Player player)
    {
        tail = (tail + 1) % capacity;
        players[tail] = player;
        count++;
    }

    // Used to remove player from the queue:
    public Player dequeue()
    {
        // Fill empty slot with -1 Id player:
        Player playerEmpty = new Player();
        playerEmpty.setId(-1);

        // Removing player from queue and adding empty player:
        Player temp = players[head];
        players[head] = playerEmpty;
        head = (head + 1) % capacity;
        count = count - 1;
        return temp;
    }
}

