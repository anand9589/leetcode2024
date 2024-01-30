namespace January
{
    public class January
    {
        #region Day 1 455. Assign Cookies

        public int FindContentChildren(int[] g, int[] s)
        {
            Array.Sort(g);
            Array.Sort(s);

            int i = 0;
            int j = 0;
            int contentChildren = 0;
            while (i < g.Length && j < s.Length)
            {
                if (s[j] >= g[i])
                {
                    contentChildren++;
                    i++;
                }
                j++;

            }
            return contentChildren;
        }

        #endregion

        #region Day 2 2610. Convert an Array Into a 2D Array With Conditions
        public IList<IList<int>> FindMatrix(int[] nums)
        {
            IList<IList<int>> ls = new List<IList<int>>();

            Dictionary<int, int> dct = new Dictionary<int, int>();
            foreach (int i in nums)
            {
                if (!dct.ContainsKey(i))
                {
                    dct.Add(i, 1);
                }
                else
                {
                    dct[i]++;
                }
            }

            dct = dct.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            int k1 = dct.Keys.First();
            int v1 = dct.Values.First();
            for (int i = 0; i < v1; i++)
            {
                ls.Add(new List<int>() { k1 });
            }
            dct.Remove(k1);

            foreach (var key in dct.Keys)
            {
                for (int i = 0; i < dct[key]; i++)
                {
                    ls[i].Add(key);
                }
            }

            return ls;
        }
        #endregion

        #region Day 3 2125. Number of Laser Beams in a Bank
        public int NumberOfBeams(string[] bank)
        {
            int res = 0;

            List<int> list = new List<int>();

            foreach (string bankString in bank)
            {
                int count = 0;
                foreach (char c in bankString)
                {
                    if (c == '1')
                    {
                        count++;
                    }
                }
                if (count > 0) list.Add(count);
            }

            if (list.Count > 1)
            {
                for (int i = 0; i < list.Count - 1; i++)
                {
                    res += list[i] * list[i + 1];
                }
            }

            return res;
        }
        #endregion

        #region Day 4 2870. Minimum Number of Operations to Make Array Empty
        public int MinOperations(int[] nums)
        {
            int res = 0;
            Dictionary<int, int> dct = new Dictionary<int, int>();

            foreach (int i in nums)
            {
                if (!dct.ContainsKey(i))
                {
                    dct.Add(i, 0);
                }
                dct[i]++;
            }

            foreach (var key in dct.Keys)
            {
                if (!makeEmpty(key, dct[key], ref res)) return -1;
            }

            return res;
        }

        private bool makeEmpty(int key, int count, ref int res)
        {
            if (count == 1) return false;
            if (count <= 3)
            {
                res++;
                return true;
            }
            if (count <= 6)
            {
                count -= 3;
                res += 2;
                return true;
            }

            res++;
            count -= 3;
            return makeEmpty(key, count, ref res);
        }
        #endregion

        #region Day 5 300. Longest Increasing Subsequence
        public int LengthOfLIS(int[] nums)
        {
            int res = 1;

            if (nums.Length > 1)
            {
                int[] dp = Enumerable.Repeat(1, nums.Length).ToArray();

                for (int i = 1; i < nums.Length; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (nums[i] > nums[j])
                        {
                            dp[i] = Math.Max(dp[i], 1 + dp[j]);
                        }
                    }
                    res = Math.Max(res, dp[i]);
                }
            }

            return res;
        }
        #endregion

        #region Day 6 1235. Maximum Profit in Job Scheduling
        public int JobScheduling(int[] start, int[] end, int[] points)
        {
            var intervals = new List<int[]>();
            for (int i = 0; i < start.Length; i++)
                intervals.Add(new int[] { start[i], end[i], points[i] });

            intervals.Sort((x, y) => x[1].CompareTo(y[1]));
            List<int> dpProfit = new List<int>() { 0 }, dpEndTime = new List<int>() { 0 };

            foreach (var interval in intervals)
            {
                int s = interval[0], e = interval[1], p = interval[2];
                int prevIndex = dpEndTime.BinarySearch(s + 1);
                if (prevIndex < 0) prevIndex = ~prevIndex;
                prevIndex--;
                int currProfit = dpProfit[prevIndex] + p, maxProfit = dpProfit[dpProfit.Count - 1];
                if (currProfit > maxProfit)
                {
                    dpProfit.Add(currProfit);
                    dpEndTime.Add(e);
                }
            }

            return dpProfit[dpProfit.Count - 1];
        }

        #endregion

        #region Day 7 446. Arithmetic Slices II - Subsequence
        public int NumberOfArithmeticSlices(int[] A)
        {
            int counts = 0;
            if (A.Length == 0)
            {
                return counts;
            }

            Dictionary<int, int>[] dp = new Dictionary<int, int>[A.Length];
            dp[0] = new Dictionary<int, int>();
            for (int i = 1; i < A.Length; i++)
            {
                dp[i] = new Dictionary<int, int>();
                for (int j = i - 1; j >= 0; j--)
                {
                    long diff = (long)A[i] - (long)A[j];
                    if (diff <= Int32.MinValue || diff > Int32.MaxValue) continue;
                    int df = (int)diff;
                    if (!dp[i].ContainsKey(df))
                    {
                        dp[i][df] = 0;
                    }

                    dp[i][df]++;

                    // Get j's dictionary for the given diff.
                    if (dp[j].ContainsKey(df))
                    {
                        var jsDiffCounts = dp[j][df];
                        dp[i][df] += jsDiffCounts;
                        counts += dp[j][df];
                    }
                }
            }

            return counts;

        }
        #endregion

        #region Day 30 150. Evaluate Reverse Polish Notation
        public int EvalRPN(string[] tokens)
        {
            Stack<int> stack = new Stack<int>();
            foreach (string token in tokens)
            {
                int n1, n2;
                switch (token)
                {
                    case "+":
                        n1 = stack.Pop();
                        n2 = stack.Pop();
                        stack.Push(n1 + n2);
                        break;
                    case "-":
                        n1 = stack.Pop();
                        n2 = stack.Pop();
                        stack.Push(n2 - n1);
                        break;
                    case "*":
                        n1 = stack.Pop();
                        n2 = stack.Pop();
                        stack.Push(n2 * n1);
                        break;
                    case "/":
                        n1 = stack.Pop();
                        n2 = stack.Pop();
                        stack.Push(n2 / n1);
                        break;
                    default:
                        stack.Push(int.Parse(token));
                        break;
                }
            }

            return stack.Pop();
        }
        #endregion
    }
}