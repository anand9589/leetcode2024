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
    }
}
