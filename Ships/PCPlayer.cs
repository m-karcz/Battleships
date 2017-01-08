using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ships
{
    class PCPlayer : Player
    {
        public PCPlayer(PreparingBoard board)
        {
            this.board = GameBoard.PreparedToGameBoard(board);
        }
        private Random rand = new Random(Guid.NewGuid().GetHashCode());
        private List<System.Drawing.Point> GetAllPossibleClicks() => Enumerable.Range(0, 10).Select(x => Enumerable.Range(0, 10).Select(y => new System.Drawing.Point(x, y))).SelectMany(p => p).Where(p => board[p].GetState() == ShipState.Hidden).ToList();
        /// <summary>
        /// Simulates click on random possible location on board
        /// </summary>
        private System.Drawing.Point GetClick()
        {
            var clicks = GetAllPossibleClicks();
            return clicks[rand.Next(0, clicks.Count)];
        }
        /// <summary>
        /// To remember last valid hit
        /// </summary>
        private System.Drawing.Point lastHit = new System.Drawing.Point(-1, -1);
        private Direction dir = Direction.Up;
        private bool IsValid(System.Drawing.Point point) => point.X >= 0 && point.X < 10 && point.Y >= 0 && point.Y < 10;
        /// <summary>
        /// Sets random direction from available and not tried yet
        /// </summary>
        private void SetNextDirection()
        {
            var availableDirections = ((Direction[])Enum.GetValues(typeof(Direction))).Where(dir => IsValid(lastHit + dir.ToSize()) && board[lastHit + dir.ToSize()].GetState() == ShipState.Hidden);
            dir = availableDirections.ElementAt(rand.Next(0, availableDirections.Count()));
        }
        private PCMoveState moveState = PCMoveState.Search;
        /// <summary>
        /// Moving action for pc player - moves as long as it is possible
        /// </summary>
        public void MakeMove()
        {
            while (true)
            {
                System.Drawing.Point hit;
                switch (moveState)
                {
                    case PCMoveState.Search:
                        hit = GetClick();
                        if (board.TryToHit(hit))
                        {
                            board.Hit(hit);
                            if (board.CheckIfSunk(hit))
                            {
                                board.SinkThem(hit);
                                board.RevealSurroundings(hit);
                            }
                            else
                            {
                                moveState = PCMoveState.HitOnce;
                                lastHit = hit;
                            }
                            break;
                        }
                        else
                        {
                            board.Missed(hit);
                            return;
                        }
                    case PCMoveState.HitOnce:
                    case PCMoveState.HitTwice:
                        if (moveState == PCMoveState.HitOnce)
                        {
                        SetNextDirection();
                        }
                        hit = lastHit + dir.ToSize();
                        if (board.TryToHit(hit))
                        {
                            board.Hit(hit);
                            if (board.CheckIfSunk(hit))
                            {
                                board.SinkThem(hit);
                                board.RevealSurroundings(hit);
                                moveState = PCMoveState.Search;
                            }
                            else
                            {
                                moveState = PCMoveState.HitTwice;
                                lastHit = hit;
                            }
                        }
                        else
                        {
                            board.Missed(hit);
                            if (moveState == PCMoveState.HitTwice)
                            {
                                moveState = PCMoveState.HitTwiceBackward;
                            }
                            return;
                        }
                        break;
                    case PCMoveState.HitTwiceBackward:
                            dir = dir.Opposite();
                            var tempHit = new System.Drawing.Point(lastHit.X, lastHit.Y);
                            while (board[tempHit].GetState() != ShipState.Hidden)
                            {
                                tempHit = tempHit + dir.ToSize();
                            }
                            hit = tempHit;
                        if (board.TryToHit(hit))
                        {
                            board.Hit(hit);
                            if (board.CheckIfSunk(hit))
                            {
                                board.SinkThem(hit);
                                board.RevealSurroundings(hit);
                                moveState = PCMoveState.Search;
                            }
                            else
                            {
                                moveState = PCMoveState.HitTwice;
                            }
                            break;
                        }
                        else
                        {
                            board.Missed(hit);
                            System.Diagnostics.Debug.WriteLine("niemożliwe");
                            return;
                        }

                }
            }
        }
    }
}
