namespace Leetcode2024
{
    public class February
    {
        #region Day 1 2966. Divide Array Into Arrays With Max Difference
        public int[][] DivideArray(int[] nums, int k)
        {
            Array.Sort(nums);
            List<int[]> lst = new List<int[]>();

            for (int i = 0; i < nums.Length - 2;)
            {

                if (nums[i + 1] - nums[i] <= k && nums[i + 2] - nums[i] <= k)
                {
                    lst.Add(new int[] { nums[i++], nums[i++], nums[i++] });
                }
                else
                {
                    return Array.Empty<int[]>();
                }
            }

            return lst.ToArray();
        }
        #endregion
    }
}
