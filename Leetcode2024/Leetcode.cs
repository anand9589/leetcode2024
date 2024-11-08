using Leetcode2024.Common.Models;
using System.ComponentModel;
using System.Text;

namespace Leetcode2024
{
    public class LeetCode
    {
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
                // Update dp array from target down to num
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
            timePoints[i] is in the format "HH:MM".

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

        #region 641. Design Circular Deque
        /*
            Design your implementation of the circular double-ended queue (deque).

            Implement the MyCircularDeque_V1 class:

            MyCircularDeque_V1(int k) Initializes the deque with a maximum size of k.
            boolean insertFront() Adds an item at the front of Deque. Returns true if the operation is successful, or false otherwise.
            boolean insertLast() Adds an item at the rear of Deque. Returns true if the operation is successful, or false otherwise.
            boolean deleteFront() Deletes an item from the front of Deque. Returns true if the operation is successful, or false otherwise.
            boolean deleteLast() Deletes an item from the rear of Deque. Returns true if the operation is successful, or false otherwise.
            int getFront() Returns the front item from the Deque. Returns -1 if the deque is empty.
            int getRear() Returns the last item from Deque. Returns -1 if the deque is empty.
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

            1 <= k <= 1000
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
         * MyCircularDeque_V1 obj = new MyCircularDeque_V1(k);
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

        #region 796. Rotate String
        public bool RotateString(string s, string goal)
        {
            if (s == goal) return true;
            if (s.Length != goal.Length) return false;


            return (s + s).Contains(goal);
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

        //    for (int i = 0; i < expr.Length; i++)
        //    {
        //        char ch = expr[i];
        //        if (ch == '(') balance++;
        //        else if (ch == ')') balance--;
        //        else if (ch == ',' && balance == 0)
        //        {
        //            subExprs.Add(expr.Substring(start, i - start));
        //            start = i + 1;
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

        //    Stack<char> stack = new Stack<char>();

        //    int i = -1;
        //    char op = ' ';
        //    int testIOndex;
        //    while (++i < expression.Length)
        //    {
        //        char c = expression[i];

        //        switch (c)
        //        {
        //            case 't':
        //            case 'f':
        //                stack.Push(c);
        //                break;
        //            case '!':

        //                op = processString(expression.Substring(i + 2), out testIOndex);
        //                i = testIOndex;
        //                break;
        //            case '|':

        //                op = processString(expression.Substring(i + 2), out testIOndex);
        //                i = testIOndex;
        //                break;
        //            case '&':

        //                op = processString(expression.Substring(i + 2), out testIOndex);
        //                i = testIOndex;
        //                break;
        //            case '(':

        //                break;
        //            case ')':
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    return stack.Pop();
        //}


        //private char processOr(string expression, int startIndex, out int processedIndex)
        //{
        //    char res = 'f';
        //    int i = startIndex-1;

        //    while (++i < expression.Length)
        //    {
        //        char c = expression[i];
        //        switch (c)
        //        {
        //            case 't':
        //                res = 't';
        //                break;
        //            case '!':
        //                char not = processNot(expression, i + 2, out processedIndex);

        //                if (not == 't')
        //                {
        //                    res = 't';
        //                }

        //                break;
        //            case '|':
        //                char or = processOr(expression, i + 2, out processedIndex);

        //                if (or == 't')
        //                {
        //                    res = 't';
        //                }
        //                break;
        //            case '&':
        //                char and = processAnd(expression, i + 2, out processedIndex);

        //                if (and == 't')
        //                {
        //                    res = 't';
        //                }
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    processedIndex = i;
        //    return res;
        //}
        //private char processNot(string expression, int startIndex, out int processedIndex)
        //{
        //    char res = 'f';
        //    int i = startIndex;

        //    while (i < expression.Length)
        //    {
        //        char c = expression[i];
        //        switch (c)
        //        {
        //            case 't':
        //                res = 't';
        //                break;
        //            case 'i':

        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    processedIndex = i;
        //    return res;
        //}
        //private char processAnd(string expression, int startIndex, out int processedIndex)
        //{
        //    char res = 'f';
        //    int i = startIndex;

        //    while (i < expression.Length)
        //    {
        //        char c = expression[i];
        //        switch (c)
        //        {
        //            case 't':
        //                res = 't';
        //                break;
        //            case 'i':

        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    processedIndex = i;
        //    return res;
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

        #region 1371. Find the Longest Substring Containing Vowels in Even Counts

        /*
            Given the string words, return the size of the longest substring containing each vowel an even number of times. That is, 'a', 'e', 'i', 'o', and 'u' must appear an even number of times. 

            Example 1:

            Input: words = "eleetminicoworoep"
            Output: 13
            Explanation: The longest substring is "leetminicowor" which contains two each of the vowels: e, i and o and zero of the vowels: a and u.
            Example 2:

            Input: words = "leetcodeisgreat"
            Output: 5
            Explanation: The longest substring is "leetc" which contains two e'words.
            Example 3:

            Input: words = "bcbcbc"
            Output: 6
            Explanation: In this case, the given string "bcbcbc" is the longest because all vowels: a, e, i, o and u appear zero times.


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

            //for (int i = 0; i < grid.Length; i++)
            //{
            //    if (grid[i][0] < grid[i][1])
            //    {
            //        result = Math.Max(result, dp_416[i][1] + 1);
            //    }

            //    if (i - 1 >= 0 && grid[i][0] < grid[i - 1][1])
            //    {
            //        result = Math.Max(result, dp_416[i - 1][1] + 1);
            //    }

            //    if (i + 1 < grid.Length && grid[i][0] < grid[i + 1][1])
            //    {
            //        result = Math.Max(result, dp_416[i + 1][1] + 1);
            //    }
            //}
            return 0;
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

    }
}
