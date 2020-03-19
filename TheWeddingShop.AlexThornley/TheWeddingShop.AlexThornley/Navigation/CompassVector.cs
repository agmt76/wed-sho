using System;
using System.Collections.Generic;
using System.Text;

namespace TheWeddingShop.AlexThornley.Navigation
{
    public class CompassVector
    {
        public CompassVector(CompassDirection direction, short xVector, short yVector)
        {
            if (xVector < -1 || xVector > 1)
                throw new ArgumentOutOfRangeException(nameof(xVector));
            if (yVector < -1 || yVector > 1)
                throw new ArgumentOutOfRangeException(nameof(yVector));

            Direction = direction;
            XVector = xVector;
            YVector = yVector;
        }

        public CompassDirection Direction { get; }

        public short XVector { get; }

        public short YVector { get; }
    }
}
