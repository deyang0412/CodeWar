using System;
using System.Collections.Generic;
using System.Linq;
namespace Playgound.Katas
{
    public static class CCC
    {
        /// <summary>
        /// https://www.codewars.com/kata/counting-change-combinations/csharp
        /// </summary>
        /// <returns>The combinations.</returns>
        /// <param name="money">Money.</param>
        /// <param name="coins">Coins.</param>
        public static int CountCombinations(int money, int[] coins)
        {
            int[] s = new int[money + 1];
            s[0] = 1;
            foreach (int coin in coins)
            {
                for (int i = coin; i <= money; i++)
                {
                    s[i] += s[i - coin];
                }
            }

            return s[money];
        }
    }
}
