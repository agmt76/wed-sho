using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TheWeddingShop.AlexThornley.Navigation;

namespace TheWeddingShop.AlexThornley.Tests
{
    public class CompassTests
    {
        [TestCase(CompassDirection.North, CompassDirection.West)]
        [TestCase(CompassDirection.West, CompassDirection.South)]
        [TestCase(CompassDirection.South, CompassDirection.East)]
        [TestCase(CompassDirection.East, CompassDirection.North)]
        public void TurnLeft(CompassDirection originalFacing, CompassDirection expectedFacing)
        {
            var compass = new Compass();
            var newFacing = compass.LeftTurns[originalFacing];
            Assert.AreEqual(expectedFacing, newFacing);
        }

        [TestCase(CompassDirection.North, CompassDirection.East)]
        [TestCase(CompassDirection.East, CompassDirection.South)]
        [TestCase(CompassDirection.South, CompassDirection.West)]
        [TestCase(CompassDirection.West, CompassDirection.North)]
        public void TurnRight(CompassDirection originalFacing, CompassDirection expectedFacing)
        {
            var compass = new Compass();
            var newFacing = compass.RightTurns[originalFacing];
            Assert.AreEqual(expectedFacing, newFacing);
        }

        [TestCase(CompassDirection.North, 0, 1)]
        [TestCase(CompassDirection.East, 1, 0)]
        [TestCase(CompassDirection.South, 0, -1)]
        [TestCase(CompassDirection.West, -1, 0)]
        public void Vectors(CompassDirection facing, int expectedX, int expectedY)
        {
            var compass = new Compass();
            var vector = compass.Vectors[facing];

            Assert.AreEqual(facing, vector.Direction);
            Assert.AreEqual(expectedX, vector.XVector);
            Assert.AreEqual(expectedY, vector.YVector);
        }
    }
}
