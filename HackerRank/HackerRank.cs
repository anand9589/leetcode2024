namespace HackerRank
{
    public class HackerRank
    {
        public static List<int> matchingStrings(List<string> stringList, List<string> queries)
        {
            Dictionary<string, int> map = new Dictionary<string, int>();
            List<int> result = new List<int>();
            foreach (string s in stringList)
            {
                if (!map.ContainsKey(s))
                {
                    map.Add(s, 1);
                }
                else
                {
                    map[s]++;
                }
            }

            foreach (var q in queries)
            {
                if (map.ContainsKey(q))
                {
                    result.Add(map[q]);
                }
                else
                {
                    result.Add(0);
                }
            }

            return result;
        }
    }
}