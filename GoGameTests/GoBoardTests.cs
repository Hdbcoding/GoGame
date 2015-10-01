using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoGame;

namespace GoGameTests
{
    [TestClass]
    public class GoBoardTests
    {
        //add piece cases
        //cluster death cases
        //ko cases
        //basic cases

        //legal move cases
        [TestMethod]
        public void CantAddEmptyPieces()
        {
            //arrange
            GoBoard testgame = new GoBoard();
            Coordinate location = new Coordinate(1,1);
            bool expected = false;

            //act
            bool actual = testgame.addPiece(location,Space.Empty);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CantAddForbiddenPieces()
        {
            //arrange
            GoBoard testgame = new GoBoard();
            Coordinate location = new Coordinate(1, 1);
            bool expected = false;

            //act
            bool actual = testgame.addPiece(location, Space.Forbidden);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CantAddOutOfBounds()
        {
            //arrange
            GoBoard testgame = new GoBoard();
            Coordinate location = new Coordinate(0, 0);
            bool expected = false;

            //act
            bool actual = testgame.addPiece(location, Space.Black);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CantAddFarOutOfBounds()
        {
            //arrange
            GoBoard testgame = new GoBoard();
            Coordinate location = new Coordinate(-1, -1);
            bool expected = false;

            //act
            bool actual = testgame.addPiece(location, Space.Black);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CantAddWhiteFirst()
        {
            //arrange
            GoBoard testgame = new GoBoard();
            Coordinate location = new Coordinate(1, 1);
            bool expected = false;

            //act
            bool actual = testgame.addPiece(location, Space.White);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CantAddBlackSecond()
        {
            //arrange
            GoBoard testgame = new GoBoard();
            Coordinate locationa = new Coordinate(1, 1);
            Coordinate locationb = new Coordinate(1, 2);
            bool expected = false;
            //act
            testgame.addPiece(locationa, Space.Black);
            bool actual = testgame.addPiece(locationb, Space.Black);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanAddWhiteSecond()
        {
            //arrange
            GoBoard testgame = new GoBoard();
            Coordinate locationa = new Coordinate(1, 1);
            Coordinate locationb = new Coordinate(1, 2);
            bool expected = true;
            //act
            testgame.addPiece(locationa, Space.Black);
            bool actual = testgame.addPiece(locationb, Space.White);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CantOverlapPieces()
        { 
            //arrange
            GoBoard testgame = new GoBoard();
            Coordinate location = new Coordinate(1, 1);
            bool expected = false;

            //act
            testgame.addPiece(location, Space.Black);
            bool actual = testgame.addPiece(location, Space.White);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AnySizeGoGame()
        {
            //arrange
            GoBoard testgame = new GoBoard(5);
            Coordinate location = new Coordinate(6, 6);
            Space expected = Space.Forbidden;

            //act
            Space actual = testgame.getSpace(location);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BoardCreatesPieceClusters()
        {
            //arrange
            GoBoard testgame = new GoBoard(5);
            Coordinate location = new Coordinate(3, 3);
            testgame.addPiece(location, Space.Black);
            bool expected = true;

            //act
            PieceCluster thiscluster = testgame.getClusterContainingCoordinate(location, Space.Black);
            bool actual = (thiscluster.Pieces.Contains(location));

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void basicClusterHasFourLiberties()
        {
            //arrange
            GoBoard testgame = new GoBoard(5);
            Coordinate location = new Coordinate(3, 3);
            testgame.addPiece(location, Space.Black);
            int expected = 4;

            //act
            PieceCluster simpleCluster = testgame.getClusterContainingCoordinate(location, Space.Black);
            int actual = simpleCluster.Liberties.Count;

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void enemyNeighborHasLessLiberties()
        {
            //arrange
            GoBoard testgame = new GoBoard(5);
            Coordinate loc = new Coordinate(3, 3);
            testgame.addPiece(loc, Space.Black);
            Coordinate loc2 = new Coordinate(4, 3);
            testgame.addPiece(loc2, Space.White);
            int expected = 3;
            
            //act
            PieceCluster simpleCluster = testgame.getClusterContainingCoordinate(loc2, Space.White);
            int actual = simpleCluster.Liberties.Count;

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void libertyUpdatingForSimpleClustersWorks()
        {
            //arrange
            GoBoard testgame = new GoBoard(5);

            Coordinate loc = new Coordinate(3, 3);
            testgame.addPiece(loc, Space.Black);

            Coordinate loc2 = new Coordinate(4, 3);
            testgame.addPiece(loc2, Space.White);

            PieceCluster simpleCluster = testgame.getClusterContainingCoordinate(loc, Space.Black);
            int expected = 3;

            //act
            int actual = simpleCluster.Liberties.Count;

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void addSinglePieceToClusterCorrectLiberties()
        {
            //arrange
            GoBoard testgame = new GoBoard(5);

            Coordinate loc = new Coordinate(3, 3);
            testgame.addPiece(loc, Space.Black);

            Coordinate loc2 = new Coordinate(4, 2);
            testgame.addPiece(loc2, Space.White);

            Coordinate loc3 = new Coordinate(4, 3);
            testgame.addPiece(loc3, Space.Black);

            PieceCluster simpleCluster = testgame.getClusterContainingCoordinate(loc, Space.Black);

            int expected = 5;

            //act
            int actual = simpleCluster.Liberties.Count;

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void libertyUpdatingWithLittleCluster()
        {
            //arrange
            GoBoard testgame = new GoBoard(5);

            Coordinate loc = new Coordinate(3, 3);
            testgame.addPiece(loc, Space.Black);

            Coordinate loc2 = new Coordinate(4, 2);
            testgame.addPiece(loc2, Space.White);

            Coordinate loc3 = new Coordinate(4, 3);
            testgame.addPiece(loc3, Space.Black);

            PieceCluster white1 = testgame.getClusterContainingCoordinate(loc2, Space.White);

            int expected = 3;

            //act
            int actual = white1.Liberties.Count;

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void edgeCapture()
        {
            //arrange
            GoBoard testgame = new GoBoard(5);
            testgame.addPiece(new Coordinate(3, 3), Space.Black);
            testgame.addPiece(new Coordinate(4, 2), Space.White);
            testgame.addPiece(new Coordinate(4, 3), Space.Black);
            testgame.addPiece(new Coordinate(3, 2), Space.White);
            testgame.addPiece(new Coordinate(2, 2), Space.Black);
            testgame.addPiece(new Coordinate(2, 1), Space.White);
            testgame.addPiece(new Coordinate(3, 1), Space.Black);
            testgame.addPiece(new Coordinate(4, 1), Space.White);
            
            Coordinate location = new Coordinate(3, 1);
            Space expected = Space.Empty;

            //act
            Space actual = testgame.getSpace(location);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void edgeClusterCapture()
        {
            //arrange
            GoBoard testgame = new GoBoard(5);
            testgame.addPiece(new Coordinate(3, 3), Space.Black);
            testgame.addPiece(new Coordinate(4, 2), Space.White);
            testgame.addPiece(new Coordinate(4, 3), Space.Black);
            testgame.addPiece(new Coordinate(3, 2), Space.White);
            testgame.addPiece(new Coordinate(2, 2), Space.Black);
            testgame.addPiece(new Coordinate(2, 1), Space.White);
            testgame.addPiece(new Coordinate(3, 1), Space.Black);
            testgame.addPiece(new Coordinate(4, 1), Space.White);
            testgame.addPiece(new Coordinate(5, 2), Space.Black);
            testgame.addPiece(new Coordinate(5, 1), Space.White);
            testgame.addPiece(new Coordinate(3, 1), Space.Black);

            Coordinate location = new Coordinate(4, 1);
            Space expected = Space.Empty;

            //act
            Space actual = testgame.getSpace(location);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void clusterCombination()
        {
            //arrange
            GoBoard testgame = new GoBoard(5);
            testgame.addPiece(new Coordinate(3, 3), Space.Black);
            testgame.addPiece(new Coordinate(4, 2), Space.White);
            testgame.addPiece(new Coordinate(4, 3), Space.Black);
            testgame.addPiece(new Coordinate(3, 2), Space.White);
            testgame.addPiece(new Coordinate(2, 2), Space.Black);
            testgame.addPiece(new Coordinate(2, 1), Space.White);
            testgame.addPiece(new Coordinate(3, 1), Space.Black);
            testgame.addPiece(new Coordinate(4, 1), Space.White);
            testgame.addPiece(new Coordinate(5, 2), Space.Black);
            testgame.addPiece(new Coordinate(5, 1), Space.White);
            testgame.addPiece(new Coordinate(3, 1), Space.Black);
            testgame.addPiece(new Coordinate(2, 4), Space.White);
            testgame.addPiece(new Coordinate(3, 2), Space.Black);

            Coordinate loc = new Coordinate(3, 2);
            PieceCluster bigCluster = testgame.getClusterContainingCoordinate(loc, Space.Black);
            int expected = 5;

            //act
            int actual = bigCluster.Pieces.Count();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void clusterCombinationLiberties()
        {
            //arrange
            GoBoard testgame = new GoBoard(5);
            testgame.addPiece(new Coordinate(3, 3), Space.Black);
            testgame.addPiece(new Coordinate(4, 2), Space.White);
            testgame.addPiece(new Coordinate(4, 3), Space.Black);
            testgame.addPiece(new Coordinate(3, 2), Space.White);
            testgame.addPiece(new Coordinate(2, 2), Space.Black);
            testgame.addPiece(new Coordinate(2, 1), Space.White);
            testgame.addPiece(new Coordinate(3, 1), Space.Black);
            testgame.addPiece(new Coordinate(4, 1), Space.White);
            testgame.addPiece(new Coordinate(5, 2), Space.Black);
            testgame.addPiece(new Coordinate(5, 1), Space.White);
            testgame.addPiece(new Coordinate(3, 1), Space.Black);
            testgame.addPiece(new Coordinate(2, 4), Space.White);
            testgame.addPiece(new Coordinate(3, 2), Space.Black);

            Coordinate loc = new Coordinate(3, 2);
            PieceCluster bigCluster = testgame.getClusterContainingCoordinate(loc, Space.Black);
            int expected = 7;

            //act
            int actual = bigCluster.Liberties.Count();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void basicKoDenial()
        {
            //arrange
            GoBoard testgame = new GoBoard(5);
            testgame.addPiece(new Coordinate(3, 3), Space.Black);
            testgame.addPiece(new Coordinate(3, 2), Space.White);
            testgame.addPiece(new Coordinate(2, 2), Space.Black);
            testgame.addPiece(new Coordinate(3, 4), Space.White);
            testgame.addPiece(new Coordinate(1, 3), Space.Black);
            testgame.addPiece(new Coordinate(4, 3), Space.White);
            testgame.addPiece(new Coordinate(2, 4), Space.Black);
            testgame.addPiece(new Coordinate(2, 3), Space.White);
            bool expected = false;

            //act
            bool actual = testgame.addPiece(new Coordinate(3, 3), Space.Black);

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
