namespace Leetcode2024
{
    public class DynamicProgrammingLC
    {
        #region 70. Climbing Stairs
        public int ClimbStairs_Recursion(int n)
        {
            if (n == 1) return 1;
            if (n == 2) return 2;

            return ClimbStairs_Recursion(n - 1) + ClimbStairs_Recursion(n - 2);
        }

        public int ClimbStairs(int n)
        {
            if (n == 1) return 1;
            if (n == 2) return 2;
            int[] dp = new int[n];
            dp[0] = 1;
            dp[1] = 2;

            for (int i = 2; i < n; i++)
            {
                dp[i] = dp[i - 1] + dp[i - 2];
            }

            return dp[n - 1];
        }
        #endregion

        #region 509. Fibonacci Number
        public int FibRecursion(int n)
        {
            if (n <= 1) return n;

            return FibRecursion(n - 1) + FibRecursion(n - 2);
        }
        public int Fib_V1(int n)
        {
            if (n <= 1) return n;
            if (n == 2) return 1;
            int[] dp = new int[n + 1];
            dp[1] = 1;
            dp[2] = 1;
            for (int i = 2; i < n + 1; i++)
            {
                dp[i] = dp[i - 1] + dp[i - 2];
            }

            return dp[n];
        }


        #endregion

        #region 746. Min Cost Climbing Stairs
        public int MinCostClimbingStairs(int[] cost)
        {
            int[] dp = new int[cost.Length];
            dp[0] = cost[0];
            dp[1] = cost[1];
            for (int i = 2; i < cost.Length; i++)
            {
                dp[i] = cost[i] + Math.Min(dp[i - 1], dp[i - 2]);
            }
            return Math.Min(dp[cost.Length - 1], dp[cost.Length - 2]);
        }

        //private int MinCostClimbingStairs(int[] cost, int index, int currentCost)
        //{
        //    if (index >= cost.Length) return currentCost;
        //    return Math.Min(MinCostClimbingStairs(cost, index + 1, currentCost + cost[index + 1]), MinCostClimbingStairs(cost, index + 2, currentCost + cost[index + 2]));
        //}
        #endregion
    }
}
