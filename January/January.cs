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

    }
}