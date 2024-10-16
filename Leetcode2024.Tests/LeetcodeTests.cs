
using static Leetcode2024.Leetcode;

namespace Leetcode2024.Tests
{
    internal class LeetcodeTests
    {
        Leetcode leetcode;
        [SetUp]
        public void Setup()
        {
            leetcode = new Leetcode();
        }

        [Test]
        public void FindTheLongestSubstringTest1()
        {
            int res = leetcode.FindTheLongestSubstring("eleetminicoworoep");
            Assert.AreEqual(13, res);
        }

        [Test]
        public void FindTheLongestSubstringTest2()
        {
            int res = leetcode.FindTheLongestSubstring("leetcodeisgreat");
            Assert.AreEqual(5, res);
        }

        [Test]
        public void FindTheLongestSubstringTest3()
        {
            int res = leetcode.FindTheLongestSubstring("bcbcbc");
            Assert.AreEqual(6, res);
        }
        [Test]
        public void FindMinDifferenceTest1()
        {
            int res = leetcode.getDiffTimeFirstLast("00:02", "23:58");
            Assert.AreEqual(4, res);
        }

        [Test]
        public void MyTestMethod()
        {
            int res = leetcode.FindMinDifference_1(new List<string>() { "01:01", "02:01", "03:00" });

            Assert.AreEqual(59, res);
        }

        [Test]
        public void MyTest241Method1()
        {
            //var res = leetcode.DiffWaysToCompute("2*3-4*5");

            //Assert.AreEqual(59, res);
            //MyCircularDeque myCircularDeque = new MyCircularDeque(4);

            //bool boolRes = myCircularDeque.InsertFront(9);

            //Assert.IsTrue(boolRes);

            //boolRes = myCircularDeque.DeleteLast();
            //Assert.IsTrue(boolRes);

            //MyCircularDeque myCircularDeque = new MyCircularDeque(3);

            //bool boolRes = myCircularDeque.InsertFront(9);

            //Assert.IsTrue(boolRes);

            //int intRes = myCircularDeque.GetRear();
            //Assert.That(intRes, Is.EqualTo(9));

        }


        [Test]
        public void MaximumGapTest1()
        {
            int res = leetcode.MaximumGap(new int[] {/*2, 999999*//*3, 6, 9, 1*/100, 3, 2, 1 });

            Assert.AreEqual(999999-2, res);
        }
    }
}
