using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ships
{
    /// <summary>
    /// Used for state machine for pc player moving
    /// </summary>
    enum PCMoveState
    {
        Search,
        HitOnce,
        HitTwice,
        HitTwiceBackward
    }
}
