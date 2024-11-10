using System.Text;

namespace Leetcode2024
{
    public class WordBreak_II_Solution
    {
        public IList<string> WordBreak(string s, IList<string> wordDict)
        {
            Trie trie = new(' ');

            foreach (var item in wordDict)
            {
                trie.Insert(item);
            }

            List<string> words = new List<string>();
            Queue<string> qwords = new Queue<string>();
            trie.GetWords(s, 0, qwords, new StringBuilder());
            int i = 0;
            while (qwords.Count > 0)
            {
                int count = qwords.Count;
                while (count-->0)
                {
                    string str = qwords.Dequeue();
                    if(str.Length - i == s.Length)
                    {
                        words.Add(str);
                        continue;
                    }
                    StringBuilder sb = new StringBuilder(str);
                    sb.Append(' ');
                    trie.GetWords(s.Substring(str.Length - i), 0, qwords, sb);
                }
                i++;

            }

            return words;

        }

        internal class Trie
        {
            internal bool WordEnd;
            internal Dictionary<char, Trie> TrieList;
            internal char Value;

            public Trie(char c)
            {
                this.TrieList = new Dictionary<char, Trie>();
                this.Value = c;
            }

            internal void Insert(string s)
            {
                char c = s[0];
                Trie trie;
                if (this.TrieList.ContainsKey(c))
                {
                    trie = this.TrieList[c];
                }
                else
                {
                    trie = new Trie(c);
                    TrieList.Add(c, trie);
                }

                if (s.Length == 1)
                {
                    trie.WordEnd = true;
                }
                else
                {
                    trie.Insert(s.Substring(1));
                }
            }

            internal void GetWords(string s, int index, Queue<string> strings, StringBuilder stringBuilder)
            {
                
                if (index<s.Length && this.TrieList.ContainsKey(s[index]))
                {
                    Trie child = this.TrieList[s[index]];
                    stringBuilder.Append(child.Value);
                    if (child.WordEnd)
                    {
                        strings.Enqueue(stringBuilder.ToString());
                    }
                    child.GetWords(s, index + 1, strings, stringBuilder);
                }
            }
        }
    }
}
