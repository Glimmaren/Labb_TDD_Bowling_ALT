using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace Labb_TDD_Bowling_ALT
{
    public class Game
    {
        private int[,] sheet = new int[13, 2];
        private int currentRound;
        private int currentRoll;
        private readonly int maxRounds = 12;
        private readonly int maxRolls = 2;

        public void Roll(int pins)
        {
            if (pins == 10 && currentRoll == 0)
            {
                sheet[currentRound++, currentRoll] = pins;
            }
            else
            {
                sheet[currentRound, currentRoll++] = pins;
            }

            if (currentRoll == 2)
            {
                currentRound++;
                currentRoll = 0;
            }
        }
        public int Score()
        {
            int score = 0;

            for (int frame = 0; frame < maxRounds; frame++)
            {
                for (int round = 0; round < maxRolls; round++)
                {
                    if (IsStrike(frame, round))
                    {
                        round = ExtraPointsStrike(frame, round, ref score);
                    }
                    else if (IsSpare(round, frame))
                    {
                        score = ExtraPointsSpare(score, frame, round);
                        break;
                    }
                    else
                    {
                        score += sheet[frame, round++];
                    }

                    round = ResetRound(round);
                }
            }
            return score;
        }

        private bool IsStrike(int frame, int round)
        {
            return sheet[frame, round] == 10 && round < 1;
        }
        private bool IsSpare(int round, int frame)
        {
            return round != 1 && sheet[frame, round] + sheet[frame, round + 1] == 10;
        }
        private int ExtraPointsStrike(int frame, int round, ref int score)
        {
            if (frame <= 8)
            {
                if (sheet[frame + 1, round] == 10)
                    score += 10 + sheet[frame + 1, round] + sheet[frame + 2, round];

                else
                    score += 10 + sheet[frame + 1, round] + sheet[frame + 1, round + 1];
            }
            else
            {
                score += sheet[frame, round++];
            }

            return round;
        }
        private int ExtraPointsSpare(int score, int frame, int round)
        {
            score += 10 + sheet[frame + 1, round];
            return score;
        }
        private static int ResetRound(int round)
        {
            if (round == 1)
                round = 0;
            return round;
        }
    }
}
