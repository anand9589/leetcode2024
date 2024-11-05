namespace Leetcode2024.Tests
{
    internal class DynamicProgrammingLCTests
    {
        DynamicProgrammingLC dp;
        [SetUp]
        public void Setup()
        {
            dp = new DynamicProgrammingLC();
        }

        [Test]
        public void ClimbStairsTest1()
        {
            int res = dp.ClimbStairs(3);
            Assert.AreEqual(3, res);
        }

        [Test]
        public void DeleteAndEarnTest1()
        {
            int res = dp.DeleteAndEarn(new int[] { 3, 4, 2 });
            Assert.AreEqual(6, res);
        }

        [Test]
        public void DeleteAndEarnTest2()
        {
            int res = dp.DeleteAndEarn(new int[] { 2, 2, 3, 3, 3, 4 });
            Assert.AreEqual(9, res);
        }

        [Test]
        public void DeleteAndEarnTest3()
        {
            int res = dp.DeleteAndEarn(new int[] { 1, 1, 1, 2, 4, 5, 5, 5, 6 });
            Assert.AreEqual(18, res);
        }

        [Test]
        public void DeleteAndEarnTest4()
        {
            int res = dp.DeleteAndEarn(new int[] { 1, 6, 3, 3, 8, 4, 8, 10, 1, 3 });
            Assert.AreEqual(43, res);
        }

        [Test]
        public void MinimumTotalTest1()
        {
            IList<IList<int>> lst = new List<IList<int>>(){
                new List<int>() { 2 },
                new List<int>() { 3, 4},
                new List<int>() { 6, 5, 7},
                new List<int>() { 4, 1, 8, 3 }
            };
            List<int> list = new List<int>();
            int res = dp.MinimumTotal(lst);
            Assert.AreEqual(11, res);
        }


        //[[2,1,3],[6,5,4],[7,8,9]]
        [Test]
        public void MinFallingPathSumTest1()
        {
            int[][] matrix = new int[][] { new int[] { 2, 1, 3 }, new int[] { 6, 5, 4 }, new int[] { 7, 8, 9 } };
            int res = dp.MinFallingPathSum(matrix);
            Assert.AreEqual(13, res);
        }



        //[[2,1,3],[6,5,4],[7,8,9]]
        [Test]
        public void PanlindreomeTest1()
        {
            string res = dp.LongestPalindrome("racecar");
            Assert.AreEqual(res, res);
        }

        //[[2,1,3],[6,5,4],[7,8,9]]
        [Test]
        public void PanlindreomeTest2()
        {
            string res = dp.LongestPalindrome("aracecarb");
            Assert.AreEqual("racecar", res);
        }


        [Test]
        public void WordBreakTest1()
        {
            bool res = dp.WordBreak("leetcodeleetleet", new List<string> { "leet", "code" });
            Assert.IsTrue(res);
        }
    }
}
