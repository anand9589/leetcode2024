using Leetcode2024.Common.Models;
using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace Leetcode2024
{
    public class LeetCode
    {
        #region 3. Longest Substring Without Repeating Characters
        public int LengthOfLongestSubstring(string s)
        {
            if (s.Length <= 1) return s.Length;
            int longestSubstringLength = 1;
            HashSet<char> map = new HashSet<char> { s[0] };
            int i = 0, start = 0;
            while (s.Length > ++i)
            {
                char c = s[i];

                if (map.Contains(c))
                {
                    longestSubstringLength = Math.Max(longestSubstringLength, i - start);

                    while (s[start] != c)
                    {
                        map.Remove(s[start]);
                        start++;
                    }
                    start++;
                }
                else
                {
                    map.Add(c);
                }
            }
            longestSubstringLength = Math.Max(longestSubstringLength, i - start);
            return longestSubstringLength;
        }

        public int LengthOfLongestSubstring1(string s)
        {
            if (s.Length <= 1) return s.Length;
            int longestSubstringLength = 1;
            HashSet<char> map = new HashSet<char>();
            Queue<(char c, int index)> q = new Queue<(char c, int index)>();

            q.Enqueue((s[0], 0));
            map.Add(s[0]);
            int i = 0;
            int start = 0;
            while (s.Length > ++i)
            {
                char c = s[i];

                if (map.Contains(c))
                {
                    longestSubstringLength = Math.Max(longestSubstringLength, i - start);
                    while (q.Peek().c != c)
                    {
                        var k = q.Dequeue();
                        map.Remove(k.c);
                    }
                    q.Dequeue();

                    q.Enqueue((c, i));
                    start = q.Peek().index;
                }
                else
                {
                    map.Add(c);
                    q.Enqueue((c, i));

                }
            }
            longestSubstringLength = Math.Max(longestSubstringLength, i - start);
            return longestSubstringLength;
        }
        #endregion

        #region 20. Valid Parentheses
        public bool IsValid(string s)
        {
            int i = -1;
            Stack<char> stack = new Stack<char>();
            while (++i < s.Length)
            {
                switch (s[i])
                {
                    case '}':
                        if (stack.Count == 0 || stack.Peek() != '{') return false;
                        stack.Pop();
                        break;
                    case ')':
                        if (stack.Count == 0 || stack.Peek() != '(') return false;
                        stack.Pop();
                        break;
                    case ']':
                        if (stack.Count == 0 || stack.Peek() != '[') return false;
                        stack.Pop();
                        break;
                    default:
                        stack.Push(s[i]);
                        break;
                }
            }
            return true;
        }
        #endregion

        #region 23. Merge k Sorted Lists
        public ListNode MergeKLists(ListNode[] lists)
        {

            return MergeKListDivide(lists, 0, lists.Length - 1);
        }

        private ListNode MergeKListDivide(ListNode[] lists, int low, int high)
        {
            if (low > high) return null;
            if (low == high) return lists[low];

            int mid = (low + high) / 2;

            ListNode l1 = MergeKListDivide(lists, low, mid);
            ListNode l2 = MergeKListDivide(lists, mid + 1, high);

            return MergeKListConquer(l1, l2);
        }

        private ListNode MergeKListConquer(ListNode l1, ListNode l2)
        {
            ListNode dummy = new ListNode(-1);
            ListNode temp = dummy;
            while (l1 != null && l2 != null)
            {
                if (l1.val <= l2.val)
                {
                    temp.next = l1;
                    l1 = l1.next;
                }
                else
                {
                    temp.next = l2;
                    l2 = l2.next;
                }
                temp = temp.next;
            }
            update(ref l1, ref temp);
            update(ref l2, ref temp);
            return dummy.next;
        }

        private static void update(ref ListNode l1, ref ListNode temp)
        {
            while (l1 != null)
            {
                temp.next = l1;
                l1 = l1.next;
                temp = temp.next;
            }
        }

        public ListNode MergeKLists1(ListNode[] lists)
        {
            int len = lists.Length;
            ListNode dummy = new ListNode(-1);

            ListNode temp = dummy;
            PriorityQueue<ListNode, int> pq = new PriorityQueue<ListNode, int>();

            foreach (var item in lists)
            {
                ListNode t = item;
                if (t != null)
                {
                    pq.Enqueue(t, t.val);
                }
            }

            while (pq.Count > 0)
            {

                temp.next = pq.Dequeue();

                temp = temp.next;

                if (temp.next != null)
                {
                    pq.Enqueue(temp.next, temp.next.val);

                    temp.next = null;
                }
            }

            return dummy.next;
        }
        #endregion

        #region 30. Substring with Concatenation of All Words
        public IList<int> FindSubstring(string s, string[] words)
        {
            HashSet<int> result = new HashSet<int>();
            int wLen = words[0].Length;
            int requiredLength = words.Length * wLen;

            if (requiredLength <= s.Length)
            {
                Dictionary<string, int> map = new Dictionary<string, int>();
                foreach (var word in words)
                {
                    if (map.ContainsKey(word))
                    {
                        map[word]++;
                    }
                    else
                    {
                        map[word] = 1;
                    }
                }
                int i = 0;

                while (i < s.Length - requiredLength + 1)
                {
                    if (result.Contains(i)) { i++; continue; }
                    string curWord = s.Substring(i, wLen);
                    if (map.ContainsKey(curWord))
                    {

                        if (allWordsFound(s.Substring(i, requiredLength), wLen, new Dictionary<string, int>(map)))
                        {
                            result.Add(i);
                            if (i + requiredLength + wLen <= s.Length)
                            {
                                int nextIndex = i + requiredLength;
                                string nextWord = s.Substring(nextIndex, wLen);
                                while (nextWord == curWord)
                                {
                                    result.Add(nextIndex - i - wLen);
                                    if (nextIndex + wLen <= s.Length)
                                    {
                                        curWord = nextWord;
                                        nextIndex += wLen;
                                        nextWord = s.Substring(nextIndex, wLen);
                                    }
                                    else
                                    {
                                        nextWord = "";
                                    }
                                }
                            }
                        }
                    }



                    i++;
                }
            }

            return result.ToList();
        }

        private bool allWordsFound(string s, int len, Dictionary<string, int> map)
        {
            int index = 0;

            while (index + len <= s.Length)
            {
                string str = s.Substring(index, len);
                if (!map.ContainsKey(str) || map[str] == 0) return false;
                map[str]--;
                index += len;
            }
            return true;
        }

        public IList<int> FindSubstring1(string s, string[] words)
        {
            List<int> result = new List<int>();
            int wLen = words[0].Length;
            int requiredLength = words.Length * wLen;

            if (requiredLength <= s.Length)
            {
                Dictionary<string, int> map = new Dictionary<string, int>();
                foreach (var word in words)
                {
                    if (map.ContainsKey(word))
                    {
                        map[word]++;
                    }
                    else
                    {
                        map[word] = 1;
                    }
                }
                int i = 0;

                while (i < s.Length - requiredLength + 1)
                {
                    var cloneDictionary = new Dictionary<string, int>(map);

                    bool found = true;
                    int j = 0;

                    while (j < requiredLength && i + j + wLen <= s.Length)
                    {
                        string e = s.Substring(i + j, wLen);
                        if (cloneDictionary.ContainsKey(e) && cloneDictionary[e] > 0)
                        {

                            j += wLen;
                            cloneDictionary[e]--;
                        }
                        else
                        {
                            found = false;
                            break;
                        }
                    }

                    if (found && j == requiredLength)
                    {
                        result.Add(i);
                    }

                    i++;
                }
            }

            return result;
        }
        //public IList<int> FindSubstring(string s, string[] words)
        //{
        //    List<int> ub = new List<int>();
        //    HashSet<int> indexes = new HashSet<int>();
        //    int wLen = words[0].Length;
        //    int requiredLength = words.Length * wLen;

        //    if (requiredLength <= s.Length)
        //    {
        //        Dictionary<string, int> map = new Dictionary<string, int>();
        //        foreach (var word in words)
        //        {
        //            if (map.ContainsKey(word))
        //            {
        //                map[word]++;
        //            }
        //            else
        //            {
        //                map[word] = 1;
        //            }
        //        }
        //        int row = -1;

        //        while (++row <= s.Length - requiredLength)
        //        {
        //            if (indexes.Contains(row)) { continue; }

        //            string curWord = s.Substring(row, wLen);

        //            if (map.ContainsKey(curWord))
        //            {
        //                int startIndex = row;

        //                var cloneDictionary = new Dictionary<string, int>(map);
        //                int col = 0;
        //                while (startIndex < s.Length - requiredLength)
        //                {

        //                    bool found = true;

        //                    while (col < requiredLength && startIndex + col + wLen < s.Length)
        //                    {
        //                        string e = s.Substring(row + col, wLen);
        //                        if (cloneDictionary.ContainsKey(e) && cloneDictionary[e] > 0)
        //                        {

        //                            col += wLen;
        //                            cloneDictionary[e]--;
        //                        }
        //                        else
        //                        {
        //                            found = false;
        //                            break;
        //                        }
        //                    }

        //                    if (found && col == requiredLength)
        //                    {
        //                        indexes.Add(row);
        //                        //startIndex = row + wLen;

        //                        ////cloneDictionary[curWord]++;

        //                        //string nextWord = s.Substring(col + startIndex, wLen);

        //                        //while (nextWord == curWord)
        //                        //{
        //                        //    indexes.Add(startIndex);
        //                        //    startIndex += wLen;
        //                        //    curWord = s.Substring()
        //                        //}

        //                    }

        //                }
        //            }
        //            row++;
        //        }
        //    }

        //    return ub;
        //}
        #endregion

        #region 36. Valid Sudoku
        public bool IsValidSudoku(char[][] board)
        {
            for (int row = 0; row < board.Length; row++)
            {
                for (int col = 0; col < board.Length; col++)
                {
                    if (board[row][col] == '.' || (
                        validInCol(board, col, board[row][col], row)
                        && validInRow(board, row, board[row][col], col)
                        && validInGrid(board, row, col, board[row][col]))) continue;

                    return false;
                }
            }

            return true;
        }

        private bool validInRow(char[][] board, int row, char c, int colIndex)
        {
            int col = -1;

            while (++col < colIndex)
            {
                if (board[row][col] == c) return false;

            }
            while (++col < board.Length)
            {
                if (board[row][col] == c) return false;
            }

            return true;
        }

        private bool validInCol(char[][] board, int col, char c, int rowIndex)
        {
            int row = -1;

            while (++row < rowIndex)
            {
                if (board[row][col] == c) return false;

            }
            while (++row < board.Length)
            {
                if (board[row][col] == c) return false;
            }

            return true;
        }

        private bool validInGrid(char[][] board, int row, int col, char c)
        {
            int rowStart = row < 3 ? 0 : row < 6 ? 3 : 6;
            int rowEnd = rowStart + 3;
            int colStart = col < 3 ? 0 : col < 6 ? 3 : 6;
            int colEnd = colStart + 3;

            for (int r = rowStart; r < rowEnd; r++)
            {
                for (int cc = colStart; cc < colEnd; cc++)
                {
                    if (r == row && cc == col) continue;

                    if (board[r][cc] == c) return false;
                }
            }

            return true;
        }


        #endregion

        #region 46. Permutations
        public IList<IList<int>> Permute(int[] nums)
        {
            IList<IList<int>> result = new List<IList<int>>();


            Permute_BackTrack(result, nums, new HashSet<int>());
            return result;
        }

        private void Permute_BackTrack(IList<IList<int>> result, int[] nums, HashSet<int> list)
        {
            if (list.Count == nums.Length)
            {
                result.Add(new List<int>(list));
                return;
            }

            for (int i = 0; i < nums.Length; i++)
            {
                if (!list.Contains(nums[i]))
                {
                    list.Add(nums[i]);
                    Permute_BackTrack(result, nums, list);
                    list.Remove(nums[i]);
                }
            }
        }
        #endregion

        #region 55. Jump Game
        public bool CanJump(int[] nums)
        {
            if (nums.Length == 1) return true;
            if (nums[0] == 0) return false;

            int CanJumpIndex = nums[0];

            for (int i = 1; i <= CanJumpIndex; i++)
            {
                CanJumpIndex = Math.Max(CanJumpIndex, i + nums[i]);
                if (CanJumpIndex >= nums.Length - 1) return true;
            }
            return false;
        }
        #endregion

        #region 60. Permutation Sequence
        public string GetPermutation(int n, int k)
        {
            int[] arr = new int[n];
            bool[] visit = new bool[n];
            for (int i = 0; i < n; i++)
            {
                arr[i] = i + 1;
            }

            int nCopy = n;

            StringBuilder stringBuilder = new StringBuilder();
            while (k > 1)
            {
                int fact = getFact(--nCopy);
                int pos = 0;
                while (k > fact)
                {
                    pos++;
                    k -= fact;
                }
                int cnt = 0;
                for (int i = 0; i < n; i++)
                {
                    if (!visit[i])
                    {
                        if (cnt == pos)
                        {
                            stringBuilder.Append(arr[i]);
                            visit[i] = true;
                            break;
                        }
                        else
                        {
                            cnt++;
                        }
                    }
                }
            }

            if (k == 0)
            {
                for (int i = n - 1; i >= 0; i--)
                {
                    if (!visit[i])
                    {
                        stringBuilder.Append(arr[i]);
                        visit[i] = true;
                    }
                }
            }
            else if (k == 1)
            {

                for (int i = 0; i < n; i++)
                {
                    if (!visit[i])
                    {
                        stringBuilder.Append(arr[i]);
                        visit[i] = true;
                    }
                }
            }

            return stringBuilder.ToString();
        }

        private int getFact(int v)
        {
            if (v <= 1) return v;

            return v * getFact(v - 1);
        }

        public string GetPermutation_1(int n, int k)
        {
            int[] ints = new int[n];
            bool[] vis = new bool[n];

            for (int i = 0; i < n; i++)
            {
                ints[i] = i + 1;
            }
            List<string> list = new List<string>();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                vis[i] = true;
                sb.Append(ints[i]);
                GetPermutation_BackTrack(list, ints, vis, sb, k);

                vis[i] = false;
                sb.Remove(sb.Length - 1, 1);
            }
            return list.Last();
        }

        private void GetPermutation_BackTrack(List<string> list, int[] ints, bool[] vis, StringBuilder sb, int k)
        {
            if (list.Count == k) return;
            if (sb.Length == ints.Length)
            {
                list.Add(sb.ToString());
                return;
            }
            for (int i = 0; i < ints.Length; i++)
            {
                if (vis[i]) continue;
                vis[i] = true;
                sb.Append(ints[i]);
                GetPermutation_BackTrack(list, ints, vis, sb, k);
                vis[i] = false;
                sb.Remove(sb.Length - 1, 1);
            }
        }
        #endregion

        #region 71. Simplify Path
        public string SimplifyPath(string path)
        {
            string[] paths = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            Stack<string> stack = new Stack<string>();
            foreach (var p in paths)
            {
                switch (p)
                {
                    case "..":
                        stack.TryPop(out string s);
                        break;
                    case ".":
                        break;
                    default:
                        stack.Push(p);
                        break;
                }
            }
            if (stack.Count == 0) return "/";

            StringBuilder sb = new StringBuilder();
            while (stack.Count > 0)
            {
                sb.Insert(0, "/" + stack.Pop());
            }
            return sb.ToString();
        }
        #endregion

        #region 75. Sort Colors
        public void SortColors(int[] nums)
        {
            int low = 0,
            high = nums.Length - 1,
            mid = 0;

            while (mid <= high)
            {
                switch (nums[mid])
                {
                    case 2:
                        swap(nums, mid, high);
                        high--;
                        break;
                    case 0:
                        swap(nums, mid, low);
                        low++;
                        mid++;
                        break;
                    default:
                        mid++;
                        break;
                }
            }

        }
        private void swap(int[] nums, int i, int j)
        {
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }
        #endregion

        #region 77. Combinations
        public IList<IList<int>> Combine(int n, int k)
        {
            IList<IList<int>> list = new List<IList<int>>();
            combine_backTracking(list, 1, n, k, new List<int>());
            return list;
        }

        private void combine_backTracking(IList<IList<int>> list, int startIndex, int n, int k, List<int> sublist)
        {
            if (sublist.Count == k)
            {
                list.Add(new List<int>(sublist));
                return;
            }
            for (int i = startIndex; i <= n; i++)
            {
                sublist.Add(i);
                combine_backTracking(list, i + 1, n, k, sublist);
                sublist.RemoveAt(sublist.Count - 1);
            }
        }
        #endregion

        #region 98. Validate Binary Search Tree
        public bool IsValidBST(TreeNode root)
        {
            return IsValidBST(root, int.MinValue, int.MaxValue);
        }

        private bool IsValidBST(TreeNode root, int minValue, int maxValue)
        {
            if (root == null) return true;

            if (root.val <= minValue || root.val >= maxValue) return false;

            return IsValidBST(root.left, minValue, root.val) && IsValidBST(root.right, root.val, maxValue);
        }


        #endregion

        #region 102. Binary Tree Level Order Traversal
        public IList<IList<int>> LevelOrder(TreeNode root)
        {
            IList<IList<int>> result = new List<IList<int>>();
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                int count = queue.Count;
                IList<int> lst = new List<int>();
                while (count-- > 0)
                {
                    TreeNode node = queue.Dequeue();

                    lst.Add(node.val);

                    if (node.left != null)
                    {
                        queue.Enqueue(node.left);
                    }
                    if (node.right != null)
                    {
                        queue.Enqueue(node.right);
                    }
                }
                result.Add(lst);
            }


            return result;
        }
        #endregion

        #region 103. Binary Tree Zigzag Level Order Traversal
        public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        {
            IList<IList<int>> result = new List<IList<int>>();

            if (root != null)
            {
                Queue<TreeNode> queue = new Queue<TreeNode>();
                queue.Enqueue(root);
                bool reverseFlag = false;
                while (queue.Count > 0)
                {
                    int count = queue.Count;
                    IList<int> lst = new List<int>();
                    while (count-- > 0)
                    {
                        TreeNode node = queue.Dequeue();

                        if (reverseFlag)
                        {
                            lst.Insert(0, node.val);
                        }
                        else
                        {
                            lst.Add(node.val);
                        }


                        if (node.left != null)
                        {
                            queue.Enqueue(node.left);
                        }
                        if (node.right != null)
                        {
                            queue.Enqueue(node.right);
                        }
                    }
                    reverseFlag = !reverseFlag;
                    result.Add(lst);
                }
            }

            return result;
        }
        #endregion

        #region 108. Convert Sorted Array to Binary Search Tree
        public TreeNode SortedArrayToBST(int[] nums)
        {
            int low = 0;
            int high = nums.Length - 1;


            return SortedArrayToBST(nums, low, high);
        }

        public TreeNode SortedArrayToBST(int[] nums, int low, int high)
        {
            if (low > high) return null;
            if (low == high) return new TreeNode(nums[low]);


            int mid = (low + high) / 2;

            TreeNode root = new TreeNode(nums[mid]);

            root.left = SortedArrayToBST(nums, low, mid - 1);
            root.right = SortedArrayToBST(nums, mid + 1, high);

            return root;

        }
        #endregion

        #region 121. Best Time to Buy and Sell Stock
        public int MaxProfit121(int[] prices)
        {
            int res = 0;
            int max = prices[prices.Length - 1];
            for (int i = prices.Length - 2; i >= 0; i--)
            {
                if (prices[i] < max)
                {
                    res = Math.Max(res, max - prices[i]);
                }
                else
                {
                    max = prices[i];
                }
            }

            return res;
        }
        #endregion

        #region 122. Best Time to Buy and Sell Stock II
        public int MaxProfit122(int[] prices)
        {
            int res = 0;

            for (int i = 1; i < prices.Length; i++)
            {
                if (prices[i] > prices[i - 1])
                {
                    res += prices[i] - prices[i - 1];
                }
            }

            return res;
        }
        #endregion

        #region 123. Best Time to Buy and Sell Stock III
        public int MaxProfit(int[] prices)
        {
            int buy1 = int.MaxValue, buy2 = int.MaxValue, profit1 = 0, profit2 = 0;

            foreach (int price in prices)
            {
                buy1 = Math.Min(buy1, price);
                profit1 = Math.Max(profit1, price - buy1);
                buy2 = Math.Min(buy2, price - profit2);
                profit2 = Math.Max(profit2, price - buy2);
            }

            return profit2;
        }
        #endregion

        #region 124. Binary Tree Maximum Path Sum
        int maxPathSum = int.MinValue;
        public int MaxPathSum(TreeNode root)
        {
            MaxPathSum_Helper(root);

            return maxPathSum;
        }


        public int MaxPathSum_Helper(TreeNode root)
        {
            if (root == null) return 0;

            int currSum = root.val;

            int leftSubTree = MaxPathSum_Helper(root.left);
            int rightSubTree = MaxPathSum_Helper(root.right);


            currSum = Math.Max(currSum, currSum + Math.Max(leftSubTree, rightSubTree));

            int sum = root.val + leftSubTree + rightSubTree;
            maxPathSum = Math.Max(sum, maxPathSum);
            maxPathSum = Math.Max(currSum, maxPathSum);
            return currSum;
        }
        #endregion

        #region 125. Valid Palindrome
        public bool IsPalindrome(string s)
        {

            List<char> chars = new List<char>();
            foreach (var item in s)
            {
                if (char.IsLetter(item))
                {
                    if (char.IsUpper(item))
                    {
                        chars.Add((char)(item + 32));
                    }
                    else
                    {
                        chars.Add(item);
                    }
                }
                else if (char.IsDigit(item))
                {
                    chars.Add(item);
                }
            }
            int left = 0;
            int right = chars.Count - 1;

            while (left <= right)
            {
                if (chars[left] != chars[right]) return false;
                left++;
                right--;
            }
            return true;
        }
        #endregion

        #region 128. Longest Consecutive Sequence
        public int LongestConsecutive(int[] nums)
        {
            if (nums.Length == 0) return 0;

            int longestStreak = 1;
            if (nums.Length > 1)
            {
                HashSet<int> set = new HashSet<int>();
                foreach (int n in nums)
                {
                    set.Add(n);
                }

                foreach (int num in nums)
                {
                    if (set.Contains(num - 1)) continue;

                    int currStreak = 1;

                    int currN = num + 1;

                    while (set.Contains(currN))
                    {
                        currN++;
                        currStreak++;
                    }

                    longestStreak = Math.Max(longestStreak, currStreak);
                }
            }
            return longestStreak;
        }
        #endregion

        #region 134. Gas Station
        public int CanCompleteCircuit(int[] gas, int[] cost)
        {
            int totalDiff = 0;
            int remGas = 0;
            int resultIndex = -1;
            for (int i = 0; i < gas.Length; i++)
            {
                int diff = gas[i] - cost[i];
                totalDiff += diff;
                remGas += diff;
                if (remGas < 0)
                {
                    remGas = 0;
                    resultIndex = i;
                }

            }
            return totalDiff < 0 ? -1 : resultIndex + 1;
        }
        public int CanCompleteCircuit_1(int[] gas, int[] cost)
        {
            int i = 0;
            while (i < gas.Length)
            {

                if (completeCircuit(gas, cost, i)) return i;
                i++;
            }
            return -1;
        }
        private bool completeCircuit(int[] gas, int[] cost, int i)
        {
            if (gas[i] < cost[i]) return false;

            int k = i;
            int gasRemaining = 0;
            while (k < gas.Length)
            {
                gasRemaining += gas[k];

                if (gasRemaining < cost[k]) return false;

                gasRemaining -= cost[k];
                k++;
            }
            k = 0;
            while (k < i)
            {
                gasRemaining += gas[k];

                if (gasRemaining < cost[k]) return false;

                gasRemaining -= cost[k];
                k++;

            }
            return true;
        }
        #endregion

        #region 135. Candy
        public int Candy(int[] ratings)
        {
            int[] counter = new int[ratings.Length];
            Array.Fill(counter, 1);

            for (int i = 1; i < ratings.Length; i++)
            {
                if (ratings[i] > ratings[i - 1])
                {
                    counter[i] = counter[i - 1] + 1;
                }
            }
            int res = counter[ratings.Length - 1];

            for (int i = ratings.Length - 2; i >= 0; i--)
            {
                if (ratings[i] > ratings[i + 1] && counter[i] <= counter[i + 1])
                {

                    counter[i] = counter[i + 1] + 1;
                }
                res += counter[i];
            }

            return res;
        }
        #endregion

        #region 139. Word Break
        public bool WordBreak_12(string s, IList<string> wordDict)
        {
            int[] dp = new int[s.Length];
            Array.Fill(dp, -1);
            return WordBreak_Helper_12(s, wordDict, dp, 0);
        }

        private bool WordBreak_Helper_12(string s, IList<string> wordDict, int[] dp, int startIndex)
        {
            if (startIndex == s.Length) return true;

            if (dp[startIndex] != -1) return dp[startIndex] == 1;

            for (int i = startIndex; i < s.Length; i++)
            {
                if (wordDict.Contains(s.Substring(startIndex, i - startIndex + 1)) && WordBreak_Helper_12(s, wordDict, dp, i + 1))
                {
                    dp[startIndex] = 1;
                    return true;
                }
            }
            dp[startIndex] = 0;
            return false;
        }

        public bool WordBreak_1(string s, IList<string> wordDict)
        {
            return WordBreak_Helper_1(s, wordDict, 0);
        }

        private bool WordBreak_Helper_1(string s, IList<string> wordDict, int startIndex)
        {
            if (startIndex == s.Length) return true;

            for (int i = startIndex; i < s.Length; i++)
            {
                if (wordDict.Contains(s.Substring(startIndex, i - startIndex + 1)) && WordBreak_Helper_1(s, wordDict, i + 1)) return true;
            }

            return false;
        }
        #endregion

        #region 140. Word Break II
        public IList<string> WordBreak(string s, IList<string> wordDict)
        {
            var map = new Dictionary<int, List<string>>();



            return WordBreak_Helper(s, wordDict, map, 0);
        }

        private IList<string> WordBreak_Helper(string s, IList<string> wordDict, Dictionary<int, List<string>> map, int start)
        {
            if (map.ContainsKey(start)) return map[start];

            var resultList = new List<string>();

            for (int end = start + 1; end <= s.Length; end++)
            {
                var substring = s.Substring(start, end - start);

                if (wordDict.Contains(substring))
                {
                    if (end == s.Length)
                    {
                        resultList.Add(substring);
                    }
                    else
                    {
                        var subList = WordBreak_Helper(s, wordDict, map, end);

                        foreach (var subResult in subList)
                            resultList.Add($"{substring} {subResult}");
                    }
                }
            }

            map[start] = resultList;
            return resultList;

        }
        #endregion

        #region 150. Evaluate Reverse Polish Notation
        public int EvalRPN(string[] tokens)
        {
            Stack<int> stack = new Stack<int>();
            int num1, num2;

            foreach (var token in tokens)
            {
                int result;
                switch (token)
                {
                    case "+":
                        result = stack.Pop() + stack.Pop();
                        break;
                    case "-":
                        num1 = stack.Pop();
                        num2 = stack.Pop();
                        result = num2 - num1;
                        break;
                    case "*":
                        result = stack.Pop() * stack.Pop();
                        break;
                    case "/":
                        num1 = stack.Pop();
                        num2 = stack.Pop();
                        result = num2 / num1;
                        break;
                    default:
                        result = int.Parse(token);
                        break;
                }
                stack.Push(result);
            }

            return stack.Pop();
        }
        #endregion

        #region 164. Maximum Gap
        public int MaximumGap(int[] nums)
        {
            int result = 0;

            int n = nums.Length;
            if (n < 2) return result;

            int min = nums.Min();
            int max = nums.Max();
            int bucketSize = Math.Max(1, (max - min) / (n - 1));

            int bucketCount = (max - min) / bucketSize + 1;

            (int min, int max)[] buckets = Enumerable.Repeat((int.MaxValue, int.MinValue), bucketCount).ToArray();

            for (int i = 0; i < nums.Length; i++)
            {
                int kIndex = (nums[i] - min) / bucketSize;

                buckets[kIndex].min = Math.Min(buckets[kIndex].min, nums[i]);
                buckets[kIndex].max = Math.Max(buckets[kIndex].max, nums[i]);
            }

            int prevMax = buckets[0].max;

            for (int i = 1; i < buckets.Length; i++)
            {
                if (buckets[i].min == int.MaxValue && buckets[i].max == int.MinValue) continue;

                result = Math.Max(result, buckets[i].min - prevMax);

                prevMax = buckets[i].max;
            }

            return result;
        }

        #endregion

        #region 165. Compare Version Numbers
        public int CompareVersion(string version1, string version2)
        {
            if (version1 != version2)
            {
                string[] ver1 = version1.Split('.');
                string[] ver2 = version2.Split('.');

                int minLength = Math.Min(ver1.Length, ver2.Length);

                for (int i = 0; i < minLength; i++)
                {
                    int n1 = int.Parse(ver1[i]);
                    int n2 = int.Parse(ver2[i]);

                    if (n1 == n2) continue;

                    if (n1 < n2) return -1;

                    return 1;
                }

                if (ver1.Length != ver2.Length)
                {
                    int result;
                    if (ver1.Length > ver2.Length)
                    {
                        result = returnResult(ver1, minLength);
                    }
                    else
                    {
                        result = returnResult(ver2, minLength);

                        if (result != 0) result *= -1;
                    }

                    return result;
                }
            }
            return 0;
        }

        static int returnResult(string[] arr, int startIndex)
        {
            for (int i = startIndex; i < arr.Length; i++)
            {
                int n = int.Parse(arr[i]);

                if (n == 0) continue;

                if (n < 0) return -1;
                return 1;
            }
            return 0;
        }

        #endregion

        #region 166. Fraction to Recurring Decimal
        public string FractionToDecimal(int numerator, int denominator)
        {
            if (numerator == 0) return "0";
            bool posAns = (numerator > 0 && denominator > 0) || (numerator < 0 && denominator < 0);

            long numerator1 = numerator < 0 ? (long)numerator * -1 : (long)numerator;
            long denominator1 = denominator < 0 ? (long)denominator * -1 : (long)denominator;
            StringBuilder stringBuilder = new StringBuilder();
            long division = numerator1 / denominator1;
            long reminder = numerator1 % denominator1;

            stringBuilder.Append(division);
            if (reminder != 0)
            {
                stringBuilder.Append('.');
                int decimalIndex = stringBuilder.Length;
                Dictionary<long, int> map = new Dictionary<long, int>();

                map.Add(reminder, decimalIndex++);
                while (reminder > 0)
                {
                    reminder *= 10;

                    division = reminder / denominator1;

                    stringBuilder.Append(division);

                    reminder = reminder % denominator1;

                    if (map.ContainsKey(reminder))
                    {
                        stringBuilder.Insert(map[reminder], '(');
                        stringBuilder.Append(')');
                        break;
                    }
                    else
                    {
                        map.Add(reminder, decimalIndex++);
                    }
                }
            }

            if (!posAns)
            {
                stringBuilder.Insert(0, '-');
            }

            return stringBuilder.ToString();
        }
        #endregion

        #region 167. Two Sum II - Input Array Is Sorted
        public int[] TwoSum(int[] numbers, int target)
        {
            int index1 = 0, index2 = numbers.Length - 1;

            while (index1 < index2)
            {
                int n1 = numbers[index1];
                int n2 = numbers[index2];

                if (n1 + n2 == target) return new int[] { ++index1, ++index2 };

                if (n1 + n2 < target)
                {
                    index1++;
                }
                else
                {
                    index2++;
                }
            }

            return new int[] { index1, index2 };
        }
        #endregion

        #region 172. Factorial Trailing Zeroes
        public int TrailingZeroes(int n)
        {
            int zeroes = 0;

            while (n >= 5)
            {
                n /= 5;
                zeroes += n;
            }

            return zeroes;
        }
        #endregion

        #region 179. Largest Number
        public string LargestNumber(int[] nums)
        {
            var numStrArr = Array.ConvertAll(nums, x => x.ToString());

            Array.Sort(numStrArr, (x, y) => string.Compare(y + x, x + y));

            if (numStrArr[0] == "0")
            {
                return "0";
            }

            return string.Join("", numStrArr);
        }
        public string LargestNumber_1(int[] nums)
        {
            StringBuilder stringBuilder = new StringBuilder();

            PriorityQueue<int, int> pq = new PriorityQueue<int, int>(new CustomSort_179());

            foreach (var n in nums)
            {
                pq.Enqueue(n, n);
            }

            while (pq.Count > 0)
            {
                stringBuilder.Append(pq.Dequeue());
            }

            return stringBuilder.ToString();
        }

        class CustomSort_179 : IComparer<int>
        {

            public int Compare(int x, int y)
            {
                if (x == y) return 0;

                List<int> lst1 = new List<int>();
                List<int> lst2 = new List<int>();

                int x1 = x;
                int y1 = y;

                while (x1 > 0)
                {
                    lst1.Insert(0, x1 % 10);
                    x1 /= 10;
                }

                while (y1 > 0)
                {
                    lst2.Insert(0, y1 % 10);
                    y1 /= 10;

                }

                if (lst1.Count == lst2.Count)
                {
                    return compare1(x, y);
                }

                int indexCounter = 0;


                while (lst1.Count > indexCounter || lst2.Count > indexCounter)
                {
                    x1 = lst1[indexCounter % lst1.Count];
                    y1 = lst2[indexCounter % lst2.Count];
                    indexCounter++;
                    if (x1 == y1) continue;

                    return compare1(x1, y1);
                }

                //if (lst1.Count < lst2.Count)
                //{
                //    x1 = lst1[0];
                //    y1 = lst2[indexCounter];
                //    if (x1 == y1) return 1;
                //    return compare1(x1, y1);
                //}
                //x1 = lst1[indexCounter];
                //y1 = lst2[0];

                //if (x1 == y1) return 1;
                x1 = lst1.Last();
                y1 = lst2.Last();

                return compare1(x1, y1);

            }

            private int compare1(int x, int y)
            {
                if (x < y) return 1;
                return -1;
            }
        }


        #endregion

        #region 189. Rotate Array
        public void Rotate(int[] nums, int k)
        {
            k = k % nums.Length;
            if (k > 0)
            {
                // reverse the array
                int left = -1;
                int right = nums.Length;

                while (++left < --right)
                {
                    swap(nums, left, right);
                }

                left = -1;
                right = k;

                while (++left < --right)
                {
                    swap(nums, left, right);
                }

                left = k - 1;
                right = nums.Length;

                while (++left < --right)
                {
                    swap(nums, left, right);
                }

            }
        }
        #endregion

        #region 199. Binary Tree Right Side View
        public IList<int> RightSideView(TreeNode root)
        {
            IList<int> list = new List<int>();

            if (root != null)
            {
                Queue<TreeNode> q = new Queue<TreeNode>();
                q.Enqueue(root);

                while (q.Count > 0)
                {
                    int count = q.Count();
                    TreeNode currNode = null;

                    while (count-- > 0)
                    {
                        currNode = q.Dequeue();

                        if (currNode.left != null)
                        {
                            q.Enqueue(currNode.left);
                        }
                        if (currNode.right != null)
                        {
                            q.Enqueue(currNode.right);
                        }
                    }

                    if (currNode != null) list.Add(currNode.val);
                }
            }
            return list;
        }
        #endregion

        #region 202. Happy Number
        HashSet<int> visited = new HashSet<int>();
        public bool IsHappy(int n)
        {
            if (n == 1) return true;
            if (visited.Contains(n)) return false;
            visited.Add(n);
            int num = 0;

            while (n > 0)
            {
                int k = n % 10;

                num = num + (k * k);

                n /= 10;
            }

            return IsHappy(num);
        }
        #endregion

        #region 207. Course Schedule
        public bool CanFinish(int numCourses, int[][] prerequisites)
        {
            int[] nodes = new int[numCourses];
            Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();
            for (int i = 0; i < numCourses; i++)
            {
                map[i] = new List<int>();
                nodes[i] = 1;
            }

            foreach (int[] prerequisite in prerequisites)
            {
                nodes[prerequisite[0]]++;
                map[prerequisite[1]].Add(prerequisite[0]);
            }
            Queue<int> queue = new Queue<int>();
            for (int i = 0; i < numCourses; i++)
            {
                if (nodes[i] == 1)
                {
                    queue.Enqueue(i);
                }
            }

            while (queue.Count > 0)
            {
                int key = queue.Dequeue();
                nodes[key] = 0;
                foreach (int val in map[key])
                {
                    if (nodes[val] <= 1) continue;
                    nodes[val]--;
                    if (nodes[val] == 1)
                    {
                        queue.Enqueue(val);
                    }
                }
            }

            for (int i = 0; i < numCourses; i++)
            {
                if (nodes[i] > 0) return false;
            }

            return true;
        }
        public bool CanFinish_1(int numCourses, int[][] prerequisites)
        {

            int[] visited = new int[numCourses];

            Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();

            for (int i = 0; i < numCourses; i++)
            {
                map[i] = new List<int>();
                //visited[row] = -1;
            }

            foreach (var pre in prerequisites)
            {
                map[pre[0]].Add(pre[1]);
            }

            for (int i = 0; i < numCourses; i++)
            {
                if (visited[i] == 0)
                {
                    visited[i] = -1;
                    if (!CanFinish_DFS_1(i, map, visited)) { return false; }
                }
            }

            return true;

        }

        private bool CanFinish_DFS_1(int key, Dictionary<int, List<int>> map, int[] visited)
        {
            bool processed = true;
            if (map[key].Count > 0)
            {
                foreach (var next in map[key])
                {
                    if (visited[next] == 1) continue;

                    if (visited[next] == -1)
                    {
                        processed = false;
                    }
                    else
                    {
                        visited[next] = -1;
                        processed = CanFinish_DFS_1(next, map, visited);
                    }
                    if (!processed) break;
                }
            }
            if (processed)
            {
                visited[key] = 1;
            }
            return processed;
        }
        #endregion

        #region 210. Course Schedule II
        public int[] FindOrder(int numCourses, int[][] prerequisites)
        {
            int[] order = new int[numCourses];
            int[] nodes = new int[numCourses];
            Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();

            for (int i = 0; i < numCourses; i++)
            {
                nodes[i] = 1;
                map[i] = new List<int>();
            }

            foreach (var prerequisite in prerequisites)
            {
                nodes[prerequisite[0]]++;
                map[prerequisite[1]].Add(prerequisite[0]);
            }

            Queue<int> queue = new Queue<int>();

            for (int i = 0; i < numCourses; i++)
            {
                if (nodes[i] == 1)
                {
                    queue.Enqueue(i);
                }
            }
            int nodeFound = 0;
            while (queue.Count > 0)
            {
                int key = queue.Dequeue();
                nodes[key] = 0;
                order[nodeFound] = key;
                nodeFound++;

                foreach (var edge in map[key])
                {
                    if (nodes[edge] <= 1) continue;

                    nodes[edge]--;

                    if (nodes[edge] == 1)
                    {
                        queue.Enqueue(edge);
                    }
                }
            }

            if (nodeFound < numCourses) return new int[0];
            return order;
        }
        #endregion

        #region 209. Minimum Size Subarray Sum
        public int MinSubArrayLen(int target, int[] nums)
        {
            int res = int.MaxValue;
            int count = nums[0];
            int left = 0;
            int right = 1;
            while (right < nums.Length && left < right)
            {
                if (count >= target)
                {
                    res = Math.Min(res, right - left);
                    count -= nums[left++];
                }
                else
                {
                    count += nums[right++];
                }
            }

            while (count >= target && left < right)
            {
                res = Math.Min(res, right - left);
                count -= nums[left++];
            }

            return res == int.MaxValue ? 0 : res;
        }
        #endregion

        #region 213. House Robber II
        public int Rob(int[] nums)
        {
            if (nums.Length <= 3) return nums.Max();

            return Math.Max(rob_helper(nums, 0, nums.Length - 1), rob_helper(nums, 1, nums.Length));
        }

        private int rob_helper(int[] nums, int startIndex, int endIndex)
        {
            int rob1 = 0, rob2 = 0;
            for (int i = startIndex; i < endIndex; i++)
            {
                int newRob = Math.Max(rob1 + nums[i], rob2);
                rob1 = rob2;
                rob2 = newRob;
            }
            return rob2;
        }

        #endregion

        #region 219. Contains Duplicate II
        public bool ContainsNearbyDuplicate(int[] nums, int k)
        {
            if (k == 1) return true;
            HashSet<int> set = new HashSet<int>();
            int startIndex = 0;
            for (int i = 0; i <= k; i++)
            {
                if (set.Contains(nums[i])) return true;

                set.Add(nums[i]);
            }


            for (int i = k + 1; i < nums.Length; i++)
            {
                set.Remove(nums[i - k - 1]);

                if (set.Contains(nums[i])) return true;

                set.Add(nums[i]);
            }

            return false;
        }
        #endregion

        #region 221. Maximal Square
        public int MaximalSquare(char[][] matrix)
        {
            int result = 0;
            int[][] intArray = new int[matrix.Length][];
            for (int i = 0; i < matrix.Length; i++)
            {
                intArray[i] = new int[matrix[i].Length];
            }

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == '1')
                    {
                        if (i == 0 || j == 0)
                        {
                            intArray[i][j] = 1;
                        }
                        else if (matrix[i - 1][j] == '1' && matrix[i][j - 1] == '1' && matrix[i - 1][j - 1] == '1')
                        {
                            int minNeighbor = Math.Min(intArray[i - 1][j - 1], Math.Min(intArray[i - 1][j], intArray[i][j - 1]));
                            intArray[i][j] = minNeighbor + 1;
                            result = Math.Max(result, intArray[i][j]);
                        }
                        else
                        {
                            intArray[i][j] = 1;
                        }

                        result = Math.Max(result, intArray[i][j]);
                    }
                }
            }
            return result * result;
        }

        #endregion

        #region 224. Basic Calculator
        public int Calculate(string s)
        {
            Stack<int> stack = new Stack<int>();

            int num = 0;
            int sign = 1;
            int result = 0;

            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];

                switch (c)
                {
                    case ' ':
                        break;
                    case '+':
                        sign = 1;
                        break;
                    case '-':
                        sign = -1;
                        break;
                    case '(':
                        stack.Push(result);
                        stack.Push(sign);
                        result = 0;
                        sign = 1;
                        break;
                    case ')':
                        result = stack.Pop() * result + stack.Pop();
                        break;
                    default:
                        num = c - '0';

                        int startIndex = i + 1;

                        while (startIndex < s.Length && char.IsDigit(s[startIndex]))
                        {
                            num = num * 10 + s[startIndex] - '0';
                            startIndex++;
                        }

                        result += sign * num;
                        i = startIndex - 1;
                        break;
                }
            }

            return result;
        }
        #endregion

        #region 228. Summary Ranges
        public IList<string> SummaryRanges(int[] nums)
        {
            IList<string> result = new List<string>();

            for (int i = 0; i < nums.Length; i++)
            {
                if (i + 1 < nums.Length && nums[i] + 1 == nums[i + 1])
                {
                    StringBuilder stringBuilder = new StringBuilder($"{nums[i]}->");
                    while (i + 1 < nums.Length && nums[i] + 1 == nums[i + 1])
                    {
                        i++;
                    }
                    stringBuilder.Append(nums[i]);
                    result.Add(stringBuilder.ToString());
                }
                else
                {
                    result.Add($"{nums[i]}");
                }
            }

            return result;
        }
        #endregion

        #region 229. Majority Element II
        public IList<int> MajorityElement(int[] nums)
        {
            if (nums.Length <= 2)
                return nums.Distinct().ToList();

            IList<int> lst = new List<int>();

            int n1 = int.MinValue, n2 = int.MinValue, count1 = 0, count2 = 0;

            int i = -1;
            while (++i < nums.Length)
            {
                if (n1 == nums[i])
                {
                    count1++;
                }
                else if (n2 == nums[i])
                {
                    count2++;
                }
                else if (count1 == 0)
                {
                    count1 = 1;
                    n1 = nums[i];
                }
                else if (count2 == 0)
                {
                    count2++;
                    n2 = nums[i];
                }
                else
                {
                    count1--;
                    count2--;


                }
            }
            count1 = 0;
            count2 = 0;
            foreach (var n in nums)
            {
                if (n == n1) count1++;
                if (n == n2) count2++;
            }

            if (count1 > nums.Length / 3)
            {
                lst.Add(n1);
            }

            if (count2 > nums.Length / 3)
            {
                lst.Add(n2);
            }
            return lst;
        }
        #endregion

        #region 230. Kth Smallest Element in a BST
        int k230 = -1;
        int kCounter230 = 0;
        public int KthSmallest(TreeNode root, int k)
        {

            inorder_230(root, k);

            return k230;
        }

        private void inorder_230(TreeNode root, int k)
        {
            if (kCounter230 == k || root == null) return;

            inorder_230(root.left, k);
            if (kCounter230 == k) return;
            kCounter230++;
            k230 = root.val;

            inorder_230(root.right, k);
        }

        public int KthSmallest_V2(TreeNode root, int k)
        {
            List<int> lst = new List<int>();

            inorder_230V1(root, lst, k);

            return lst.Last();
        }

        private void inorder_230V1(TreeNode root, List<int> lst, int k)
        {

            if (root.left != null)
            {
                inorder_230V1(root.left, lst, k);
            }

            if (lst.Count == k) return;
            lst.Add(root.val);

            if (root.right != null)
            {
                inorder_230V1(root.right, lst, k);
            }
        }

        public int KthSmallest_V1(TreeNode root, int k)
        {
            PriorityQueue<int, int> priorityQueue = new PriorityQueue<int, int>(k, Comparer<int>.Create((x, y) => y - x));

            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);

            while (q.Count > 0 && priorityQueue.Count < k)
            {
                var dq = q.Dequeue();
                priorityQueue.Enqueue(dq.val, dq.val);

                if (dq.right != null) q.Enqueue(dq.right);
                if (dq.left != null) q.Enqueue(dq.left);
            }

            while (q.Count > 0)
            {
                var dq = q.Dequeue();

                priorityQueue.EnqueueDequeue(dq.val, dq.val);

                if (dq.right != null) q.Enqueue(dq.right);
                if (dq.left != null) q.Enqueue(dq.left);
            }

            return priorityQueue.Dequeue();
        }
        #endregion

        #region 238. Product of Array Except Self
        public int[] ProductExceptSelf(int[] nums)
        {
            int[] result = new int[nums.Length];

            result[0] = 1;

            for (int i = 1; i < nums.Length; i++)
            {
                result[i] = result[i - 1] * nums[i - 1];
            }

            int suffix = 1;

            for (int i = nums.Length - 1; i >= 0; i--)
            {
                result[i] = suffix * result[i];
                suffix *= nums[i];
            }

            return result;
        }
        public int[] ProductExceptSelf2(int[] nums)
        {
            int[] prefix = new int[nums.Length];
            prefix[0] = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                prefix[i] = prefix[i - 1] * nums[i];
            }

            for (int i = nums.Length - 2; i >= 0; i--)
            {
                nums[i] *= nums[i + 1];
            }

            nums[0] = nums[1];
            for (int i = 1; i < nums.Length - 1; i++)
            {
                nums[i] = prefix[i - 1] * nums[i + 1];
            }
            nums[nums.Length - 1] = prefix[nums.Length - 2];

            return nums;
        }
        public int[] ProductExceptSelf1(int[] nums)
        {
            int[] prefixProduct = new int[nums.Length + 1];

            int[] suffixProduct = new int[nums.Length + 1];

            prefixProduct[0] = 1;
            suffixProduct[suffixProduct.Length - 1] = 1;
            for (int i = 0; i < nums.Length; i++)
            {
                prefixProduct[i + 1] = prefixProduct[i] * nums[i];
                suffixProduct[nums.Length - i - 1] = suffixProduct[nums.Length - i] * nums[nums.Length - 1 - i];
            }


            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = prefixProduct[i] * suffixProduct[i + 1];
            }

            return nums;
        }
        #endregion

        #region 240. Search a 2D Matrix II
        public bool SearchMatrix(int[][] matrix, int target)
        {
            int row = 0;
            int col = matrix[0].Length - 1;

            while (row < matrix.Length && col >= 0)
            {
                if (matrix[row][col] == target) { return true; }

                else if (matrix[row][col] > target)
                {
                    col--;
                }
                else
                {
                    row++;
                }
            }

            return false;
        }

        public bool SearchMatrix_1(int[][] matrix, int target)
        {
            int rows = matrix.Length;
            int cols = matrix[0].Length;

            int colMin = 0, colMax = cols - 1, rowMin = 0, rowMax = rows - 1;

            return SearchMatrix_Helper_1(matrix, target, rowMin, colMin, rowMax, colMax);

        }

        private bool SearchMatrix_Helper_1(int[][] matrix, int target, int rowMin, int colMin, int rowMax, int colMax)
        {
            if (rowMin == rowMax && rowMax == colMin && colMin == colMax)
            {
                return target == matrix[rowMax][colMax];
            }

            int topLeft = matrix[rowMin][colMin];
            int topRight = matrix[rowMin][colMax];
            int bottomLeft = matrix[rowMax][colMin];
            int bottomRight = matrix[rowMax][colMax];

            if (target == topLeft || target == bottomLeft || target == bottomRight || target == topRight) return true;

            if (target < topLeft || target > bottomRight) return false;

            int newColMin = -1, newRowMin = -1, newColMax = -1, newRowMax = -1;
            bool targetFound = false;
            if (target < topRight)
            {
                newColMax = findMaxCol(matrix, target, rowMin, colMin, colMax, out targetFound);

                if (targetFound) return targetFound;
            }
            else
            {
                newRowMin = findMinRow(matrix, target, rowMin, rowMax, colMax, out targetFound);

                if (targetFound) return targetFound;
            }

            if (target < bottomLeft)
            {
                newRowMax = findMaxRow(matrix, target, rowMin, colMin, rowMax, out targetFound);
                if (targetFound) return targetFound;
            }
            else
            {
                newColMin = findMinCol(matrix, target, colMin, rowMax, colMax, out targetFound);
                if (targetFound) return targetFound;
            }

            return SearchMatrix_Helper_1(matrix, target,
                newRowMin == -1 ? rowMin : newRowMin,
                newColMin == -1 ? colMin : newColMin,
                newRowMax == -1 ? rowMax : newRowMax,
                newColMax == -1 ? colMax : newColMax);

        }

        private int findMinCol(int[][] matrix, int target, int colMin, int rowMax, int colMax, out bool targetFound)
        {
            targetFound = false;
            int low = colMin;
            int high = colMax;

            while (low < high)
            {
                int mid = (low + high) / 2;
                if (matrix[rowMax][mid] == target)
                {
                    targetFound = true;
                    break;
                }

                if (matrix[rowMax][mid] < target)
                {
                    low = mid + 1;

                    if (matrix[rowMax][low] == target)
                    {
                        targetFound = true;
                        break;
                    }
                }
                else
                {
                    high = mid - 1;

                    if (matrix[rowMax][high] == target)
                    {
                        targetFound = true;
                        break;
                    }
                }
            }

            return low == high ? matrix[rowMax][low] > target ? low : low + 1 : Math.Max(low, high);
        }

        private int findMaxRow(int[][] matrix, int target, int rowMin, int colMin, int rowMax, out bool targetFound)
        {
            targetFound = false;
            int low = rowMin;
            int high = rowMax;

            while (low < high)
            {
                int mid = (low + high) / 2;

                if (matrix[mid][colMin] == target)
                {
                    targetFound = true;
                    break;
                }

                if (matrix[mid][colMin] > target)
                {
                    high = mid - 1;
                    if (matrix[high][colMin] == target)
                    {
                        targetFound = true;
                        break;
                    }
                }
                else
                {
                    low = mid + 1;
                    if (matrix[low][colMin] == target)
                    {
                        targetFound = true;
                        break;
                    }
                }
            }

            return low == high ? matrix[low][colMin] < target ? low : low - 1 : Math.Min(low, high);
        }

        private int findMinRow(int[][] matrix, int target, int rowMin, int rowMax, int colMax, out bool targetFound)
        {
            targetFound = false;

            int low = rowMin;
            int high = rowMax;

            while (low < high)
            {
                int mid = (low + high) / 2;

                if (matrix[mid][colMax] == target)
                {
                    targetFound = true;
                    break;
                }

                if (matrix[mid][colMax] > target)
                {
                    high = mid - 1;

                    if (matrix[high][colMax] == target)
                    {
                        targetFound = true;
                        break;
                    }
                }
                else
                {
                    low = mid + 1;

                    if (matrix[low][colMax] == target)
                    {
                        targetFound = true;
                        break;
                    }
                }
            }

            return low == high ? matrix[low][colMax] > target ? low : low + 1 : Math.Max(low, high);
        }

        private int findMaxCol(int[][] matrix, int target, int rowMin, int colMin, int colMax, out bool targetFound)
        {
            targetFound = false;
            int low = colMin;
            int high = colMax;

            while (low < high)
            {
                int mid = (low + high) / 2;

                if (matrix[rowMin][mid] == target)
                {
                    targetFound = true; break;
                }

                if (matrix[rowMin][mid] < target)
                {
                    low = mid + 1;
                    if (matrix[rowMin][low] == target)
                    {
                        targetFound = true; break;
                    }
                }
                else
                {
                    high = mid - 1;
                    if (matrix[rowMin][high] == target)
                    {
                        targetFound = true; break;
                    }
                }
            }


            return low == high ? matrix[rowMin][low] < target ? low : low - 1 : Math.Min(low, high);
        }
        #endregion

        #region 241. Different Ways to Add Parentheses
        public IList<int> DiffWaysToCompute(string expression)
        {
            Dictionary<string, IList<int>> maps = new Dictionary<string, IList<int>>();

            return DiffWaysToCompute(expression, maps);
        }

        private IList<int> DiffWaysToCompute(string expression, Dictionary<string, IList<int>> maps)
        {
            if (maps.ContainsKey(expression)) return maps[expression];

            IList<int> result = new List<int>();

            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];

                if (!char.IsDigit(c))
                {
                    var l = DiffWaysToCompute(expression[..i], maps);
                    var r = DiffWaysToCompute(expression[(i + 1)..], maps);

                    foreach (var left in l)
                    {
                        foreach (var right in r)
                        {
                            if (c == '+')
                            {
                                result.Add(left + right);
                            }
                            else if (c == '-')
                            {
                                result.Add(left - right);
                            }
                            else
                            {
                                result.Add(left * right);
                            }
                        }
                    }
                }
            }

            if (result.Count == 0)
            {
                result.Add(int.Parse(expression));
            }

            maps.Add(expression, result);
            return result;
        }
        #endregion

        #region 242. Valid Anagram
        public bool IsAnagram(string s, string t)
        {
            if (s.Length != t.Length) return false;

            char[] sA = s.ToCharArray();

            Array.Sort(sA);
            char[] tA = t.ToCharArray();

            Array.Sort(tA);

            return new string(tA) == new string(sA);
        }
        #endregion

        #region 260. Single Number III
        public int[] SingleNumber(int[] nums)
        {
            // Step 1: XOR all elements to get xor_total (XOR of the two unique numbers)
            int xor_total = 0;
            foreach (int num in nums)
            {
                xor_total ^= num;
            }

            // Step 2: Find the rightmost set bit (diff_bit) in xor_total
            int diff_bit = xor_total & -xor_total; // This isolates the rightmost set bit

            // Step 3: Separate numbers based on diff_bit and XOR within each group
            int x = 0, y = 0;
            foreach (int num in nums)
            {
                if ((num & diff_bit) == 0)
                    x ^= num; // Group with diff_bit not set
                else
                    y ^= num; // Group with diff_bit set
            }

            // x and y are the two unique numbers
            return new int[] { x, y };
        }
        #endregion

        #region 264. Ugly Number II
        public int NthUglyNumber(int n)
        {
            int[] arr = new int[n];

            arr[0] = 1;
            int index2 = 0, index3 = 0, index5 = 0;
            int next2 = 2, next3 = 3, next5 = 5;

            for (int i = 1; i < n; i++)
            {
                arr[i] = Math.Min(next2, Math.Min(next3, next5));
                if (arr[i] == next2)
                {
                    next2 = arr[++index2] * 2;
                }
                if (arr[i] == next3)
                {
                    next3 = arr[++index3] * 3;
                }
                if (arr[i] == next5)
                {
                    next5 = arr[++index5] * 5;
                }
            }

            return arr[n - 1];
        }


        #endregion

        #region 268. Missing Number
        public int MissingNumber(int[] nums)
        {
            int totalSum = 0;
            int currentSum = nums[0];
            int i = 1;
            for (; i < nums.Length; i++)
            {
                totalSum += i;
                currentSum += nums[i];
            }

            return totalSum + i - currentSum;
        }
        #endregion

        #region 273. Integer to English Words

        public string NumberToWords(int num)
        {
            if (num == 0) return "Zero";

            StringBuilder stringBuilder = new StringBuilder();
            int[] partitions = { 0, 1000, 1000000, 1000000000 };
            Dictionary<int, string> map = new Dictionary<int, string>()
            {
                { 0,"" } ,
                { 1,"One" } ,
                { 2,"Two" } ,
                { 3,"Three" } ,
                { 4,"Four" } ,
                { 5,"Five" },
                { 6,"Six" } ,
                { 7,"Seven" } ,
                { 8,"Eight" } ,
                { 9,"Nine" },
                { 10,"Ten" } ,
                { 11,"Eleven" } ,
                { 12,"Twelve" } ,
                { 13,"Thirteen" },
                { 14,"Fourteen" } ,
                { 15,"Fifteen" } ,
                { 16,"Sixteen" } ,
                { 17,"Seventeen" },
                { 18,"Eighteen" } ,
                { 19,"Nineteen" } ,
                { 20,"Twenty" } ,
                { 30,"Thirty" },
                { 40,"Forty" },
                { 50,"Fifty" },
                { 60,"Sixty" },
                { 70,"Seventy" },
                { 80,"Eighty" },
                { 90,"Ninety" },
                { 100,"Hundred" },
                { 1000,"Thousand" },
                { 1000000,"Million" },
                { 1000000000,"Billion" }
            };
            if (map.ContainsKey(num))
            {
                if (num >= 100)
                {
                    return "One " + map[num];
                }
                return map[num];
            }
            int i = 0;

            while (num > 0)
            {
                int k = num % 1000;


                string s = getStringForThreeDigit(k, map);
                if (!string.IsNullOrEmpty(s))
                {
                    s = $" {s} {map[partitions[i]]}";
                }

                stringBuilder.Insert(0, s);

                num /= 1000;
                i++;
            }

            return stringBuilder.ToString().Trim();
        }

        private string getStringForThreeDigit(int k, Dictionary<int, string> map)
        {
            string result = string.Empty;
            int b = k / 100;
            if (b > 0)
            {
                result = $"{map[b]} {map[100]}";
                k %= 100;
            }
            if (map.ContainsKey(k))
            {
                return result = $"{result} {map[k]}".Trim();
            }
            b = k / 10;

            result = $"{result} {map[b * 10]}".Trim();

            k %= 10;

            if (k > 0)
            {
                result = $"{result} {map[k]}";
            }

            return result.Trim();
        }
        #endregion

        #region 274. H-Index
        public int HIndex_V1(int[] citations)
        {
            Array.Sort(citations);
            int n = citations.Length;
            int counter = 0;
            for (int i = citations.Length - 1; i >= 0; i--)
            {
                if (citations[i] > counter)
                {
                    counter++;
                }
                else
                {
                    return counter;
                }
            }
            return counter;
        }
        #endregion

        #region 275. H-Index II
        public int HIndex(int[] citations)
        {
            int counter = 0;
            int low = 0;
            int high = citations.Length - 1;

            while (low < high)
            {
                int mid = (low + high) / 2;
                int countToEnd = high - mid;
                int countToStart = mid - low;


                if (citations[mid] > countToEnd)
                {
                    counter = mid + 1;

                    low = counter;
                }
                else
                {

                }
            }

            return counter;
        }
        #endregion

        #region 300. Longest Increasing Subsequence
        public int LengthOfLIS(int[] nums)
        {
            List<int> lst = new List<int>() { nums[0] };
            for (int i = 1; i < nums.Length; i++)
            {
                int k = lst.BinarySearch(nums[i]);

                if (k < 0) k = ~k;

                if (k == lst.Count)
                {
                    lst.Add(nums[i]);
                }
                else
                {
                    lst[k] = nums[i];
                }
            }
            return lst.Count;
        }
        #endregion

        #region 316. Remove Duplicate Letters
        public string RemoveDuplicateLetters(string s)
        {
            Stack<char> stk = new Stack<char>();
            int[] freq = new int[26];
            bool[] visited = new bool[26];
            foreach (char c in s)
            {
                freq[c - 'a']++;
            }

            foreach (char c in s)
            {
                if (!visited[c - 'a'])
                {
                    while (stk.Count > 0 && stk.Peek() > c && freq[stk.Peek() - 'a'] > 0)
                    {
                        visited[stk.Peek() - 'a'] = false;
                        stk.Pop();
                    }
                    stk.Push(c);
                    visited[c - 'a'] = true;
                }
                freq[c - 'a']--;
            }
            return new String(stk.Reverse().ToArray());
        }
        public string RemoveDuplicateLetters1(string s)
        {
            Dictionary<char, List<int>> mapIndex = new Dictionary<char, List<int>>();

            int i = -1;

            while (++i < s.Length)
            {
                char c = s[i];
                if (mapIndex.ContainsKey(c))
                {
                    mapIndex[c].Add(i);
                }
                else
                {
                    mapIndex.Add(c, new List<int>() { i });
                }
            }

            int reqLen = mapIndex.Count;

            HashSet<char> visited = new HashSet<char>();
            List<string> results = new List<string>();

            RemoveDuplicateLetters_Helper_1(new StringBuilder(), visited, results, reqLen, s, 0);
            results.Sort();
            return results[0];
        }

        public void RemoveDuplicateLetters_Helper_1(StringBuilder stringBuilder, HashSet<char> set, List<string> result, int requiredLength, string s, int index)
        {
            if (index == s.Length) return;


            for (int i = index; i < s.Length; i++)
            {

                char c = s[i];
                if (set.Contains(c)) continue;
                stringBuilder.Append(c);
                set.Add(c);
                if (stringBuilder.Length == requiredLength)
                {
                    result.Add(stringBuilder.ToString());
                    //return;
                }
                else
                {
                    RemoveDuplicateLetters_Helper_1(stringBuilder, set, result, requiredLength, s, i + 1);
                }
                set.Remove(c);
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
            }


        }

        #endregion

        #region 322. Coin Change
        public int CoinChange(int[] coins, int amount)
        {
            int[] dp = new int[amount + 1];
            for (int i = 1; i <= amount; i++)
            {
                int minCoins = amount + 1;
                foreach (var coin in coins)
                {
                    int lowerBound = i - coin;

                    if (lowerBound >= 0)
                    {
                        minCoins = Math.Min(minCoins, 1 + dp[lowerBound]);
                    }
                }
                dp[i] = minCoins;
            }

            return dp[amount] > amount ? -1 : dp[amount];
        }

        //public int CoinChange(int[] coins, int amount)
        //{
        //    int[] grid = new int[amount + 1];
        //    Array.Fill(grid, int.MaxValue);
        //    foreach (var next in coins)
        //    {
        //        int count = 1;
        //        for (int size = next; size <= amount; size += next)
        //        {
        //            grid[next] = count++;
        //        }
        //    }

        //    return grid[amount];
        //}
        #endregion

        #region 392. Is Subsequence
        public bool IsSubsequence(string s, string t)
        {
            int tIndex = -1;
            int sIndex = 0;
            while (sIndex < s.Length && ++tIndex < t.Length)
            {
                if (s[sIndex] == t[tIndex]) sIndex++;
            }

            return sIndex == s.Length;
        }
        #endregion

        #region 416. Partition Equal Subset Sum
        int[][] dp_416;
        public bool CanPartition(int[] nums)
        {
            int totalSum = nums.Sum();

            if (totalSum % 2 != 0)
            {
                return false;
            }

            int target = totalSum / 2;
            bool[] dp = new bool[target + 1];
            dp[0] = true;  // Base case: zero sum is always achievable

            foreach (int num in nums)
            {
                // Update grid array from target down to num
                for (int i = target; i >= num; i--)
                {
                    dp[i] = dp[i] || dp[i - num];
                }
            }

            return dp[target];
        }
        public bool CanPartition_V1416(int[] nums)
        {
            int sum = nums.Sum();

            if (sum % 2 == 0)
            {
                int reqSum = sum / 2;

                dp_416 = new int[nums.Length + 1][];

                for (int i = 0; i <= nums.Length; i++)
                {
                    dp_416[i] = Enumerable.Repeat(-1, reqSum + 1).ToArray();
                }

                return CanPartition_helper_416(nums, 0, sum / 2);
            }
            return false;
        }

        private bool CanPartition_helper_416(int[] nums, int index, int sum)
        {
            if (sum < 0 || index == nums.Length) return false;
            if (sum == 0) return true;
            if (dp_416[index][sum] != -1) return dp_416[index][sum] == 1;
            if (CanPartition_helper_416(nums, index + 1, sum) || CanPartition_helper_416(nums, index + 1, sum - nums[index]))
            {
                dp_416[index][sum] = 1;
                return true;
            }
            else
            {
                dp_416[index][sum] = 0;
                return false;
            }
        }
        #endregion

        #region 432. All O`one Data Structure
        /*
            Design a data structure to store the strings' count_2044 with the ability to return the strings with minimum and maximum counts.

            Implement the AllOne class:

            AllOne() Initializes the object of the data structure.
            inc(String key) Increments the count_2044 of the string key by 1. If key does not exist in the data structure, insert it with count_2044 1.
            dec(String key) Decrements the count_2044 of the string key by 1. If the count_2044 of key is 0 after the decrement, remove it from the data structure. It is guaranteed that key exists in the data structure before the decrement.
            getMaxKey() Returns one of the keys with the maximal count_2044. If no element exists, return an empty string "".
            getMinKey() Returns one of the keys with the minimum count_2044. If no element exists, return an empty string "".
            Note that each function must run in O(1) average time complexity.



            Example 1:

            Input
            ["AllOne", "inc", "inc", "getMaxKey", "getMinKey", "inc", "getMaxKey", "getMinKey"]
            [[], ["hello"], ["hello"], [], [], ["leet"], [], []]
            Output
            [null, null, null, "hello", "hello", null, "hello", "leet"]

            Explanation
            AllOne allOne = new AllOne();
            allOne.inc("hello");
            allOne.inc("hello");
            allOne.getMaxKey(); // return "hello"
            allOne.getMinKey(); // return "hello"
            allOne.inc("leet");
            allOne.getMaxKey(); // return "hello"
            allOne.getMinKey(); // return "leet"


            Constraints:

            1 <= key.length <= 10
            key consists of lowercase English letters.
            It is guaranteed that for each call to dec, key is existing in the data structure.
            At most 5 * 104 calls will be made to inc, dec, getMaxKey, and getMinKey.
         */
        public class NodeWithFreqKeys
        {
            public int Frequency { get; set; }
            public NodeWithFreqKeys Next { get; set; }
            public NodeWithFreqKeys Previous { get; set; }
            public HashSet<string> Keys { get; set; }

            public NodeWithFreqKeys()
            {
                Keys = new HashSet<string>();
            }
        }
        public class AllOne
        {
            private NodeWithFreqKeys head;
            private NodeWithFreqKeys tail;

            Dictionary<string, NodeWithFreqKeys> keys;

            public AllOne()
            {
                keys = new Dictionary<string, NodeWithFreqKeys>();
                head = new NodeWithFreqKeys();
                tail = new NodeWithFreqKeys();
                head.Next = tail;
                head.Frequency = 0;
                tail.Previous = head;
                tail.Frequency = 100000;
            }

            public void Inc(string key)
            {

            }

            public void Dec(string key)
            {

            }

            public string GetMaxKey()
            {
                return "";
            }

            public string GetMinKey()
            {
                return "";
            }
        }

        public class AllOne_V1
        {
            Dictionary<string, int> data;

            public AllOne_V1()
            {
                data = new Dictionary<string, int>();
            }

            public void Inc(string key)
            {
                if (data.ContainsKey(key))
                {
                    data[key]++;
                }
                else
                {
                    data.Add(key, 1);
                }
            }

            public void Dec(string key)
            {
                data[key]--;

                if (data[key] == 0) { data.Remove(key); }
            }

            public string GetMaxKey()
            {
                if (data.Count > 0)
                {
                    return data.OrderByDescending(x => x.Value).FirstOrDefault().Key;
                }
                return "";
            }

            public string GetMinKey()
            {
                if (data.Count > 0)
                {
                    return data.OrderBy(x => x.Value).FirstOrDefault().Key;
                }
                return "";
            }
        }
        #endregion

        #region 530. Minimum Absolute Difference in BST
        int min = int.MaxValue;
        int prev = int.MaxValue;
        public int GetMinimumDifference(TreeNode root)
        {
            inorder_530(root);

            return min;
        }

        private void inorder_530(TreeNode root)
        {
            if (root != null)
            {
                inorder_530(root.left);
                if (prev != int.MaxValue)
                {
                    min = Math.Min(min, Math.Abs(prev - root.val));
                }

                prev = root.val;
                inorder_530(root.right);
            }
        }
        #endregion

        #region 539. Minimum Time Difference
        /*
            Given a list of 24-hour clock time points in "HH:MM" format, return the minimum minutes difference between any two time-points in the list. 

            Example 1:

            Input: timePoints = ["23:59","00:00"]
            Output: 1
            Example 2:

            Input: timePoints = ["00:00","23:59","00:00"]
            Output: 0


            Constraints:

            2 <= timePoints.length <= 2 * 104
            timePoints[size] is in the format "HH:MM".

         */

        public int FindMinDifference(IList<string> timePoints)
        {
            bool[] foundTimes = new bool[1440];

            foreach (string timePoint in timePoints)
            {
                int minutes = getMinutes(timePoint.Split(':'));

                if (foundTimes[minutes]) return 0;

                foundTimes[minutes] = true;
            }

            int res = 1439;

            int previousTime = -1, nextTime = 0, firstTime = 0;

            for (int i = 0; i < 1440; i++)
            {
                if (foundTimes[i])
                {
                    if (previousTime < 0) firstTime = i;
                    else
                    {
                        nextTime = i;
                        res = Math.Min(res, nextTime - previousTime);
                    }
                    previousTime = i;
                }
            }

            return Math.Min(res, 1440 - previousTime + firstTime);
        }
        private int getMinutes(string[] timeArray)
        {
            return (Convert.ToInt32(timeArray[0]) * 60) + Convert.ToInt32(timeArray[1]);
        }

        public int FindMinDifference_1(IList<string> timePoints)
        {
            int result = int.MaxValue;

            string[] timePointsArray = timePoints.OrderBy(x => x).ToArray();

            string prevTime = timePointsArray[0];
            string lastTime = timePointsArray.Last();
            result = getDiffTimeFirstLast(prevTime, lastTime);
            for (int i = 1; i < timePointsArray.Length; i++)
            {
                string currTime = timePointsArray[i];

                if (currTime == prevTime) return 0;

                result = Math.Min(result, getDiffTime(prevTime, currTime));

                prevTime = currTime;
            }

            return result;
        }

        public int getDiffTime(string time1, string time2)
        {
            string[] timeArray1 = time1.Split(':');
            string[] timeArray2 = time2.Split(':');

            int min1 = getMinutes(timeArray1);
            int min2 = getMinutes(timeArray2);

            return min2 - min1;
        }

        public int getDiffTimeFirstLast(string first, string last)
        {
            string[] timeArray1 = first.Split(':');
            string[] timeArray2 = last.Split(':');

            int min1 = getMinutes(timeArray1);
            int min2 = getMinutes(timeArray2);

            return ((24 * 60) - min2) + min1;
        }
        #endregion

        #region 637. Average of Levels in Binary Tree
        public IList<double> AverageOfLevels(TreeNode root)
        {
            IList<double> result = new List<double>();

            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);

            while (q.Count > 0)
            {
                int count = q.Count;
                double nodeCount = (double)count;
                long sum = 0;

                while (count-- > 0)
                {
                    TreeNode node = q.Dequeue();
                    sum += node.val;

                    if (node.left != null)
                    {
                        q.Enqueue(node.left);
                    }

                    if (node.right != null)
                    {
                        q.Enqueue(node.right);
                    }
                }
                double avg = (sum / nodeCount);

                result.Add(avg);
            }
            return result;
        }
        #endregion

        #region 641. Design Circular Deque
        /*
            Design your implementation of the circular double-ended queue (deque).

            Implement the MyCircularDeque_V1 class:

            MyCircularDeque_V1(int key) Initializes the deque with a maximum size of key.
            boolean insertFront() Adds an next at the front of Deque. Returns true if the operation is successful, or false otherwise.
            boolean insertLast() Adds an next at the rear of Deque. Returns true if the operation is successful, or false otherwise.
            boolean deleteFront() Deletes an next from the front of Deque. Returns true if the operation is successful, or false otherwise.
            boolean deleteLast() Deletes an next from the rear of Deque. Returns true if the operation is successful, or false otherwise.
            int getFront() Returns the front next from the Deque. Returns -1 if the deque is empty.
            int getRear() Returns the last next from Deque. Returns -1 if the deque is empty.
            boolean isEmpty() Returns true if the deque is empty, or false otherwise.
            boolean isFull() Returns true if the deque is full, or false otherwise.


            Example 1:

            Input
            ["MyCircularDeque_V1", "insertLast", "insertLast", "insertFront", "insertFront", "getRear", "isFull", "deleteLast", "insertFront", "getFront"]
            [[3], [1], [2], [3], [4], [], [], [], [4], []]
            Output
            [null, true, true, true, false, 2, true, true, true, 4]

            Explanation
            MyCircularDeque_V1 myCircularDeque = new MyCircularDeque_V1(3);
            myCircularDeque.insertLast(1);  // return True
            myCircularDeque.insertLast(2);  // return True
            myCircularDeque.insertFront(3); // return True
            myCircularDeque.insertFront(4); // return False, the queue is full.
            myCircularDeque.getRear();      // return 2
            myCircularDeque.isFull();       // return True
            myCircularDeque.deleteLast();   // return True
            myCircularDeque.insertFront(4); // return True
            myCircularDeque.getFront();     // return 4


            Constraints:

            1 <= key <= 1000
            0 <= value <= 1000
            At most 2000 calls will be made to insertFront, insertLast, deleteFront, deleteLast, getFront, getRear, isEmpty, isFull.



        "MyCircularDeque_V1"       4                           
        "insertFront"           9
        "deleteLast"
        "getRear"
        "getFront"
        "getFront"
        "deleteFront"
        "insertFront"           6
        "insertLast"            5
        "insertFront"           9
        "getFront"
        "insertFront"           6

         */
        public class MyCircularDeque
        {
            private Node head;
            private Node tail;
            int currSize;
            int maxSize;
            public MyCircularDeque(int k)
            {
                head = new Node();
                tail = new Node();

                head.Previous = tail;
                head.Next = tail;
                tail.Next = head;
                tail.Previous = head;
                currSize = 0;
                maxSize = k;
            }

            public bool InsertFront(int value)
            {
                if (currSize < maxSize)
                {
                    currSize++;

                    Node headNext = head.Next;

                    Node newNode = new Node()
                    {
                        Next = headNext,
                        Previous = head,
                        Val = value
                    };

                    head.Next = newNode;
                    headNext.Previous = newNode;

                    return true;
                }
                return false;
            }

            public bool InsertLast(int value)
            {
                if (currSize < maxSize)
                {
                    currSize++;

                    Node tailPrevious = tail.Previous;

                    Node newNode = new Node()
                    {
                        Next = tail,
                        Previous = tailPrevious,
                        Val = value
                    };

                    tail.Previous = newNode;
                    tailPrevious.Next = newNode;
                    return true;
                }
                return false;
            }

            public bool DeleteFront()
            {
                if (currSize > 0)
                {
                    currSize--;

                    Node deleteNode = head.Next;

                    Node deleteNodeNextNode = deleteNode.Next;

                    head.Next = deleteNodeNextNode;
                    deleteNodeNextNode.Previous = head;

                    return true;
                }
                return false;

            }

            public bool DeleteLast()
            {
                if (currSize > 0)
                {
                    currSize--;

                    Node deleteNode = tail.Previous;

                    Node deleteNodePreviousNode = deleteNode.Previous;

                    tail.Previous = deleteNodePreviousNode;
                    deleteNodePreviousNode.Next = tail;

                    return true;
                }
                return false;

            }

            public int GetFront()
            {
                if (currSize > 0)
                {
                    return head.Next.Val;
                }
                return -1;

            }

            public int GetRear()
            {
                if (currSize > 0)
                {
                    return tail.Previous.Val;
                }
                return -1;
            }

            public bool IsEmpty()
            {
                return currSize == 0;

            }

            public bool IsFull()
            {
                return currSize == maxSize;
            }
        }
        public class MyCircularDeque_V1
        {
            int[] arr;
            int size, front, rear;
            public MyCircularDeque_V1(int k)
            {
                size = k;
                arr = new int[4001];
                front = 2000;
                rear = 2000;
            }

            public bool InsertFront(int value)
            {
                if (IsEmpty())
                {
                    arr[front] = value;
                    front--;
                    rear++;
                    return true;
                }
                else if (rear - front - 1 < size)
                {
                    arr[front] = value;
                    front--;
                    return true;
                }

                return false;
            }

            public bool InsertLast(int value)
            {
                if (IsEmpty())
                {
                    arr[rear] = value;
                    front--;
                    rear++;
                    return true;
                }
                else if (rear - front - 1 < size)
                {
                    arr[rear] = value;
                    rear++;
                    return true;
                }

                return false;
            }

            public bool DeleteFront()
            {
                if (rear != front)
                {
                    front++;

                    if (rear - front == 1) rear--;

                    return true;
                }
                return false;
            }

            public bool DeleteLast()
            {
                if (rear != front)
                {
                    rear--;

                    if (rear - front == 1) front++;

                    return true;
                }
                return false;
            }

            public int GetFront()
            {
                if (rear != front)
                {
                    return arr[front + 1];
                }
                return -1;
            }

            public int GetRear()
            {
                if (rear != front)
                {
                    return arr[rear - 1];
                }
                return -1;
            }

            public bool IsEmpty()
            {
                return rear == front;
            }

            public bool IsFull()
            {
                return rear - front - 1 >= size;
            }
        }

        /**
         * Your MyCircularDeque_V1 object will be instantiated and called as such:
         * MyCircularDeque_V1 obj = new MyCircularDeque_V1(key);
         * bool param_1 = obj.InsertFront(value);
         * bool param_2 = obj.InsertLast(value);
         * bool param_3 = obj.DeleteFront();
         * bool param_4 = obj.DeleteLast();
         * int param_5 = obj.GetFront();
         * int param_6 = obj.GetRear();
         * bool param_7 = obj.IsEmpty();
         * bool param_8 = obj.IsFull();
         */
        #endregion

        #region 670. Maximum Swap
        public int MaximumSwap(int num)
        {
            List<int> swap = new List<int>();

            while (num > 0)
            {
                swap.Insert(0, num % 10);
                num /= 10;
            }

            for (int i = 0; i < swap.Count - 1; i++)
            {
                int index = i;
                for (int j = swap.Count - 1; j > i; j--)
                {
                    if (swap[index] < swap[j])
                    {
                        index = j;
                    }
                }
                if (index != i)
                {
                    int temp = swap[index];
                    swap[index] = swap[i];
                    swap[i] = temp;
                    break;
                }
            }

            num = swap[0];

            for (int i = 1; i < swap.Count; i++)
            {
                num = num * 10 + swap[i];
            }

            return num;
        }
        #endregion

        #region 773. Sliding Puzzle
        public int SlidingPuzzle(int[][] board)
        {
            string curr = string.Join("", board[0]) + string.Join("", board[1]);
            string target = "123450";

            Queue<(string state, int move)> q = new Queue<(string state, int move)>();

            q.Enqueue((curr, 0));
            Dictionary<int, int[]> map = new Dictionary<int, int[]>()
            {
                {0,new int[]{1,3} },
                {1,new int[]{0,2,4} },
                {2,new int[]{1,5} },
                {3,new int[]{0,4} },
                {4,new int[]{1,3,5} },
                {5,new int[]{2,4} }
            };
            HashSet<string> visited = new HashSet<string>();
            visited.Add(curr);

            while (q.Count > 0)
            {
                var current = q.Dequeue();

                if (current.state == target) return current.move;
                int zeroIndex = current.state.IndexOf('0');
                foreach (var item in map[zeroIndex])
                {
                    char[] newState = current.state.ToCharArray();

                    swap(newState, zeroIndex, item);

                    string newStateString = new string(newState);

                    if (!visited.Contains(newStateString))
                    {
                        visited.Add(newStateString);
                        q.Enqueue((newStateString, current.move + 1));
                    }
                }
            }

            return -1;
        }

        private void swap(char[] chars, int i, int j)
        {
            char temp = chars[i];
            chars[i] = chars[j];
            chars[j] = temp;
        }
        #endregion

        #region 796. Rotate String
        public bool RotateString(string s, string goal)
        {
            if (s == goal) return true;
            if (s.Length != goal.Length) return false;


            return (s + s).Contains(goal);
        }
        #endregion

        #region 828. Count Unique Characters of All Substrings of a Given String
        public int UniqueLetterString(string s)
        {
            int result = 0;
            List<int>[] charIndexes = new List<int>[26];
            for (int i = 0; i < 26; i++)
            {
                charIndexes[i] = new List<int>();
            }
            int index = 0;
            foreach (char c in s)
            {
                charIndexes[c - 'A'].Add(index++);
            }

            foreach (var occurences in charIndexes)
            {
                if (occurences.Count > 0)
                {
                    result += getCount(occurences, s.Length);
                }
            }

            return result;
        }

        private int getCount(List<int> occurences, int length)
        {
            if (occurences.Count == 1)
            {
                return (occurences[0] + 1) * (length - occurences[0]);
            }
            int result = (occurences[0] + 1) * (occurences[1] - occurences[0]);

            int i = 1;
            for (; i < occurences.Count - 1; i++)
            {
                result += (occurences[i] - occurences[i - 1]) * (occurences[i + 1] - occurences[i]);
            }

            result += (length - occurences[i]) * (occurences[i] - occurences[i - 1]);

            return result;
        }

        //private int getCount(List<int> occurences, int end, int length)
        //{
        //    if (occurences.Count == 1)
        //    {
        //        return (occurences[end] + 1) * (length - occurences[end]);
        //    }

        //    int left = occurences[end];
        //    int right = length - occurences[]
        //    if (end == 0)
        //    {
        //        left++;
        //    }
        //    else
        //    {
        //        left -= occurences[end - 1];
        //    }



        //    int prevIndex = end == 0 ? end : end - 1;
        //    int nextIndex = end == occurences.Count - 1 ? end : end + 1;

        //    return (occurences[end] - occurences[prevIndex] + 1) * (occurences[nextIndex] - occurences[end]);
        //}
        #endregion

        #region 862. Shortest Subarray with Sum at Least K
        public int ShortestSubarray(int[] nums, int k)
        {
            int result = nums.Length + 1;

            PriorityQueue<int, long> pq = new PriorityQueue<int, long>();
            long cummSum = 0;
            long[] cummSumAtIndex = new long[nums.Length];

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] >= k) return 1;

                cummSum += nums[i];

                if (cummSum >= k)
                {
                    result = Math.Min(result, i + 1);
                }

                while (pq.Count > 0 && cummSum - cummSumAtIndex[pq.Peek()] >= k)
                {
                    result = Math.Min(result, (i - pq.Dequeue()));
                }

                cummSumAtIndex[i] = cummSum;
                pq.Enqueue(i, cummSum);
            }



            return result > nums.Length ? -1 : result;
        }
        #endregion

        #region 884. Uncommon Words from Two Sentences

        /*
            A sentence is a string of single-space separated words where each word consists only of lowercase letters.

            A word is uncommon if it appears exactly once in one of the sentences, and does not appear in the other sentence.

            Given two sentences s1 and s2, return a list of all the uncommon words. You may return the answer in any order.



            Example 1:

            Input: s1 = "this apple is sweet", s2 = "this apple is sour"

            Output: ["sweet","sour"]

            Explanation:

            The word "sweet" appears only in s1, while the word "sour" appears only in s2.

            Example 2:

            Input: s1 = "apple apple", s2 = "banana"

            Output: ["banana"]



            Constraints:

            1 <= s1.length, s2.length <= 200
            s1 and s2 consist of lowercase English letters and spaces.
            s1 and s2 do not have leading or trailing spaces.
            All the words in s1 and s2 are separated by a single space.
         */
        public string[] UncommonFromSentences(string s1, string s2)
        {
            HashSet<string> seen = new HashSet<string>();
            HashSet<string> discarded = new HashSet<string>();

            foreach (string s in s1.Split(' '))
            {
                if (discarded.Contains(s)) continue;

                if (seen.Contains(s))
                {
                    seen.Remove(s);
                    discarded.Add(s);
                    continue;
                }

                seen.Add(s);
            }
            foreach (string s in s1.Split(' '))
            {
                if (discarded.Contains(s)) continue;

                if (seen.Contains(s))
                {
                    seen.Remove(s);
                    discarded.Add(s);
                    continue;
                }

                seen.Add(s);
            }

            return seen.ToArray();
        }

        public string[] UncommonFromSentences_1(string s1, string s2)
        {
            return (s1 + " " + s2).Split(' ').GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count()).Where(x => x.Value == 1).Select(x => x.Key).ToArray();
        }
        #endregion

        #region 951. Flip Equivalent Binary Trees
        public bool FlipEquiv(TreeNode root1, TreeNode root2)
        {
            if (root1 == null && root2 == null) return true;

            if (root1 == null || root2 == null || root1.val != root2.val) return false;

            return (FlipEquiv(root1.left, root2.right) && FlipEquiv(root1.right, root2.left)) || (FlipEquiv(root1.left, root2.left) && FlipEquiv(root1.right, root2.right));
        }
        #endregion

        #region 1072. Flip Columns For Maximum Number of Equal Rows
        public int MaxEqualRowsAfterFlips(int[][] matrix)
        {
            int result = 0;

            Dictionary<string, int> patternFrequency = new Dictionary<string, int>();

            foreach (var row in matrix)
            {
                var xorArray = normalizeRow(row);
                string pattern = string.Join(",", xorArray);
                if (patternFrequency.ContainsKey(pattern))
                {
                    patternFrequency[pattern]++;
                }
                else
                {
                    patternFrequency[pattern] = 1;
                }
            }

            return patternFrequency.Values.Max();
        }
        private int[] normalizeRow(int[] row)
        {
            int first = row[0];
            return row.Select(x => x ^ first).ToArray();
        }
        #endregion

        #region 1106. Parsing A Boolean Expression
        public bool ParseBoolExpr(string expression)
        {
            if (expression == "t") return true;

            if (expression == "f") return false;

            char res = 'f';
            int processedIndex = 0;

            if (expression[0] == '&') processedIndex = processAnd(expression, 2, out res);

            return res == 't';
        }

        private int processAnd(string expression, int startIndex, out char res)
        {
            res = 'f';
            while (startIndex < expression.Length)
            {
                switch (expression[startIndex])
                {
                    case '|':
                        startIndex = processOr(expression, startIndex + 2, out res);
                        break;
                    default: break;
                }
                startIndex++;
            }
            return startIndex;
        }

        private int processOr(string expression, int startIndex, out char res)
        {
            res = 't';

            while (startIndex < expression.Length)
            {
                switch ((char)expression[startIndex])
                {
                    case '|':
                        break;
                    default: break;
                }
            }

            return startIndex;
        }

        //public bool ParseBoolExpr(string expression)
        //{
        //    return ParseBoolExpr_helper(expression);
        //}

        //private bool ParseBoolExpr_helper(string expr)
        //{
        //    if (expr == "t") return true;
        //    if (expr == "f") return false;

        //    if (expr[0] == '!')
        //    {
        //        return !ParseBoolExpr_helper(expr.Substring(2, expr.Length - 3));
        //    }
        //    else if (expr[0] == '&')
        //    {
        //        List<string> subExprs = SplitExpr(expr.Substring(2, expr.Length - 3));
        //        foreach (var sub in subExprs)
        //        {
        //            if (!ParseBoolExpr_helper(sub)) return false;
        //        }
        //        return true;
        //    }
        //    else if (expr[0] == '|')
        //    {
        //        List<string> subExprs = SplitExpr(expr.Substring(2, expr.Length - 3));
        //        foreach (var sub in subExprs)
        //        {
        //            if (ParseBoolExpr_helper(sub)) return true;
        //        }
        //        return false;
        //    }

        //    return false;
        //}

        //private List<string> SplitExpr(string expr)
        //{
        //    List<string> subExprs = new List<string>();
        //    int balance = 0;
        //    int start = 0;

        //    for (int size = 0; size < expr.Length; size++)
        //    {
        //        char ch = expr[size];
        //        if (ch == '(') balance++;
        //        else if (ch == ')') balance--;
        //        else if (ch == ',' && balance == 0)
        //        {
        //            subExprs.Add(expr.Substring(start, size - start));
        //            start = size + 1;
        //        }
        //    }

        //    subExprs.Add(expr.Substring(start)); 
        //    return subExprs;
        //}


        //public bool ParseBoolExpr(string expression)
        //{
        //    int processedIndex = -1;
        //    switch (expression[0])
        //    {
        //        case 't': return true;
        //        case '!': return processNot(expression, 2, out processedIndex) == 't';
        //        case '&': return processAnd(expression, 2, out processedIndex) == 't';
        //        case '|': return processOr(expression, 2, out processedIndex) == 't';
        //        default:
        //            return false;
        //    }

        //}

        //private char processString(string expression, out int processedIndex)
        //{
        //    processedIndex = 0;
        //    if (expression.Length == 1) return expression[0];

        //    Stack<char> obstacleQueue = new Stack<char>();

        //    int size = -1;
        //    char op = ' ';
        //    int testIOndex;
        //    while (++size < expression.Length)
        //    {
        //        char c = expression[size];

        //        switch (c)
        //        {
        //            case 't':
        //            case 'f':
        //                obstacleQueue.Push(c);
        //                break;
        //            case '!':

        //                op = processString(expression.Substring(size + 2), out testIOndex);
        //                size = testIOndex;
        //                break;
        //            case '|':

        //                op = processString(expression.Substring(size + 2), out testIOndex);
        //                size = testIOndex;
        //                break;
        //            case '&':

        //                op = processString(expression.Substring(size + 2), out testIOndex);
        //                size = testIOndex;
        //                break;
        //            case '(':

        //                break;
        //            case ')':
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    return obstacleQueue.Pop();
        //}


        //private char processOr(string expression, int startIndex, out int processedIndex)
        //{
        //    char longestStreak = 'f';
        //    int size = startIndex-1;

        //    while (++size < expression.Length)
        //    {
        //        char c = expression[size];
        //        switch (c)
        //        {
        //            case 't':
        //                longestStreak = 't';
        //                break;
        //            case '!':
        //                char not = processNot(expression, size + 2, out processedIndex);

        //                if (not == 't')
        //                {
        //                    longestStreak = 't';
        //                }

        //                break;
        //            case '|':
        //                char or = processOr(expression, size + 2, out processedIndex);

        //                if (or == 't')
        //                {
        //                    longestStreak = 't';
        //                }
        //                break;
        //            case '&':
        //                char and = processAnd(expression, size + 2, out processedIndex);

        //                if (and == 't')
        //                {
        //                    longestStreak = 't';
        //                }
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    processedIndex = size;
        //    return longestStreak;
        //}
        //private char processNot(string expression, int startIndex, out int processedIndex)
        //{
        //    char longestStreak = 'f';
        //    int size = startIndex;

        //    while (size < expression.Length)
        //    {
        //        char c = expression[size];
        //        switch (c)
        //        {
        //            case 't':
        //                longestStreak = 't';
        //                break;
        //            case 'size':

        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    processedIndex = size;
        //    return longestStreak;
        //}
        //private char processAnd(string expression, int startIndex, out int processedIndex)
        //{
        //    char longestStreak = 'f';
        //    int size = startIndex;

        //    while (size < expression.Length)
        //    {
        //        char c = expression[size];
        //        switch (c)
        //        {
        //            case 't':
        //                longestStreak = 't';
        //                break;
        //            case 'size':

        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    processedIndex = size;
        //    return longestStreak;
        //}
        #endregion

        #region 1233. Remove Sub-Folders from the Filesystem

        public IList<string> RemoveSubfolders(string[] folder)
        {
            HashSet<string> prefix = new HashSet<string>(folder);
            IList<string> strings = new List<string>();
            foreach (var str in folder)
            {
                var dir = str.Split('/', StringSplitOptions.RemoveEmptyEntries);

                bool includeFlag = true;
                string fullDir = string.Empty;
                for (int i = 0; i < dir.Length - 1; i++)
                {
                    fullDir += "/" + dir[i];

                    if (prefix.Contains(fullDir))
                    {
                        includeFlag = false;
                        break;
                    }
                }

                if (includeFlag)
                {
                    strings.Add(str);
                }
            }
            return strings;
        }
        public IList<string> RemoveSubfolders_V1(string[] folder)
        {
            Array.Sort(folder);
            string previousFolder = folder[0];
            IList<string> strings = new List<string>() { previousFolder };

            for (int i = 1; i < folder.Length; i++)
            {
                string currFolder = folder[i];

                if (currFolder.StartsWith(previousFolder))
                {
                    char c = currFolder[previousFolder.Length];

                    if (c == '/') continue;
                }
                previousFolder = folder[i];
                strings.Add(previousFolder);
            }

            return strings;
        }
        #endregion

        #region 1277. Count Square Submatrices with All Ones
        public int CountSquares(int[][] matrix)
        {
            int max = -1;
            Dictionary<int, int> resultMap = new Dictionary<int, int>() { { 1, 0 } };

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {

                    if (matrix[i][j] == 0) continue;

                    if (i == 0 || j == 0)
                    {
                        resultMap[1]++;
                        continue;
                    }

                    resultMap[1]++;

                    int top = matrix[i - 1][j];
                    if (top == 0) continue;

                    int left = matrix[i][j - 1];
                    if (left == 0) continue;

                    int topLeft = matrix[i - 1][j - 1];
                    if (topLeft == 0) continue;

                    matrix[i][j] += Math.Min(topLeft, Math.Min(top, left));
                    if (!resultMap.ContainsKey(matrix[i][j]))
                    {
                        max = matrix[i][j];
                        resultMap.Add(matrix[i][j], 0);
                    }

                    resultMap[matrix[i][j]]++;
                }
            }

            int oldCount = 0;
            int result = resultMap[1];
            for (int i = max; i > 1; i--)
            {
                int currCount = resultMap[i] + oldCount;

                result += currCount;
                oldCount = currCount;
            }

            return result;
        }
        #endregion

        #region 1346. Check If N and Its Double Exist
        public bool CheckIfExist(int[] arr)
        {
            HashSet<int> set = new HashSet<int>();

            set.Add(arr[0]);

            for (int i = 1; i < arr.Length; i++)
            {
                if (set.Contains(arr[i] * 2) || (arr[i] % 2 == 0 && set.Contains(arr[i] / 2))) return true;

                set.Add(arr[i]);
            }


            return false;
        }
        #endregion

        #region 1371. Find the Longest Substring Containing Vowels in Even Counts

        /*
            Given the string words, return the size of the longest substring containing each vowel an even number of times. That is, 'a', 'e', 'size', 'o', and 'u' must appear an even number of times. 

            Example 1:

            Input: words = "eleetminicoworoep"
            Output: 13
            Explanation: The longest substring is "leetminicowor" which contains two each of the vowels: e, size and o and zero of the vowels: a and u.
            Example 2:

            Input: words = "leetcodeisgreat"
            Output: 5
            Explanation: The longest substring is "leetc" which contains two e'words.
            Example 3:

            Input: words = "bcbcbc"
            Output: 6
            Explanation: In this case, the given string "bcbcbc" is the longest because all vowels: a, e, size, o and u appear zero times.


            Constraints:

            1 <= words.length <= 5 x 10^5
            words contains only lowercase English letters.

         */
        public int FindTheLongestSubstring(string s)
        {
            Dictionary<char, int> hash = new Dictionary<char, int>() {
                {'a',1 },
                {'e',2 },
                {'i',4 },
                {'o',8 },
                {'u',16 }
            };

            int mask = 0;
            int maxlen = 0;
            int[] seen = Enumerable.Repeat(-1, 32).ToArray();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (hash.ContainsKey(c))
                {
                    mask ^= hash[c];
                }

                if (mask != 0 && seen[mask] == -1)
                {
                    seen[mask] = i;
                }

                maxlen = Math.Max(maxlen, i - seen[mask]);
            }

            return maxlen;
        }
        #endregion

        #region 1405. Longest Happy String
        public string LongestDiverseString(int a, int b, int c)
        {
            StringBuilder stringBuilder = new StringBuilder();

            PriorityQueue<(char c, int count), int> priorityQueue = new PriorityQueue<(char, int), int>(Comparer<int>.Create((x, y) => y - x));

            if (a > 0) priorityQueue.Enqueue(('a', a), a);
            if (b > 0) priorityQueue.Enqueue(('b', b), b);
            if (c > 0) priorityQueue.Enqueue(('c', c), c);

            (char c, int count) characterOnHold = (' ', 0);
            char prevChar = ' ';
            int prevCount = 0;
            while (priorityQueue.Count > 0)
            {
                var dq = priorityQueue.Dequeue();

                if (prevCount == 2 && prevChar == dq.c)
                {
                    if (priorityQueue.Count == 0) break;

                    characterOnHold = dq;

                    dq = priorityQueue.Dequeue();
                    int maxChar = 1;
                    if (characterOnHold.count >= dq.count * 2) maxChar = 2;
                    appendToString(stringBuilder, priorityQueue, dq, 1);
                    appendToString(stringBuilder, priorityQueue, characterOnHold, maxChar);
                    prevCount = maxChar;
                }
                else
                {
                    prevChar = dq.c;
                    prevCount++;
                    appendToString(stringBuilder, priorityQueue, dq);
                }
            }

            return stringBuilder.ToString();
        }

        private static void appendToString(StringBuilder stringBuilder, PriorityQueue<(char c, int count), int> priorityQueue, (char c, int count) dq, int maxChar = 1)
        {
            stringBuilder.Append(dq.c, maxChar);
            if (dq.count > maxChar)
            {
                dq.count -= maxChar;

                priorityQueue.Enqueue(dq, dq.count);
            }
        }
        #endregion

        #region 1492. The kth Factor of n
        public int KthFactor(int n, int k)
        {
            List<int> factors = new List<int>() { 1, n };

            for (int i = 2; i <= Math.Sqrt(n); i++)
            {
                if (n % i == 0)
                {
                    factors.Add(i);

                    if (i != n / i)
                    {
                        factors.Add(n / i);
                    }
                }
            }
            factors.Sort();
            return factors.Count < k ? -1 : factors[k - 1];
        }
        public int KthFactor_1(int n, int k)
        {
            if (k == 1) return 1;

            int low = 1;
            int high = n;
            int i = 0;

            List<int> list = new List<int>();
            list.Add(high);
            list.Insert(i, low);
            i++;

            while (++low < high)
            {
                high = n / low;
                if (n % low == 0)
                {
                    list.Insert(i, high);
                    if (low == high) break;
                    list.Insert(i, low);
                    i++;
                }
            }
            return list.Count < k ? -1 : list[k - 1];
        }
        #endregion

        #region 1545. Find Kth Bit in Nth Binary String
        public char FindKthBit(int n, int k)
        {
            if (n == 1) return '0';

            string str = getString(n);

            return str[k - 1];

        }

        private string getString(int n)
        {
            if (n == 1) return "0";

            string prefix = getString(n - 1);

            char[] chs = invert(prefix);

            return prefix + '1' + new string(chs);
        }

        private char[] invert(string prefix)
        {
            int len = prefix.Length;
            char[] chs = new char[len];

            for (int i = 0; i < len; i++)
            {
                chs[len - 1 - i] = prefix[i] == '0' ? '1' : '0';
            }

            return chs;
        }

        #endregion

        #region 1574. Shortest Subarray to be Removed to Make Array Sorted
        public int FindLengthOfShortestSubarray(int[] arr)
        {
            int left = 0;
            int right = arr.Length - 1;

            while (right > 0 && arr[right] >= arr[right - 1]) { right--; }

            int res = right;


            while (right < arr.Length && arr[left] > arr[right])
            {
                right++;
            }
            res = Math.Min(res, right - left - 1);
            left = 1;
            while (left < right && (left == 0 || arr[left - 1] <= arr[left]))
            {
                while (right < arr.Length && arr[left] > arr[right])
                {
                    right++;
                }
                res = Math.Min(res, right - left - 1);
                left++;
            }

            return res;
        }
        #endregion

        #region 1652. Defuse the Bomb
        public int[] Decrypt(int[] code, int k)
        {
            if (k == 0) return new int[code.Length];

            if (k < 0) Array.Reverse(code);

            int counter = Math.Abs(k);
            int[] result = new int[code.Length];


            int i = 1;
            for (; i <= counter; i++)
            {
                result[0] += code[i];
            }

            i = 0;
            while (++i < code.Length)
            {
                int prevCount = result[i - 1];
                prevCount -= code[i];
                prevCount += code[(i + counter) % code.Length];
                result[i] = prevCount;
            }

            if (k < 0)
            {
                Array.Reverse(result);
            }
            return result;
        }
        #endregion

        #region 1671. Minimum Number of Removals to Make Mountain Array
        public int MinimumMountainRemovals(int[] nums)
        {
            int[] longestIncreasingSequence = getLongestIncreasingSequence(nums);

            Array.Reverse(nums);

            int[] longestDecreasingSequence = getLongestIncreasingSequence(nums);
            Array.Reverse(nums);

            Array.Reverse(longestDecreasingSequence);
            int result = 0;
            for (int i = 1; i < nums.Length - 1; i++)
            {
                if (longestIncreasingSequence[i] > 1 && longestDecreasingSequence[i] > 1)
                {
                    result = Math.Max(result, longestIncreasingSequence[i] + longestDecreasingSequence[i] - 1);
                }
            }


            return nums.Length - result;
        }

        private int[] getLongestIncreasingSequence(int[] nums)
        {
            int[] result = new int[nums.Length];
            List<int> lst = new List<int>() { nums[0] };
            result[0] = 1;
            int maxCount = 1;
            for (int i = 1; i < nums.Length; i++)
            {
                int k = lst.BinarySearch(nums[i]);
                if (k < 0)
                {
                    k = ~k;
                }

                if (k == lst.Count)
                {
                    lst.Add(nums[i]);
                    result[i] = result[i - 1] + 1;
                }
                else
                {

                    lst[k] = nums[i];
                    result[i] = result[i - 1];
                }
            }

            return result;
        }
        #endregion

        #region 1760. Minimum Limit of Balls in a Bag
        public int MinimumSize(int[] nums, int maxOperations)
        {
            int left = 1;
            int right = nums.Max();
            int result = right;
            while (left <= right)
            {
                int mid = (left + right) / 2;

                if (CanDivide(nums, maxOperations, mid))
                {
                    result = mid;
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
            return result;
        }

        private bool CanDivide(int[] nums, int maxOperations, int maxBageSize)
        {
            int totalOperations = 0;
            foreach (var balls in nums)
            {
                if (balls > maxBageSize)
                {
                    totalOperations += (balls - 1) / maxBageSize;
                }
                if (totalOperations > maxOperations) return false;
            }
            return true;
        }
        #endregion

        #region 1792. Maximum Average Pass Ratio
        public double MaxAverageRatio(int[][] classes, int extraStudents)
        {
            PriorityQueue<(double x, double y), double> pq = new PriorityQueue<(double x, double y), double>();

            double sum = 0.0;
            foreach (int[] classArr in classes)
            {
                double n1 = (double)classArr[0];
                double n2 = (double)classArr[1];
                double currentRatio = n1 / n2;
                double nextRatio = (n1 + 1) / (n2 + 1);
                double priority = currentRatio - nextRatio;
                sum += currentRatio;
                pq.Enqueue((n1, n2), priority);
            }

            while (extraStudents-- > 0)
            {
                var pp = pq.Dequeue();

                double n1 = pp.x;
                double n2 = pp.y;

                double currentRatio = n1 / n2;
                sum -= currentRatio;
                n1 += 1;
                n2 += 1;
                currentRatio = n1 / n2;
                double nextRatio = (n1 + 1) / (n2 + 1);
                double priority = currentRatio - nextRatio;

                sum += currentRatio;
                pq.Enqueue((n1, n2), priority);
            }


            return sum / classes.Length;
        }

        public class DoubleComparer : IComparer<double>
        {
            public int Compare(double x, double y)
            {
                if (x < y) return 1;
                if (x > y) return -1;
                return 0;
            }
        }
        #endregion

        #region 1829. Maximum XOR for Each Query
        public int[] GetMaximumXor(int[] nums, int maximumBit)
        {
            int[] result = new int[nums.Length];

            for (int i = 1; i < nums.Length; i++)
            {
                nums[i] = nums[i] ^ nums[i - 1];
            }

            int maxXor = (1 << maximumBit) - 1;
            for (int i = 0; i < nums.Length; i++)
            {
                result[i] = nums[nums.Length - i - 1] ^ maxXor;
            }

            return result;
        }
        #endregion

        #region 1861. Rotating the Box
        public char[][] RotateTheBox(char[][] box)
        {
            char[][] rotatedBox = new char[box[0].Length][];
            for (int row = 0; row < box[0].Length; row++)
            {
                rotatedBox[row] = new char[box.Length];
                for (int col = 0; col < box.Length; col++)
                {
                    rotatedBox[row][col] = box[box.Length - col - 1][row];
                }
            }

            for (int col = 0; col < rotatedBox[0].Length; col++)
            {
                Queue<int> q = new Queue<int>();

                for (int row = rotatedBox.Length - 1; row >= 0; row--)
                {
                    char ch = rotatedBox[row][col];

                    switch (ch)
                    {
                        case '#':
                            if (q.Count > 0)
                            {
                                int i = q.Dequeue();
                                rotatedBox[i][col] = '#';
                                rotatedBox[row][col] = '.';
                                q.Enqueue(row);
                            }
                            break;
                        case '*':
                            q.Clear();
                            break;
                        default:
                            q.Enqueue(row);
                            break;
                    }
                }

            }


            return rotatedBox;
        }
        #endregion

        #region 1957. Delete Characters to Make Fancy String
        public string MakeFancyString(string s)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int count = 1;
            int i = 0;
            char prev = s[0];
            stringBuilder.Append(prev);

            while (++i < s.Length)
            {
                if (prev == s[i])
                {
                    if (count == 2) continue;
                }
                else
                {
                    prev = s[i];
                    count = 0;
                }
                count++;
                stringBuilder.Append(prev);
            }

            return stringBuilder.ToString();
        }
        #endregion

        #region 1975. Maximum Matrix Sum
        public long MaxMatrixSum(int[][] matrix)
        {
            long sum = 0;
            int minValue = 100001;
            int negCount = 0;
            bool zeroFound;
            for (int row = 0; row < matrix.Length; row++)
            {
                for (int col = 0; col < matrix[row].Length; col++)
                {
                    int val = matrix[row][col];
                    if (val <= 0)
                    {
                        val = Math.Abs(val);
                        negCount++;
                    }
                    minValue = Math.Min(minValue, val);
                    sum += val;
                }
            }

            if (minValue == 0 || negCount % 2 == 0) return sum;

            sum -= (minValue * 2);

            return sum;
        }
        #endregion

        #region 2044. Count Number of Maximum Bitwise-OR Subsets
        int maxOr_2044 = 0, count_2044 = 0;
        public int CountMaxOrSubsets(int[] nums)
        {

            foreach (var n in nums)
            {
                maxOr_2044 |= n;
            }
            getSubSets(nums, 0, 0);
            return count_2044;
        }
        private void getSubSets(int[] nums, int i, int currOr)
        {
            if (i == nums.Length)
            {
                if (currOr == maxOr_2044)
                {
                    count_2044++;
                }
                return;
            }

            getSubSets(nums, i + 1, currOr | nums[i]);
            getSubSets(nums, i + 1, currOr);
        }
        #endregion

        #region 2054. Two Best Non-Overlapping Events
        int[][] dp2054;
        public int MaxTwoEvents(int[][] events)
        {
            Array.Sort(events, (x, y) => x[1] - y[1]);

            int[] dp = new int[events.Length];

            int res = 0;

            dp[0] = events[0][2];

            int[] ends = new int[events.Length];

            for (int i = 0; i < events.Length; i++)
            {
                ends[i] = events[i][1];
            }

            for (int i = 1; i < events.Length; i++)
            {
                int index = getIndex(ends, events[i][0] - 1);

                int currSum = events[i][2];

                if (index != -1)
                {
                    currSum += dp[index];
                }

                dp[i] = Math.Max(dp[i - 1], currSum);

                res = Math.Max(res, dp[i]);
            }

            return res;
        }

        private int getIndex(int[] ends, int target)
        {
            int low = 0; int high = ends.Length - 1;

            int result = -1;
            while (low <= high)
            {
                int mid = (low + high) / 2;

                if (ends[mid] <= target)
                {
                    result = mid;
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }
            return result;
        }

        public int MaxTwoEvents_2(int[][] events)
        {
            Array.Sort(events, (x, y) => x[0] - y[0]);
            dp2054 = new int[events.Length][];
            for (int i = 0; i < events.Length; i++)
            {
                dp2054[i] = new int[3];

                Array.Fill(dp2054[i], -1);
            }



            return MaxTwoEvents_2(events, 0, 0);
        }

        private int MaxTwoEvents_2(int[][] events, int index, int count)
        {
            if (count == 2 || index >= events.Length) return 0;

            if (dp2054[index][count] == -1)
            {
                int end = events[index][1];

                int low = index + 1;
                int high = events.Length - 1;

                while (low < high)
                {
                    int mid = (low + high) / 2;

                    if (events[mid][0] > end)
                    {
                        high = mid;
                    }
                    else
                    {
                        low = mid + 1;
                    }
                }

                int include = events[index][2] + (low < events.Length && events[low][0] > end ? MaxTwoEvents_2(events, low, count + 1) : 0);

                int exclude = MaxTwoEvents_2(events, index + 1, count);

                dp2054[index][count] = Math.Max(include, exclude);
            }

            return dp2054[index][count];
        }

        private int getNextOverlappingIndexId(int[][] events, int end, int start)
        {
            int low = start;
            int high = events.Length - 1;
            int resultIndex = -1;
            while (low <= high)
            {
                int mid = (low + high) / 2;

                if (events[mid][0] > end)
                {
                    resultIndex = mid;
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }

            return resultIndex;
        }

        public int MaxTwoEvents_1(int[][] events)
        {
            Array.Sort(events, (x, y) => x[0] - y[0]);
            dp2054 = new int[events.Length][];
            for (int i = 0; i < events.Length; i++)
            {
                dp2054[i] = new int[events.Length];

                Array.Fill(dp2054[i], -1);
            }
            return MaxTwoEvents_1(events, 0, 1);
        }

        public int MaxTwoEvents_1(int[][] events, int index1, int index2)
        {
            if (index1 >= events.Length || index2 >= events.Length || index1 == index2) return 0;
            if (dp2054[index1][index2] != -1) return dp2054[index1][index2];
            int currSum = 0;
            if (overLappingEvents(events[index1], events[index2]) || overLappingEvents(events[index2], events[index1]))
            {
                currSum = Math.Max(events[index1][2], events[index2][2]);
            }
            else
            {
                currSum = events[index1][2] + events[index2][2];
            }
            return dp2054[index1][index2] = Math.Max(currSum, Math.Max(MaxTwoEvents_1(events, index1, index2 + 1), MaxTwoEvents_1(events, index2, index2 + 1)));
        }

        private bool overLappingEvents(int[] source, int[] target)
        {
            return (target[0] >= source[0] && target[0] <= source[1]) || (target[1] >= source[0] && target[1] <= source[1]);
        }
        #endregion

        #region 2064. Minimized Maximum of Products Distributed to Any Store
        public int MinimizedMaximum(int n, int[] quantities)
        {
            int low = 1;
            int high = quantities.Max();
            while (low < high)
            {
                int mid = (low + high) / 2;


                if (canDistribute(quantities, mid, n))
                {
                    high = mid;
                }
                else
                {

                    low = mid + 1;
                }
            }

            return high;
        }

        private bool canDistribute(int[] quantities, int mid, int n)
        {
            int j = 0;

            while (j < quantities.Length && n > 0)
            {
                if (quantities[j] <= mid)
                {
                    n--;
                    j++;
                }
                else
                {
                    n -= quantities[j] / mid;

                    if (quantities[j] % mid > 0)
                    {
                        if (n < 0) return false;
                        n--;
                    }
                    if (n < 0) return false;
                    j++;
                }
            }

            return j == quantities.Length;
        }
        #endregion

        #region 2070. Most Beautiful Item for Each Query
        public int[] MaximumBeauty(int[][] items, int[] queries)
        {
            Array.Sort(items, (x, y) =>
            {
                if (x[0] != y[0]) return x[0] - y[0];

                return y[1] - x[1];
            });
            int[] result = new int[queries.Length];

            Dictionary<int, int> map = new Dictionary<int, int>();
            int max = 0;
            foreach (var item in items)
            {
                max = Math.Max(max, item[1]);

                map[item[0]] = max;
            }
            int[] keys = map.Keys.ToArray();

            for (int i = 0; i < queries.Length; i++)
            {
                int cost = queries[i];
                if (map.ContainsKey(cost))
                {
                    result[i] = map[cost];
                }
                else
                {
                    int v = Array.BinarySearch(keys, cost);
                    v = ~v - 1;
                    if (v >= 0)
                    {
                        result[i] = map[keys[v]];
                    }

                }

            }

            return result;
        }
        #endregion

        #region 2097. Valid Arrangement of Pairs
        public int[][] ValidArrangement(int[][] pairs)
        {
            int[][] result = new int[pairs.Length][];
            Dictionary<int, Stack<int>> map = new Dictionary<int, Stack<int>>();
            Dictionary<int, int> degree = new Dictionary<int, int>();

            foreach (var pair in pairs)
            {
                if (!map.ContainsKey(pair[0]))
                {
                    map[pair[0]] = new Stack<int>();
                }

                map[pair[0]].Push(pair[1]);
                degree[pair[0]] = degree.GetValueOrDefault(pair[0], 0) + 1;

                degree[pair[1]] = degree.GetValueOrDefault(pair[1], 0) - 1;
            }

            int startNode = -1;

            foreach (var key in degree.Keys)
            {
                if (degree[key] == 1)
                {
                    startNode = key;
                    break;
                }
            }
            if (startNode == -1) return pairs;
            Stack<int> nodes = new Stack<int>();
            nodes.Push(startNode);
            int[] paths = new int[pairs.Length + 1];
            int index = -1;

            while (nodes.Count > 0)
            {
                if (map.ContainsKey(nodes.Peek()) && map[nodes.Peek()].Count > 0)
                {
                    nodes.Push(map[nodes.Peek()].Pop());
                }
                else
                {
                    paths[++index] = nodes.Pop();
                }
            }

            int start = paths[--index];
            for (int i = 0; i < result.Length; i++)
            {
                int end = paths[--index];
                result[i] = new int[] { start, end };

                start = end;
            }

            return result;
        }
        public int[][] ValidArrangement4(int[][] pairs)
        {
            int[][] result = new int[pairs.Length][];
            Dictionary<int, Stack<int>> map = new Dictionary<int, Stack<int>>();
            Dictionary<int, int> degree = new Dictionary<int, int>();

            foreach (var pair in pairs)
            {
                if (!map.ContainsKey(pair[0]))
                {
                    map[pair[0]] = new Stack<int>();
                }

                map[pair[0]].Push(pair[1]);
                degree[pair[0]] = degree.GetValueOrDefault(pair[0], 0) + 1;

                degree[pair[1]] = degree.GetValueOrDefault(pair[1], 0) - 1;
            }

            int startNode = -1;

            foreach (var key in degree.Keys)
            {
                if (degree[key] == 1)
                {
                    startNode = key;
                    break;
                }
            }
            if (startNode == -1) return pairs;
            Stack<int> nodes = new Stack<int>();
            nodes.Push(startNode);
            Stack<int> paths = new Stack<int>();

            while (nodes.Count > 0)
            {
                if (map.ContainsKey(nodes.Peek()) && map[nodes.Peek()].Count > 0)
                {
                    nodes.Push(map[nodes.Peek()].Pop());
                }
                else
                {
                    paths.Push(nodes.Pop());
                }
            }

            int start = paths.Pop();
            for (int i = 0; i < result.Length; i++)
            {
                int end = paths.Pop();
                result[i] = new int[] { start, end };

                start = end;
            }

            return result;
        }
        public int[][] ValidArrangement3(int[][] pairs)
        {
            var adjacencyList = new Dictionary<int, List<int>>();
            var inOutDegree = new Dictionary<int, int>();

            // Build graph and count in/out degrees
            foreach (var pair in pairs)
            {
                if (!adjacencyList.ContainsKey(pair[0]))
                {
                    adjacencyList[pair[0]] = new List<int>();
                }
                adjacencyList[pair[0]].Add(pair[1]);

                inOutDegree[pair[0]] = inOutDegree.GetValueOrDefault(pair[0], 0) + 1;  // out-degree
                inOutDegree[pair[1]] = inOutDegree.GetValueOrDefault(pair[1], 0) - 1;  // in-degree
            }

            // Find starting node
            int startNode = pairs[0][0];
            foreach (var kvp in inOutDegree)
            {
                if (kvp.Value == 1)
                {
                    startNode = kvp.Key;
                    break;
                }
            }

            var path = new List<int>();
            var nodeStack = new Stack<int>();
            nodeStack.Push(startNode);

            while (nodeStack.Count > 0)
            {
                if (!adjacencyList.ContainsKey(nodeStack.Peek()) ||
                    adjacencyList[nodeStack.Peek()].Count == 0)
                {
                    path.Add(nodeStack.Pop());
                }
                else
                {
                    var neighbors = adjacencyList[nodeStack.Peek()];
                    int nextNode = neighbors[neighbors.Count - 1];
                    nodeStack.Push(nextNode);
                    neighbors.RemoveAt(neighbors.Count - 1);
                }
            }

            var arrangement = new List<int[]>();
            int pathSize = path.Count;

            for (int i = pathSize - 1; i > 0; --i)
            {
                arrangement.Add(new int[] { path[i], path[i - 1] });
            }

            return arrangement.ToArray();
        }

        public int[][] ValidArrangement2(int[][] pairs)
        {
            var adjacencyMatrix = new Dictionary<int, Queue<int>>();
            var inDegree = new Dictionary<int, int>();
            var outDegree = new Dictionary<int, int>();

            // Build the adjacency list and track in-degrees and out-degrees
            foreach (var pair in pairs)
            {
                int start = pair[0], end = pair[1];
                if (!adjacencyMatrix.ContainsKey(start))
                    adjacencyMatrix[start] = new Queue<int>();
                adjacencyMatrix[start].Enqueue(end);

                outDegree[start] = outDegree.GetValueOrDefault(start, 0) + 1;
                inDegree[end] = inDegree.GetValueOrDefault(end, 0) + 1;
            }

            var result = new List<int>();

            // Find the start node (outDegree == inDegree + 1)
            int startNode = -1;
            foreach (var node in outDegree.Keys)
            {
                if (outDegree[node] == inDegree.GetValueOrDefault(node, 0) + 1)
                {
                    startNode = node;
                    break;
                }
            }

            // If no such node exists, start from the first pair's first element
            if (startNode == -1)
            {
                startNode = pairs[0][0];
            }

            // Start DFS traversal
            Visit(startNode, adjacencyMatrix, result);

            // Reverse the ub since DFS gives us the path in reverse
            result.Reverse();

            // Construct the ub pairs
            int[][] pairedResult = new int[result.Count - 1][];
            for (int i = 1; i < result.Count; i++)
            {
                pairedResult[i - 1] = new int[] { result[i - 1], result[i] };
            }

            return pairedResult;
        }

        private void Visit(int node, Dictionary<int, Queue<int>> adjMatrix, List<int> res)
        {
            if (adjMatrix.TryGetValue(node, out var neighbors))
            {
                while (neighbors.Count > 0)
                {
                    int nextNode = neighbors.Dequeue();
                    Visit(nextNode, adjMatrix, res);
                }
            }
            res.Add(node);
        }

        public int[][] ValidArrangement1(int[][] pairs)
        {
            int[][] result = new int[pairs.Length][];
            int keyToStart = -1;
            Dictionary<int, HashSet<int>> outMap = new Dictionary<int, HashSet<int>>();
            Dictionary<int, HashSet<int>> inMap = new Dictionary<int, HashSet<int>>();

            foreach (var pair in pairs)
            {
                updateMap(outMap, pair[0], pair[1]);
                updateMap(inMap, pair[1], pair[0]);
            }

            foreach (var outMapKey in outMap.Keys)
            {
                if (!inMap.ContainsKey(outMapKey))
                {
                    keyToStart = outMapKey;
                    break;
                }
            }

            if (keyToStart == -1)
            {
                return reverseValidArrangements(inMap, outMap, pairs);
            }

            for (int i = 0; i < result.Length; i++)
            {
                int key = keyToStart;
                int value = outMap[key].FirstOrDefault();
                result[i] = new int[] { key, value };

                outMap[key].Remove(value);
                inMap[value].Remove(key);

                if (outMap[key].Count == 0) outMap.Remove(key);
                if (inMap[value].Count == 0) inMap.Remove(value);

                keyToStart = value;
            }


            return result;
        }

        private int[][] reverseValidArrangements(Dictionary<int, HashSet<int>> inMap, Dictionary<int, HashSet<int>> outMap, int[][] pairs)
        {
            int[][] result = new int[pairs.Length][];
            int endValueToStart = -1;
            foreach (var inMapKey in inMap.Keys)
            {
                if (!outMap.ContainsKey(inMapKey))
                {
                    endValueToStart = inMapKey;
                    break;
                }
            }
            if (endValueToStart == -1) return pairs;
            for (int i = result.Length - 1; i >= 0; i--)
            {
                int key = getKey(inMap, endValueToStart);
                int value = endValueToStart;

                result[i] = new int[] { key, value };
                outMap[key].Remove(value);

                if (outMap[key].Count == 0) outMap.Remove(key);

                endValueToStart = key;
            }

            return result;
        }

        private int getKey(Dictionary<int, HashSet<int>> map, int endValueToStart)
        {
            int key = map[endValueToStart].FirstOrDefault();

            map[endValueToStart].Remove(key);

            if (map[endValueToStart].Count == 0) map.Remove(endValueToStart);

            return key;
        }

        private static void updateMap(Dictionary<int, HashSet<int>> map, int key, int value)
        {
            if (!map.ContainsKey(key))
            {
                map[key] = new HashSet<int>();
            }
            map[key].Add(value);
        }
        #endregion

        #region 2109. Adding Spaces to a String 
        public string AddSpaces(string s, int[] spaces)
        {
            char[] newString = new char[s.Length + spaces.Length];
            int addedSpace = 0;

            int i = -1;
            int spaceIndex = 0;

            while (++i < s.Length)
            {
                if (i == spaces[spaceIndex])
                {
                    newString[i + addedSpace] = ' ';
                    spaceIndex++;
                    addedSpace++;
                }
                newString[i + addedSpace] = s[i];

            }
            return new string(newString);
        }
        public string AddSpaces_1(string s, int[] spaces)
        {
            int curLen = spaces[0];
            int curIndex = 0;


            StringBuilder sb = new StringBuilder();
            if (curLen == 0)
            {
                sb.Append(" ");
            }
            else
            {
                sb.Append(s.Substring(curIndex, curLen));
                sb.Append(" ");
            }
            curIndex = spaces[0];
            for (int i = 1; i < spaces.Length; i++)
            {
                curLen = spaces[i] - spaces[i - 1];
                sb.Append(s.Substring(curIndex, curLen));
                sb.Append(' ');
                curIndex = spaces[i];
            }

            sb.Append(s.Substring(curIndex));

            return sb.ToString();
        }
        #endregion

        #region 2182. Construct String With Repeat Limit
        public string RepeatLimitedString(string s, int repeatLimit)
        {
            int[] charCount = new int[26];
            foreach (char c in s)
            {
                charCount[c - 'a']++;
            }

            StringBuilder sb = new StringBuilder();
            int nextAvailable = 25;
            for (int i = 25; i >=0; i--)
            {
                nextAvailable = i - 1;
                while (charCount[i]>0)
                {
                    if (charCount[i] <= repeatLimit)
                    {
                        sb.Append((char)(i + 'a'), charCount[i]);
                        charCount[i] = 0;
                    }
                    else
                    {
                        sb.Append((char)(i + 'a'), repeatLimit);
                        charCount[i] -= repeatLimit;
                        while (nextAvailable >=0 && charCount[nextAvailable]==0)
                        {
                            nextAvailable--;   
                        }
                        if (nextAvailable >= 0 && charCount[nextAvailable]>0)
                        {
                            sb.Append((char)(nextAvailable + 'a'));
                            charCount[nextAvailable]--;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }


            return sb.ToString();
        }
        #endregion

        #region 2221. Find Triangular Sum of an Array
        public int TriangularSum(int[] nums)
        {
            for (int j = 0; j < nums.Length; j++)
            {

                for (int i = 1; i < nums.Length - j; i++)
                {
                    nums[i] = (nums[i] + nums[i - 1]) % 10;
                }

            }
            return nums[0];
        }
        #endregion

        #region 2222. Number of Ways to Select Buildings
        public long NumberOfWays(string s)
        {
            long result = 0;



            return result;
        }
        #endregion

        #region 2257. Count Unguarded Cells in the Grid
        public int CountUnguarded(int m, int n, int[][] guards, int[][] walls)
        {
            int count = 0;

            int[][] grid = new int[m][];
            for (int i = 0; i < m; i++)
            {
                grid[i] = new int[n];
            }

            const int w = -1;
            const int g = -2;
            const int c = -3;

            foreach (var wall in walls)
            {
                grid[wall[0]][wall[1]] = w;
                count++;
            }
            Queue<(int x, int y)> q = new Queue<(int, int)>();
            foreach (var guard in guards)
            {
                q.Enqueue((guard[0], guard[1]));
                grid[guard[0]][guard[1]] = g;
                count++;
            }

            while (q.Count > 0)
            {
                (int x, int y) = q.Dequeue();

                int up = x;
                int down = x;

                int left = y;
                int right = y;

                while (--up >= 0 && (grid[up][y] == 0 || grid[up][y] == c))
                {
                    if (grid[up][y] == c) { continue; }
                    grid[up][y] = c;
                    count++;
                }

                while (++down < m && (grid[down][y] == 0 || grid[down][y] == c))
                {
                    if (grid[down][y] == c) continue;
                    grid[down][y] = c;
                    count++;
                }

                while (--left >= 0 && (grid[x][left] == 0 || grid[x][left] == c))
                {
                    if (grid[x][left] == c) continue;
                    grid[x][left] = c;
                    count++;
                }

                while (++right < n && (grid[x][right] == 0 || grid[x][right] == c))
                {
                    if (grid[x][right] == c) continue;
                    grid[x][right] = c;
                    count++;
                }
            }

            //for (int row = 0; row < m; row++)
            //{
            //    for (int col = 0; col < n; col++)
            //    {
            //        if (grid1[row][col] == 0) count++;
            //    }
            //}

            return m * n - count;
        }
        public int CountUnguarded_1(int m, int n, int[][] guards, int[][] walls)
        {
            int count = 0;

            int[][] grid = new int[m][];
            for (int i = 0; i < m; i++)
            {
                grid[i] = new int[n];
            }

            const int w = -1;
            const int g = -2;
            const int c = -3;

            foreach (var wall in walls)
            {
                grid[wall[0]][wall[1]] = w;
            }
            Queue<(int x, int y)> q = new Queue<(int, int)>();
            foreach (var guard in guards)
            {
                q.Enqueue((guard[0], guard[1]));
                grid[guard[0]][guard[1]] = g;
            }

            while (q.Count > 0)
            {
                (int x, int y) = q.Dequeue();

                int up = x;
                int down = x;

                int left = y;
                int right = y;

                while (--up >= 0 && (grid[up][y] == 0 || grid[up][y] == c))
                {
                    grid[up][y] = c;
                }

                while (++down < m && (grid[down][y] == 0 || grid[down][y] == c))
                {
                    grid[down][y] = c;
                }

                while (--left >= 0 && (grid[x][left] == 0 || grid[x][left] == c))
                {
                    grid[x][left] = c;
                }

                while (++right < n && (grid[x][right] == 0 || grid[x][right] == c))
                {
                    grid[x][right] = c;
                }
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i][j] == 0) count++;
                }
            }

            return count;
        }
        #endregion

        #region 2275. Largest Combination With Bitwise AND Greater Than Zero

        public int LargestCombination(int[] candidates)
        {
            int[] bitCounts = new int[24]; //based on input contraints;
            int result = 0;
            foreach (int candidate in candidates)
            {
                for (int i = 0; i < 24; i++)
                {
                    int count = candidate & (1 << i);
                    if (count > 0)
                    {
                        bitCounts[i]++;
                        result = Math.Max(result, bitCounts[i]);
                    }
                }
            }

            return result;
        }
        public int LargestCombination_1(int[] candidates)
        {
            int[] bitCounts = new int[24]; //based on input contraints;
            foreach (int candidate in candidates)
            {
                string number = Convert.ToString(candidate, 2);

                int i = number.Length;
                int k = 23;
                while (--i >= 0)
                {
                    if (number[i] == '1')
                    {
                        bitCounts[k]++;
                    }
                    k--;
                }
            }

            return bitCounts.Max();
        }
        #endregion

        #region 2290. Minimum Obstacle Removal to Reach Corner
        public int MinimumObstacles(int[][] grid)
        {
            PriorityQueue<(int row, int col, int obstacles), int> pq = new PriorityQueue<(int row, int col, int obstacles), int>();
            int ob = 0;
            if (grid[0][0] == 1)
            {
                ob = 1;
            }
            pq.Enqueue((0, 0, ob), ob);
            grid[0][0] = int.MaxValue;

            while (pq.Count > 0)
            {
                var pp = pq.Dequeue();

                if (pp.row == grid.Length - 1 && pp.col == grid[pp.row].Length - 1) return pp.obstacles;
                checkAndAddNeighbours(grid, pp.row + 1, pp.col, pp.obstacles, pq);
                checkAndAddNeighbours(grid, pp.row - 1, pp.col, pp.obstacles, pq);
                checkAndAddNeighbours(grid, pp.row, pp.col + 1, pp.obstacles, pq);
                checkAndAddNeighbours(grid, pp.row, pp.col - 1, pp.obstacles, pq);
            }

            return grid.Length * grid[0].Length;
        }

        private void checkAndAddNeighbours(int[][] grid, int row, int col, int obstacles, PriorityQueue<(int row, int col, int obstacles), int> pq)
        {
            if (row < 0 || col < 0 || row == grid.Length || col == grid[row].Length || grid[row][col] == int.MaxValue) return;

            if (grid[row][col] == 1)
            {
                obstacles = obstacles + 1;
            }

            pq.Enqueue((row, col, obstacles), obstacles);
        }

        public int MinimumObstacles_1(int[][] grid)
        {
            for (int row = 0; row < grid.Length; row++)
            {
                for (int col = 0; col < grid[row].Length; col++)
                {
                    grid[row][col] *= -1;
                }
            }


            Queue<(int row, int col)> queue = new Queue<(int row, int col)>();
            Queue<(int row, int col)> obstacleQueue = new Queue<(int row, int col)>();

            if (grid[0][0] == -1)
            {
                grid[0][0] = 2;
            }
            else
            {
                grid[0][0] = 1;
            }

            queue.Enqueue((0, 0));

            while (obstacleQueue.Count > 0 || queue.Count > 0)
            {
                while (queue.Count > 0)
                {
                    var dq = queue.Dequeue();
                    if (dq.row == grid.Length - 1 && dq.col == grid[dq.row].Length - 1) { return grid[grid.Length - 1][grid[0].Length - 1] - 1; }
                    addInQueue(grid, dq.row + 1, dq.col, grid[dq.row][dq.col], queue, obstacleQueue);
                    addInQueue(grid, dq.row - 1, dq.col, grid[dq.row][dq.col], queue, obstacleQueue);
                    addInQueue(grid, dq.row, dq.col + 1, grid[dq.row][dq.col], queue, obstacleQueue);
                    addInQueue(grid, dq.row, dq.col - 1, grid[dq.row][dq.col], queue, obstacleQueue);

                }

                if (obstacleQueue.Count > 0)
                {
                    var pop = obstacleQueue.Dequeue();

                    int min = int.MaxValue;

                    min = Math.Min(min, getNeighbour(pop.row - 1, pop.col, grid));

                    min = Math.Min(min, getNeighbour(pop.row + 1, pop.col, grid));

                    min = Math.Min(min, getNeighbour(pop.row, pop.col - 1, grid));

                    min = Math.Min(min, getNeighbour(pop.row, pop.col + 1, grid));

                    grid[pop.row][pop.col] = min + 1;

                    queue.Enqueue(pop);
                }
            }




            return grid[grid.Length - 1][grid[0].Length - 1] - 1;

        }

        private void addInQueue(int[][] grid, int row, int col, int value, Queue<(int row, int col)> queue, Queue<(int row, int col)> obstacleQueue)
        {
            if (row < 0 || col < 0 || row >= grid.Length || col >= grid[row].Length || grid[row][col] > 0 || grid[row][col] == -2) return;

            if (grid[row][col] == 0)
            {
                grid[row][col] = value;
                queue.Enqueue((row, col));
            }
            else
            {
                grid[row][col] = -2;
                obstacleQueue.Enqueue((row, col));
            }
        }

        private static int getNeighbour(int row, int col, int[][] dp)
        {
            if (row < 0 || col < 0 || row >= dp.Length || col >= dp[row].Length || dp[row][col] <= 0) return int.MaxValue;

            return dp[row][col];
        }


        #endregion

        #region 2357. Make Array Zero by Subtracting Equal Amounts
        public int MinimumOperations(int[] nums)
        {
            bool[] arr = new bool[100];
            int count = 0;
            foreach (int num in nums)
            {
                if (num == 0) continue;
                if (!arr[num - 1])
                {
                    arr[num - 1] = true;
                    count++;
                }
            }
            return count;
        }
        #endregion

        #region 2458. Height of Binary Tree After Subtree Removal Queries

        public int[] TreeQueries(TreeNode root, int[] queries)
        {
            int[] result = new int[queries.Length];
            Dictionary<int, int> nodeHeights = new Dictionary<int, int>();
            List<List<int>> levelNodes = new List<List<int>>();
            int nodeHeight = calculateNodeHeights(root, nodeHeights, levelNodes, 1);

            for (int i = 0; i < levelNodes.Count; i++)
            {
                levelNodes[i] = levelNodes[i].OrderByDescending(x => nodeHeights[x]).ToList();
            }


            for (int i = 0; i < queries.Length; i++)
            {
                int level = -1;

                for (int j = 0; j < levelNodes.Count; j++)
                {
                    if (levelNodes[j][0] == queries[i])
                    {
                        level = j;
                        break;
                    }
                }

                if (level == -1)
                {
                    result[i] = nodeHeight;
                }
                else
                {
                    int newHeight = nodeHeight - nodeHeights[queries[i]];

                    if (levelNodes[level].Count > 1)
                    {
                        newHeight += nodeHeights[levelNodes[level][1]];
                    }
                    else
                    {
                        newHeight -= 1;
                    }

                    result[i] = newHeight;
                }
            }
            return result;
        }

        private int calculateNodeHeights(TreeNode root, Dictionary<int, int> nodeHeights, List<List<int>> levelNodes, int level)
        {
            if (root == null) return -1;

            int leftHeight = calculateNodeHeights(root.left, nodeHeights, levelNodes, level + 1);
            int rightHeight = calculateNodeHeights(root.right, nodeHeights, levelNodes, level + 1);

            int currentHeight = 1;

            currentHeight += Math.Max(leftHeight, rightHeight);

            nodeHeights.Add(root.val, currentHeight);

            while (levelNodes.Count < level)
            {
                levelNodes.Add(new List<int>());
            }

            levelNodes[level - 1].Add(root.val);

            return currentHeight;
        }

        private int calculateNodeHeights_V2(TreeNode root, Dictionary<int, int> nodeHeights, Dictionary<int, int> maxPart)
        {
            if (root == null) return -1;

            int leftHeight = calculateNodeHeights_V2(root.left, nodeHeights, maxPart);
            int rightHeight = calculateNodeHeights_V2(root.right, nodeHeights, maxPart);

            int currentHeight = 1;

            if (leftHeight == rightHeight)
            {
                currentHeight += leftHeight;
            }
            else if (leftHeight > rightHeight)
            {
                maxPart.Add(root.left.val, rightHeight + currentHeight);
                currentHeight += leftHeight;
            }
            else
            {
                maxPart.Add(root.right.val, leftHeight + currentHeight);
                currentHeight += rightHeight;
            }
            //+ Math.Max(leftHeight, rightHeight);

            nodeHeights.Add(root.val, currentHeight);

            return currentHeight;

        }

        public int[] TreeQueries_1(TreeNode root, int[] queries)
        {
            int[] result = new int[queries.Length];

            Dictionary<int, int> nodeHeights = new Dictionary<int, int>();
            Dictionary<int, int> siblingHeights = new Dictionary<int, int>();

            int treeHeight = calculateNodeHeights_1(root, nodeHeights, siblingHeights);

            for (int i = 0; i < queries.Length; i++)
            {
                int removedHeight = siblingHeights[queries[i]];
                int height = treeHeight - removedHeight - 1;
                result[i] = height;
            }

            return result;
        }

        private int calculateNodeHeights_1(TreeNode root, Dictionary<int, int> nodeHeights, Dictionary<int, int> siblingHeights)
        {
            if (root == null) return -1;

            int leftHeight = calculateNodeHeights_1(root.left, nodeHeights, siblingHeights);
            int rightHeight = calculateNodeHeights_1(root.right, nodeHeights, siblingHeights);

            int currHeight = 1 + Math.Max(leftHeight, rightHeight);



            nodeHeights.Add(root.val, currHeight);

            if (root.left != null)
            {
                siblingHeights.Add(root.left.val, rightHeight);
            }

            if (root.right != null)
            {
                siblingHeights.Add(root.right.val, leftHeight);
            }

            return currHeight;
        }
        #endregion

        #region 2461. Maximum Sum of Distinct Subarrays With Length K
        public long MaximumSubarraySum(int[] nums, int k)
        {
            if (k == 1) return nums.Max();
            long sum = 0;
            Dictionary<int, int> map = new Dictionary<int, int>() { { nums[0], 0 } };
            int i = 0;
            int j = 1;
            long currSum = nums[i];

            while (j < k + i && j < nums.Length)
            {
                if (map.ContainsKey(nums[j]))
                {
                    int prevIndex = map[nums[j]];

                    while (i <= prevIndex)
                    {
                        currSum -= nums[i];
                        map.Remove(nums[i]);
                        i++;
                    }

                    currSum += nums[j];
                }
                else
                {
                    currSum += nums[j];
                }

                map[nums[j]] = j;
                if (i + k == j + 1)
                {
                    sum = Math.Max(sum, currSum);

                    currSum -= nums[i];
                    map.Remove(nums[i]);
                    i++;
                }
                j++;
            }

            return sum;
        }
        #endregion

        #region 2463. Minimum Total Distance Traveled
        public long MinimumTotalDistance_V1(IList<int> robot, int[][] factory)
        {
            long result = long.MaxValue;
            robot = robot.OrderBy(x => x).ToList();
            Array.Sort(factory, (x, y) => x[0].CompareTo(y[0]));
            int n = robot.Count;
            int m = factory.Length;

            int[,] dp = new int[n + 1, m + 1];

            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= m; j++)
                {
                    dp[i, j] = int.MaxValue / 2;
                }
            }

            dp[0, 0] = 0;

            for (int j = 1; j <= m; j++)
            {
                int pos = factory[j - 1][0];
                int limit = factory[j - 1][1];

                for (int i = 0; i <= n; i++)
                    dp[i, j] = dp[i, j - 1];

                for (int k = 1; k <= limit; k++)
                {
                    for (int i = k; i <= n; i++)
                    {
                        int distance = Math.Abs(pos - robot[i - k]);
                        dp[i, j] = Math.Min(dp[i, j], dp[i - k, j - 1] + distance * k);
                    }
                }
            }
            return dp[n, m];
        }

        public long MinimumTotalDistance(IList<int> robot, int[][] factory)
        {

            var sortedRobot = robot.OrderBy(x => x).ToArray();
            Array.Sort(factory, (a, b) => a[0].CompareTo(b[0]));

            int m = sortedRobot.Length;
            int n = factory.Length;

            long[,] dp = new long[m + 1, n + 1];
            for (int i = 0; i <= m; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    dp[i, j] = long.MaxValue;
                }
            }
            dp[m, n] = 0;

            for (int j = n - 1; j >= 0; j--)
            {
                long prefixSum = 0;
                var deque = new LinkedList<(int, long)>();
                deque.AddLast((m, 0));

                for (int i = m - 1; i >= 0; i--)
                {
                    prefixSum += Math.Abs(sortedRobot[i] - factory[j][0]);

                    if (deque.Count > 0 && deque.First.Value.Item1 > i + factory[j][1])
                    {
                        deque.RemoveFirst();
                    }

                    long currentValue = dp[i, j + 1] - prefixSum;
                    while (deque.Count > 0 && deque.Last.Value.Item2 >= currentValue)
                    {
                        deque.RemoveLast();
                    }
                    deque.AddLast((i, currentValue));
                    dp[i, j] = deque.First.Value.Item2 + prefixSum;
                }
            }
            return dp[0, 0];
        }
        #endregion

        #region 2490. Circular Sentence
        public bool IsCircularSentence(string sentence)
        {
            string[] words = sentence.Split(' ');

            char endWith = words[words.Length - 1][words[words.Length - 1].Length - 1];

            int i = -1;

            while (++i < words.Length)
            {
                if (words[i][0] != endWith) return false;

                endWith = words[i][words[i].Length - 1];
            }
            return true;
        }

        public bool IsCircularSentence_1(string sentence)
        {
            string[] words = sentence.Split(' ');
            for (int i = 1; i < words.Length; i++) if (words[i - 1][words[i - 1].Length - 1] != words[i][0]) return false;
            return (words[0][0] == words[words.Length - 1][words[words.Length - 1].Length - 1]);
        }
        #endregion

        #region 2501. Longest Square Streak in an Array
        public int LongestSquareStreak(int[] nums)
        {
            Array.Sort(nums);
            int result = -1;

            int i = -1;

            while (++i < nums.Length)
            {
                int cnt = -1;

                int sqr = nums[i] * nums[i];

                int found = Array.BinarySearch(nums, i + 1, nums.Length - 1 - i, sqr);
                if (found > 0) cnt = 1;
                while (found > 0)
                {
                    cnt++;
                    sqr = nums[found] * nums[found];
                    found = Array.BinarySearch(nums, found + 1, nums.Length - 1 - found, sqr);
                }

                result = Math.Max(result, cnt);



            }
            return result;
        }
        #endregion

        #region 2516. Take K of Each Character From Left and Right
        public int TakeCharacters(string s, int k)
        {
            int[] count = new int[3];
            int n = s.Length;

            // Count total occurrences
            foreach (char c in s)
            {
                count[c - 'a']++;
            }

            // Check if we have enough characters
            for (int i = 0; i < 3; i++)
            {
                if (count[i] < k) return -1;
            }

            int[] window = new int[3];
            int left = 0, maxWindow = 0;

            // Find the longest window that leaves key of each character outside
            for (int right = 0; right < n; right++)
            {
                window[s[right] - 'a']++;

                // Shrink window if we take too many characters
                while (
                    left <= right &&
                    (count[0] - window[0] < k ||
                        count[1] - window[1] < k ||
                        count[2] - window[2] < k)
                )
                {
                    window[s[left] - 'a']--;
                    left++;
                }

                maxWindow = Math.Max(maxWindow, right - left + 1);
            }

            return n - maxWindow;
        }
        public int TakeCharacters_2(string s, int k)
        {
            Dictionary<char, int> map = new Dictionary<char, int>()
            {
                {'a', 0},
                {'b', 0},
                {'c', 0}
            };

            foreach (char c in s)
            {
                map[c]++;
            }

            if (map['a'] < k || map['b'] < k || map['c'] < k) return -1;

            int result = s.Length;

            int left = 0;

            Dictionary<char, int> currentCount = new Dictionary<char, int> { { 'a', 0 }, { 'b', 0 }, { 'c', 0 } };

            for (int right = 0; right < s.Length; right++)
            {
                char rightChar = s[right];
                currentCount[rightChar]++;

                while (map['a'] - currentCount['a'] >= k &&
                       map['b'] - currentCount['b'] >= k &&
                       map['c'] - currentCount['c'] >= k)
                {
                    result = Math.Min(result, right - left + 1);
                    char leftChar = s[left];
                    currentCount[leftChar]--;
                    left++;
                }
            }

            return s.Length - result;
        }

        //public int TakeCharacters_Helper(string s, int key, int left, int right, int countA, int countB, int countC)
        //{
        //    if (countA >= key && countB >= key && countC >= key) return 0;

        //    int currCountA = countA, currCountB = countB, currCountC = countC;

        //    switch (s[left])
        //    {
        //        case 'a':
        //            currCountA++;
        //            break;
        //        case 'b':
        //            currCountB++;
        //            break;
        //        case 'c':
        //            currCountC++;
        //            break;
        //        default:
        //            break;
        //    }

        //    int leftCount = TakeCharacters_Helper(s, key, left, right, currCountA, currCountB, currCountC);

        //    return 1 + Math.Min(TakeCharacters_Helper(s, key, left + 1, right, countA, countB, countC), TakeCharacters_Helper(s, key, left, right - 1, countA, countB, countC));
        //}
        #endregion

        #region 2554. Maximum Number of Integers to Choose From a Range I

        public int MaxCount(int[] banned, int n, int maxSum)
        {

            int result = 0;
            HashSet<int> seen = new HashSet<int>(banned);
            int sum = 0;
            for (int i = 1; i <= n; i++)
            {
                if (seen.Contains(i)) continue;
                if (sum + i > maxSum) return result;

                sum += i;
                result++;

            }

            return result;
        }
        #endregion

        #region 2558. Take Gifts From the Richest Pile
        public long PickGifts(int[] gifts, int k)
        {
            long result = 0;
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>(Comparer<int>.Create((x, y) => y - x));
            //int i = -1;
            foreach (var gift in gifts)
            {
                result += gift;
                pq.Enqueue(gift, gift);
            }

            while (k-- > 0)
            {
                var x = pq.Dequeue();

                var sqrtFloor = (int)Math.Floor(Math.Sqrt(x));

                result -= (x - sqrtFloor);

                pq.Enqueue(sqrtFloor, sqrtFloor);

            }

            return result;
        }
        #endregion

        #region 2563. Count the Number of Fair Pairs
        public long CountFairPairs(int[] nums, int lower, int upper)
        {
            long count = 0;
            Array.Sort(nums);
            for (int i = 0; i < nums.Length; i++)
            {

                int lowerBoundIndex = getLowerBound(nums, lower - nums[i], i);
                int upperBoundIndex = getLowerBound(nums, upper - nums[i] + 1, i) - 1;
                int currCount = upperBoundIndex - lowerBoundIndex + 1;

                count += currCount;
            }

            return count;
        }

        private int getLowerBound(int[] nums, int target, int left)
        {
            int low = left;
            int high = nums.Length;

            while (low + 1 < high)
            {
                int mid = (low + high) / 2;

                if (nums[mid] < target)
                {
                    low = mid;
                }
                else
                {
                    high = mid;
                }
            }
            return high;
        }
        #endregion

        #region 2577. Minimum Time to Visit a Cell In a Grid
        public int MinimumTime(int[][] grid)
        {
            if (grid[0][1] <= 1 || grid[1][0] <= 1)
            {

                Queue<(int row, int col, int time)> q = new Queue<(int row, int col, int time)>();
                PriorityQueue<(int row, int col, int time), int> pq = new PriorityQueue<(int row, int col, int time), int>();
                q.Enqueue((0, 0, 0));

                while (q.Count > 0 || pq.Count > 0)
                {
                    var dq = q.Dequeue();

                    if (dq.row == grid.Length - 1 && dq.col == grid[dq.row].Length - 1) return dq.time;

                    int nextTime = dq.time + 1;

                    while (pq.Count > 0 && pq.Peek().time == nextTime)
                    {
                        q.Enqueue(pq.Dequeue());
                    }

                    checkAndAddInQueue(q, pq, grid, dq.row + 1, dq.col, dq.time + 1);

                    checkAndAddInQueue(q, pq, grid, dq.row, dq.col + 1, dq.time + 1);

                    checkAndAddInQueue(q, pq, grid, dq.row - 1, dq.col, dq.time + 1);

                    checkAndAddInQueue(q, pq, grid, dq.row, dq.col - 1, dq.time + 1);

                    if (q.Count == 0 && pq.Count > 0)
                    {
                        nextTime = pq.Peek().time;

                        while (pq.Count > 0 && pq.Peek().time == nextTime)
                        {
                            q.Enqueue(pq.Dequeue());
                        }
                    }
                }
            }
            return -1;
        }

        private void checkAndAddInQueue(Queue<(int row, int col, int time)> q, PriorityQueue<(int row, int col, int time), int> pq, int[][] grid, int row, int col, int time)
        {
            if (row < 0 || col < 0 || row >= grid.Length || col >= grid[row].Length || grid[row][col] == -1) return;

            if (grid[row][col] <= time)
            {
                q.Enqueue((row, col, time));
            }
            else
            {
                int diff = grid[row][col] - time;

                if (diff % 2 == 1) diff += 1;

                pq.Enqueue((row, col, time + diff), time + diff);
            }
            grid[row][col] = -1;
        }
        #endregion

        #region 2583. Kth Largest Sum in a Binary Tree
        public long KthLargestLevelSum(TreeNode root, int k)
        {
            if (root == null) return 0;
            long result = 0;
            List<long> levelSum = new List<long>();
            Queue<(TreeNode node, int level)> queue = new Queue<(TreeNode node, int level)>();
            queue.Enqueue((root, 0));
            int lvl = 0;
            while (queue.Count > 0)
            {
                var dq = queue.Dequeue();

                levelSum.Add(dq.node.val);
                lvl = dq.level;
                if (dq.node.left != null) queue.Enqueue((dq.node.left, lvl + 1));
                if (dq.node.right != null) queue.Enqueue((dq.node.right, lvl + 1));
                while (queue.Count > 0 && queue.Peek().level == lvl)
                {
                    dq = queue.Dequeue();
                    levelSum[lvl] += dq.node.val;
                    if (dq.node.left != null) queue.Enqueue((dq.node.left, lvl + 1));
                    if (dq.node.right != null) queue.Enqueue((dq.node.right, lvl + 1));
                }

            }

            if (levelSum.Count < k) { return -1; }

            PriorityQueue<long, long> pq = new PriorityQueue<long, long>(k);

            foreach (var item in levelSum)
            {
                if (pq.Count < k)
                {
                    pq.Enqueue(item, item);
                }
                else
                {
                    pq.EnqueueDequeue(item, item);
                }
            }

            return pq.Count < k ? -1 : pq.Dequeue();
        }
        #endregion

        #region 2593. Find Score of an Array After Marking All Elements

        public long FindScore(int[] nums)
        {
            long result = 0;

            List<(int value, int index)> lst = new List<(int value, int index)>();

            for (int i = 0; i < nums.Length; i++)
            {
                lst.Add((nums[i], i));
            }
            //
            lst.Sort((x, y) =>
            {
                int c = x.value.CompareTo(y.value);
                return c == 0 ? x.index.CompareTo(y.index) : c;
            });


            foreach (var item in lst)
            {
                if (nums[item.index] == -1) continue;

                result += item.value;
                nums[item.index] = -1;
                if (item.index + 1 < nums.Length) nums[item.index + 1] = -1;
                if (item.index - 1 >= 0) nums[item.index - 1] = -1;
            }

            return result;
        }

        public long FindScore_3(int[] nums)
        {
            long result = 0;

            List<(int value, int index)> lst = new List<(int value, int index)>();

            for (int i = 0; i < nums.Length; i++)
            {
                lst.Add((nums[i], i));
            }
            //
            lst.Sort((x, y) =>
            {
                int c = x.value.CompareTo(y.value);
                return c == 0 ? x.index.CompareTo(y.index) : c;
            });

            HashSet<int> marked = new HashSet<int>();
            foreach (var item in lst)
            {
                if (marked.Contains(item.index)) continue;

                result += item.value;

                marked.Add(item.index);
                marked.Add(item.index + 1);
                marked.Add(item.index - 1);
            }

            return result;
        }


        public long FindScore_2(int[] nums)
        {
            long result = 0;

            List<(int value, int index)> lst = new List<(int value, int index)>();

            for (int i = 0; i < nums.Length; i++)
            {
                lst.Add((nums[i], i));
            }

            var l = lst.OrderBy(x => x.value).ThenBy(x => x.index);

            HashSet<int> marked = new HashSet<int>();
            foreach (var item in l)
            {
                if (marked.Contains(item.index)) continue;

                result += item.value;

                marked.Add(item.index);
                marked.Add(item.index + 1);
                marked.Add(item.index - 1);
            }

            return result;
        }

        public long FindScore_1(int[] nums)
        {
            long result = 0;
            HashSet<int> marked = new HashSet<int>();
            PriorityQueue<int, (int, int)> pq = new PriorityQueue<int, (int, int)>();
            for (int i = 0; i < nums.Length; i++)
            {
                pq.Enqueue(i, (nums[i], i));
            }

            while (pq.Count > 0)
            {
                int dq = pq.Dequeue();
                if (!marked.Contains(dq))
                {
                    result += nums[dq];

                    marked.Add(dq);
                    marked.Add(dq - 1);
                    marked.Add(dq + 1);
                }
            }
            return result;
        }
        #endregion

        #region 2601. Prime Subtraction Operation
        public bool PrimeSubOperation(int[] nums)
        {
            if (nums.Length == 1) return true;
            bool sortRequired = false;
            int max = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] <= nums[i - 1])
                {
                    sortRequired = true;
                }
                else
                {
                    max = Math.Max(max, nums[i]);
                }

            }
            if (sortRequired)
            {
                List<int> primeNumbers = getPrimeNumbers(max);
                int primeNumberToSubstract = getPrimeNumberToSubstract(primeNumbers, nums[0] - 1);

                nums[0] = nums[0] - primeNumberToSubstract;

                for (int i = 1; i < nums.Length - 1; i++)
                {
                    if (nums[i] <= nums[i - 1]) return false;
                    primeNumberToSubstract = getPrimeNumberToSubstract(primeNumbers, nums[i] - nums[i - 1] - 1);
                    nums[i] = nums[i] - primeNumberToSubstract;
                }
            }

            return nums[nums.Length - 1] > nums[nums.Length - 2];
        }

        private int getPrimeNumberToSubstract(List<int> primeNumbers, int v)
        {
            int k = primeNumbers.BinarySearch(v);

            if (k < 0)
            {
                k = ~k - 1;

                if (k < 0) return 0;

            }

            return primeNumbers[k];
        }

        private List<int> getPrimeNumbers(int max)
        {
            List<int> primeNumbers = new List<int>() { 2, 3, 5 };
            for (int i = 7; i <= max; i += 2)
            {
                bool isPrime = true;
                for (int j = 3; j <= Math.Sqrt(i); j += 2)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    primeNumbers.Add(i);
                }
            }

            return primeNumbers;
        }


        #endregion

        #region 2641. Cousins in Binary Tree II
        public TreeNode ReplaceValueInTree(TreeNode root)
        {
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
            int currentLevelSum = root.val;
            while (q.Count > 0)
            {
                int nextLeveSum = 0;
                int qLen = q.Count;
                for (int i = 0; i < qLen; i++)
                {
                    int siblingSum = 0;
                    var dq = q.Dequeue();
                    dq.val = currentLevelSum - dq.val;

                    if (dq.right != null)
                    {
                        nextLeveSum += dq.right.val;
                        siblingSum += dq.right.val;
                    }

                    if (dq.left != null)
                    {
                        nextLeveSum += dq.left.val;
                        siblingSum += dq.left.val;
                    }

                    if (dq.right != null) { dq.right.val = siblingSum; q.Enqueue(dq.right); }
                    if (dq.left != null) { dq.left.val = siblingSum; q.Enqueue(dq.left); }
                }


                currentLevelSum = nextLeveSum;
            }

            return root;
        }
        #endregion

        #region 2684. Maximum Number of Moves in a Grid
        public int MaxMoves(int[][] grid)
        {
            int result = 0;
            int[][] dp = new int[grid.Length][];
            for (int i = 0; i < grid.Length; i++)
            {
                dp[i] = new int[grid[i].Length];
            }


            for (int j = 1; j < grid[0].Length; j++)
            {
                bool found = false;
                for (int i = 0; i < grid.Length; i++)
                {
                    int max = 0;
                    if (grid[i][j] < grid[i][j - 1] && dp[i][j - 1] > 0)
                    {
                        max = dp[i][j - 1];
                    }

                    if (i - 1 >= 0 && grid[i][j] < grid[i - 1][j - 1])
                    {
                        found = true;
                        max = Math.Max(dp[i - 1][j - 1], max);
                    }

                    if (i + 1 < grid.Length && grid[i][j] < grid[i + 1][j + 1])
                    {
                        found = true;
                        max = Math.Max(dp[i + 1][j + 1], max);
                    }
                }
                if (!found) return j - 1;
            }

            //for (int size = 0; size < grid1.Length; size++)
            //{
            //    if (grid1[size][0] < grid1[size][1])
            //    {
            //        ub = Math.Max(ub, dp_416[size][1] + 1);
            //    }

            //    if (size - 1 >= 0 && grid1[size][0] < grid1[size - 1][1])
            //    {
            //        ub = Math.Max(ub, dp_416[size - 1][1] + 1);
            //    }

            //    if (size + 1 < grid1.Length && grid1[size][0] < grid1[size + 1][1])
            //    {
            //        ub = Math.Max(ub, dp_416[size + 1][1] + 1);
            //    }
            //}
            return 0;
        }
        #endregion

        #region 2762. Continuous Subarrays
        public long ContinuousSubarrays(int[] nums)
        {
            int left = 0;
            long result = 0;
            int maxVal = nums[0];
            int minVal = nums[0];

            for (int right = 0; right < nums.Length; right++)
            {
                // Update the max and min values in the current window
                maxVal = Math.Max(maxVal, nums[right]);
                minVal = Math.Min(minVal, nums[right]);

                // Shrink the window if the condition is violated
                while (maxVal - minVal > 2)
                {
                    left++;
                    maxVal = int.MinValue;
                    minVal = int.MaxValue;
                    for (int i = left; i <= right; i++)
                    {
                        maxVal = Math.Max(maxVal, nums[i]);
                        minVal = Math.Min(minVal, nums[i]);
                    }
                }

                // Add the count of subarrays ending at `right`
                result += (right - left + 1);
            }

            return result;
        }

        #endregion

        #region 2779. Maximum Beauty of an Array After Applying Operation
        public int MaximumBeauty(int[] nums, int k)
        {
            if (nums.Length == 1) return 1;

            Array.Sort(nums);
            int maxBeauty = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                int ub = getUpperBound(nums, nums[i] + 2 * k);
                maxBeauty = Math.Max(maxBeauty, ub - i + 1);
            }

            return maxBeauty;
        }

        private int getUpperBound(int[] nums, int val)
        {
            int low = 0, high = nums.Length - 1, ub = 0;
            while (low <= high)
            {
                int mid = (low + high) / 2;

                if (nums[mid] <= val)
                {
                    ub = mid;
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }
            return ub;
        }

        public int MaximumBeauty2(int[] nums, int k)
        {
            if (nums.Length == 1) return 1;
            Array.Sort(nums);
            int maxBeauty = 0;
            int right = 0;
            for (int left = 0; left < nums.Length; left++)
            {
                while (right < nums.Length && nums[right] - nums[left] <= 2 * k)
                {
                    right++;
                }

                maxBeauty = Math.Max(maxBeauty, right - left);
            }

            return maxBeauty;
        }
        public int MaximumBeauty_1(int[] nums, int k)
        {
            if (nums.Length == 1) return 1;

            int maxBeauty = 0;
            int maxValue = 0;

            foreach (var n in nums)
            {
                if (n > maxValue) maxValue = n;
            }

            int[] counterArray = new int[maxValue + 1];

            foreach (var n in nums)
            {
                counterArray[Math.Max(n - k, 0)]++;
                counterArray[Math.Min(n + k + 1, maxValue)]--;
            }

            int currSum = 0;

            foreach (var count in counterArray)
            {
                currSum += count;
                maxBeauty = Math.Max(maxBeauty, currSum);
            }

            return maxBeauty;
        }
        #endregion

        #region 2824. Count Pairs Whose Sum is Less than Target
        public int CountPairs(IList<int> nums, int target)
        {
            int count = 0;

            nums = nums.OrderBy(x => x).ToList();
            int low = 0;
            int high = nums.Count - 1;

            while (low < high)
            {
                if (nums[low] + nums[high] < target)
                {
                    count += high - low;
                    low++;
                }
                else
                {
                    high++;
                }
            }
            return count;
        }
        public int CountPairs1(IList<int> nums, int target)
        {
            int count = 0;

            for (int i = 0; i < nums.Count - 1; i++)
            {
                for (int j = i + 1; j < nums.Count; j++)
                {
                    if (nums[i] + nums[j] < target) count++;
                }
            }

            return count;
        }
        #endregion

        #region 2825. Make String a Subsequence Using Cyclic Increments
        public bool CanMakeSubsequence(string str1, string str2)
        {
            int s1Index = 0, s2Index = 0;

            while (s1Index < str1.Length && s2Index < str2.Length && str1.Length - s1Index >= str2.Length - s2Index)
            {
                if (str1[s1Index] == str2[s2Index] || str1[s1Index] + 1 == str2[s2Index] || (str1[s1Index] == 'z' && str2[s2Index] == 'a'))
                {
                    s1Index++;
                    s2Index++;
                }
                else
                {
                    s1Index++;
                }
            }

            return s2Index == str2.Length;
        }
        #endregion

        #region 2914. Minimum Number of Changes to Make Binary String Beautiful
        public int MinChanges(string s)
        {

            int i = -1;
            bool zeroCount = false;
            bool oneCount = false;
            bool check = false;
            int result = 0;
            while (++i < s.Length)
            {
                if (s[i] == '0')
                {
                    zeroCount = true;
                }
                else
                {
                    oneCount = true;
                }
                if (check)
                {
                    if (zeroCount && oneCount)
                    {
                        result++;
                    }
                    zeroCount = false;
                    oneCount = false;
                }

                check = !check;
            }

            return result;
        }
        #endregion

        #region 2924. Find Champion II
        public int FindChampion(int n, int[][] edges)
        {
            HashSet<int> result = new HashSet<int>();
            for (int i = 0; i < n; i++)
            {
                result.Add(i);
            }

            foreach (var edge in edges)
            {
                result.Remove(edge[1]);
            }

            if (result.Count != 1) return -1;

            return result.First();
        }
        #endregion

        #region 2938. Separate Black and White Balls
        public long MinimumSteps(string s)
        {
            s = s.TrimStart('0');
            s = s.TrimEnd('1');

            long result = 0;

            if (s.Length > 0)
            {

            }

            return result;
        }
        #endregion

        #region 3011. Find if Array Can Be Sorted
        public bool CanSortArray(int[] nums)
        {
            Dictionary<int, List<int>> bitMap = new Dictionary<int, List<int>>();
            bool alreadySorted = true;
            int prevMin = 0;
            foreach (int num in nums)
            {
                if (alreadySorted && prevMin > num)
                {
                    alreadySorted = false;
                }
                prevMin = num;
                int bitCount = countSetBits(num);
                if (!bitMap.ContainsKey(bitCount))
                {
                    bitMap.Add(bitCount, new List<int>());
                }
                bitMap[bitCount].Add(num);
            }

            if (!alreadySorted)
            {
                prevMin = 0;
                foreach (var key in bitMap.Keys)
                {
                    bitMap[key].Sort();

                    foreach (var val in bitMap[key])
                    {
                        if (val < prevMin) return false;

                        prevMin = val;

                    }
                }


            }
            return true;
        }

        private int countSetBits(int num)
        {
            int count = 0;

            while (num > 0)
            {
                count += num & 1;
                num >>= 1;
            }
            return count;
        }
        #endregion

        #region 3097. Shortest Subarray With OR at Least K II
        public int MinimumSubarrayLength(int[] nums, int k)
        {
            int result = int.MaxValue;

            int sum = nums[0];
            if (sum >= k) return 1;
            int[] bitCounts = new int[32];
            addBitCounts(bitCounts, nums[0]);
            int startIndex = 0;
            int endIndex = 1;

            while (endIndex < nums.Length)
            {
                if (nums[endIndex] >= k) { return 1; }

                addBitCounts(bitCounts, nums[endIndex]);
                sum = getNumber(bitCounts);

                while (sum >= k)
                {
                    result = Math.Min(result, endIndex - startIndex + 1);

                    deleteBitCounts(bitCounts, nums[startIndex++]);
                    sum = getNumber(bitCounts);
                }
                endIndex++;
            }

            return result == int.MaxValue ? -1 : result;
        }

        private void addBitCounts(int[] bitCounts, int number)
        {
            for (int i = 0; i < 32; i++)
            {
                if ((number & (1 << i)) > 0)
                {
                    bitCounts[i]++;
                }
            }
        }

        private void deleteBitCounts(int[] bitCounts, int number)
        {
            for (int i = 0; i < 32; i++)
            {
                if (((int)number & (1 << i)) > 0)
                {
                    bitCounts[i]--;
                }
            }
        }

        private int getNumber(int[] bitCounts)
        {
            int n = 0;
            int pow = 1;

            for (int i = 0; i < 32; i++)
            {
                if (bitCounts[i] > 0)
                {
                    n += pow;
                }
                pow *= 2;
            }
            return n;
        }
        #endregion

        #region 3133. Minimum Array End
        public long MinEnd(int n, int x)
        {

            bool[] bitMask = new bool[64];
            int index = 64;
            while (x > 0)
            {
                index--;
                if (x % 2 == 1)
                {
                    bitMask[index] = true;
                }

                x /= 2;
            }

            int k = n - 1;
            index = 63;
            long result = 0;
            while (index >= 0)
            {
                if (!bitMask[index])
                {
                    bitMask[index] = k % 2 == 1;
                    k /= 2;
                }
                if (bitMask[index])
                {
                    result = (long)(result) | (long)((long)1 << (63 - index));
                }
                index--;
            }

            return result;
        }
        public long MinEnd_1(int n, int x)
        {
            long result = 0;
            bool[] bitMask = new bool[64];
            string binaryNumber = Convert.ToString(x, 2);

            int len = binaryNumber.Length;
            int i = 64;
            while (--len >= 0)
            {
                bitMask[--i] = binaryNumber[len] == '1';
            }

            string bitMaskNumber = Convert.ToString(n - 1, 2);

            len = bitMaskNumber.Length - 1;
            i = 64;
            StringBuilder stringBuilder = new StringBuilder();
            while (len >= 0 && --i >= 0)
            {
                if (!bitMask[i])
                {
                    bitMask[i] = bitMaskNumber[len--] == '1';
                }

                stringBuilder.Insert(0, bitMask[i] ? '1' : '0');
            }



            return Convert.ToInt64(stringBuilder.ToString(), 2);
        }
        #endregion

        #region 3152. Special Array II
        public bool[] IsArraySpecial(int[] nums, int[][] queries)
        {
            int n = nums.Length;
            int[] transitions = new int[n - 1];

            // Compute the transitions array
            for (int i = 0; i < n - 1; i++)
            {
                if (nums[i] % 2 != nums[i + 1] % 2)
                {
                    transitions[i] = 1;
                }
                else
                {
                    transitions[i] = 0;
                }
            }

            // Compute the prefix sum of transitions
            int[] prefix = new int[n];
            for (int i = 1; i < n; i++)
            {
                prefix[i] = prefix[i - 1] + transitions[i - 1];
            }

            // Evaluate each query
            bool[] result = new bool[queries.Length];

            for (int i = 0; i < queries.Length; i++)
            {

                var query = queries[i];
                int from = query[0];
                int to = query[1];

                // Count the valid transitions in the range
                int count = prefix[to] - prefix[from];

                // The range is special if all transitions are valid
                result[i] = count == (to - from);
            }

            return result;

        }

        public bool[] IsArraySpecial_1(int[] nums, int[][] queries)
        {
            bool[] array = new bool[queries.Length];

            bool[][] dp = new bool[nums.Length][];
            for (int i = 0; i < dp.Length; i++)
            {
                dp[i] = new bool[dp.Length];
            }


            for (int i = 0; i < dp.Length; i++)
            {
                dp[i][i] = true;
                bool zeroFlag = nums[i] % 2 == 0;
                for (int j = i + 1; j < dp.Length; j++)
                {
                    if (zeroFlag == (nums[j] % 2 == 0))
                    {
                        break;
                    }
                    else
                    {
                        dp[i][j] = true;
                        zeroFlag = !zeroFlag;
                    }
                }
            }

            for (int i = 0; i < queries.Length; i++)
            {
                array[i] = dp[queries[i][0]][queries[i][1]];
            }

            return array;
        }
        #endregion

        #region 3163. String Compression III
        public string CompressedString(string word)
        {
            int i = 0;
            StringBuilder stringBuilder = new StringBuilder();
            while (i < word.Length)
            {
                char currentChar = word[i];
                int count = 1;

                while (++i < word.Length && count <= 9 && currentChar == word[i])
                {
                    count++;
                }

                stringBuilder.Append(count);
                stringBuilder.Append(currentChar);
            }

            return stringBuilder.ToString();
        }
        #endregion

        #region 3243. Shortest Distance After Road Addition Queries I
        public int[] ShortestDistanceAfterQueries(int n, int[][] queries)
        {
            int[] result = new int[queries.Length];
            Dictionary<int, HashSet<int>> map = new Dictionary<int, HashSet<int>>();
            int[] dp = new int[n];
            for (int i = 0; i < n - 1; i++)
            {
                dp[i] = i;
                map[i] = new HashSet<int>() { i + 1 };
            }

            dp[n - 1] = n - 1;
            map[n - 1] = new HashSet<int>();

            Queue<(int dest, int weight)> q = new Queue<(int dest, int weight)>();

            for (int j = 0; j < queries.Length; j++)
            {
                q.Clear();
                var query = queries[j];

                int source = query[0];
                int destination = query[1];

                map[source].Add(destination);
                int currWeight = dp[source];


                q.Enqueue((source, currWeight));

                while (q.Count > 0)
                {
                    var dq = q.Dequeue();

                    foreach (var item in map[dq.dest])
                    {
                        if (dp[item] > dq.weight + 1)
                        {
                            dp[item] = dq.weight + 1;
                            q.Enqueue((item, dp[item]));
                        }
                    }
                }


                result[j] = dp[n - 1];
            }

            return result;
        }
        #endregion

        #region 3254. Find the Power of K-Size Subarrays I
        public int[] ResultsArray(int[] nums, int k)
        {
            int[] results = new int[nums.Length - k + 1];
            int[] greater = new int[nums.Length];

            greater[0] = 1;

            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] == nums[i - 1] + 1) { greater[i] = greater[i - 1] + 1; }
                else
                {
                    greater[i] = 1;
                }
            }

            for (int i = k - 1; i < nums.Length; i++)
            {
                if (greater[i] >= k)
                {
                    results[i - k + 1] = nums[i];
                }
                else
                {
                    results[i - k + 1] = -1;
                }
            }

            return results;
        }
        #endregion

        #region 3264. Final Array State After K Multiplication Operations I
        public int[] GetFinalState(int[] nums, int k, int multiplier)
        {
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                pq.Enqueue(i, nums[i]);
            }

            while (k-- > 0)
            {
                var dq = pq.Dequeue();

                nums[dq] *= multiplier;

                pq.Enqueue(dq, nums[dq]);
            }
            return nums;
        }
        #endregion

        #region 3340. Check Balanced String
        public bool IsBalanced(string num)
        {
            int oddCount = 0, evenCount = 0;
            bool oddFlag = false;

            int i = -1;

            while (++i < num.Length)
            {
                if (oddFlag)
                {
                    oddCount += num[i] - '0';
                }
                else
                {
                    evenCount += num[i] - '0';
                }
                oddFlag = !oddFlag;
            }

            return oddCount == evenCount;
        }
        #endregion

        #region 3349. Adjacent Increasing Subarrays Detection I
        public bool HasIncreasingSubarrays(IList<int> nums, int k)
        {
            int max = -1;
            int currMax = 1;
            int lastMaxEndIndex = -1;
            for (int i = 1; i < nums.Count; i++)
            {
                if (nums[i] > nums[i - 1])
                {
                    currMax++;
                }
                else
                {
                    if (currMax >= k)
                    {
                        if (max >= k)
                        {
                            if (i - currMax == lastMaxEndIndex) return true;
                        }
                        max = currMax;
                        if (max >= k * 2) return true;
                        lastMaxEndIndex = i;

                    }
                    currMax = 1;
                }
            }

            return currMax >= k * 2 || (currMax >= k && max >= k && nums.Count - currMax == lastMaxEndIndex);
        }
        public bool HasIncreasingSubarrays1(IList<int> nums, int k)
        {
            int i = 0;
            while (i + k + k - 1 < nums.Count)
            {
                if (checkIncreasingSubArray(nums, i, k)) return true;

                i++;
            }
            return false;
        }

        private bool checkIncreasingSubArray(IList<int> nums, int i, int k)
        {
            int nextIndex = i + k;
            while (k-- > 1)
            {
                if (nums[nextIndex] >= nums[nextIndex + 1] || nums[i] >= nums[i + 1]) return false;
                nextIndex++;
                i++;
            }
            return true;
        }
        #endregion

        #region 3350. Adjacent Increasing Subarrays Detection II
        public int MaxIncreasingSubarrays(IList<int> nums)
        {
            int res = 1;
            int lastMaxIndex = 0;
            int currCount = 1;
            int lastMaxCount = 1; int i = 1;
            for (; i < nums.Count; i++)
            {
                if (nums[i] > nums[i - 1])
                {
                    currCount++;
                }
                else
                {
                    res = Math.Max(res, currCount / 2);
                    if (i - currCount - 1 == lastMaxIndex)
                    {
                        res = Math.Max(res, Math.Min(currCount, lastMaxCount));
                    }
                    lastMaxCount = currCount;
                    currCount = 1;
                    lastMaxIndex = i - 1;
                }
            }

            res = Math.Max(res, currCount / 2);
            if (i - currCount - 1 == lastMaxIndex)
            {
                res = Math.Max(res, Math.Min(currCount, lastMaxCount));
            }

            return res;
        }
        #endregion

        #region 3386. Button with Longest Push Time
        public int ButtonWithLongestTime(int[][] events)
        {
            int btnLongest = events[0][0];
            int longestTime = events[0][1];
            for (int i = 1; i < events.Length; i++)
            {
                int timeTakenToPress = events[i][1] - events[i - 1][1];
                if (timeTakenToPress > longestTime)
                {
                    longestTime = timeTakenToPress;
                    btnLongest = events[i][0];
                }
                else if(timeTakenToPress == longestTime && btnLongest > events[i][0])
                {
                    btnLongest = events[i][0];
                }
            }
            return btnLongest;
        }

        public int ButtonWithLongestTime1(int[][] events)
        {
            int btnLongest = events[0][0];
            int longestTime = events[0][1];
            Dictionary<int, int> map = new Dictionary<int, int>();
            map.Add(btnLongest, 0);
            int prevTime = longestTime;
            for (int i = 1; i < events.Length; i++)
            {
                int timeTakenToPress = events[i][1] - prevTime;
                if (timeTakenToPress == longestTime)
                {
                    if (map.ContainsKey(events[i][0]) && map[events[i][0]] < map[btnLongest])
                    {
                        btnLongest = events[i][0];
                    }
                }
                else if (timeTakenToPress > longestTime)
                {
                    btnLongest = events[i][0];
                }
                if (!map.ContainsKey(events[i][0]))
                {
                    map.Add(events[i][0], i);
                }
                prevTime += timeTakenToPress;
            }



            return btnLongest;
        }
        #endregion

        #region MockTests
        public int FindPeakElement(int[] nums)
        {
            if (nums.Length == 1) return 0;

            int low = 0;
            int high = nums.Length - 1;
            if (nums[low] > nums[1]) return low;
            if (nums[high] > nums[high - 1]) return high;

            while (low < high)
            {
                int mid = (low + high) / 2;

                if (nums[mid] > nums[mid + 1] && nums[mid] > nums[mid - 1]) return mid;
                if (mid == low) return high;
                if (mid == high) return low;
                if (nums[mid - 1] > nums[mid + 1])
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }

            return low;

        }

        public bool RepeatedSubstringPattern(string s)
        {
            int[] visited = new int[26];
            int i = -1;
            StringBuilder stringBuilder = new StringBuilder();
            while (++i < s.Length)
            {
                char c = s[i];

                if (visited[c - 'a'] == 0)
                {
                    visited[c - 'a'] = 1;
                    stringBuilder.Append(c);
                }
                else
                {
                    if (checkRepeatedPattern(s, stringBuilder.ToString()))
                    {
                        return true;
                    }
                    else
                    {
                        visited[c - 'a']++;
                        stringBuilder.Append(c);
                    }
                }
            }
            return false;
        }

        private bool checkRepeatedPattern(string s, string v)
        {
            s = s.Replace(v, "");

            return s.Length == 0;
        }
        #endregion

        #region PrimeNumbers And Factors
        public int GetPrimeNumbers(int limit)
        {

            bool[] bools = new bool[limit + 1];
            Array.Fill(bools, true);


            for (int i = 2; i * i <= limit; i++)
            {
                if (bools[i])
                {
                    for (int j = i * i; j <= limit; j = j + i)
                    {
                        bools[j] = false;
                    }
                }
            }


            return bools.Count(x => x) - 2;
        }
        public int GetPrimeNumbers_1(int limit)
        {

            bool[] bools = new bool[limit + 1];
            Array.Fill(bools, true);


            for (int i = 2; i * i <= limit; i++)
            {
                if (bools[i])
                {
                    for (int j = i * 2; j <= limit; j = j + i)
                    {
                        bools[j] = false;
                    }
                }
            }


            return bools.Count(x => x) - 2;
        }

        public List<int> GetFactors(int limit)
        {
            List<int> list = new List<int>();
            int i = 2;
            for (; i * i < limit; i++)
            {
                if (limit % i == 0)
                {
                    list.Add(i);
                    list.Add(limit / i);
                }
            }

            if (limit % i == 0) list.Add(i);

            return list;
        }
        #endregion

        #region LinkedList Problems

        #region 2. Add Two Numbers
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode dummy = new ListNode();

            int sum = l1.val + l2.val;

            bool carry = sum > 9;


            dummy.next = new ListNode(sum % 10);

            ListNode temp = dummy.next;

            while (l1.next != null && l2.next != null)
            {
                l1 = l1.next;
                l2 = l2.next;

                sum = l1.val + l2.val;

                if (carry)
                {
                    sum++;
                }

                carry = sum > 9;

                temp.next = new ListNode(sum % 10);
                temp = temp.next;
            }

            updateLinkNode(ref l1, ref carry, ref temp);
            updateLinkNode(ref l2, ref carry, ref temp);

            if (carry)
            {
                temp.next = new ListNode(1);
            }

            return dummy.next;
        }

        private static void updateLinkNode(ref ListNode l1, ref bool carry, ref ListNode temp)
        {
            while (l1.next != null)
            {
                l1 = l1.next;
                int sum = l1.val;

                if (carry)
                {
                    sum++;
                }

                carry = sum > 9;
                temp.next = new ListNode(sum % 10);
                temp = temp.next;
            }

        }
        #endregion

        #region 21. Merge Two Sorted Lists
        public ListNode MergeTwoLists(ListNode list1, ListNode list2)
        {
            if (list1 == null && list2 == null) return null;

            if (list1 == null) return list2;
            if (list2 == null) return list1;


            if (list1.val < list2.val)
            {
                return new ListNode(list1.val, MergeTwoLists(list1.next, list2));
            }
            return new ListNode(list2.val, MergeTwoLists(list1, list2.next));
        }
        #endregion

        #region 19. Remove Nth Node From End of List
        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            ListNode dummy = new ListNode(-1, head);

            ListNode fast = dummy.next;

            int i = 0;
            while (++i <= n)
            {
                fast = fast.next;
            }
            if (fast == null) return head.next;
            ListNode slow = dummy.next;
            while (fast.next != null)
            {
                slow = slow.next;
                fast = fast.next;
            }

            slow.next = slow.next.next;

            return dummy.next;
        }
        #endregion

        #region  61. Rotate List
        public ListNode RotateRight(ListNode head, int k)
        {
            if (head == null) return null;

            ListNode dummy = new ListNode(-1, head);

            ListNode fast = dummy.next;

            int size = 0;
            while (size < k && fast != null)
            {
                fast = fast.next;
                size++;
            }

            if (size < k)
            {
                k %= size;

                size = 0;
                fast = dummy.next;
                while (size < k && fast != null)
                {
                    fast = fast.next;
                    size++;
                }
            }

            if (fast != null)
            {
                ListNode slow = dummy.next;

                while (fast.next != null)
                {
                    fast = fast.next;
                    slow = slow.next;
                }

                //slow.next = null;
                fast.next = dummy.next;

                ListNode res = slow.next;

                slow.next = null;
                return res;
            }

            return dummy.next;
        }
        #endregion

        #region 82. Remove Duplicates from Sorted List II
        public ListNode DeleteDuplicates(ListNode head)
        {
            if (head == null || head.next == null) return head;

            ListNode dummy = new ListNode(-1, head);
            ListNode temp = dummy.next;
            ListNode prev = dummy;
            while (temp != null)
            {
                if (temp.next != null && temp.val == temp.next.val)
                {
                    ListNode next = temp.next;
                    while (next != null && temp.val == next.val)
                    {
                        next = next.next;
                    }

                    prev.next = next;
                    //temp = prev.next;
                }
                else
                {
                    prev = temp;
                }
                temp = prev.next;
            }

            return dummy.next;
        }
        #endregion

        #region 92. Reverse Linked List II
        public ListNode ReverseBetween(ListNode head, int left, int right)
        {
            if (left == right) return head;

            ListNode dummy = new ListNode(-1, head);

            ListNode temp = dummy;
            int i = 0;

            while (i < left - 1)
            {

            }

            //for (int size = 0; size < length; size++)
            //{

            //}

            return dummy.next;
        }
        #endregion

        #region 138. Copy List with Random Pointer
        //public Node CopyRandomList(Node head)
        //{
        //    return head;
        //}

        //public class Node
        //{
        //    public int val;
        //    public Node next { get; set; }
        //    public Node random { get; set; }

        //    public Node(int _val)
        //    {
        //        this.val = val;
        //        next = null;
        //        random = null;
        //    }
        //}
        #endregion 

        #region 141. Linked List Cycle
        public bool HasCycle(ListNode head)
        {
            ListNode slow = head;
            ListNode fast = head;

            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;

                if (slow == fast) return true;
            }

            return false;
        }

        #endregion

        public ListNode BuildListNode(int[] nodes)
        {
            ListNode dummy = new ListNode(-1);

            if (nodes.Length > 0)
            {
                dummy.next = new ListNode(nodes[0]);

                ListNode temp = dummy.next;
                int i = 0;
                while (++i < nodes.Length)
                {
                    temp.next = new ListNode(nodes[i]);
                    temp = temp.next;
                }
            }
            return dummy.next;
        }

        public bool CompareListNode(ListNode l1, ListNode l2)
        {
            if (l1 == null && l2 == null) return true;
            if (l1 == null || l2 == null) return false;

            return l1.val == l2.val && CompareListNode(l1.next, l2.next);
        }
        #endregion

    }
}
