namespace Leetcode2024.Common.Models
{
    public class Node
    {
        public Node Next { get; set; }
        public Node Previous { get; set; }
        public int Val { get; set; }

        public Node()
        {
            Val = -1;
        }
        public Node(Node next, Node previous, int val)
        {
            Next = next;
            Previous = previous;
            Val = val;
        }
    }
}
