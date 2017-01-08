using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ships
{
    /// <summary>
    /// This type of Board is used in game, used by both players
    /// </summary>
    class GameBoard : Board
    {
        private GameBoard() : base() { }
        public static GameBoard PreparedToGameBoard(PreparingBoard prepared)
        {
            var gameBoard = new GameBoard();
            gameBoard.ships = prepared.ships;
            foreach (var line in gameBoard.ships)
                foreach (var ship in line)
                {
                    ship.SetState(ShipState.Hidden);
                }
            return gameBoard;
        }
        public bool IsHidden(System.Drawing.Point hit) => this[hit].GetState() == ShipState.Hidden;
        public bool TryToHit(System.Drawing.Point hit) => this[hit].isShip();
        public bool CheckIfSunk(System.Drawing.Point hit) => Ship.GetAllShipsInLine(this[hit].start, this[hit].end).All(point => this[point].GetState() == ShipState.Hit);
        public void SinkThem(System.Drawing.Point hit) => Ship.GetAllShipsInLine(this[hit].start, this[hit].end).ForEach(point => this[point].SetState(ShipState.Sunk));
        public void RevealSurroundings(System.Drawing.Point hit) => this[hit].GetNeighbourhood().ForEach(point => this[point].SetState(ShipState.MissOrEmpty));
        public void Missed(System.Drawing.Point hit) => this[hit].SetState(ShipState.MissOrEmpty);
        public bool IsWon() => hitsToGoal == 0;
        private int hitsToGoal = 20;
        public void Hit(System.Drawing.Point hit)
        {
            this[hit].SetState(ShipState.Hit);
            hitsToGoal--;
        }
    }
}
