using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ships
{
    class Ship
    {
        ShipState _state = ShipState.Hidden;
        bool shipExistence = false;
        public ShipState GetState()
        {
            return _state;
        }
        public void SetState(ShipState newState)
        {
            _state = newState;
        }
        public bool isShip()
        {
            return shipExistence;
        }
        public void SetShip(System.Drawing.Point newStart, System.Drawing.Point newEnd)
        {
            shipExistence = true;
            start = newStart;
            end = newEnd;
        }
        public System.Drawing.Point start = new System.Drawing.Point(-1, -1);
        public System.Drawing.Point end = new System.Drawing.Point(-1, -1);
        public static List<System.Drawing.Point> GetAllShipsInLine(System.Drawing.Point start, System.Drawing.Point end)
        {
            var pointsList = new List<System.Drawing.Point>();
            if (start.X == end.X)
                for (int y = start.Y; y <= end.Y; y++)
                    pointsList.Add(new System.Drawing.Point(start.X, y));
            else
                for (int x = start.X; x <= end.X; x++)
                    pointsList.Add(new System.Drawing.Point(x, start.Y));
            return pointsList;
        }
        public List<System.Drawing.Point> GetAllShipsInLine()
        {
            return Ship.GetAllShipsInLine(start, end);
        }
        public List<System.Drawing.Point> GetNeighbourhood()
        {
            var line = this.GetAllShipsInLine();
            var neighbourhood = new HashSet<System.Drawing.Point>();
            foreach(var ship in line)
            {
                for (int x = ship.X == 0 ? 0 : -1; x <= (ship.X < 10 - 1 ? 1 : 0); x++)
                    for (int y = ship.Y == 0 ? 0 : -1; y <= (ship.Y < 10 - 1 ? 1 : 0); y++)
                        neighbourhood.Add(new System.Drawing.Point(ship.X + x, ship.Y + y));
            }
            neighbourhood.ExceptWith(line);
            return neighbourhood.ToList();
        }
        public void Clear()
        {
            shipExistence = false;
            start = new System.Drawing.Point(-1, -1);
            end = new System.Drawing.Point(-1, -1);
        }
        
    }
}
