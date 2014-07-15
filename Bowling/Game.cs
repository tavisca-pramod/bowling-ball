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
      
        private int score = 0;
        private int currentRollIndex = 1;
        private int rollCount = 0;

        private int strike = 0;
        private int spare = 0;


        public void Roll(int pins)
        {
            ScorePerRoll.Add(GetRoll(pins));
          
            UpdateScore();
        }

        private Roll GetRoll(int pins)
        {
            Roll currentRoll = new Roll();

            currentRoll.RollScore = pins;

            if (strike > 0 || spare > 0)
            {
                currentRoll.Bonus = true;
                if (spare > 0)
                    spare--;

                UpdateSpareStrikeCounter(pins, currentRoll);
            }
            else 
            {
                UpdateSpareStrikeCounter(pins, currentRoll);
            }
            currentRollIndex++;
            rollCount++;
            return currentRoll;
        }

        private void UpdateSpareStrikeCounter(int pins, Roll currentRoll)
        {
            if (rollCount == 0 || rollCount % 2 == 0)
            {
                if (pins == 10 && rollCount < 10)
                {
                    strike += 2;
                    rollCount++;
                }
            }
            else
            {
                if (rollCount % 2 == 1 && rollCount < 10)
                {
                    int frameScore = currentRoll.RollScore
                            + ScorePerRoll[currentRollIndex - 2].RollScore;
                    if (frameScore == 10)
                    {
                        spare = 1;
                    }
                }
            }
        }

        private void UpdateScore()
        {
            score += ScorePerRoll[ScorePerRoll.Count - 1].RollScore;
            
            if (ScorePerRoll[ScorePerRoll.Count - 1].Bonus)
            {

                if (strike <= 2)
                {
                    score += ScorePerRoll[ScorePerRoll.Count - 1].RollScore;
                    strike--;
                }
             
                while (strike > 2)
                {
                    score += ScorePerRoll[ScorePerRoll.Count - 1].RollScore*2 ;
                    strike--;
                }
            }
            

        }

        public int GetScore()
        {
            return score;
        }

    }
}
