using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cribbage_V1
{
    // From: https://stackoverflow.com/questions/53383468/creating-a-deck-of-cards-in-c-sharp

    public class Card
    {
        public enum Suites
        {
            Hearts = 0,
            Diamonds,
            Clubs,
            Spades
        }

        public int Value
        {
            get;
            set;
        }

        public Suites Suite
        {
            get;
            set;
        }

        //Used to get full name, also useful 
        //if you want to just get the named value
        public string NamedValue
        {
            get
            {
                string name = string.Empty;
                switch (Value)
                {
                    case (14):
                        name = "A";
                        break;

                    case (13):
                        name = "K";
                        break;

                    case (12):
                        name = "Q";
                        break;

                    case (11):
                        name = "J";
                        break;

                    default:
                        name = Value.ToString();
                        break;
                }

                return name;
            }
        }

        public string Name
        {
            get
            {
                return NamedValue + " / " + Suite.ToString();
            }
        }

        public Card(int Value, Suites Suite)
        {
            this.Value = Value;
            this.Suite = Suite;
        }
    }



    // Modded From: https://stackoverflow.com/questions/53383468/creating-a-deck-of-cards-in-c-sharp

    public class Deck
    {
        public Card TopCard;
        public Card Crib_Card = new Card(999, Card.Suites.Hearts);
        public List<Card> Cards = new List<Card>();

        public void Deal(List<Player> PlayersList)
        {
            foreach (Player player in PlayersList)
            {
                // Check: the game has two players, and the crib hand (empty)
                if (PlayersList.Count == 3)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (player.playerName == "Crib")
                        {
                            player.hand.Add(Crib_Card);
                            break;
                        }                        

                        player.hand.Add(Cards.ElementAt(i));
                        Cards.RemoveAt(i);
                    }
                }

                // Check: the game has three players, and the crib hand (1 card)
                else if (PlayersList.Count == 4)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (player.playerName == "Crib")
                        {
                            player.hand.Add(Cards.ElementAt(0));
                            Cards.RemoveAt(0);
                            break;
                        }

                        player.hand.Add(Cards.ElementAt(i));
                        Cards.RemoveAt(i);
                    }
                }

                player.SortHand();
            }
        }

        public void FillDeck()
        {
            //Can use a single loop utilising the mod operator % and Math.Floor
            //Using divition based on 13 cards in a suited
            for (int i = 0; i < 52; i++)
            {
                Card.Suites suite = (Card.Suites)(Math.Floor((decimal)i / 13));
                //Add 2 to value as a cards start a 2
                int val = i % 13 + 2;
                Cards.Add(new Card(val, suite));
            }
        }

        public void PrintDeck()
        {
            foreach (Card card in Cards)
            {
                if (card.Suite.ToString() == "Hearts" || card.Suite.ToString() == "Diamonds")
                    Console.ForegroundColor = ConsoleColor.Red;
                else
                    Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine(card.Name);
            }
        }



        // Modded From: https://stackoverflow.com/questions/1150646/card-shuffling-in-c-sharp
        static Random rnd = new Random();

        public void ShuffleDeck(int amount)
        {
            int x = 0;

            while (x < amount)
            {
                for (int i = Cards.Count - 1; i > 0; i--)
                {
                    int k = rnd.Next(i + 1);
                    Card temp = Cards.ElementAt(i);

                    Cards[i] = Cards[k];
                    Cards[k] = temp;
                }
                x++;
            }
        }



        int split = 0;

        public void SplitDeck()
        {
            Console.ForegroundColor = ConsoleColor.White;

            split = Cards.Count / 2;
            int k = rnd.Next(split - 5, split + 5);
            TopCard = Cards[k];

            Console.Write("Top Card: ");

            if(TopCard.Suite.ToString() == "Hearts" || TopCard.Suite.ToString() == "Diamonds")
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.White;

            Console.Write(TopCard.Name + "\n\r");

        }

        public void ReturnToDeck(List<Player> PlayerList)
        {
            foreach(Player player in PlayerList)
            {
                foreach(Card card in player.hand)
                    Cards.Add(card);

                player.hand.Clear();
            }
        }
    }
}
