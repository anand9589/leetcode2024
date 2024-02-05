﻿using Leetcode2024.Common.Models;

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

            int[] arr = Enumerable.Repeat(-2,26).ToArray();

            int index = -1;

            while (++index < s.Length)
            {
                int norm =  s[index]-97;
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
            for(int i = 0; i<26; i++) 
            {
                if (arr[i] < 0) continue;

                minIndex = Math.Min(minIndex, arr[i]);
            }

            return minIndex == int.MaxValue ? -1 : minIndex;
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
