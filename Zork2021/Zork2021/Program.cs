using System;

namespace Zork2021
{
    class Program
    {
        static void Main(string[] args)
        {
            Area a1 = new Area("test", "");
            Area a2 = new Area("other", "");
            //a1.AddConnection(Directions.North, a2);
            a1.AddConnection(Directions.North, a2, true);
        }
    }
}
