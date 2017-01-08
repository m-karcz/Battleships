using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ships
{
    /// <summary>
    /// Preparing board is used later to produce GameBoard for human and pc player
    /// </summary>
    class PreparingBoard : Board
    {
        public PreparingBoard() : base()
        {
        }
        /// <summary>
        /// Move 'white', already putting ships on board
        /// </summary>
        public void MovePutting(System.Drawing.Point newStart)
        {
            var newEnd = getNewEnd(newStart);

            if (newEnd.X < 10 && newEnd.Y < 10)
            {
                foreach (var line in ships)
                    foreach (var ship in line)
                        if (ship.GetState() == ShipState.Putting)
                            ship.SetState(ShipState.Hidden);
                var shipList = Ship.GetAllShipsInLine(newStart, newEnd);
                if (shipList.All(point => this[point].GetState() == ShipState.Hidden))
                    shipList.ForEach(point => this[point].SetState(ShipState.Putting));
            }
        }
        protected System.Drawing.Point getNewEnd(System.Drawing.Point newStart) => new System.Drawing.Point(newStart.X + (horizontal ? shipSize - 1 : 0), newStart.Y + (!horizontal ? shipSize - 1 : 0));
        public bool CheckIfNeighbourhoodIsFree(System.Drawing.Point location)
        {
            return GetNearbyShipWithoutState(location, ShipState.Put).X == -1;
        }
        public bool PutNewShips(System.Drawing.Point newStart)
        {
            if (shipSize == 0)
            {
                return false;
            }
            var newEnd = getNewEnd(newStart);
            if (newEnd.X < 10 && newEnd.Y < 10)
            {
                var shipList = Ship.GetAllShipsInLine(newStart, newEnd);
                if(shipList.All(point => CheckIfNeighbourhoodIsFree(point))){
                    foreach(var point in shipList)
                    {
                        this[point].SetShip(newStart, newEnd);
                        this[point].SetState(ShipState.Put);
                    }
                    if (--size2toPut[shipSize] == 0)
                    {
                        var availables = size2toPut.Where(obj => obj.Value != 0);
                        shipSize = availables.Any() ? availables.First().Key : 0;
                        
                    }
                    return true;
                }
            }
            return false;
        }

        protected bool horizontal = true;
        public void RightClick()
        {
            horizontal = !horizontal;
        }
        private Dictionary<int, int> size2toPut = new Dictionary<int, int>
        {
            {1, 4},
            {2, 3},
            {3, 2},
            {4, 1}
        };
        protected int shipSize=1;
        public void IncreaseSize()
        {
            var bigger = size2toPut.Where(obj => obj.Key > shipSize && obj.Value!=0);
            if (bigger.Any())
            {
                shipSize = bigger.First().Key;
            }
        }
        public void DecreaseSize()
        {
            var bigger = size2toPut.Where(obj => obj.Key < shipSize && obj.Value!=0);
            if (bigger.Any())
            {
                shipSize = bigger.Last().Key;
            }
        }
        public void Clear()
        {
            foreach (var line in ships)
                foreach (var ship in line)
                {
                    ship.SetState(ShipState.Hidden);
                    ship.Clear();
                }
            foreach (var size in Enumerable.Range(1, 4))
                size2toPut[size] = 5 - size;
            shipSize = 1;
        }
        public bool isReady()
        {
            return shipSize == 0;
        }
    }
}
