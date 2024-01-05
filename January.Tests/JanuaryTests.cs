namespace January.Tests
{
    public class JanuaryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void Test2()
        {
            int[] num = { 2, 3, 3, 2, 2, 4, 2, 3, 4 };
            January january = new January();
            int res = january.MinOperations(num);
            Assert.AreEqual(4, res);
        }
    }
}