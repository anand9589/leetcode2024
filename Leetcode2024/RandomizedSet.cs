namespace Leetcode2024
{
    public class RandomizedSet
    {
        Dictionary<int, int> map;
        int[] dataStore;
        Random random;
        int curIndex = -1;
        public RandomizedSet()
        {
            map = new Dictionary<int, int>();
            dataStore = new int[200001];
            random = new Random();
        }

        public bool Insert(int val)
        {
            if (map.ContainsKey(val)) { return false; }

            dataStore[++curIndex] = val;
            map.Add(val, curIndex);
            return true;
        }

        public bool Remove(int val)
        {
            if (!map.ContainsKey(val)) { return false; }
            swap(map[val]);
            map.Remove(val);
            return true;

        }

        private void swap(int indexToRemove)
        {
            dataStore[indexToRemove] = dataStore[curIndex--];
            map[dataStore[indexToRemove]] = indexToRemove;
        }

        public int GetRandom()
        {
            int randomIndex = random.Next(0, curIndex + 1);
            return dataStore[randomIndex];
        }
    }
}
