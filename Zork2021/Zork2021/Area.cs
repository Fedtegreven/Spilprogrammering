using System;
using System.Collections.Generic;
using System.Text;

namespace Zork2021
{
    class Area
    {
        string description;
        string name;
        //todo add list of items
        //connections as key -> value       directions -> another area
        Dictionary<Directions, Area> connections = new Dictionary<Directions,Area>();

        public Area(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public void AddConnection(Directions direction, Area other)
        {
            if(!connections.ContainsKey(direction))
            connections.Add(direction, other);
        }

        public void AddConnection(Directions direction, Area other, bool bidirectional = false)
        {
            if (bidirectional) AddConnectionBidirectional(direction, other);
            else AddConnection(direction, other);
        }

        private void AddConnectionBidirectional(Directions direction, Area other)
        {
            Directions oppostie;
            switch(direction)
            {
                case Directions.North:
                    oppostie = Directions.South;
                    break;
                case Directions.South:
                    oppostie = Directions.North;
                    break;
                case Directions.West:
                    oppostie = Directions.East;
                    break;
                case Directions.East:
                    oppostie = Directions.West;
                    break;
                default:
                    oppostie = Directions.South;
                    break;
            }
            //add a connection between this and other with "direction"
            this.AddConnection(direction, other);
            //add a copnnection between other and this with "opposite"
            other.AddConnection(oppostie, this);
        }
    }

    public enum Directions
    {
        East, West, North, South
    }
}
