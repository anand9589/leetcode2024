using Leetcode2024.Common.Helper;
using Leetcode2024.Common.Models;
using System.Text;

namespace Leetcode2024
{
    public class Leetcode
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

        #region 1371. Find the Longest Substring Containing Vowels in Even Counts

        /*
            Given the string s, return the size of the longest substring containing each vowel an even number of times. That is, 'a', 'e', 'i', 'o', and 'u' must appear an even number of times. 

            Example 1:

            Input: s = "eleetminicoworoep"
            Output: 13
            Explanation: The longest substring is "leetminicowor" which contains two each of the vowels: e, i and o and zero of the vowels: a and u.
            Example 2:

            Input: s = "leetcodeisgreat"
            Output: 5
            Explanation: The longest substring is "leetc" which contains two e's.
            Example 3:

            Input: s = "bcbcbc"
            Output: 6
            Explanation: In this case, the given string "bcbcbc" is the longest because all vowels: a, e, i, o and u appear zero times.


            Constraints:

            1 <= s.length <= 5 x 10^5
            s contains only lowercase English letters.

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
    }
}
