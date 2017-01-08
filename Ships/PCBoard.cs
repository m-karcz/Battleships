using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ships
{
    class PCBoard : PreparingBoard
    {
        /// <summary>
        /// Fills board for human player with ships
        /// </summary>
        public PCBoard() : base()
        {
            this.shipSize = 4;
            while (!this.isReady())
            {
                horizontal = rand.Next(0, 2) == 0;
                var availableLocations = GetAvailableLocations();
                if (!availableLocations.Any())
                {
                    this.Clear();
                    continue;
                }
                PutNewShips(availableLocations[rand.Next(0, availableLocations.Count)]);
            }
        }
        Random rand = new Random(Guid.NewGuid().GetHashCode());
        List<System.Drawing.Point> GetAvailableLocations()
        {
            var result = new List<System.Drawing.Point>();
            for(int y=0; y<10; y++)
            {
                for(int x=0; x<10; x++)
                {
                    var newStart = new System.Drawing.Point(x, y);
                    var newEnding = new System.Drawing.Point(horizontal ? x + shipSize - 1 : x, horizontal ? y : y + shipSize - 1);
                    if(newEnding.X < 10 && newEnding.Y < 10)
                    {
                        if(Ship.GetAllShipsInLine(newStart, newEnding).All(point => CheckIfNeighbourhoodIsFree(point)))
                        {
                            result.Add(newStart);
                        }
                    }
                }
            }
            return result;
        }
        
    }
}
