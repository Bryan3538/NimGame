using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NimGame;

namespace NimGameTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PileSetCountNegativeTest()
        {
            Pile p = new Pile(-1);
        }

        [TestMethod]
        public void NimAIIndexTest()
        {
            NimAi AI = new NimAi();
            Pile[] piles = new Pile[5];

            piles[0] = new Pile(25);
            piles[1] = new Pile(30);
            piles[2] = new Pile(4);
            piles[3] = new Pile(5);
            piles[4] = new Pile(50);

            int index, sticksToRemove;
            int expectedIndex = 4;
            AI.GetMove(piles, out index, out sticksToRemove);

            Assert.AreEqual(index, expectedIndex);
        }

        [TestMethod]
        public void NimAISticksToRemoveTest()
        {
            NimAi AI = new NimAi();
            Pile[] piles = new Pile[5];

            piles[0] = new Pile(25);
            piles[1] = new Pile(30);
            piles[2] = new Pile(4);
            piles[3] = new Pile(5);
            piles[4] = new Pile(50);

            int index, sticksToRemove;
            int expectedSticksToRemove = 44;
            AI.GetMove(piles, out index, out sticksToRemove);

            Assert.AreEqual(sticksToRemove, expectedSticksToRemove);
        }

        //[TestMethod]
        //public void NimCheckForWinnerTest()
        //{
        //    Pile[] piles = new Pile[5];

        //    for (int i = 0; i < piles.Length; i++)
        //        piles[i] = new Pile();

        //    Assert.AreEqual(Nim.CheckForWinner(piles), true);
        //}
    }
}
