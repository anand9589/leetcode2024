namespace GeeksForGeeks
{
    public class Solution
    {
        #region Array SubSet Problem
        public string isSubset(int[] a1, int[] a2, int n, int m)
        {
            if (m > n) return "No";

            Dictionary<int, int> map = new Dictionary<int, int>();
            int i = 0;
            for (; i < m; i++)
            {
                if (!map.ContainsKey(a1[i]))
                {
                    map.Add(a1[i], 0);
                }
                if (!map.ContainsKey(a2[i]))
                {
                    map.Add(a2[i], 0);
                }
                map[a1[i]] += 1;
                map[a2[i]] -= 1;
            }

            for (; i < n; i++)
            {
                if (!map.ContainsKey(a1[i]))
                {
                    map.Add(a1[i], 0);
                }
                map[a1[i]] += 1;
            }

            foreach (var key in map.Keys)
            {
                if (map[key] < 0) return "No";
            }

            return "Yes";
        }
        #endregion
    }
}