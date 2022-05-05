using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cribbage_V1
{
    public class Board
    {
        private int playerNum;

        public void SetUp(int players)
        {
            playerNum = players;
        }

        public void SetBoard(List<Player> PlayerList, int startX, int startY)
        {
            string startArea = "| . . | ";
            string endZone = ". | ";
            string row_H = ". . . . .";
            string border_H = "=" ;

            Console.Clear();

            #region Upper board
            // Draw upper border
            Console.SetCursorPosition(startX, startY - 1);
            for (int i = 0; i < 150; i++)
                Console.Write(border_H);

            Console.Write(@"\");

            // Draw starting area + horizontal strip
            for (int i = 0; i < playerNum; i++)
            {
                Console.SetCursorPosition(startX, startY + i);
                Console.Write(startArea);

                for (int j = 0; j < 11; j++)
                {
                    Console.Write(row_H + " | ");
                }
                Console.WriteLine(row_H + @" \");
            }

            // Draw lower border
            Console.SetCursorPosition(startX, startY + playerNum);
            for (int i = 0; i < 150; i++)
                Console.Write(border_H);

            Console.Write(@"\");

            // Draw line numbers
            for (int i = 1; i < 13; i++)
            {
                Console.SetCursorPosition((startX + 6) + (12 * i), startY + playerNum + 1);
                Console.Write(5 * i);
            }
            #endregion

            #region Lower board
            // Draw upper secondary border
            Console.SetCursorPosition(startX, startY + (playerNum + 4));
            Console.Write(@"\");

            for (int i = 0; i < 148; i++)
                Console.Write(border_H);

            // Draw endzone + horizontal strip
            for (int i = 0; i < playerNum; i++)
            {
                Console.SetCursorPosition(startX, startY + (playerNum + 5 + i));
                Console.Write(@"\ ");

                for (int j = 0; j < 12; j++)
                {
                    Console.Write(row_H + " | ");
                }
                Console.WriteLine(endZone);
            }

            // Draw lower secondary border
            if(playerNum == 2)
                Console.SetCursorPosition(startX, startY + (playerNum + 7));
            else
                Console.SetCursorPosition(startX, startY + (playerNum + 8));

            Console.Write(@"\");

            for (int i = 0; i < 148; i++)
                Console.Write(border_H);

            // Draw line numbers, cont.
            for (int i = 0; i < 13; i++)
            {
                if(playerNum == 2)
                    Console.SetCursorPosition((startX - 1) + (12 * i), startY + (playerNum + 8));
                else
                    Console.SetCursorPosition((startX - 1) + (12 * i), startY + (playerNum + 9));

                Console.Write(60 + (5 * i));
            }
            #endregion

            //Take player peg coords and colors and apply to board
            foreach (Player player in PlayerList)
            {
                if (player.playerName != "Crib")
                {
                    Console.ForegroundColor = player.pegColor;

                    // Draws the pegs on the board
                        Console.SetCursorPosition(player.peg_2[0], player.peg_2[1]);
                        Console.WriteLine("2");

                        Console.SetCursorPosition(player.peg_1[0], player.peg_1[1]);
                        Console.WriteLine("1");
                }
            }
        }        
    }
}
