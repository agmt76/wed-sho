using System;
using System.Collections.Generic;
using System.Linq;
using TheWeddingShop.AlexThornley.Navigation;
using TheWeddingShop.AlexThornley.Parsing;

namespace TheWeddingShop.AlexThornley
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO - read this from file path specified in args?
            string[] lines = new[]
            {
                "5 5",
                "1 2 N",
                "LMLMLMLMM",
                "3 3 E",
                "MMRMMRMRRM"
            };

            var parseResult = RoverSimInputParser.Parse(lines);
            if (!parseResult.Ok)
            {
                Console.Out.WriteLine("Parse error: " + parseResult.ParseError);
                Environment.Exit(0);
            }

            var rovers = ProcessRovers(parseResult);

            OutputRovers(rovers);
        }

        private static void OutputRovers(List<Rover> rovers)
        {
            foreach (var r in rovers)
            {
                Console.Out.WriteLine($"{r.X} {r.Y} {r.Facing.ToString().First()}");
            }
        }

        private static List<Rover> ProcessRovers(RoverSimParseResult parseResult)
        {
            var rovers = new List<Rover>();
            var compass = new Compass();
            foreach (var s in parseResult.RoverSpecs)
            {
                var rover = new Rover(s.X, s.Y, s.Facing, parseResult.MaxX, parseResult.MaxY, compass);
                ApplyInstructions(rover, s.Instructions);
                rovers.Add(rover);
            }

            return rovers;
        }

        private static void ApplyInstructions(Rover rover, RoverInstruction[] instructions)
        {
            foreach (var i in instructions)
            {
                switch (i)
                {
                    case RoverInstruction.Move: rover.Move(); break;
                    case RoverInstruction.TurnLeft: rover.TurnLeft(); break;
                    case RoverInstruction.TurnRight: rover.TurnRight(); break;
                }
            }
        }



        




    }
}
