using static System.Net.Mime.MediaTypeNames;

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
                for (int i = 0; i < list.Count-1; i++)
                {
                    res += list[i] * list[i+1];
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
    }
}