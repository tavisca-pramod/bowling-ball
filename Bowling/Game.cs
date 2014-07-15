using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling
{
    public class Game
    {
        List<Roll> ScorePerRoll = new List<Roll>();

        private int totalScore = 0;
        private int currentRollIndex = 1;
        private int rollCounter = 0;

        private int strikeCounter = 0;
        private int spareCounter = 0;

        private readonly int MAX_ROLL_COUNT_FOR_SPARE_COUNTER = 19;
        private readonly int MAX_ROLL_COUNT_FOR_STRIKE_COUNTER = 10;


        public void Roll(int pins)
        {
            ScorePerRoll.Add(GetRoll(pins));

            UpdateScore();
        }

        private Roll GetRoll(int pins)
        {
            Roll currentRoll = new Roll();

            currentRoll.RollScore = pins;

            if (strikeCounter > 0 || spareCounter > 0)
            {
                currentRoll.Bonus = true;
                if (spareCounter > 0)
                    spareCounter--;

                UpdateSpareStrikeCounter(pins, currentRoll);
            }
            else
            {
                UpdateSpareStrikeCounter(pins, currentRoll);
            }

            UpdateCounters();

            return currentRoll;
        }

        private void UpdateCounters()
        {
            currentRollIndex++;
            rollCounter++;
        }

        private void UpdateSpareStrikeCounter(int pins, Roll currentRoll)
        {
            if (rollCounter == 0 || rollCounter % 2 == 0)
            {
                UpdateStrikeCounter(pins);
            }
            else
            {
                UpdateSpareCounter(currentRoll);
            }
        }

        private void UpdateStrikeCounter(int pins)
        {
            if (pins == 10 && rollCounter < MAX_ROLL_COUNT_FOR_STRIKE_COUNTER)
            {
                strikeCounter += 2;
                rollCounter++;
            }
        }

        private void UpdateSpareCounter(Roll currentRoll)
        {
            if (rollCounter % 2 == 1)
            {
                if (rollCounter < MAX_ROLL_COUNT_FOR_SPARE_COUNTER)
                {
                    int frameScore = currentRoll.RollScore
                            + ScorePerRoll[currentRollIndex - 2].RollScore;
                    if (frameScore == 10)
                    {
                        spareCounter = 1;
                    }
                }
            }
        }

        private void UpdateScore()
        {
            totalScore += ScorePerRoll[ScorePerRoll.Count - 1].RollScore;

            UpdateScoreByBonus();
        }

        private void UpdateScoreByBonus()
        {
            if (ScorePerRoll[ScorePerRoll.Count - 1].Bonus)
            {   
                {
                    UpdateScoreByStrikeBonus();
                }
            }
        }

        private void UpdateScoreByStrikeBonus()
        {
            if (strikeCounter <= 2)
            {
                totalScore += ScorePerRoll[ScorePerRoll.Count - 1].RollScore;
                strikeCounter--;
            }
            else
            {
                while (strikeCounter > 2)
                {
                    totalScore += ScorePerRoll[ScorePerRoll.Count - 1].RollScore * 2;
                    strikeCounter--;
                }
            }
        }

        public int GetScore()
        {
            return totalScore;
        }

    }
}
