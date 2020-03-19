using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheWeddingShop.AlexThornley.Navigation;

namespace TheWeddingShop.AlexThornley.Parsing
{
    public class RoverSimInputParser
    {
        public static RoverSimParseResult Parse(string[] lines)
        {
            if (lines is null)
                throw new ArgumentNullException(nameof(lines));

            try
            {
                if (lines.Length % 2 == 0)
                    throw new Exception("Expected odd number of lines");

                var (maxX, maxY) = ParseHeader(lines[0]);
                var roverSpecs = ParseRoverSpecs(lines[1..]).ToArray();

                CheckBounds(maxX, maxY, roverSpecs);

                return RoverSimParseResult.OK(maxX, maxY, roverSpecs);

            } catch (Exception x)
            {
                return RoverSimParseResult.Fail(x.Message);
            }
        }

        private static void CheckBounds(int maxX, int maxY, RoverSpec[] roverSpecs)
        {
            var outOfBounds = roverSpecs.FirstOrDefault(r => r.X < 0 || r.X > maxX || r.Y < 0 || r.Y > maxY);
            if (outOfBounds != null)
                throw new Exception($"Rover at {outOfBounds.X}, {outOfBounds.Y} is out of bounds");
        }

        private static IEnumerable<RoverSpec> ParseRoverSpecs(string[] lines)
        {
            for (int i=0; i<lines.Length; i+=2)
            {
                var (x, y, facing) = ParseRoverPosition(lines[i]);
                var instructions = ParseInstructions(lines[i + 1]).ToArray();

                yield return new RoverSpec(x, y, facing, instructions);
            }
        }

        private static IEnumerable<RoverInstruction> ParseInstructions(string line)
        {
            return line.Select(x => x switch
            {
                'M' => RoverInstruction.Move,
                'L' => RoverInstruction.TurnLeft,
                'R' => RoverInstruction.TurnRight,
                _ => throw new Exception("Unexpected instruction on line: " + line)
            });
        }

        private static (int, int, CompassDirection) ParseRoverPosition(string line)
        {
            var tokens = line.Split(' ');
            if (tokens.Length != 3)
                throw new Exception("Expected 3 tokens on line: " + line);

            if (!(int.TryParse(tokens[0], out int x)))
                throw new Exception("Non integer on line: "+ line);

            if (!(int.TryParse(tokens[1], out int y)))
                throw new Exception("Non integer on line: " + line);

            var facing = tokens[2] switch
            {
                "N" => CompassDirection.North,
                "E" => CompassDirection.East,
                "S" => CompassDirection.South,
                "W" => CompassDirection.West,
                _ => throw new Exception("Unexpected compass facing on line: " + line)
            };

            return (x, y, facing);
        }

        private static (int, int) ParseHeader(string s)
        {
            var tokens = s.Split(' ');
            if (tokens.Length != 2)
                throw new Exception("Expected 2 tokens on header line");

            if (!(int.TryParse(tokens[0], out int x)))
                throw new Exception("Non integer on header line");

            if (!(int.TryParse(tokens[1], out int y)))
                throw new Exception("Non integer on header line");

            return (x, y);
        }
    }
}
