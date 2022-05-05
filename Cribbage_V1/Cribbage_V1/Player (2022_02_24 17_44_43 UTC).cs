using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cribbage_V1
{
    public class Player
    {
        // Public stuff
        public int score;
        public int points;
        public string playerName;
        public bool isTurn;
        public bool isWinner;
        public List<Card> hand;

        public int[] peg_1;
        public int[] peg_2;
        public ConsoleColor pegColor;

        // Private stuff
        private int spaces;
        private List<int> indexes;

        public void SetUpPlayer(string Name, ConsoleColor Color, int startX, int startY)
        {
            hand = new List<Card>();
            playerName = Name;
            score = 0;
            points = 0;

            spaces = 0;

            peg_1 = new int[] {startX + 2, startY};
            peg_2 = new int[] {startX, startY};

            pegColor = Color;

            isTurn = false;
            isWinner = false;
        }

        public void DisplayHand()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(playerName);

            foreach (Card card in hand)
            {
                // Change: color of suits changes from red to white
                if (card.Suite.ToString() == "Hearts" || card.Suite.ToString() == "Diamonds")
                    Console.ForegroundColor = ConsoleColor.Red;
                else
                    Console.ForegroundColor = ConsoleColor.White;

                // Catch: crib contains card that is not part of deck
                if (card.Value == 999)
                    Console.WriteLine("[BLANK]");
                else
                    Console.WriteLine(card.Name);
            }

            Console.WriteLine("");
        }

        public void SortHand()
        {
            int x = 0;
            int count = 0;
            int currentNum = 0;
            int prevNum = 0;

            bool isSorted = false;

            Card card = hand.ElementAt(0);
            Card prevCard = card;

            // Sorting Loop: swaps the first value with the second if the first is greater than the second
            while (true)
            {
                // Catch: if x-value is greater than the capacity of the list
                if (x >= hand.Count - 1)
                    x = 0;

                // Check: if x-values in list are sorted numerically ascending
                for (int i = 0; i < hand.Count; i++)
                {
                    if (i == hand.Count - 1)
                    {
                        //Console.ForegroundColor = ConsoleColor.White;
                        //Console.WriteLine("Iterations: " + count);
                        isSorted = true;
                        break;
                    }

                    prevNum = hand[i].Value;
                    currentNum = hand[i + 1].Value;

                    if (prevNum > currentNum)
                        break;
                }

                // Check: if the list is sorted
                if (isSorted == true)
                    break;

                prevNum = hand[x].Value;
                currentNum = hand[x + 1].Value;

                if (prevNum > currentNum)
                {
                    card = hand.ElementAt(x);
                    hand.RemoveAt(x);
                    hand.Insert(x + 1, card);
                }
                x++;
                count++;
            }
        }

        // bool isCrib: for determining if the card(s) is being sent to the crib or for play
        public void ChoiceSelection(int x, int y, int players, string pointer, Player Crib, bool isCrib) 
        {
            int cardChoices = 0;
            int picks = 0;

            int index = 0;

            // Set top and bottom
            int top = y;
            int bottom = y + (hand.Count - 1);

            bool isFinished = false;

            indexes = new List<int>();

            Console.CursorVisible = false;
            ConsoleKeyInfo input;

            // Determine the number of picks based on player amount
            if (players == 2)
                picks = 2;
            else
                picks = 1;

            // Print: current hand is printed away from the displayed hand
            for(int i = 0; i < hand.Count; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.WriteLine(hand[i].Name);
            }

            Console.SetCursorPosition(x, bottom + 1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[Use [W] to go up, [S] to go down, and [SPACE] to choose card] ");

            Console.SetCursorPosition(x, bottom + 2);         
            Console.WriteLine("[Press [ENTER] when cards have been chosen]");

            Console.ForegroundColor = ConsoleColor.DarkGreen;

            #region Choice Selection
            // Choice selection algorithm
            while (isFinished == false)
            {
                Console.SetCursorPosition(x, bottom + 3);
                Console.WriteLine("Card choices remaining: " + (picks - cardChoices));

                Console.SetCursorPosition(x - 3, y);
                Console.Write(pointer);

                input = Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.W:
                        Console.SetCursorPosition(x + hand[index].Name.Length + 1, y);
                        Console.Write(" ");

                        Console.SetCursorPosition(x - 3, y);
                        Console.Write("   ");

                        y--;

                        if (y < top)
                            y = bottom;

                        Console.SetCursorPosition(x - 3, y);
                        Console.Write(pointer);

                        index--;

                        if (index < 0)
                            index = hand.Count - 1;

                        break;

                    case ConsoleKey.S:
                        Console.SetCursorPosition(x + hand[index].Name.Length + 1, y);
                        Console.Write(" ");

                        Console.SetCursorPosition(x - 3, y);
                        Console.Write("   ");

                        y++;

                        if (y > bottom)
                            y = top;

                        Console.SetCursorPosition(x - 3, y);
                        Console.Write(pointer);

                        index++;

                        if (index > hand.Count - 1)
                            index = 0;

                        break;

                    case ConsoleKey.Spacebar:
                        // Makes sure no more cards are added than the amount required
                        if (cardChoices == picks && !indexes.Contains(index))
                        {
                            Console.SetCursorPosition(x, bottom + 4);
                            Console.WriteLine("Cannot get rid of anymore cards.");
                            break;
                        }
                        else
                        {
                            Console.SetCursorPosition(x + hand[index].Name.Length, y);
                            Console.Write("*");

                            // When the current index already exists in the list and the spacebar is pressed,
                            // then it is removed and the star is removed
                            if (indexes.Contains(index))
                            {
                                Console.SetCursorPosition(x + hand[index].Name.Length, y);
                                Console.Write(" ");
                                indexes.Remove(index);
                                cardChoices--;
                            }
                            else
                            {
                                indexes.Add(index);
                                cardChoices++;
                            }
                            break;
                        }                   

                    case ConsoleKey.Enter:
                        if(cardChoices == picks)
                            isFinished = true;

                        break;
                }
            }
            #endregion

            if (isCrib == true)
                ToTheCrib(Crib);

            //else: go to playHand (gameplay "hand")
        }

        public void ToTheCrib(Player Crib)
        {
            // Check: takes the indexes and removes them from the player hand and puts them in the crib "hand"
            if (indexes.Count > 1)
            {
                if(Crib.hand[0].Value == 999)
                    Crib.hand.RemoveAt(0);

                // Check: makes sure index[0] value is greater than index[1] value
                if(indexes[0] > indexes[1])
                {
                    Crib.hand.Add(hand[indexes[0]]);
                    Crib.hand.Add(hand[indexes[1]]);

                    hand.RemoveAt(indexes[0]);
                    hand.RemoveAt(indexes[1]);
                }
                // Check: makes sure index[0] value is lesse than index[1] value
                else
                {
                    Crib.hand.Add(hand[indexes[1]]);
                    Crib.hand.Add(hand[indexes[0]]);

                    hand.RemoveAt(indexes[1]);
                    hand.RemoveAt(indexes[0]);
                }
            }
            // Default order if the index count is 1 (for 3 players)
            else
            {
                Crib.hand.Add(hand[indexes[0]]);
                hand.RemoveAt(indexes[0]);
            }
        }

        public void MovePegs(int playerNum)
        {
            // Spaces equal points
            spaces = points;

            #region Back Peg Move
            // Check which peg is behind the other
            // Move the back peg up to first peg, then the move the correct amount of spaces
            if (peg_1[0] > peg_2[0])
            {
                if ((peg_2[0] == 12 && peg_2[1] == 3) || (peg_2[0] == 12 && peg_2[1] == 4))
                {
                    if (points == 0)
                        peg_2[0] = 12;
                    else
                        peg_2[0] = 16;
                }
                else
                    peg_2[0] = peg_1[0];

                // If the score is greater than the 60 line
                if (score > 60)
                {
                    peg_2[1] += playerNum + 5;
                    peg_2[0] = 12 + (score - 61);
                }

                // If the score is divisible by 5, then reduce by 1 to account for the 5-line;
                for(int i = 0; i < score; i += 5)
                {
                    if (score % 5 == 0)
                        spaces -= 1;
                    else
                        spaces++;
                }

                // Checks if the peg has moved past a 5-line
                //for (int i = 1; i < 14; i++)
                //    if (points >= 5 * i)
                //        spaces++;

                peg_2[0] += spaces * 2;
            }
            #endregion

            #region Front Peg Move
            else if (peg_1[0] < peg_2[0])
            {
                if (points == 0)
                    peg_1[0] = 14;
                else
                    peg_1[0] = peg_2[0];

                // If the score is greater than the 60 line
                if (score > 60)
                {
                    peg_1[1] += playerNum + 5;
                    peg_1[0] = 12 + (score - 61);
                }

                // If the score is divisible by 5, then reduce by 1 to account for the 5-line;
                if (score % 5 == 0)
                    spaces -= 1;

                // Checks if the peg has moved past a 5-line
                //for (int i = 1; i < 14; i++)
                //    if (points >= 5 * i)
                //        spaces++;

                peg_1[0] += spaces * 2;
            }
            #endregion

        }



        #region For the AI
        public void LowestPointThrow()
        {
            // Throws cards that gives it the least amount of points
        }

        public void AveragePointThrow()
        {
            // Throws cards that gives it the average amount of points (highest + lowest / 2)
        }

        public void HighestPointThrow()
        {
            // Throws cards that gives it the highest amount of points
        }
        #endregion
    }
}
