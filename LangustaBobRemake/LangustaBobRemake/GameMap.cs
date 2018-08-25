using System;
using System.Collections.Generic;

namespace LangustaBobRemake
{
    internal class GameMap
    {
        public Dictionary<Coordinates,IPositionable> CurrentMap = new Dictionary<Coordinates, IPositionable>();
    }
}