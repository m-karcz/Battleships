using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ships
{
    class Human : Player
    {
        public Human(PreparingBoard board)
        {
            this.board = GameBoard.PreparedToGameBoard(board);
        }
        /// <summary>
        /// Makes move on board of player, returns true if hit was successful
        /// </summary>
        public bool MakeMove(System.Drawing.Point hit)
        {
            if (board.TryToHit(hit))
            {
                board.Hit(hit);
                if (board.CheckIfSunk(hit))
                {
                    board.SinkThem(hit);
                    board.RevealSurroundings(hit);
                }
                return true;
            }
            else
            {
                board.Missed(hit);
                return false;
            }
        }
        /// <summary>
        /// Checks if player tries to hit valid point
        /// </summary>
        public bool IsHidden(System.Drawing.Point hit) => board.IsHidden(hit);
    }
}
