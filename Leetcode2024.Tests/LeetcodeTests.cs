
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

            Assert.AreEqual(999999 - 2, res);
        }

        [Test]
        public void LongestDiverseStringTest1()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);

            var res1 = leetcode.LongestDiverseString(1, 1, 7);
            //Assert.AreEqual(999999 - 2, res);
        }

        [Test]
        public void CompareVersionTest()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);

            var res1 = leetcode.CompareVersion("1", "1.1");
            //Assert.AreEqual(999999 - 2, res);
        }


        [Test]
        public void FractionToDecimalTest1()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);

            var res1 = leetcode.FractionToDecimal(16, 4);
            Assert.AreEqual("4", res1);
        }

        [Test]
        public void FractionToDecimalTest2()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);

            var res1 = leetcode.FractionToDecimal(18, 4);
            Assert.AreEqual("4.5", res1);
        }

        [Test]
        public void FractionToDecimalTest3()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);

            var res1 = leetcode.FractionToDecimal(17, 6);
            Assert.AreEqual("2.8(3)", res1);
        }

        [Test]
        public void FractionToDecimalTest4()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);

            var res1 = leetcode.FractionToDecimal(-50, 8);
            Assert.AreEqual("-6.25", res1);
        }

        [Test]
        public void FractionToDecimalTest5()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);

            var res1 = leetcode.FractionToDecimal(50, -8);
            Assert.AreEqual("-6.25", res1);
        }

        [Test]
        public void FractionToDecimalTest6()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);

            var res1 = leetcode.FractionToDecimal(-50, -8);
            Assert.AreEqual("6.25", res1);
        }

        [Test]
        public void FractionToDecimalTest7()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);

            var res1 = leetcode.FractionToDecimal(-1, -2147483648);
            Assert.AreEqual("0.0000000004656612873077392578125", res1);
        }

        [Test]
        public void MaximumSwapTes1()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);

            var res1 = leetcode.MaximumSwap(47483648);
            Assert.AreEqual(87483644, res1);
        }

        [Test]
        public void MaximumSwapTes2()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);

            var res1 = leetcode.MaximumSwap(98368);
            Assert.AreEqual(98863, res1);
        }

        [Test]
        public void LargestNumberTest1()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);
            int[] nums = new int[] { 3, 30, 34, 5, 9 };
            var res1 = leetcode.LargestNumber(nums);
            Assert.AreEqual("9534330", res1);
        }

        

        [Test]
        public void LargestNumberTest2()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);
            int[] nums = new int[] { 10, 2 };
            var res1 = leetcode.LargestNumber(nums);
            Assert.AreEqual("210", res1);
        }

        [Test]
        public void LargestNumberTest3()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);
            int[] nums = new int[] { 8308, 8308, 830 };
            var res1 = leetcode.LargestNumber(nums);
            Assert.AreEqual("83088308830", res1);
        }

        [Test]
        public void LargestNumberTest4()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);
            int[] nums = new int[] { 34323, 3432 };
            var res1 = leetcode.LargestNumber(nums);
            Assert.AreEqual("343234323", res1);
        }

        [Test]
        public void CountMaxOrSubsetsTest1()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);
            int[] nums = new int[] { 3,1 };
            var res1 = leetcode.CountMaxOrSubsets(nums);
            Assert.AreEqual(2, res1);
        }

        [Test]
        public void FindKthBitTest1()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);
            //int[] nums = new int[] { 3, 1 };
            var res1 = leetcode.FindKthBit(4,11);
            Assert.AreEqual('1', res1);
        }

        [Test]
        public void ParseBoolExprTest1()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);
            //int[] nums = new int[] { 3, 1 };
            var res1 = leetcode.ParseBoolExpr("&(|(f))");
            Assert.IsFalse(res1);
        }

        [Test]
        public void ParseBoolExprTest2()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);
            //int[] nums = new int[] { 3, 1 };
            var res1 = leetcode.ParseBoolExpr("|(f,f,f,t)");
            Assert.IsTrue(res1);
        }

        [Test]
        public void ParseBoolExprTest3()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);
            //int[] nums = new int[] { 3, 1 };
            var res1 = leetcode.ParseBoolExpr("!(&(f,t))");
            Assert.IsTrue(res1);
        }

        [Test]
        public void ParseBoolExprTest4()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);
            //int[] nums = new int[] { 3, 1 };
            var res1 = leetcode.ParseBoolExpr("|(f,f,f,!(&(t,t)),t,t)");
            Assert.IsTrue(res1);
        }
    }
}
