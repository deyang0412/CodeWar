using System;
using System.Linq;
using System.Collections.Generic;
namespace Playgound.Katas
{
    public enum Result
    {
        Win,
        Loss,
        Tie
    }

    public class PokerHand
    {
        public int Points { get; private set; } = 0;
        public int Level { get; private set; } = 0;

        public PokerHand(string hand)
        {
            CalculatePower(hand.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
        }

        private void CalculatePower(string[] pokers)
        {
            var numbers = pokers.Select(m => TransformPoint(m.Substring(0, 1))).OrderBy(maa => maa);
            Points = SumPoints(numbers);

            if(IsContinuous(numbers.ToArray()))
            {
                if(IsAllSameSuit(pokers))
                {
                    if(Points == 9*10*11*12*13)
                    {
                        Level = 10;
                    }
                    else
                    {
                        Level = 9;
                    }
                }
                else
                {
                    Level = 5;
                }
            }
            else
            {
                var groupNumbers = numbers.GroupBy(m => m,(key, group) => new { number = key, count = group.Count()  });


                switch(groupNumbers.Count())
                {
                    case 2:
                        if(groupNumbers.Max(m => m.count) == 4)
                        {
                            Level = 8;
                        }
                        else
                        {
                            Level = 7;
                        }
                        break;
                    case 3:
                        if(groupNumbers.Max(m => m.count) == 3 )
                        {
                            Level = 4;
                        }
                        else
                        {
                            Level = 3;
                        }
                        break;
                    case 4:
                        Level = 2;
                        break;
                    case 5:
                        if(IsAllSameSuit(pokers))
                        {
                            Level = 6;
                        }
                        else
                        {
                            Level = 1;
                        }
                        break;
                    default:
                        Level = 0;
                        break;
                }
            }
        }

        private int SumPoints(IEnumerable<int> points)
        {
            int result = 1;
            foreach(int pokerNumber in points)
            {
                result += pokerNumber;
            }

            return result;
        }

        private int TransformPoint(string poker)
        {
            switch(poker)
            {
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    return int.Parse(poker) - 1;
                case "T":
                    return 9;
                case "J":
                    return 10;
                case "Q":
                    return 11;
                case "K":
                    return 12;
                case "A":
                    return 13;
                default:
                    return 0;
            }
        }

        private bool IsContinuous(int[] numbers)
        {
            bool result = true;

            for (int i = 0; i < numbers.Length - 1; i ++)
            {
                if(numbers[i] + 1 != numbers[i + 1])
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private bool IsAllSameSuit(string[] pokers)
        {
            return pokers.Select(m => m.Substring(1, 1)).Distinct().Count() == 1;
        }

        public Result CompareWith(PokerHand hand)
        {
            if(Level > hand.Level)
            {
                return Result.Win;
            }
            else if(Level == hand.Level)
            {
                return Points == hand.Points ? Result.Tie : Points > hand.Points ? Result.Win : Result.Loss;
            }
            else
            {
                return Result.Loss;
            }
        }
    }
}
