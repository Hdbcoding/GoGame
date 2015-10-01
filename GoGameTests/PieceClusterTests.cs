using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoGame;

namespace GoGameTests
{
    [TestClass]
    public class PieceClusterTests
    {
        //basic cluster formation
        //legal cluster additions
        //illegal cluster additions
        //cluster liberty updates

        [TestMethod]
        public void basicClusterContainsSelf()
        {
            //arrange
            GoBoard testgame = new GoBoard(5);
            Coordinate loc = new Coordinate(3, 3);
            testgame.addPiece(loc, Space.Black);
            bool expected = true;

            //act
            PieceCluster simpleCluster = new PieceCluster(loc, Space.Black, testgame);
            bool actual = simpleCluster.Pieces.Contains(loc);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void basicClusterHasFourLiberties()
        {
            //arrange
            GoBoard testgame = new GoBoard(5);
            Coordinate loc = new Coordinate(3, 3);
            testgame.addPiece(loc, Space.Black);
            int expected = 4;

            //act
            PieceCluster simpleCluster = new PieceCluster(loc, Space.Black, testgame);
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
            Coordinate loc2 = new Coordinate(4,3);
            testgame.addPiece(loc2, Space.White);
            int expected = 3;

            //act
            PieceCluster simpleCluster = new PieceCluster(loc2, Space.White, testgame);
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
            PieceCluster simpleCluster = new PieceCluster(loc, Space.Black, testgame);

            Coordinate loc2 = new Coordinate(4, 3);
            testgame.addPiece(loc2, Space.White);
            simpleCluster.removeLiberty(loc2);

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
            PieceCluster simpleCluster = new PieceCluster(loc, Space.Black, testgame);

            Coordinate loc2 = new Coordinate(4, 2);
            testgame.addPiece(loc2, Space.White);

            Coordinate loc3 = new Coordinate(4, 3);
            testgame.addPiece(loc3, Space.Black);
            simpleCluster.piecesAdd(loc3);
            simpleCluster.removeLiberty(loc3);
            simpleCluster.addLibertiesOf(loc3);

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
            PieceCluster black1 = new PieceCluster(loc, Space.Black, testgame);

            Coordinate loc2 = new Coordinate(4, 2);
            testgame.addPiece(loc2, Space.White);
            PieceCluster white1 = new PieceCluster(loc2, Space.White, testgame);

            Coordinate loc3 = new Coordinate(4, 3);
            testgame.addPiece(loc3, Space.Black);

            black1.piecesAdd(loc3);
            black1.removeLiberty(loc3);
            black1.addLibertiesOf(loc3);

            white1.removeLiberty(loc3);

            int expected = 3;

            //act
            int actual = white1.Liberties.Count;

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
