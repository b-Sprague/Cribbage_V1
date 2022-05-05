using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cribbage_V1
{
    public class GamePlay
    {
        // Default
        public string playerNum = "";
        public int players = 2;

        private int k = 0;

        // Set up new deck
        Deck deck = new Deck();

        // Set up new board
        Board board = new Board();

        // Set up players and crib
        public Player player_1 = new Player();
        public Player cpu_1 = new Player();
        public Player cpu_2 = new Player();
        public Player crib = new Player();

        // List for players involved in game
        public List<Player> PlayerList = new List<Player>();

        // Set up new score for scoring hands
        public Scoring score = new Scoring();

        // New random variable
        Random rnd = new Random();



        public void Start()
        {
            // Set up player 
            player_1.SetUpPlayer("Player", ConsoleColor.Red, 12, 3);
            PlayerList.Add(player_1);

            #region PlayerNumber
            while (true)
            {
                Console.Write("How many players?: ");
                playerNum = Console.ReadLine();

                if (playerNum == "1")
                    Console.WriteLine("Invalid input. Try Again.");

                else if (playerNum == "2")
                {
                    players = 2;

                    // CPU hand
                    cpu_1.SetUpPlayer("CPU-1", ConsoleColor.Blue, 12, 4);
                    PlayerList.Add(cpu_1);

                    break;
                }

                else if (playerNum == "3")
                {
                    players = 3;

                    // CPU(s) hand(s)
                    cpu_1.SetUpPlayer("CPU-1", ConsoleColor.Blue, 12, 4);
                    PlayerList.Add(cpu_1);

                    cpu_2.SetUpPlayer("CPU-2", ConsoleColor.DarkGreen, 12, 5);
                    PlayerList.Add(cpu_2);

                    break;
                }
                else
                    Console.WriteLine("Invalid input. Try Again.");
            }
            #endregion

            // Set up crib
            crib.SetUpPlayer("Crib", ConsoleColor.Yellow, 0, 0);
            PlayerList.Insert(0, crib);

            // Set up board
            board.SetUp(players);
        }

        public void GameLoop()
        {
            // Initialize Deck
            deck.FillDeck();

            #region Game Loop

            while (true)
            {
                // Shuffle and Deal
                deck.ShuffleDeck(7);

                //deck.PrintDeck();

                board.SetBoard(PlayerList, 10, 3);
                Console.WriteLine("Player: " + player_1.score);
                Console.WriteLine("CPU: " + cpu_1.score);

                Console.ReadLine();

                deck.Deal(PlayerList);

                // Set Crib to Correct Player
                k++;

                if (k > PlayerList.Count - 1)
                    k = 1;

                PlayerList[k].isTurn = true;

                // Card Choice
                Console.ForegroundColor = ConsoleColor.White;
                foreach (Player player in PlayerList)
                {
                    if (player.playerName != "Crib")
                    {
                        Console.SetCursorPosition(0, 0);
                        player.DisplayHand();
                        player.ChoiceSelection(25, 25, players, "->", crib, true);
                    }
                    Console.Clear();
                }
                
                crib.SortHand();

                Console.SetCursorPosition(0, 0);

                // Split
                deck.SplitDeck();

                player_1.DisplayHand();
                cpu_1.DisplayHand();
                crib.DisplayHand();

                // {
                // Play

                // Hand and Crib Point Count 
                foreach (Player player in PlayerList)
                {
                    score.PointCalculator(player, deck.TopCard, true, players);

                    // Crib points
                    if (player.isTurn == true)
                    {
                        player.points += crib.points;
                        player.score += crib.score;
                        crib.score = 0;

                        PlayerList[k].isTurn = false;
                    }

                    if(player.playerName != "Crib")
                        player.MovePegs(players);

                    Console.WriteLine(player.playerName + " Score: " + player.score + "\n\r");                  
                }

                Console.ReadLine();
                // }

                // Return cards to deck
                deck.ReturnToDeck(PlayerList);

                Console.Clear();
            }

            #endregion

        }

        public void GameHand()
        {

        }
    }
}