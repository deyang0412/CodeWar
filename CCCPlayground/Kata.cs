using System;
using System.Collections.Generic;
using System.Linq;

namespace CCCPlayground
{
    public static class Kata
    {
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