using System;
using System.Collections.Generic;
using System.Text;
using TheWeddingShop.AlexThornley.Parsing;

namespace TheWeddingShop.AlexThornley
{
    public class RoverSimParseResult
    {
        private RoverSimParseResult() { }

        public bool Ok { get; private set; }

        public string ParseError { get; private set; }

        public int MaxX { get; private set; }

        public int MaxY { get; private set; }

        public RoverSpec[] RoverSpecs { get; private set; }

        public static RoverSimParseResult OK(int maxX, int maxY, RoverSpec[] roverSpecs) => new RoverSimParseResult
        {
            Ok = true,
            ParseError = null,
            MaxX = maxX,
            MaxY = maxY,
            RoverSpecs = roverSpecs ?? throw new ArgumentNullException(nameof(roverSpecs))
        };

        public static RoverSimParseResult Fail(string error) => new RoverSimParseResult
        {
            Ok = false,
            ParseError = error
        };
    }
}
