using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling
{
    public class Game
    {
         private List<Roll> _scoresPerRoll = new List<Roll>();

        private int _totalScore = 0;
        private int _currentRollIndex = 1;
        private int _rollCounter = 0;

        private int _strikeCounter = 0;
        private int _spareCounter = 0;

        private readonly int MAX_ROLL_COUNT_FOR_SPARE_COUNTER = 19;
        private readonly int MAX_ROLL_COUNT_FOR_STRIKE_COUNTER = 10;


        public void Roll(int pins)
        {
            _scoresPerRoll.Add(GetRoll(pins));

            UpdateScore();
        }

        private Roll GetRoll(int pins)
        {
            Roll currentRoll = new Roll();

            currentRoll.RollScore = pins;

            if (_strikeCounter > 0 || _spareCounter > 0)
            {
                currentRoll.Bonus = true;
                if (_spareCounter > 0)
                    _spareCounter--;

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
            _currentRollIndex++;
            _rollCounter++;
        }

        private void UpdateSpareStrikeCounter(int pins, Roll currentRoll)
        {
            if (_rollCounter == 0 || _rollCounter % 2 == 0)
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
            if (pins == 10 && _rollCounter < MAX_ROLL_COUNT_FOR_STRIKE_COUNTER)
            {
                _strikeCounter += 2;
                _rollCounter++;
            }
        }

        private void UpdateSpareCounter(Roll currentRoll)
        {
            if (_rollCounter % 2 == 1)
            {
                if (_rollCounter < MAX_ROLL_COUNT_FOR_SPARE_COUNTER)
                {
                    int frameScore = currentRoll.RollScore
                            + _scoresPerRoll[_currentRollIndex - 2].RollScore;
                    if (frameScore == 10)
                    {
                        _spareCounter = 1;
                    }
                }
            }
        }

        private void UpdateScore()
        {
            _totalScore += _scoresPerRoll[_scoresPerRoll.Count - 1].RollScore;

            UpdateScoreByBonus();
        }

        private void UpdateScoreByBonus()
        {
            if (_scoresPerRoll[_scoresPerRoll.Count - 1].Bonus)
            {   
                {
                    UpdateScoreByStrikeBonus();
                }
            }
        }

        private void UpdateScoreByStrikeBonus()
        {
            if (_strikeCounter <= 2)
            {
                _totalScore += _scoresPerRoll[_scoresPerRoll.Count - 1].RollScore;
                _strikeCounter--;
            }
            else
            {
                while (_strikeCounter > 2)
                {
                    _totalScore += _scoresPerRoll[_scoresPerRoll.Count - 1].RollScore * 2;
                    _strikeCounter--;
                }
            }
        }

        public int GetScore()
        {
            return _totalScore;
        }

    }
}
