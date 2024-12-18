﻿namespace Leetcode2024
{
    public class DynamicProgrammingLC
    {
        #region 5. Longest Palindromic Substring
        public string LongestPalindrome(string s)
        {
            int start = 0, end = 0;
            bool[][] dp = new bool[s.Length][];

            for (int i = 0; i < s.Length; i++)
            {
                dp[i] = new bool[s.Length];
                dp[i][i] = true;
            }

            for (int i = 0; i < s.Length - 1; i++)
            {
                if (s[i] == s[i + 1])
                {
                    dp[i][i + 1] = true;
                    start = i;
                    end = i + 1;
                }
            }

            for (int len = 2; len <= s.Length; len++)
            {
                for (int i = 0; i < s.Length - len; i++)
                {
                    if (s[i] == s[i + len] && dp[i + 1][i + len - 1])
                    {
                        dp[i][i + len] = true;
                        start = i;
                        end = i + len;
                    }
                }
            }


            return s.Substring(start, end - start + 1);
        }
        public string LongestPalindrome_1(string s)
        {
            string result = string.Empty;

            for (int i = 0; i < s.Length; i++)
            {
                string oddLen = LongestPalindrome_helper_1(s, i, i);
                string evnLen = LongestPalindrome_helper_1(s, i, i + 1);

                if (oddLen.Length > result.Length)
                {
                    result = oddLen;
                }
                if (evnLen.Length > result.Length)
                {
                    result = evnLen;
                }
            }
            return result;
        }

        private string LongestPalindrome_helper_1(string s, int left, int right)
        {
            while (left >= 0 && right < s.Length && s[left] == s[right])
            {
                left--;
                right++;
            }

            return s.Substring(left + 1, right - left - 1);
        }
        #endregion

        #region 62. Unique Paths
        public int UniquePaths(int m, int n)
        {

            int[] dp = new int[n];
            for (int i = 0; i < n; i++)
            {
                dp[i] = 1;
            }

            for (int i = 1; i < m; i++)
            {
                for (int j = 1; j < n; j++)
                {
                    dp[j] += dp[j - 1];
                }
            }

            return dp[n - 1];
        }

        public int UniquePaths_V1(int m, int n)
        {
            int[][] dp = new int[n][];
            for (int i = 0; i < n; i++)
            {
                dp[i] = new int[m];
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        dp[i][j] = 1;
                        continue;
                    }

                    dp[i][j] = dp[i - 1][j] + dp[i][j - 1];
                }
            }

            return dp[n - 1][m - 1];
        }
        #endregion

        #region 63. Unique Paths II
        public int UniquePathsWithObstacles(int[][] obstacleGrid)
        {
            int m = obstacleGrid.Length;
            int n = obstacleGrid[0].Length;

            if (obstacleGrid[0][0] == 1 || obstacleGrid[m - 1][n - 1] == 1) return 0;

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (obstacleGrid[i][j] == 1)
                    {
                        obstacleGrid[i][j] = -1;
                        continue;
                    }

                    if (i == 0 && j == 0) { obstacleGrid[i][j] = 1; continue; }

                    if (i == 0)
                    {
                        obstacleGrid[i][j] = obstacleGrid[i - 1][j] == -1 ? -1 : 1;
                        continue;
                    }

                    if (j == 0)
                    {
                        obstacleGrid[i][j] = obstacleGrid[i][j - 1] == -1 ? -1 : 1;
                        continue;
                    }

                    if (obstacleGrid[i - 1][j] == -1 && obstacleGrid[i][j - 1] == -1)
                    {
                        obstacleGrid[i][j] = -1;
                        continue;
                    }

                    if (obstacleGrid[i - 1][j] == -1)
                    {
                        obstacleGrid[i][j] = obstacleGrid[i][j - 1];
                        continue;
                    }


                    if (obstacleGrid[i][j - 1] == -1)
                    {
                        obstacleGrid[i][j] = obstacleGrid[i - 1][j];
                        continue;
                    }

                    obstacleGrid[i][j] = obstacleGrid[i - 1][j] + obstacleGrid[i][j - 1];
                }
            }

            return obstacleGrid[m - 1][n - 1] == -1 ? 0 : obstacleGrid[m - 1][n - 1];
        }
        #endregion

        #region 64. Minimum Path Sum
        public int MinPathSum(int[][] grid)
        {
            int m = grid.Length;
            int n = grid[0].Length;

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == 0 && j == 0) continue;
                    if (i == 0) { grid[i][j] += grid[i][j - 1]; continue; }
                    if (j == 0) { grid[i][j] += grid[i - 1][j]; continue; }
                    grid[i][j] += Math.Min(grid[i - 1][j], grid[i][j - 1]);
                }
            }

            return grid[m - 1][n - 1];
        }
        #endregion

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

        #region 120. Triangle
        public int MinimumTotal(IList<IList<int>> triangle)
        {
            int[] dp = new int[triangle.Count + 1];

            for (int i = triangle.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < triangle[i].Count; j++)
                {
                    dp[j] = Math.Min(dp[j], dp[j + 1]) + triangle[i][j];
                }
            }
            return dp[0];
        }

        public int MinimumTotal_1(IList<IList<int>> triangle)
        {
            for (int i = 1; i < triangle.Count; i++)
            {
                triangle[i][0] += triangle[i - 1][0];
                int j = 1;
                for (; j < triangle[i].Count - 1; j++)
                {
                    triangle[i][j] += Math.Min(triangle[i - 1][j - 1], triangle[i - 1][j]);
                }
                triangle[i][j] += triangle[i - 1][j - 1];
            }
            return Min(triangle[triangle.Count - 1]);
        }

        #endregion

        #region 139. Word Break
        //private int[] memo;
        public bool WordBreak(string s, IList<string> wordDict)
        {
            HashSet<string> set = new HashSet<string>(wordDict);
            int[] memo = new int[s.Length];
            Array.Fill(memo, -1);

            return WordBreakHelper(s, set, memo, s.Length - 1);
        }

        private bool WordBreakHelper(string s, HashSet<string> set, int[] memo, int index)
        {
            if (index < 0) return true;

            if (memo[index] != -1) return memo[index] == 1;

            foreach (string word in set)
            {
                if (index - word.Length + 1 < 0) continue;

                if (s.Substring(index - word.Length + 1, word.Length) == word && WordBreakHelper(s, set, memo, index - word.Length))
                {
                    memo[index] = 1;
                    return true;
                }
            }
            memo[index] = 0;
            return false;
        }

        public bool WordBreak_1(string s, IList<string> wordDict)
        {
            HashSet<string> set = new HashSet<string>(wordDict);
            bool[] dp = new bool[s.Length + 1];

            dp[0] = true;

            for (int i = 1; i <= s.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (dp[j] && set.Contains(s.Substring(j, i - j)))
                    {
                        dp[i] = true;
                        break;
                    }
                }
            }

            return dp[s.Length];
        }

        public bool WordBreak_WithHashset(string s, IList<string> wordDict)
        {
            HashSet<string> strings = new HashSet<string>(wordDict);
            return WordBreak_helper_withHashSet(s, 0, strings);
        }

        private bool WordBreak_helper_withHashSet(string s, int start, HashSet<string> set)
        {
            if (start == s.Length) return true;

            for (int end = start + 1; end <= s.Length; end++)
            {
                string prefix = s.Substring(start, end - start);

                if (set.Contains(prefix) && WordBreak_helper_withHashSet(s, end, set)) { return true; }
            }
            return false;
        }

        public bool WordBreak_RecursionList(string s, IList<string> wordDict)
        {
            return WordBreak_helper_RecursionList(s, 0, wordDict);
        }

        public bool WordBreak_helper_RecursionList(string s, int start, IList<string> wordDict)
        {
            if (start == s.Length) return true;

            for (int end = start + 1; end <= s.Length; end++)
            {
                string prefix = s.Substring(start, end - start);

                if (wordDict.Contains(prefix) && WordBreak_helper_RecursionList(s, end, wordDict))
                {
                    return true;
                }
            }

            return false;
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

        #region 516. Longest Palindromic Subsequence
        public int LongestPalindromeSubseq(string s)
        {
            int[][] dp = new int[s.Length][];

            for (int i = 0; i < s.Length; i++)
            {
                dp[i] = new int[s.Length];
                dp[i][i] = 1;
            }

            for (int i = 0; i < s.Length - 1; i++)
            {
                if (s[i] == s[i + 1])
                {
                    dp[i][i + 1] = 2;
                }
                else
                {
                    dp[i][i + 1] = 1;
                }
            }

            for (int j = 2; j < s.Length; j++)
            {
                for (int i = 0; i < s.Length - j; i++)
                {
                    if (s[i] == s[i + j])
                    {
                        dp[i][i + j] = 2 + dp[i + 1][i + j - 1];
                    }
                    else
                    {
                        dp[i][i + j] = Math.Max(dp[i][i + j - 1], dp[i + 1][i + j]);
                    }
                }
            }
            return dp[0][s.Length - 1];
        }
        #endregion

        #region 740. Delete and Earn

        public int DeleteAndEarn(int[] nums)
        {
            int maxNum = Max(nums);

            int[] dp1 = new int[(int)maxNum + 1];

            foreach (int n in nums) dp1[n] += n;

            int[] dp = new int[(int)maxNum + 1];
            dp[1] = dp1[1];

            for (int i = 2; i <= maxNum; i++)
            {
                dp[i] = Math.Max(dp[i - 1], dp[i - 2] + dp1[i]);
            }

            return dp[maxNum];
        }

        public int DeleteAndEarn_V2(int[] nums)
        {
            Array.Sort(nums);
            Dictionary<int, int> map = new Dictionary<int, int>();

            foreach (int n in nums)
            {
                if (!map.ContainsKey(n))
                {
                    map.Add(n, 0);
                }
                map[n] += n;
            }

            int prev1 = 0;

            int[] keys = map.Keys.ToArray();
            int[] values = map.Values.ToArray();
            int prev2 = values[0];
            for (int i = 1; i < map.Keys.Count; i++)
            {
                int currEarn = values[i];
                int temp;
                if (keys[i] == keys[i - 1] + 1)
                {
                    temp = prev2;
                    prev2 = Math.Max(currEarn + prev1, prev2);
                    prev1 = temp;
                }
                else
                {
                    temp = prev2;
                    prev2 = currEarn + prev2;
                    prev1 = temp;
                }

            }
            return prev2;
        }
        public int DeleteAndEarn_v1(int[] nums)
        {
            SortedDictionary<int, int> map = new SortedDictionary<int, int>();

            foreach (int n in nums)
            {
                if (!map.ContainsKey(n))
                {
                    map.Add(n, 0);
                }
                map[n] += n;
            }

            if (map.Count == 1) return map.Values.First();

            int[] dp = new int[map.Count];


            int[] keys = map.Keys.ToArray();
            int[] values = map.Values.ToArray();

            dp[0] = values[0];
            dp[1] = values[1];

            if (keys[1] - keys[0] == 1) { dp[1] = Math.Max(dp[1], dp[0]); }
            else dp[1] += dp[0];

            if (map.Count > 2)
            {
                dp[2] = values[2];

                if (keys[2] - keys[1] == 1)
                {
                    dp[2] += dp[0];
                    dp[2] = Math.Max(dp[2], dp[1]);
                }
                else dp[2] += dp[1];


            }


            for (int i = 3; i < map.Count; i++)
            {
                dp[i] = values[i];

                if (keys[i] - keys[i - 1] == 1)
                {
                    dp[i] += dp[i - 2];
                    dp[i] = Math.Max(dp[i], dp[i - 2]);
                }
                else
                {
                    dp[i] += dp[i - 1];
                }
            }

            return Math.Max(dp[map.Count - 1], dp[map.Count - 2]);

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

        #region 931. Minimum Falling Path Sum
        public int MinFallingPathSum(int[][] matrix)
        {
            if (matrix.Length == 1) return matrix[0][0];

            if (matrix.Length == 2)
            {
                return Math.Min(matrix[0][0], matrix[0][1]) + Math.Min(matrix[1][0], matrix[1][1]);
            }

            for (int i = 1; i < matrix.Length; i++)
            {
                matrix[i][0] += Math.Min(matrix[i - 1][0], matrix[i - 1][1]);
                int j = 1;
                for (; j < matrix[i].Length - 1; j++)
                {
                    matrix[i][j] += Math.Min(matrix[i - 1][j], Math.Min(matrix[i - 1][j - 1], matrix[i - 1][j + 1]));
                }
                matrix[i][j] += Math.Min(matrix[i - 1][j], matrix[i - 1][j - 1]);
            }
            return Min(matrix[matrix.Length - 1]);
        }
        #endregion

        #region Private Common Methods
        private int Max(int[] array)
        {
            int maxNum = 0;
            foreach (var item in array)
            {
                if (item > maxNum)
                {
                    maxNum = item;
                }
            }
            return maxNum;
        }
        private int Min(IList<int> list)
        {
            int maxNum = list[0];
            foreach (var item in list)
            {
                if (item < maxNum)
                {
                    maxNum = item;
                }
            }
            return maxNum;
        }
        private int Min(int[] array)
        {
            int maxNum = array[0];
            foreach (var item in array)
            {
                if (item < maxNum)
                {
                    maxNum = item;
                }
            }
            return maxNum;
        }
        #endregion
    }
}
