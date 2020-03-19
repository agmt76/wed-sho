using System;
using System.Collections.Generic;
using System.Text;
using TheWeddingShop.AlexThornley.Navigation;

namespace TheWeddingShop.AlexThornley.Parsing
{
    public class RoverSpec
    {
        public RoverSpec(int x, int y, CompassDirection facing, RoverInstruction[] instructions)
        {
            X = x;
            Y = y;
            Facing = facing;
            Instructions = instructions ?? throw new ArgumentNullException(nameof(instructions));
        }

        public int X { get; }

        public int Y { get; }

        public CompassDirection Facing { get; }

        public RoverInstruction[] Instructions { get; }
    }
}
