using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ships
{
    class Player
    {
        protected GameBoard board;
        public bool IsWon() => board.IsWon();
        public void Draw(System.Windows.Controls.Canvas can) => board.DrawShips(can);
    }
}
