using System.Collections;

namespace Leetcode2024.Common.Helper
{
    public class TimeComparer : IComparer<string>
    {
        public int Compare(string? x, string? y)
        {
            if (string.IsNullOrEmpty(x) || string.IsNullOrEmpty(y)) throw new ArgumentException();

            string[] x1 = x.Split(':');
            string[] y1 = y.Split(':');

            if (x1.Length != y1.Length
                || x1.Length != 2
                || !int.TryParse(x1[0], out int hr1)
                || !int.TryParse(y1[0], out int hr2)
                || !int.TryParse(x1[1], out int min1)
                || !int.TryParse(y1[1], out int min2)) throw new ArgumentException();

            if (x != y)
            {
                if (hr1 == hr2)
                {
                    return min1 < min2 ? -1 : 1;
                }

                return hr1 < hr2 ? -1 : 1;

            }
            return 0;
        }
    }
}
