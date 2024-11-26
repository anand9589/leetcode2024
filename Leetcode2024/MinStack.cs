namespace Leetcode2024
{
    public class MinStack
    {
        int capacity;
        int[] datastore;
        int currIndex;
        Stack<int> minElementIndex;
        public MinStack()
        {
            capacity = 3 * 10000 + 1;
            this.datastore = new int[capacity];
            this.currIndex = -1;
            minElementIndex = new Stack<int>();
        }

        public void Push(int val)
        {
            this.currIndex++;

            this.datastore[this.currIndex] = val;

            if(minElementIndex.Count == 0 || this.datastore[minElementIndex.Peek()]>val)
            {
                minElementIndex.Push(currIndex);
            }
        }

        public void Pop()
        {
            if(this.currIndex == minElementIndex.Peek())
            {
                minElementIndex.Pop();
            }

            this.currIndex--;
        }

        public int Top()
        {
            return this.datastore[this.currIndex];
        }

        public int GetMin()
        {
            return this.datastore[minElementIndex.Peek()];
        }
    }
}
