using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ships
{
    public static class Extensions
    {
        public static System.Drawing.Size ToSize(this Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return new System.Drawing.Size(0, -1);
                case Direction.Right:
                    return new System.Drawing.Size(1, 0);
                case Direction.Down:
                    return new System.Drawing.Size(0, 1);
                case Direction.Left:
                    return new System.Drawing.Size(-1, 0);
                default:
                    return new System.Drawing.Size(0, 0);
            }
        }
        public static Direction Opposite(this Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Right:
                    return Direction.Left;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Left:
                    return Direction.Right;
                default:
                    return Direction.Down;
            }
        }
    }
}
