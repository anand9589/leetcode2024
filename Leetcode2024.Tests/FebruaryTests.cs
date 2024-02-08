using Leetcode2024.Common.Models;

namespace Leetcode2024.Tests
{
    internal class FebruaryTests
    {
        February february;
        [SetUp]
        public void Setup()
        {
             february = new February();
        }

        [Test]
        public void Test1()
        {
            int[] arr = new int[] {1, 2, 3, 4};

            ListNode listNode = new ListNode();
            ListNode temp = listNode;

            foreach (var item in arr)
            {
                temp.next = new ListNode(item);
                temp = temp.next;
            }

            february.ReorderList(listNode.next);
        }

        [Test]
        public void FirstUniqCharTest()
        {
            int res = february.FirstUniqChar("loveleetcode");

            Assert.AreEqual(2, res);
        }

        [Test]
        public void FrequencySortTest()
        {
            string res = february.FrequencySort("cccaaa");

            Assert.AreEqual(res, "eetr", "eert");
        }
    }
}
