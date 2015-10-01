using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using GoGame;


namespace GoGameTests
{
    [TestClass]
    public class CoordinateTests
    {
        [TestMethod]
        public void SameCoordinateEquals()
        {
            //arrange
            Coordinate first = new Coordinate(1, 1);
            Coordinate second = new Coordinate(1, 1);
            bool expected = true;

            //act
            bool actual = first.Equals(second);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DiffCoordinateNotEquals()
        {
            //arrange
            Coordinate first = new Coordinate(1, 1);
            Coordinate second = new Coordinate(2, 2);
            bool expected = false;

            //act
            bool actual = first.Equals(second);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpNeighborContainedInNeighbors()
        {
            //arrange
            Coordinate first = new Coordinate(1, 1);
            Coordinate up = new Coordinate(1, 2);
            bool expected = true;

            //act
            List<Coordinate> neighbors = first.getNeighbors();
            bool actual = neighbors.Contains(up);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DownNeighborContainedInNeighbors()
        {
            //arrange
            Coordinate first = new Coordinate(1, 1);
            Coordinate down = new Coordinate(1, 0);
            bool expected = true;

            //act
            List<Coordinate> neighbors = first.getNeighbors();
            bool actual = neighbors.Contains(down);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LeftNeighborContainedInNeighbors()
        {
            //arrange
            Coordinate first = new Coordinate(1, 1);
            Coordinate left = new Coordinate(0, 1);
            bool expected = true;

            //act
            List<Coordinate> neighbors = first.getNeighbors();
            bool actual = neighbors.Contains(left);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RightNeighborContainedInNeighbors()
        {
            //arrange
            Coordinate first = new Coordinate(1, 1);
            Coordinate right = new Coordinate(2, 1);
            bool expected = true;

            //act
            List<Coordinate> neighbors = first.getNeighbors();
            bool actual = neighbors.Contains(right);

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
