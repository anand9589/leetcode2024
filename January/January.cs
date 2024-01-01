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

    }
}