using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cribbage_V1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            GamePlay gP = new GamePlay();
            gP.Start();
            gP.GameLoop();


            Console.Read();
        }
    }
}
