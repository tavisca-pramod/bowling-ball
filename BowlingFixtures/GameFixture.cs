using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bowling;

namespace BowlingFixtures
{
    [TestClass]
    public class GameFixture
    {
        public Game game;
        public int strike = 0;
        public int Spare = 0;

        [TestInitialize]
        public void init()
        {
            game = new Game();
        }

        [TestCleanup]
        public void CleanUp()
        {
            game = null;
        }

        [TestMethod]
        public void CheckUpdatedScoreAfterFirstRoll()
        {
            int numberOfPinsHit = 4;

            game.Roll(numberOfPinsHit);

            Assert.AreEqual(4, game.GetScore());
        }

        [TestMethod]
        public void CheckUpdatedScoreAfterSecondRoll()
        {
            int numberOfPinsHitOnFirstRoll = 4;
            int numberOfPinsHitOnSecondRoll = 3;
            game.Roll(numberOfPinsHitOnFirstRoll);
            game.Roll(numberOfPinsHitOnSecondRoll);

            Assert.AreEqual(7, game.GetScore());
        }

        [TestMethod]
        public void CheckUpdatedScoresForSpare()
        {
            int numberOfPinsHitOnFirstRoll = 4;
            int numberOfPinsHitOnSecondRoll = 6;
            int numberOfPinsHitOnThirdRoll = 3;
            game.Roll(numberOfPinsHitOnFirstRoll);
            game.Roll(numberOfPinsHitOnSecondRoll);
            game.Roll(numberOfPinsHitOnThirdRoll);

            Assert.AreEqual(16, game.GetScore());
        }

        [TestMethod]
        public void CheckUpdatedScoresForStrike()
        {
            int numberOfPinsHitOnFirstRoll = 10;
            int numberOfPinsHitOnSecondRoll = 6;
            int numberOfPinsHitOnThirdRoll = 3;
            game.Roll(numberOfPinsHitOnFirstRoll);
            game.Roll(numberOfPinsHitOnSecondRoll);
            game.Roll(numberOfPinsHitOnThirdRoll);

            Assert.AreEqual(28, game.GetScore());
        }

        [TestMethod]
        public void CheckUpdatedScoresForAllStrike()
        {
            int numberOfPinsHitOnFirstRoll = 10;

            for (int i = 1; i <= 12; i++)
            {
                game.Roll(numberOfPinsHitOnFirstRoll);
            }
            int result = game.GetScore();
            Assert.AreEqual(300, game.GetScore());
        }

        [TestMethod]
        public void CheckUpdatedScoresForAllSpare()
        {
            int numberOfPinsHitOnFirstRoll = 5;

            for (int i = 1; i <= 21; i++)
            {
                game.Roll(numberOfPinsHitOnFirstRoll);
            }
            int result = game.GetScore();
            Assert.AreEqual(150, game.GetScore());
        }
    }
}
