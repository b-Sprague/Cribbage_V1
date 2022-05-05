using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cribbage_V1
{
    class Board
    {
        public static void new_Board()
        {
            #region Horizontal Boundaries
            B_Horizontal(0, 2, "-", 54);

            B_Horizontal(0, 7, "-", 49);
            B_Horizontal(0, 13, "-", 49);

            B_Horizontal(0, 18, "-", 54);
            #endregion

            #region Horizontal Traacks
            B_Horizontal_Track(0, 4, " ", 50, ConsoleColor.Red);
            B_Horizontal_Track(0, 5, " ", 50, ConsoleColor.Green);

            B_Horizontal_Track(4, 9, " ", 11, ConsoleColor.Red);
            B_Horizontal_Track(4, 10, " ", 11, ConsoleColor.Green);

            B_Horizontal_Track(4, 16, " ", 46, ConsoleColor.Red);
            B_Horizontal_Track(4, 15, " ", 46, ConsoleColor.Green);
            #endregion
        }

        public static void B_Horizontal(int x, int y, string wallV, int end)
        {
            int NewX = x;
            int Counter = 0;

            while (true)
            {
                Console.SetCursorPosition(NewX, y);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(wallV);

                if (Counter == end)
                    break;

                else
                {
                    NewX++;
                    Counter++;
                }
            }
        }

        public static void B_Horizontal_Track(int x, int y, string wallV, int end, ConsoleColor color)
        {
            int NewX = x;
            int Counter = 0;

            while (true)
            {
                Console.SetCursorPosition(NewX, y);
                Console.BackgroundColor = color;
                Console.WriteLine(wallV);

                if (Counter == end)
                    break;

                else
                {
                    NewX++;
                    Counter++;
                }
            }
        }
    }
}
