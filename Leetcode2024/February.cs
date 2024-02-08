using Leetcode2024.Common.Models;
using System.Text;

namespace Leetcode2024
{
    public class February
    {
        #region Day 1 2966. Divide Array Into Arrays With Max Difference
        public int[][] DivideArray(int[] nums, int k)
        {
            Array.Sort(nums);
            List<int[]> lst = new List<int[]>();

            for (int i = 0; i < nums.Length - 2;)
            {

                if (nums[i + 1] - nums[i] <= k && nums[i + 2] - nums[i] <= k)
                {
                    lst.Add(new int[] { nums[i++], nums[i++], nums[i++] });
                }
                else
                {
                    return Array.Empty<int[]>();
                }
            }

            return lst.ToArray();
        }
        #endregion

        #region Day 5 387. First Unique Character in a String
        public int FirstUniqChar(string s)
        {
            if (s.Length == 1) { return 0; }

            int[] arr = Enumerable.Repeat(-2, 26).ToArray();

            int index = -1;

            while (++index < s.Length)
            {
                int norm = s[index] - 97;
                if (arr[norm] == -2)
                {
                    arr[norm] = index;
                }
                else
                {
                    arr[norm] = -1;
                }
            }

            int minIndex = int.MaxValue;
            for (int i = 0; i < 26; i++)
            {
                if (arr[i] < 0) continue;

                minIndex = Math.Min(minIndex, arr[i]);
            }

            return minIndex == int.MaxValue ? -1 : minIndex;
        }
        #endregion

        #region Day 6 49. Group Anagrams

        public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();

            foreach (string str in strs)
            {
                char[] ch = str.ToCharArray();
                Array.Sort(ch);

                if (!map.ContainsKey(new string(ch)))
                {
                    map.Add(new string(ch), new List<string>());
                }

                map[new string(ch)].Add(str);
            }

            return new List<IList<string>>(map.Values);
        }

        #endregion

        #region Day 7 451. Sort Characters By Frequency
        public string FrequencySort(string s)
        {
            int[] arr = new int[123];

            foreach (char c in s)
            {
                arr[(int)c]++;
            }

            var sortedDictionary = arr.Select((count, ch) => new { count, ch }).Where(x => x.count > 0).OrderByDescending(x => x.count);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in sortedDictionary)
            {
                int count = item.count;
                while (count > 0)
                {
                    stringBuilder.Append((char)item.ch);
                    count--;
                }
            }
            return stringBuilder.ToString();
        }
        #endregion

        #region Day 8 101. Symmetric Tree

        public bool IsSymmetric(TreeNode root)
        {
            return IsSymmetric(root.left, root.right);
        }

        private bool IsSymmetric(TreeNode left, TreeNode right)
        {
            if (left == null && right == null) return true;

            return left?.val == right?.val && IsSymmetric(left.left, right.right) && IsSymmetric(left.right, right.left);
        }

        #endregion

        #region 143. Reorder List

        public void ReorderList(ListNode head)
        {
            ListNode slow = head;
            ListNode fast = head;

            while (fast != null && fast.next != null)
            {
                slow = slow.next; fast = fast.next.next;
            }

            ListNode second = reverseList(slow.next);

            slow.next = null;
            ListNode first = head;

            while (second != null)
            {
                ListNode temp1 = first.next;
                ListNode temp2 = second.next;
                first.next = second;
                second.next = temp1;
                first = temp1;
                second = temp2;
            }
        }


        private ListNode reverseList(ListNode head)
        {
            ListNode prev = null;
            ListNode current = head;
            ListNode next = null;

            while (current != null)
            {
                next = current.next;
                current.next = prev;
                prev = current;
                current = next;
            }

            return prev;
        }
        #endregion
    }
}
