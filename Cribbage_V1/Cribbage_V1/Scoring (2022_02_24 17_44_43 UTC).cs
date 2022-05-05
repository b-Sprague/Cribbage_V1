using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cribbage_V1
{
    public class Scoring
    {
        int value;
        int points;

        int numOfFifteens;
        int numOfRuns;
        int numOfPairs;
        int numOfTriple;
        int numOfQuad;
        int sameSuit;
        int rightJack;

        bool run_5;
        bool run_4;
        bool isQuad;
        bool isTriple;

        public void BestHand()
        {

        }

        public void BestToss()
        {

        }

        public void RoundPoints()
        {

        }

        public void PointCalculator(Player Player, Card TopCard, bool displayPoints, int players)
        {
            value = 0;
            points = 0;

            numOfFifteens = 0;
            numOfRuns = 0;
            numOfPairs = 0;
            numOfTriple = 0;
            numOfQuad = 0;
            sameSuit = 0;
            rightJack = 0;

            run_5 = false;
            run_4 = false;
            isQuad = false;
            isTriple = false;

            List<int> totalValue = new List<int>();
            Card tempTC = TopCard;

            // Add: top card to player hand and sort
            Player.hand.Add(tempTC);
            Player.SortHand();

            #region 15s

            // Take: player cards, determine value and add it to the temporary list
            foreach (Card card in Player.hand)
            {
                // J, Q, K
                if (card.Value == 11 || card.Value == 12 || card.Value == 13)
                    value = 10;

                // A
                else if (card.Value == 14)
                    value = 1;

                else
                    value = card.Value;

                totalValue.Add(value);
            }

            for (int i = 0; i < Player.hand.Count; i++)
            {
                for (int j = i + 1; j < Player.hand.Count; j++)
                {
                    if (totalValue[i] + totalValue[j] == 15)
                        numOfFifteens++;

                    for (int k = j + 1; k < Player.hand.Count; k++)
                    {
                        if (totalValue[i] + totalValue[j] + totalValue[k] == 15)
                            numOfFifteens++;

                        for (int l = k + 1; l < Player.hand.Count; l++)
                        {
                            if (totalValue[i] + totalValue[j] + totalValue[k] + totalValue[l] == 15)
                                numOfFifteens++;

                            for (int m = l + 1; m < Player.hand.Count; m++)
                            {
                                if (totalValue[i] + totalValue[j] + totalValue[k] + totalValue[l] + totalValue[m] == 15)
                                    numOfFifteens++;
                            }
                        }
                    }

                }
            }

            totalValue.Clear();

            points += numOfFifteens * 2;
            #endregion

            #region Runs
            // Runs cannot be more than one type: ie a run of 4 does not contain two runs of 3

            // Run for 5 cards
            if (Player.hand[0].Value + 1 == Player.hand[1].Value && Player.hand[1].Value + 1 == Player.hand[2].Value
                && Player.hand[2].Value + 1 == Player.hand[3].Value && Player.hand[3].Value + 1 == TopCard.Value)
            {
                points += 5;
                numOfRuns++;
                run_5 = true;
            }

            if (run_5 == false)
            {
                // Run for 4 cards
                for (int i = 0; i < Player.hand.Count - 3; i++)
                {
                    for (int j = i + 1; j < Player.hand.Count - 2; j++)
                    {
                        for (int k = i + 2; k < Player.hand.Count - 1; k++)
                        {
                            for (int l = i + 3; l < Player.hand.Count; l++)
                            {
                                if (Player.hand[i].Value + 1 == Player.hand[j].Value && Player.hand[j].Value + 1 == Player.hand[k].Value
                                    && Player.hand[k].Value + 1 == Player.hand[l].Value)
                                {
                                    points += 4;
                                    numOfRuns++;
                                    run_4 = true;
                                }
                            }
                        }
                    }
                }
            }

            if (run_5 == false && run_4 == false)
            {
                // Runs for 3 cards
                for (int i = 0; i < Player.hand.Count - 2; i++)
                {
                    for (int j = i + 1; j < Player.hand.Count - 1; j++)
                    {
                        for (int k = i + 2; k < Player.hand.Count; k++)
                        {
                            if (Player.hand[i].Value + 1 == Player.hand[j].Value && Player.hand[j].Value + 1 == Player.hand[k].Value)
                            {
                                points += 3;
                                numOfRuns++;
                            }
                        }
                    }
                }
            }
            #endregion

            #region Pairs

            // 4 of a Kind
            for (int i = 0; i < Player.hand.Count - 4; i++)
            {
                for (int j = i + 1; j < Player.hand.Count - 3; j++)
                {
                    for (int k = i + 2; k < Player.hand.Count - 2; k++)
                    {
                        for (int l = i + 3; l < Player.hand.Count - 1; l++)
                        {
                            if (Player.hand[i].Value == Player.hand[j].Value && Player.hand[j].Value == Player.hand[k].Value 
                                && Player.hand[k].Value == Player.hand[l].Value)
                            {
                                numOfQuad++;
                                isQuad = true;
                                break;
                            }
                        }
                    }
                }
            }

            points += numOfQuad * 12;

            if (isQuad == false)
            {
                // 3 of a Kind
                for (int i = 0; i < Player.hand.Count - 3; i++)
                {
                    for (int j = i + 1; j < Player.hand.Count - 2; j++)
                    {
                        for (int k = i + 2; k < Player.hand.Count - 1; k++)
                        {
                            if (Player.hand[i].Value == Player.hand[j].Value && Player.hand[j].Value == Player.hand[k].Value)
                            {
                                numOfTriple++;
                                isTriple = true;
                                break;
                            }
                        }
                    }
                }

                points += numOfTriple * 6;
            }

            if (isQuad == false && isTriple == false)
            {
                // 2 of a Kind
                for (int i = 0; i < Player.hand.Count - 1; i++)
                {
                    for (int j = i + 1; j < Player.hand.Count; j++)
                    {
                        if (Player.hand[i].NamedValue == Player.hand[j].NamedValue && Player.hand[i].Name != Player.hand[j].Name)
                            numOfPairs++;
                    }
                }

                points += numOfPairs * 2;
            }
            #endregion

            #region Same Suit

            if (Player.hand[0].Suite == Player.hand[1].Suite && Player.hand[1].Suite == Player.hand[2].Suite
                && Player.hand[2].Suite == Player.hand[3].Suite && Player.hand[3].Suite == Player.hand[4].Suite
                && Player.hand[4].Suite == TopCard.Suite)
            {
                sameSuit = 5;
                points += 5;
            }
            else
                sameSuit = 0;

            #endregion

            // Remove: top card from player hand and sort
            Player.hand.Remove(tempTC);
            Player.SortHand();

            #region RightJack

            if (TopCard.NamedValue == "J / Spades")
            {
                rightJack = 1;
                points += 1;
            }
            else
                rightJack = 0;

            #endregion

            Console.ForegroundColor = ConsoleColor.DarkYellow;

            if (displayPoints == true)
                DisplayPoints();
            else
                Console.WriteLine("\n\r" + " Total Points: " + points);

            Player.score += points;
            Player.points = points;
        }

        public void DisplayPoints()
        {          
            // 15s
            Console.WriteLine("");
            Console.WriteLine("15s: " + numOfFifteens);

            // Runs
            Console.WriteLine("Runs: " + numOfRuns);

            // Pairs
            Console.WriteLine("Pairs: " + numOfPairs);
            Console.WriteLine("Triple: " + numOfTriple);
            Console.WriteLine("Quad: " + numOfQuad);

            // Same Suit
            Console.WriteLine("Same Suit: " + sameSuit);

            // Right Jack
            Console.WriteLine("Right facing Jack: " + rightJack);

            // Total Points
            Console.WriteLine("Total Points: " + points);
        }
    }
}