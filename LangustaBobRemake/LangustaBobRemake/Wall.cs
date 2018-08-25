using System;
using System.Collections.Generic;
using System.Text;

namespace LangustaBobRemake
{
    class Wall : IPositionable
    {
        public Coordinates Position { get; }
        public ObjectCategory Category => ObjectCategory.DumbStand;
        public string[] CharacterRepresentation { get; } = {"XXXXX", "XXXXX", "XXXXX"};

        public Wall(Coordinates position)
        {
            Position = position;
        }
    }
}
