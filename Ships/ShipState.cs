using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ships
{
    /// <summary>
    /// Enum used to determine ship state
    /// </summary>
    enum ShipState
    {
        MissOrEmpty,
        Hidden,
        Hit,
        Sunk,
        Putting,
        Put
    }
}
