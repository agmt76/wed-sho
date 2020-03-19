using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TheWeddingShop.AlexThornley.Navigation;

namespace TheWeddingShop.AlexThornley.Tests
{
    public class RoverTests
    {
        private const int MAX_X = 10;

        private const int MAX_Y = 12;

        private readonly Compass compass = new Compass();

        [TestCase(CompassDirection.North, CompassDirection.West)]
        [TestCase(CompassDirection.West, CompassDirection.South)]
        [TestCase(CompassDirection.South, CompassDirection.East)]
        [TestCase(CompassDirection.East, CompassDirection.North)]
        public void TurnLeft(CompassDirection originalFacing, CompassDirection expectedNewFacing)
        {
            var rover = new Rover(5, 6, originalFacing, MAX_X, MAX_Y, compass);
            rover.TurnLeft();

            Assert.AreEqual(5, rover.X);
            Assert.AreEqual(6, rover.Y);
            Assert.AreEqual(expectedNewFacing, rover.Facing);
        }

        [TestCase(CompassDirection.North, CompassDirection.East)]
        [TestCase(CompassDirection.East, CompassDirection.South)]
        [TestCase(CompassDirection.South, CompassDirection.West)]
        [TestCase(CompassDirection.West, CompassDirection.North)]
        public void TurnRight(CompassDirection originalFacing, CompassDirection expectedNewFacing)
        {
            var rover = new Rover(5, 6, originalFacing, MAX_X, MAX_Y, compass);
            rover.TurnRight();

            Assert.AreEqual(5, rover.X);
            Assert.AreEqual(6, rover.Y);
            Assert.AreEqual(expectedNewFacing, rover.Facing);
        }

        [TestCase(CompassDirection.North, 3, 7)]
        [TestCase(CompassDirection.East, 4, 6 )]
        [TestCase(CompassDirection.South, 3, 5)]
        [TestCase(CompassDirection.West, 2, 6)]
        public void Move(CompassDirection originalFacing, int expectedX, int expectedY)
        {
            var rover = new Rover(3, 6, originalFacing, MAX_X, MAX_Y, compass);
            rover.Move();

            Assert.AreEqual(expectedX, rover.X);
            Assert.AreEqual(expectedY, rover.Y);
            Assert.AreEqual(originalFacing, rover.Facing);
        }

        [TestCase(CompassDirection.North, 5, MAX_Y)]
        [TestCase(CompassDirection.East, MAX_X, 5)]
        [TestCase(CompassDirection.South, 5, 0)]
        [TestCase(CompassDirection.West, 0, 5)]
        public void DoNotMoveOutOfBounds(CompassDirection originalFacing, int originalX, int originalY)
        {
            var rover = new Rover(originalX, originalY, originalFacing, MAX_X, MAX_Y, compass);
            rover.Move();

            // assert rover has not moved
            Assert.AreEqual(originalX, rover.X);
            Assert.AreEqual(originalY, rover.Y);
            Assert.AreEqual(originalFacing, rover.Facing);
        }

        [Test]
        public void ChainMultipleMoves()
        {
            var rover = new Rover(3, 3, CompassDirection.East, MAX_X, MAX_Y, compass);
            rover.Move();       //4,3,East
            rover.Move();       //5,3,East
            rover.TurnRight();  //5,3,South
            rover.Move();       //5,2,South
            rover.Move();       //5,1,South
            rover.TurnLeft();   //5,1,East
            rover.TurnLeft();   //5,1,North
            rover.TurnLeft();   //5,1,West

            Assert.AreEqual(5, rover.X);
            Assert.AreEqual(1, rover.Y);
            Assert.AreEqual(CompassDirection.West, rover.Facing);
        }
    }
}
