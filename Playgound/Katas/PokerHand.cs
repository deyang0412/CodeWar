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
        public string originPokers { get; private set; }
        public long Power { get; private set; } = 0;

        public PokerHand(string hand)
        {
            originPokers = hand;
            CalculatePower(hand.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
        }

        private void CalculatePower(string[] pokers)
        {
            string[] numberStrings = pokers.Select(m => TransformPoint(m.Substring(0, 1))).OrderByDescending(maa => maa).ToArray();
            string level = "10", levelPoint = "00";

            if (IsContinuous(numberStrings))
            {
                if (IsAllSameSuit(pokers))
                {
                    if (numberStrings.Select(m => int.Parse(m)).Sum() == 9 + 10 + 11 + 12 + 13)
                    {
                        level = "20";
                        levelPoint = "13";
                    }
                    else
                    {
                        level = "19";
                        levelPoint = numberStrings.FirstOrDefault();
                    }
                }
                else
                {
                    level = "15";
                    levelPoint = numberStrings.FirstOrDefault();
                }
            }
            else
            {
                var groupNumbers = numberStrings.GroupBy(m => m, (key, group) => new { number = key, count = group.Count() });


                switch (groupNumbers.Count())
                {
                    case 2:
                        if (groupNumbers.Max(m => m.count) == 4)
                        {
                            level = "18";
                            levelPoint = groupNumbers.Where(m => m.count == 4).Select(m => m.number).SingleOrDefault();
                        }
                        else
                        {
                            level = "17";
                            levelPoint = groupNumbers.Where(m => m.count == 3).Select(m => m.number).SingleOrDefault();
                        }
                        break;
                    case 3:
                        if (groupNumbers.Max(m => m.count) == 3)
                        {
                            level = "14";
                            levelPoint = groupNumbers.Where(m => m.count == 3).Select(m => m.number).SingleOrDefault();
                        }
                        else
                        {
                            level = "13";
                            levelPoint = groupNumbers.Where(m => m.count == 2).Select(m => m.number).OrderByDescending(m => m).FirstOrDefault();
                        }
                        break;
                    case 4:
                        level = "12";
                        levelPoint = groupNumbers.Where(m => m.count == 2).Select(m => m.number).SingleOrDefault();
                        break;
                    case 5:
                        if (IsAllSameSuit(pokers))
                        {
                            level = "16";
                        }
                        else
                        {
                            level = "11";
                        }
                        levelPoint = groupNumbers.OrderByDescending(m => m.number).Select(m => m.number).FirstOrDefault();
                        break;
                    default:
                        level = "10";
                        break;
                }
            }

            Power = Convert.ToInt64($"{level}{levelPoint}{string.Join("", numberStrings)}");
        }

        private string TransformPoint(string poker)
        {
            switch (poker)
            {
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    return (int.Parse(poker) - 1).ToString().PadLeft(2, '0');
                case "T":
                    return "09";
                case "J":
                    return "10";
                case "Q":
                    return "11";
                case "K":
                    return "12";
                case "A":
                    return "13";
                default:
                    return "00";
            }
        }

        private bool IsContinuous(string[] numbers)
        {
            bool result = true;
            var numbersAsc = numbers.OrderBy(m => m).ToArray();

            for (int i = 0; i < numbersAsc.Length - 1; i++)
            {
                if (int.Parse(numbersAsc[i]) + 1 != int.Parse(numbersAsc[i + 1]))
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
            Console.WriteLine($"my poker         = {originPokers}");
            Console.WriteLine($"my power         = {Power}");
            Console.WriteLine($"opponent's poker = {hand.originPokers}");
            Console.WriteLine($"opponent's power = {hand.Power}");

            return Power == hand.Power ? Result.Tie : Power > hand.Power ? Result.Win : Result.Loss;
        }
    }
}
