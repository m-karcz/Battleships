using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ships
{
    class Board
    {
        /// <summary>
        /// Simple two-dimensional list do store ships
        /// </summary>
        internal List<List<Ship>> ships;
        public Board()
        {
            ships = new List<List<Ship>>();
            foreach (var i in Enumerable.Range(0, 10))
            {
                var line = new List<Ship>();
                foreach (var j in Enumerable.Range(0, 10))
                    line.Add(new Ship());
                ships.Add(line);
            }
        }
        /// <summary>
        /// Draw ships on given canvas, respectively to each ship state
        /// </summary>
        public void DrawShips(Canvas can)
        {
            can.Children.Clear();
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    var point = new System.Drawing.Point(x, y);
                    can.Children.Add(Ship2Can.GetCanvas(point, this[point].GetState()));
                }
            }
        }
        public System.Drawing.Point GetNearbyShipWithoutState(System.Drawing.Point location, ShipState state)
        {
            for (int xx = location.X > 0 ? -1 : 0; xx <= (location.X < 10 - 1 ? 1 : 0); xx++)
                for (int yy = location.Y > 0 ? -1 : 0; yy <= (location.Y < 10 - 1 ? 1 : 0); yy++)
                    if (ships[location.Y + yy][location.X + xx].GetState() == state)
                        return new System.Drawing.Point(location.X + xx, location.Y + yy);
            return new System.Drawing.Point(-1, -1);
        }
        /// <summary>
        /// Syntactic sugar used many times
        /// </summary>
        internal Ship this[System.Drawing.Point point]{
            get
            {
                //failsafe, sometimes wrong point is given, dunno why
                point.X = point.X < this.ships.Count ? point.X : this.ships.Count-1;
                point.X = point.X >= 0 ? point.X : 0;
                point.Y = point.Y < this.ships.Count ? point.Y : this.ships.Count -1;
                point.Y = point.Y >= 0 ? point.Y : 0;
                return this.ships[point.Y][point.X];
            }
            set
            {
                //failsafe, sometimes wrong point is given, dunno why
                point.X = point.X < this.ships.Count ? point.X : this.ships.Count-1;
                point.X = point.X >= 0 ? point.X : 0;
                point.Y = point.Y < this.ships.Count ? point.Y : this.ships.Count -1;
                point.Y = point.Y >= 0 ? point.Y : 0;
                this.ships[point.Y][point.X] = value;
            }
        }
    }
}
