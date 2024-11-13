
using Leetcode2024.Common.Models;
using System.Drawing;

namespace Leetcode2024.Tests
{
    internal class LeetcodeTests
    {
        //[Test]
        //public void Test()
        //{

        //}

        #region Setup
        LeetCode leetcode;
        [SetUp]
        public void Setup()
        {
            leetcode = new LeetCode();
        }
        #endregion

        #region 2563. Count the Number of Fair Pairs

        [Test]
        public void CountFairPairsTest1()
        {
            int[] arr = { 0, 1, 7, 4, 4, 5 };
            int lower = 3;
            int upper = 6;
            int expected = 6;
            var res = leetcode.CountFairPairs(arr, lower, upper);

            Assert.AreEqual(expected, res);
        }

        [Test]
        public void CountFairPairsTest2()
        {
            int[] arr = { 0, 0, 0, 0, 0, 0 };
            int lower = 0;
            int upper = 0;
            int expected = 15;
            var res = leetcode.CountFairPairs(arr, lower, upper);

            Assert.AreEqual(expected, res);
        }

        [Test]
        public void CountFairPairsTest3()
        {
            int[] arr = { 0, 0, 0, 0, 0, 0 };
            int lower = -1000000000;
            int upper = 1000000000;
            int expected = 15;
            var res = leetcode.CountFairPairs(arr, lower, upper);

            Assert.AreEqual(expected, res);
        }

        #endregion

        #region 75. Sort Colors

        [Test]
        public void SortColorsTest1()
        {
            int[] arr = { 2, 0, 2, 1, 1, 0 };
            int[] exp = { 2, 0, 2, 1, 1, 0 };
            Array.Sort(exp);
            leetcode.SortColors(arr);

            CollectionAssert.AreEqual(exp, arr);
        }

        [Test]
        public void SortColorsTest2()
        {
            int[] arr = { 2, 0, 1 };
            int[] exp = { 2, 0, 1 };
            Array.Sort(exp);
            leetcode.SortColors(arr);

            CollectionAssert.AreEqual(exp, arr);
        }

        #endregion

        #region 209. Minimum Size Subarray Sum

        [Test]
        public void MinimumSizeSubarraySumTest1()
        {
            int[] arr = { 12, 28, 83, 4, 25, 26, 25, 2, 25, 25, 25, 12 };
            var res = leetcode.MinSubArrayLen(213, arr);

            Assert.AreEqual(8, res);
        }

        [Test]
        public void MinimumSizeSubarraySumTest2()
        {
            int[] arr = { 2, 3, 1, 2, 4, 3 };
            var res = leetcode.MinSubArrayLen(7, arr);

            Assert.AreEqual(2, res);
        }

        #endregion

        #region coin changes

        [Test]
        public void CoinChangeTest1()
        {
            var res = leetcode.CoinChange(new int[] { 1, 2, 5 }, 11);
            Assert.AreEqual(3, res);
        }

        [Test]
        public void CoinChangeTest2()
        {
            var res = leetcode.CoinChange(new int[] { 2 }, 3);
            Assert.AreEqual(-1, res);
        }

        [Test]
        public void CoinChangeTest3()
        {
            var res = leetcode.CoinChange(new int[] { 1 }, 0);
            Assert.AreEqual(0, res);
        }
        #endregion

        #region 2070. Most Beautiful Item for Each Query

        [Test]
        public void MaximumBeautyTest1()
        {
            int[][] items = new int[][]
            {
                new int[] {1, 2 },
                new int[] { 3, 2},
                new int[] {2, 4},
                new int[] {5, 6},
                new int[] {3, 5 }
            };

            int[] queries = { 1, 2, 3, 4, 5, 6 };
            var res = leetcode.MaximumBeauty(items, queries);
            int[] expected = { 2, 4, 5, 5, 6, 6 };

            CollectionAssert.AreEqual(expected, res);
        }

        [Test]
        public void MaximumBeautyTest2()
        {
            int[][] items = new int[][]
            {
                new int[] {193,732},
                new int[] { 781,962},
                new int[] { 864, 954}, new int[] { 749, 627}, new int[] {136, 746}, new int[] {478, 548}, new int[] {640, 908}, new int[] {210, 799}, new int[] {567, 715}, new int[] {914, 388}, new int[] {487, 853}, new int[] {533, 554}, new int[] {247, 919}, new int[] {958, 150}, new int[] {193, 523}, new int[] {176, 656}, new int[] {395, 469}, new int[] {763, 821}, new int[] {542, 946}, new int[] {701, 676}
            };

            int[] queries = { 885, 1445, 1580, 1309, 205, 1788, 1214, 1404, 572, 1170, 989, 265, 153, 151, 1479, 1180, 875, 276, 1584 };
            var res = leetcode.MaximumBeauty(items, queries);
            int[] expected = { 962, 962, 962, 962, 746, 962, 962, 962, 946, 962, 962, 919, 746, 746, 962, 962, 962, 919, 962 };

            CollectionAssert.AreEqual(expected, res);
        }
        #endregion

        #region Prime Number Test

        [Test]
        public void PrimeNumberTest1()
        {
            var res = leetcode.GetPrimeNumbers(10000);
            Assert.AreEqual(1229, res);
        }


        [Test]
        public void FactorTest1()
        {
            var res = leetcode.GetFactors(10000);
        }
        [Test]
        public void FactorTest2()
        {
            var res = leetcode.GetFactors(625);
        }
        #endregion

        #region 2601. Prime Subtraction Operation
        [Test]
        public void PrimeSubOperationTest1()
        {
            int[] arr = { 4, 9, 6, 10 };
            var res = leetcode.PrimeSubOperation(arr);
            Assert.IsTrue(res);
        }
        [Test]
        public void PrimeSubOperationTest2()
        {
            int[] arr = { 6, 8, 11, 12 };
            var res = leetcode.PrimeSubOperation(arr);
            Assert.IsTrue(res);
        }
        [Test]
        public void PrimeSubOperationTest3()
        {
            int[] arr = { 5, 8, 3 };
            var res = leetcode.PrimeSubOperation(arr);
            Assert.IsFalse(res);
        }
        [Test]
        public void PrimeSubOperationTest4()
        {
            int[] arr = { 17, 20, 5, 15, 6 };
            var res = leetcode.PrimeSubOperation(arr);
            Assert.IsFalse(res);
        }
        [Test]
        public void PrimeSubOperationTest5()
        {
            int[] arr = { 6, 17, 2, 9, 20 };
            var res = leetcode.PrimeSubOperation(arr);
            Assert.IsFalse(res);
        }
        [Test]
        public void PrimeSubOperationTest6()
        {
            int[] arr = { 19, 10 };
            var res = leetcode.PrimeSubOperation(arr);
            Assert.IsTrue(res);
        }
        [Test]
        public void PrimeSubOperationTest7()
        {
            int[] arr = { 15, 20, 17, 7, 16 };
            var res = leetcode.PrimeSubOperation(arr);
            Assert.IsTrue(res);
        }
        [Test]
        public void PrimeSubOperationTest8()
        {
            int[] arr = { 3, 1, 6 };
            var res = leetcode.PrimeSubOperation(arr);
            Assert.IsFalse(res);
        }
        [Test]
        public void PrimeSubOperationTest9()
        {
            int[] arr = { 8, 19, 3, 4, 9 };
            var res = leetcode.PrimeSubOperation(arr);
            Assert.IsTrue(res);
        }
        #endregion

        #region 273. Integer to English Words
        [Test]
        public void NumberToWordsTest1()
        {
            //int num = 2,147,483,647; TWO BILLION ONE HUNDRED FORTY SEVEN MILLI0NS FOUR HUNDRED EIGHTY THREE SIX HUNDRED FOURTY SEVEN

            var k = leetcode.NumberToWords(int.MaxValue);
        }
        [Test]
        public void NumberToWordsTest2()
        {
            var k = leetcode.NumberToWords(123);
            Assert.AreEqual("One Hundred Twenty Three", k);
        }
        [Test]
        public void NumberToWordsTest3()
        {
            var k = leetcode.NumberToWords(12345);
            Assert.AreEqual("Twelve Thousand Three Hundred Forty Five", k);
        }
        [Test]
        public void NumberToWordsTest4()
        {
            var k = leetcode.NumberToWords(1234567);
            Assert.AreEqual("One Million Two Hundred Thirty Four Thousand Five Hundred Sixty Seven", k);
        }
        [Test]
        public void NumberToWordsTest5()
        {
            var k = leetcode.NumberToWords(1001);
            Assert.AreEqual("One Thousand One", k);
        }
        [Test]
        public void NumberToWordsTest6()
        {
            var k = leetcode.NumberToWords(1099);
            Assert.AreEqual("One Thousand Ninety Nine", k);
        }
        [Test]
        public void NumberToWordsTest7()
        {
            var k = leetcode.NumberToWords(1000010);
            Assert.AreEqual("One Million Ten", k);
        }
        #endregion

        #region 140. Word Break II Tests
        [Test]
        public void WordBreakTest1()
        {
            WordBreak_II_Solution solution = new WordBreak_II_Solution();
            var res = solution.WordBreak("catsanddog", new List<string> { "cat", "cats", "and", "sand", "dog" });

        }
        [Test]
        public void WordBreakTest2()
        {
            var res = leetcode.WordBreak("catsanddog", new List<string> { "cat", "cats", "and", "sand", "dog" });

        }
        #endregion

        #region 3097. Shortest Subarray With OR at Least K II
        [Test]
        public void MinimumSubarrayLengthTest1()
        {
            int[] arr = { 1, 2, 32, 21 };
            int k = 55;

            int expected = 3;
            int actual = leetcode.MinimumSubarrayLength(arr, k);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MinimumSubarrayLengthTest2()
        {
            int[] arr = { 36, 2, 12, 1 };
            int k = 46;

            int expected = 3;
            int actual = leetcode.MinimumSubarrayLength(arr, k);

            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region 2357. Make Array Zero by Subtracting Equal Amounts

        [Test]
        public void MinimumOperationsTest1()
        {
            int[] arr = { 1, 5, 0, 3, 5 };
            var res = leetcode.MinimumOperations(arr);
            Assert.AreEqual(3, res);
        }
        #endregion

        #region 3133. Minimum Array End
        [Test]
        public void MinEndTest1()
        {
            var res = leetcode.MinEnd(4, 5);
            Assert.AreEqual(15, res);
        }
        [Test]
        public void MinEndTest2()
        {
            var res = leetcode.MinEnd(3, 4);
            Assert.AreEqual(6, res);
        }
        [Test]
        public void MinEndTest3()
        {
            var res = leetcode.MinEnd(6715154, 7193485);
            Assert.AreEqual(55012476815, res);
        }
        #endregion

        #region FindTheLongestSubstringTest
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

        #endregion

        #region FindMinDifferenceTest
        [Test]
        public void FindMinDifferenceTest1()
        {
            int res = leetcode.getDiffTimeFirstLast("00:02", "23:58");
            Assert.AreEqual(4, res);
        }
        #endregion

        #region 241
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
        #endregion

        #region MaximumGapTest

        [Test]
        public void MaximumGapTest1()
        {
            int res = leetcode.MaximumGap(new int[] {/*2, 999999*//*3, 6, 9, 1*/100, 3, 2, 1 });

            Assert.AreEqual(999999 - 2, res);
        }
        #endregion

        #region FractionToDecimalTest
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
        #endregion

        #region MaximumSwapTest
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
        #endregion

        #region LargestNumberTest

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

        #endregion

        #region ParseBoolExprTest
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

        #endregion

        #region MajorityElementTest1

        [Test]
        public void MajorityElementTest1()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);
            //int[] nums = new int[] { 3, 1 };
            var res1 = leetcode.MajorityElement(new int[] { 5, 1, 5, 5, 5, 2, 1, 2, 1, 1, 1, 2, 5, 2, 2, 2 });

        }

        [Test]
        public void MajorityElementTest2()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);
            //int[] nums = new int[] { 3, 1 };
            var res1 = leetcode.MajorityElement(new int[] { 3, 2, 3 });

            CollectionAssert.AreEqual(new List<int>() { 3 }, res1);
        }

        [Test]
        public void MajorityElementTest3()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);
            //int[] nums = new int[] { 3, 1 };
            var res1 = leetcode.MajorityElement(new int[] { 1 });
            CollectionAssert.AreEqual(new List<int>() { 1 }, res1);

        }

        [Test]
        public void MajorityElementTest4()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);
            //int[] nums = new int[] { 3, 1 };
            var res1 = leetcode.MajorityElement(new int[] { 1, 2 });

            CollectionAssert.AreEqual(new List<int>() { 1, 2 }, res1);

        }

        [Test]
        public void MajorityElementTest5()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);
            //int[] nums = new int[] { 3, 1 };
            var res1 = leetcode.MajorityElement(new int[] { 1, 2, 3 });

            CollectionAssert.AreEqual(new List<int>() { }, res1);

        }

        [Test]
        public void MajorityElementTest6()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);
            //int[] nums = new int[] { 3, 1 };
            var res1 = leetcode.MajorityElement(new int[] { 2, 1, 1, 3, 1, 4, 5, 6 });

            CollectionAssert.AreEqual(new List<int>() { 1 }, res1);

        }

        #endregion

        #region KthSmallestTest
        [Test]
        public void KthSmallestTest1()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);
            //int[] nums = new int[] { 3, 1 };
            var res1 = leetcode.KthSmallest(new TreeNode(3, new TreeNode(1, null, new TreeNode(2)), new TreeNode(4)), 1);

            Assert.AreEqual(1, res1);
        }

        [Test]
        public void KthSmallestTest2()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);
            //int[] nums = new int[] { 3, 1 };
            //[5,3,6,2,4,null,null,1]
            TreeNode treeNode = new TreeNode(5,
                                    new TreeNode(3,
                                        new TreeNode(2,
                                            new TreeNode(1),
                                            null),
                                        new TreeNode(4)),
                                    new TreeNode(6));
            var res1 = leetcode.KthSmallest(treeNode, 3);

            Assert.AreEqual(3, res1);
        }

        #endregion

        #region SearchMatrixTest

        [Test]
        public void SearchMatrixTest1()
        {
            int[][] matrix = { new int[] { 1, 4, 7, 11, 15 }, new int[] { 2, 5, 8, 12, 19 }, new int[] { 3, 6, 9, 16, 22 }, new int[] { 10, 13, 14, 17, 24 }, new int[] { 18, 21, 23, 26, 30 } };

            var r = leetcode.SearchMatrix(matrix, 5);

            Assert.IsTrue(r);
        }

        [Test]
        public void SearchMatrixTest2()
        {
            int[][] matrix = { new int[] { 1, 4, 7, 11, 15 }, new int[] { 2, 5, 8, 12, 19 }, new int[] { 3, 6, 9, 16, 22 }, new int[] { 10, 13, 14, 17, 24 }, new int[] { 18, 21, 23, 26, 30 } };

            var r = leetcode.SearchMatrix(matrix, 6);

            Assert.IsTrue(r);
        }

        [Test]
        public void SearchMatrixTest3()
        {
            int[][] matrix = { new int[] { 1, 4, 7, 11, 15 }, new int[] { 2, 5, 8, 12, 19 }, new int[] { 3, 6, 9, 16, 22 }, new int[] { 10, 13, 14, 17, 24 }, new int[] { 18, 21, 23, 26, 30 } };

            var r = leetcode.SearchMatrix(matrix, 20);

            Assert.IsFalse(r);
        }

        [Test]
        public void SearchMatrixTest4()
        {
            int[][] matrix = { new int[] { 1, 4, 7, 11, 15 }, new int[] { 2, 5, 8, 12, 19 }, new int[] { 3, 6, 9, 16, 22 }, new int[] { 10, 13, 14, 17, 24 }, new int[] { 18, 21, 23, 26, 30 } };

            var r = leetcode.SearchMatrix(matrix, 12);

            Assert.IsTrue(r);
        }

        [Test]
        public void SearchMatrixTest5()
        {
            int[][] matrix = { new int[] { 1, 4, 7, 11, 15 }, new int[] { 2, 5, 8, 12, 19 }, new int[] { 3, 6, 9, 16, 22 }, new int[] { 10, 13, 14, 17, 24 }, new int[] { 18, 21, 23, 26, 30 } };

            int[] falseResult = { 0, 20, 25, 27, 28, 29, 31 };
            bool result = false;

            for (int i = 0; i < 32; i++)
            {
                result = leetcode.SearchMatrix(matrix, i);

                if (falseResult.Contains(i))
                {
                    if (result)
                    {

                    }
                    Assert.IsFalse(result);
                }
                else
                {
                    if (!result)
                    {

                    }
                    Assert.IsTrue(result);
                }
            }

        }
        [Test]
        public void SearchMatrixTest6()
        {
            int[][] matrix = { new int[] { -1, 3 } };

            var r = leetcode.SearchMatrix(matrix, 3);

            Assert.IsTrue(r);
        }

        [Test]
        public void SearchMatrixTest7()
        {
            int[][] matrix = { new int[] { 2, 5 }, new int[] { 2, 8 }, new int[] { 7, 9 }, new int[] { 7, 11 }, new int[] { 9, 11 } };

            var r = leetcode.SearchMatrix(matrix, 7);

            Assert.IsTrue(r);
        }

        #endregion

        #region RemoveSubfoldersTest1

        [Test]
        public void RemoveSubfoldersTest1()
        {
            string[] folder = { "/a", "/a/b", "/c/d", "/c/d/e", "/c/f" };
            var res = leetcode.RemoveSubfolders(folder);
            IList<string> expected = new List<string>() { "/a", "/c/d", "/c/f" };
            CollectionAssert.AreEqual(expected, res);
        }

        [Test]
        public void RemoveSubfoldersTest2()
        {
            string[] folder = { "/a", "/a/b/c", "/a/b/d" };

            var res = leetcode.RemoveSubfolders(folder);
            IList<string> expected = new List<string>() { "/a" };
            CollectionAssert.AreEqual(expected, res);

        }

        [Test]
        public void RemoveSubfoldersTest3()
        {
            string[] folder = { "/a/b/c", "/a/b/ca", "/a/b/d" };

            var res = leetcode.RemoveSubfolders(folder);
            CollectionAssert.AreEqual(folder, res);

        }


        [Test]
        public void RemoveSubfoldersTest4()
        {
            string[] folder = { "/ah/al/am", "/ah/al" };

            var res = leetcode.RemoveSubfolders(folder);
            IList<string> expected = new List<string>() { "/ah/al" };
            CollectionAssert.AreEqual(expected, res);

        }

        #endregion

        #region TreeQueriesTest

        [Test]
        public void TreeQueriesTest1()
        {
            TreeNode root = buildTree(new int?[] { 1, 3, 4, 2, null, 6, 5, null, null, null, null, null, 7 });
            int[] queries = { 4 };

            var res = leetcode.TreeQueries(root, queries);

            CollectionAssert.AreEqual(new int[] { 2 }, res);
        }

        //[5,8,9,2,1,3,7,4,6]
        [Test]
        public void TreeQueriesTest2()
        {
            TreeNode root = buildTree(new int?[] { 5, 8, 9, 2, 1, 3, 7, 4, 6 });
            int[] queries = { 3, 2, 4, 8 };

            var res = leetcode.TreeQueries(root, queries);

            CollectionAssert.AreEqual(new int[] { 3, 2, 3, 2 }, res);
        }


        //[8,6,37,3,7,33,65,1,4,null,null,29,36,51,66,null,2,null,5,26,31,35,null,45,58,null,null,null,null,null,null,22,28,30,32,34,null,44,47,55,59,21,25,27,null,null,null,null,null,null,null,41,null,46,48,54,56,null,62,13,null,24,null,null,null,38,42,null,null,null,49,53,null,null,57,60,64,11,20,23,null,null,39,null,43,null,50,52,null,null,null,null,61,63,null,10,12,18,null,null,null,null,40,null,null,null,null,null,null,null,null,null,null,9,null,null,null,16,19,null,null,null,null,15,17,null,null,14]
        [Test]
        public void TreeQueriesTest3()
        {
            TreeNode root = buildTree(new int?[] { 8, 6, 37, 3, 7, 33, 65, 1, 4, null, null, 29, 36, 51, 66, null, 2, null, 5, 26, 31, 35, null, 45, 58, null, null, null, null, null, null, 22, 28, 30, 32, 34, null, 44, 47, 55, 59, 21, 25, 27, null, null, null, null, null, null, null, 41, null, 46, 48, 54, 56, null, 62, 13, null, 24, null, null, null, 38, 42, null, null, null, 49, 53, null, null, 57, 60, 64, 11, 20, 23, null, null, 39, null, 43, null, 50, 52, null, null, null, null, 61, 63, null, 10, 12, 18, null, null, null, null, 40, null, null, null, null, null, null, null, null, null, null, 9, null, null, null, 16, 19, null, null, null, null, 15, 17, null, null, 14 });
            int[] queries = { 57, 7, 32, 55, 20, 25, 45, 34, 5, 19, 45, 30, 48, 1, 47, 32, 60, 31, 22, 15, 31 };

            var res = leetcode.TreeQueries(root, queries);

            CollectionAssert.AreEqual(new int[] { 12, 12, 12, 12, 10, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 9, 11, 12 }, res);
        }

        //[1,null,5,3,null,2,4]
        [Test]
        public void TreeQueriesTest4()
        {
            TreeNode root = buildTree(new int?[] { 1, null, 5, 3, null, 2, 4 });
            int[] queries = { 3, 5, 4, 2, 4 };

            var res = leetcode.TreeQueries(root, queries);

            CollectionAssert.AreEqual(new int[] { 1, 0, 3, 3, 3 }, res);
        }

        #endregion

        #region CountSquaresTest

        [Test]
        public void CountSquaresTest1()
        {
            int[][] matrix = { new int[] { 0, 1, 1, 1 }, new int[] { 1, 1, 1, 1 }, new int[] { 0, 1, 1, 1 } };

            var res = leetcode.CountSquares(matrix);

            Assert.AreEqual(15, res);
        }

        //[[1,0,1}, new int[] {1,1,0],[1,1,0]]
        [Test]
        public void CountSquaresTest2()
        {
            int[][] matrix = { new int[] { 1, 0, 1 }, new int[] { 1, 1, 0 }, new int[] { 1, 1, 0 } };

            var res = leetcode.CountSquares(matrix);

            Assert.AreEqual(7, res);
        }

        #endregion

        #region NthUglyNumberTest

        [Test]
        public void NthUglyNumberTest1()
        {
            var res = leetcode.NthUglyNumber(10);
            Assert.AreEqual(12, res);
        }

        [Test]
        public void NthUglyNumberTest2()
        {
            var res = leetcode.NthUglyNumber(11);
            Assert.AreEqual(15, res);
        }

        #endregion

        #region HIndex-I

        [Test]
        public void HIndexTest1()
        {
            int[] arr = { 3, 0, 6, 1, 5 };
            Array.Sort(arr);
            var res = leetcode.HIndex(arr);

            Assert.AreEqual(3, res);
        }

        [Test]
        public void HIndexTest2()
        {
            int[] arr = { 1, 3, 1 };
            Array.Sort(arr);
            var res = leetcode.HIndex(arr);

            Assert.AreEqual(1, res);
        }



        [Test]
        public void HIndexTest3()
        {
            int[] arr = { 1, 2, 3, 4 };
            var res = leetcode.HIndex(arr);

            Assert.AreEqual(2, res);
        }

        #endregion

        #region MaxMovesTest
        //grid = [[2,4,3,5],[5,4,9,3],[3,4,2,11],[10,9,13,15]]
        [Test]
        public void MaxMovesTest1()
        {
            int[][] grid = { new int[] { 2, 4, 3, 5 }, new int[] { 5, 4, 9, 3 }, new int[] { 3, 4, 2, 11 }, new int[] { 10, 9, 13, 15 } };
            var res = leetcode.MaxMoves(grid);

            Assert.AreEqual(3, res);
        }

        [Test]
        public void MaxMovesTest2()
        {
            //[[160,212,75,136,62,270,218,41,90,72,75],[223,24,6,157,59,99,107,14,244,266,249]]
            int[][] grid = { new int[] { 160, 212, 75, 136, 62, 270, 218, 41, 90, 72, 75 },
                            new int[] { 223, 24, 6, 157, 59, 99, 107, 14, 244, 266, 249 }};
            var res = leetcode.MaxMoves(grid);

            Assert.AreEqual(3, res);
        }
        #endregion

        #region UniqueLetterString
        [Test]
        public void UniqueLetterStringTest1()
        {
            var res = leetcode.UniqueLetterString("LEETCODE");
            Assert.AreEqual(92, res);
        }
        [Test]
        public void UniqueLetterStringTest2()
        {
            var res = leetcode.UniqueLetterString("ABC");
            Assert.AreEqual(10, res);
        }
        [Test]
        public void UniqueLetterStringTest3()
        {
            var res = leetcode.UniqueLetterString("ABA");
            Assert.AreEqual(8, res);
        }
        #endregion

        #region LengthOfLISTest
        [Test]
        public void LengthOfLISTest1()
        {
            //[[160,212,75,136,62,270,218,41,90,72,75],[223,24,6,157,59,99,107,14,244,266,249]]
            int[] arr = { 10, 9, 2, 5, 3, 7, 101, 18 };
            var res = leetcode.LengthOfLIS(arr);

            Assert.AreEqual(4, res);
        }


        [Test]
        public void LengthOfLISTest2()
        {
            //[[160,212,75,136,62,270,218,41,90,72,75],[223,24,6,157,59,99,107,14,244,266,249]]
            int[] arr = { 0, 1, 0, 3, 2, 3 };
            var res = leetcode.LengthOfLIS(arr);

            Assert.AreEqual(4, res);
        }

        #endregion

        #region MinimumMountainRemovalsTest

        [Test]
        public void MinimumMountainRemovalsTest1()
        {
            //[[160,212,75,136,62,270,218,41,90,72,75],[223,24,6,157,59,99,107,14,244,266,249]]
            int[] arr = { 1, 3, 1 };
            var res = leetcode.MinimumMountainRemovals(arr);

            Assert.AreEqual(0, res);
        }

        [Test]
        public void MinimumMountainRemovalsTest2()
        {
            //[[160,212,75,136,62,270,218,41,90,72,75],[223,24,6,157,59,99,107,14,244,266,249]]
            int[] arr = { 2, 1, 1, 5, 6, 2, 3, 1 };
            var res = leetcode.MinimumMountainRemovals(arr);

            Assert.AreEqual(3, res);
        }



        [Test]
        public void MinimumMountainRemovalsTest3()
        {
            //[[160,212,75,136,62,270,218,41,90,72,75],[223,24,6,157,59,99,107,14,244,266,249]]
            int[] arr = { 4, 3, 2, 1, 1, 2, 3, 1 };
            var res = leetcode.MinimumMountainRemovals(arr);

            Assert.AreEqual(4, res);
        }

        #endregion

        #region FindPeakElementTest
        [Test]
        public void FindPeakElementTest1()
        {
            int[] arr = new int[] { 1, 6, 5, 4, 3, 2, 1 };
            var res = leetcode.FindPeakElement(arr);

            Assert.AreEqual(1, res);
        }
        [Test]
        public void FindPeakElementTest2()
        {
            int[] arr = new int[] { 1, 2, 3, 1 };
            var res = leetcode.FindPeakElement(arr);

            Assert.AreEqual(2, res);
        }
        [Test]
        public void FindPeakElementTest3()
        {
            int[] arr = new int[] { 1, 2, 1, 3, 5, 6, 4 };
            var res = leetcode.FindPeakElement(arr);

            Assert.AreEqual(5, res);
        }
        [Test]
        public void FindPeakElementTest4()
        {
            int[] arr = new int[] { 3, 4, 3, 2, 1 };
            var res = leetcode.FindPeakElement(arr);

            Assert.AreEqual(1, res);
        }
        [Test]
        public void FindPeakElementTest5()
        {
            int[] arr = new int[] { 3, 4, 3, 2, 1 };
            var res = leetcode.FindPeakElement(arr);

            Assert.AreEqual(1, res);
        }

        #endregion

        #region KthFactorTest
        [Test]
        public void KthFactorTest1()
        {
            var res = leetcode.KthFactor(12, 3);

            Assert.AreEqual(3, res);
        }

        [Test]
        public void KthFactorTest2()
        {
            var res = leetcode.KthFactor(7, 2);

            Assert.AreEqual(7, res);
        }

        [Test]
        public void KthFactorTest3()
        {
            var res = leetcode.KthFactor(4, 4);

            Assert.AreEqual(-1, res);
        }
        #endregion

        #region CanSortArrayTest
        [Test]
        public void CanSortArrayTest1()
        {
            var res = leetcode.CanSortArray(new int[] { 8, 4, 2, 30, 15 });

            Assert.IsTrue(res);
        }

        [Test]
        public void CanSortArrayTest2()
        {
            var res = leetcode.CanSortArray(new int[] { 75, 34, 30 });

            Assert.IsFalse(res);
        }

        [Test]
        public void CanSortArrayTest3()
        {
            var res = leetcode.CanSortArray(new int[] { 1, 2, 3, 4, 5 });

            Assert.IsTrue(res);
        }

        [Test]
        public void CanSortArrayTest4()
        {
            var res = leetcode.CanSortArray(new int[] { 136, 256, 10 });

            Assert.IsFalse(res);
        }
        #endregion

        #region LargestCombinationTest


        [Test]
        public void LargestCombinationTest1()
        {
            var res = leetcode.LargestCombination(new int[] { 16, 17, 71, 62, 12, 24, 14 });
            Assert.AreEqual(4, res);
        }

        [Test]
        public void LargestCombinationTest2()
        {
            var res = leetcode.LargestCombination(new int[] { 8, 8 });
            Assert.AreEqual(2, res);
        }

        [Test]
        public void LargestCombinationTest3()
        {
            var res = leetcode.LargestCombination(new int[] { 33, 93, 31, 99, 74, 37, 3, 4, 2, 94, 77, 10, 75, 54, 24, 95, 65, 100, 41, 82, 35, 65, 38, 49, 85, 72, 67, 21, 20, 31 });
            Assert.AreEqual(18, res);
        }
        #endregion

        #region RemoveDuplicateLettersTest
        [Test]
        public void RemoveDuplicateLettersTest1()
        {
            var res = leetcode.RemoveDuplicateLetters("bcabc");
            Assert.AreEqual("abc", res);
        }

        [Test]
        public void RemoveDuplicateLettersTest2()
        {
            var res = leetcode.RemoveDuplicateLetters("cbacdcbc");
            Assert.AreEqual("acdb", res);
        }

        [Test]
        public void RemoveDuplicateLettersTest3()
        {
            var res = leetcode.RemoveDuplicateLetters("bbcaac");
            Assert.AreEqual("bac", res);
        }
        #endregion

        [Test]
        public void GetMaximumXorTest1()
        {
            var res = leetcode.GetMaximumXor(new int[] { 0, 1, 1, 3 }, 2);
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
        public void CountMaxOrSubsetsTest1()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);
            int[] nums = new int[] { 3, 1 };
            var res1 = leetcode.CountMaxOrSubsets(nums);
            Assert.AreEqual(2, res1);
        }

        [Test]
        public void FindKthBitTest1()
        {
            //var res = leetcode.LongestDiverseString(/*7,1,0*/0,8,11);
            //int[] nums = new int[] { 3, 1 };
            var res1 = leetcode.FindKthBit(4, 11);
            Assert.AreEqual('1', res1);
        }

        [Test]
        public void FlipEquivTest1()
        {
            int?[] r1 = { 1, 2, 3, 4, 5, 6, null, null, null, 7, 8 };
            int?[] r2 = { 1, 3, 2, null, 6, 4, 5, null, null, null, null, 8, 7 };

            TreeNode root1 = buildTree(r1);
            TreeNode root2 = buildTree(r2);

            var res = leetcode.FlipEquiv(root1, root2);

            Assert.IsTrue(res);
        }

        [Test]
        public void DiffWaysToComputeTest1()
        {
            var res = leetcode.DiffWaysToCompute("2-1-1");
        }

        //1,2,1,3,2,5
        [Test]
        public void SingleNumberTes1()
        {
            var res = leetcode.SingleNumber(new int[] { 1, 2, 1, 3, 2, 5 });
            CollectionAssert.AreEqual(new int[] { 5, 3 }, res);
        }

        [Test]
        public void LongestSquareStreakTest1()
        {
            int[] arr = { 4, 3, 6, 16, 8, 2 };
            var res = leetcode.LongestSquareStreak(arr);

            Assert.AreEqual(3, res);
        }


        [Test]
        public void MakeFancyStringTest1()
        {
            var res = leetcode.MakeFancyString("leeetcode");

            Assert.AreEqual("leetcode", res);
        }
        [Test]
        public void CompressedStringTest1()
        {
            var res = leetcode.CompressedString("leeetcode");

            Assert.AreEqual("leetcode", res);
        }

        [Test]
        public void MinChangesTest1()
        {
            var res = leetcode.MinChanges("1001100110011001");

            Assert.AreEqual(8, res);
        }
        #region Private Methods

        private TreeNode buildTree(int?[] arr)
        {
            TreeNode treeNode = new TreeNode(arr[0].Value);
            Queue<TreeNode> queue = new Queue<TreeNode>();

            if (arr.Length > 1 && arr[1].HasValue)
            {
                treeNode.left = new TreeNode(arr[1].Value);
                queue.Enqueue(treeNode.left);
            }

            if (arr.Length > 2 && arr[2].HasValue)
            {
                treeNode.right = new TreeNode(arr[2].Value);

                queue.Enqueue(treeNode.right);

            }

            int i = 2;

            while (queue.Count > 0 && ++i < arr.Length)
            {
                TreeNode node = queue.Dequeue();
                if (arr[i].HasValue)
                {
                    node.left = new TreeNode((int)arr[i].Value);

                    queue.Enqueue(node.left);
                }

                if (++i < arr.Length && arr[i].HasValue)
                {
                    node.right = new TreeNode((int)arr[i].Value);

                    queue.Enqueue(node.right);
                }
            }

            return treeNode;
        }

        private static TreeNode enqueNode(int?[] arr, Queue<(int index, TreeNode node)> q, int leftIndex, ref int nullIncr)
        {
            TreeNode node = null;
            if (leftIndex < arr.Length && arr[leftIndex].HasValue)
            {
                node = new TreeNode(arr[leftIndex].Value);
                q.Enqueue((leftIndex, node));
            }
            else
            {
                nullIncr += 2;
            }
            return node;
        }
        #endregion
    }
}
