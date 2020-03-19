using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWeddingShop.AlexThornley.Navigation
{
    public class Compass
    {
        public IReadOnlyDictionary<CompassDirection, CompassDirection> LeftTurns { get; }

        public IReadOnlyDictionary<CompassDirection, CompassDirection> RightTurns { get; }

        public IReadOnlyDictionary<CompassDirection, CompassVector> Vectors { get; }

        public ISet<CompassDirection> ValidDirections => new HashSet<CompassDirection>(Vectors.Keys);

        public Compass()
        {
            LeftTurns = new Dictionary<CompassDirection, CompassDirection>
            {
                { CompassDirection.North, CompassDirection.West},
                { CompassDirection.West, CompassDirection.South},
                { CompassDirection.South, CompassDirection.East},
                { CompassDirection.East, CompassDirection.North}
            };

            RightTurns = LeftTurns.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

            var v = new[]
            {
                new CompassVector(CompassDirection.North, 0, 1 ),
                new CompassVector(CompassDirection.West, -1, 0 ),
                new CompassVector(CompassDirection.South, 0, -1 ),
                new CompassVector(CompassDirection.East, 1, 0 )
            };
            Vectors = v.ToDictionary(x => x.Direction, x => x);
        }        
    }
}
