using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class IntSkipList : IEnumerable
    {
        private class Node
        {
            public Node[] Next { get; private set; }
            public int Value { get; private set; }

            public Node(int value, int level)
            {
                Value = value;
                Next = new Node[level];
            }
        }

        private Node _head = new Node(0, 33);
        private Random _rand = new Random();
        private int _levels = 1;


        public void Insert(int value)
        {

            int level = 0;
            for (int R = _rand.Next(); (R & 1) == 1; R >>= 1)
            {
                level++;
                if (level == _levels) { _levels++; break; }
            }


            Node newNode = new Node(value, level + 1);
            Node cur = _head;
            for (int i = _levels - 1; i >= 0; i--)
            {
                for (; cur.Next[i] != null; cur = cur.Next[i])
                {
                    if (cur.Next[i].Value > value) break;
                }

                if (i <= level) { newNode.Next[i] = cur.Next[i]; cur.Next[i] = newNode; }
            }
        }


        public bool Contains(int value)
        {
            Node cur = _head;
            for (int i = _levels - 1; i >= 0; i--)
            {
                for (; cur.Next[i] != null; cur = cur.Next[i])
                {
                    if (cur.Next[i].Value > value) break;
                    if (cur.Next[i].Value == value) return true;
                }
            }
            return false;
        }


        public bool Remove(int value)
        {
            Node cur = _head;

            bool found = false;
            for (int i = _levels - 1; i >= 0; i--)
            {
                for (; cur.Next[i] != null; cur = cur.Next[i])
                {
                    if (cur.Next[i].Value == value)
                    {
                        found = true;
                        cur.Next[i] = cur.Next[i].Next[i];
                        break;
                    }

                    if (cur.Next[i].Value > value) break;
                }
            }

            return found;
        }



        public void Clear()
        {
            _head = new Node(0, 33);
        }

        public IEnumerable<int> Enumerate()
        {
            Node cur = _head.Next[0];
            while (cur != null)
            {
                yield return cur.Value;
                cur = cur.Next[0];
            }
        }

        public void Show()
        {
            foreach (var item in this)
            {
                Console.WriteLine(item);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Node cur = _head.Next[0];
            while (cur != null)
            {
                yield return cur.Value;
                cur = cur.Next[0];
            }
        }
    }
}