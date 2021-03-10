using System;
using System.Collections.Generic;
using System.Text;

namespace Zork2021
{
    class Area
    {
        public string name;
        public string description;
        //todo add list of items

        //connections as key -> value       direction -> another area
        Dictionary<Directions, Area> connections = new Dictionary<Directions, Area>();

        public Area(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public void AddConnection(Directions direction, Area other)
        {
            if (!connections.ContainsKey(direction))
                connections.Add(direction, other);
        }

        //Kirk didn't like having two separate methods for adding connections (with different names) :C 
        //so here's a "combination" of them, where you choose if the link is bidirectional using an extra parameter (false by default)
        public void AddConnection(Directions direction, Area other, bool bidirectional = false)
        {
            if (bidirectional)
                AddConnectionBidirectional(direction, other);
            else
                AddConnection(direction, other);
        }

        private void AddConnectionBidirectional(Directions direction, Area other)
        {
            Directions opposite;
            switch (direction)
            {
                case Directions.North:
                    opposite = Directions.South;
                    break;
                case Directions.South:
                    opposite = Directions.North;
                    break;
                case Directions.East:
                    opposite = Directions.West;
                    break;
                case Directions.West:
                    opposite = Directions.East;
                    break;
                default:
                    opposite = Directions.South;
                    break;
            }
            //add a connection between this and other with "direction"
            this.AddConnection(direction, other);
            //add a connection between other and this with "opposite"
            other.AddConnection(opposite, this);
        }
    }

    public enum Directions
    {
        East, West, North, South
    }
}
