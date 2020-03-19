using System;
using System.Collections.Generic;
using System.Text;

namespace TheWeddingShop.AlexThornley.Navigation
{
    public class Rover
    {
        public int X { get; private set; }

        public int Y { get; private set;  }

        public CompassDirection Facing { get; private set; }

        private readonly int maxX;

        private readonly int maxY;

        private readonly Compass compass;

        public Rover(int x, int y, CompassDirection facing, int maxX, int maxY, Compass compass)
        {
            this.compass = compass ?? throw new ArgumentNullException(nameof(compass));

            if (x < 0 || x > maxX)
                throw new ArgumentException(nameof(x));

            if (y < 0 || y > maxY)
                throw new ArgumentException(nameof(y));

            if (!compass.ValidDirections.Contains(facing))
                throw new ArgumentException(nameof(facing));

            X = x;
            Y = y;
            Facing = facing;
            this.maxX = maxX;
            this.maxY = maxY;     
        }

        public void Move()
        {
            var vector = compass.Vectors[Facing];
            int newX = X + vector.XVector;
            int newY = Y + vector.YVector;

            // what to do here? 
            // reasonable to ignore instruction to go out of bounds, and wait for new instruction
            // maybe raise an event or log warning somewhere
            if (newX < 0 || newX > maxX || newY < 0 || newY > maxY)
                return;

            X = newX;
            Y = newY;
        }

        public void TurnLeft() => Facing = compass.LeftTurns[Facing];

        public void TurnRight() => Facing = compass.RightTurns[Facing];
    }
}
