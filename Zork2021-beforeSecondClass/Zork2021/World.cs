using System;
using System.Collections.Generic;
using System.Text;

namespace Zork2021
{
    class World
    {
        //we need to hold all the areas
        List<Area> allAreas;

        //current area the player is in
        public Area currentArea;

        //when we make it we should: make areas, and make connections
        public World(){
            //todo: build the map and set the current area
        }

        /// <summary>
        /// Prints in the console the description of the current area
        /// </summary>
        internal void PrintCurrentAreaDescription()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tries to move the player ot a different area by moving in the specified direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns>true if successful, false if the move was not possible</returns>
        internal bool Go(Directions direction)
        {
            throw new NotImplementedException();
        }
    }
}
