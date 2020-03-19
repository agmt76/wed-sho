using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheWeddingShop.AlexThornley.Navigation;
using TheWeddingShop.AlexThornley.Parsing;

namespace TheWeddingShop.AlexThornley.Tests
{
    public class RoverSimInputParserTests
    {
        [Test]
        public void RejectsEvenLineCount()
        {
            var lines = new[]
            {
                "5 5",
                "1 1 N"
            };

            var result = RoverSimInputParser.Parse(lines);

            Assert.IsFalse(result.Ok);
            Assert.AreEqual("Expected odd number of lines", result.ParseError);
        }

        [TestCase("a 5", "Non integer on header line")]
        [TestCase("5 a", "Non integer on header line")]
        [TestCase("5", "Expected 2 tokens on header line")]
        public void RejectsBadHeader(string line, string expectedError)
        {
            var lines = new[]
            {
                line
            };

            var result = RoverSimInputParser.Parse(lines);

            Assert.IsFalse(result.Ok);
            Assert.AreEqual(expectedError, result.ParseError);
        }

        [TestCase("a 5 N", "Non integer on line: a 5 N")]
        [TestCase("5 a N", "Non integer on line: 5 a N")]
        [TestCase("5 5 foo", "Unexpected compass facing on line: 5 5 foo")]
        [TestCase("5", "Expected 3 tokens on line: 5")]
        public void Rejects_Bad_Rover(string line, string expectedError)
        {
            var lines = new[]
            {
                "10 10",
                line,
                "M"
            };

            var result = RoverSimInputParser.Parse(lines);

            Assert.IsFalse(result.Ok);
            Assert.AreEqual(expectedError, result.ParseError);
        }

        [Test]
        public void RejectsBadInstruction()
        {
            var lines = new[]
            {
                "10 10",
                "5 5 N",
                "MxLR"
            };

            var result = RoverSimInputParser.Parse(lines);

            Assert.IsFalse(result.Ok);
            Assert.AreEqual("Unexpected instruction on line: MxLR", result.ParseError);
        }

        [Test]
        public void ParseSingleRover()
        {
            var lines = new[]
{
                "10 12",
                "5 6 N",
                "MMLR"
            };

            var result = RoverSimInputParser.Parse(lines);

            Assert.IsTrue(result.Ok);
            Assert.AreEqual((10, 12), (result.MaxX, result.MaxY));
            
            Assert.AreEqual(1, result.RoverSpecs.Length);
            var roverSpec = result.RoverSpecs[0];

            Assert.AreEqual((5, 6, CompassDirection.North), (roverSpec.X, roverSpec.Y, roverSpec.Facing));
            Assert.AreEqual(
                new[]
                {
                    RoverInstruction.Move,
                    RoverInstruction.Move,
                    RoverInstruction.TurnLeft,
                    RoverInstruction.TurnRight
                },
                roverSpec.Instructions);
        }

        [Test]
        public void ParseTwoRovers()
        {
            var lines = new[]
{
                "10 12",
                "5 6 N",
                "M",
                "7 8 E",
                "L"
            };

            var result = RoverSimInputParser.Parse(lines);

            Assert.IsTrue(result.Ok);
            Assert.AreEqual((10, 12), (result.MaxX, result.MaxY));

            Assert.AreEqual(2, result.RoverSpecs.Length);
            var roverSpecA = result.RoverSpecs[0];
            var roverSpecB = result.RoverSpecs[1];

            Assert.AreEqual(
                (5, 6, CompassDirection.North, RoverInstruction.Move), 
                (roverSpecA.X, roverSpecA.Y, roverSpecA.Facing, roverSpecA.Instructions.Single()));

            Assert.AreEqual(
                (7, 8, CompassDirection.East, RoverInstruction.TurnLeft),
                (roverSpecB.X, roverSpecB.Y, roverSpecB.Facing, roverSpecB.Instructions.Single()));
        }

        [TestCase(0, 0)]
        [TestCase(0, 12)]
        [TestCase(10, 0)]
        [TestCase(10, 12)]
        public void AllCornersAreInBounds(int x, int y)
        {
            var lines = new[]
            {
                "10 12",
                $"{x} {y} N",
                "M"
            };

            var result = RoverSimInputParser.Parse(lines);

            Assert.IsTrue(result.Ok); 
        }

        [TestCase(-1, 5)]
        [TestCase(5, -1)]
        [TestCase(11, 5)]
        [TestCase(5, 13)]
        public void RejectsOutOfBounds(int x, int y)
        {
            var lines = new[]
            {
                "10 12",
                $"{x} {y} N",
                "M"
            };

            var result = RoverSimInputParser.Parse(lines);

            Assert.IsFalse(result.Ok);
            Assert.AreEqual($"Rover at {x}, {y} is out of bounds", result.ParseError);
        }

    }
}
