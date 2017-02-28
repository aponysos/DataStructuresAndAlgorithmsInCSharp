using System;
using System.Collections.Generic;

namespace SkipList
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("TestSkipList:");
            TestSkipList();
            //Console.Read();
        }
        static void TestSkipList()
        {
            SkipList sl = new SkipList();
            sl.Init();
            Console.WriteLine(sl.ToString());
            sl.Insert(1);
            sl.Insert(2);
            sl.Insert(3);
            sl.Insert(4);
            sl.Insert(5);
            sl.Insert(6);
            sl.Insert(7);
            sl.Insert(8);
            sl.Insert(9);
            Console.WriteLine(sl.ToString());
        }
    }

    class SkipList
    {
        private class Node
        {
            public int value;
            public Node down;
            public Node next;
            public Node(int value, Node down, Node next)
            {
                this.value = value;
                this.down = down;
                this.next = next;
            }
            public Node(int value) : this(value, null, null)
            {
            }
        }
        private Node head_;
        private Random rand_ = new Random();
        private const double PROB = 0.25;
        private int GetRandomLevel()
        {
            int randomLevel = 0;
            while (rand_.NextDouble() < PROB)
                ++randomLevel;
            return randomLevel;
        }
        public SkipList()
        {
        }
        public void Init()
        {
            head_ = new Node(0);
        }
        public void Clear()
        {
            head_ = null;
        }
        private Node InsertLevelNode(Node curNode, int value)
        {
            Node newNode = new Node(value, null, curNode.next);
            curNode.next = newNode;
            return newNode;
        }
        public bool Insert(int value)
        {
            if (head_.next == null) // empty skiplist
            {
                head_.next = new Node(value, null, null);
                return true;
            }

            if (Search(value)) // replicated value
                return false;

            int curLevel = head_.value;
            int randomLevel = GetRandomLevel();
            Stack<Node> s = new Stack<Node>();
            while (curLevel < randomLevel)
            {
                Node newNode = InsertLevelNode(head_, value);
                newNode.down = s.Peek();
                s.Push(newNode);
                ++curLevel;
            }

            Node current = head_;
            do
            {
                while (current.next != null && value > current.next.value)
                    current = current.next;

                Node newNode = s.Pop();
                newNode.next = current.next;
                current.next = newNode;
                current = current.down; // move to next level
                --curLevel;
            }
            while (curLevel >= 0);

            return false;
        }
        public bool Remove(int value)
        {
            if (head_.next == null)
                return false;

            Node current = head_;
            do
            {
                while (current.next != null && value > current.next.value)
                    current = current.next;

                if (current.next != null && value == current.next.value)
                    current.next = current.next.next;

                current = current.down;
            }
            while (current != null);

            return true;
        }
        public bool Search(int value)
        {
            if (head_.next == null)
                return false;

            Node current = head_;
            do
            {
                while (current.next != null && value > current.next.value)
                    current = current.next;

                if (current.next != null && value == current.next.value)
                    return true;

                current = current.down;
            }
            while (current != null);

            return false;
        }
        public override string ToString()
        {
            if (head_.next == null)
                return "";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            Node levelHead = head_.next;
            do
            {
                Node current = levelHead;
                do
                {
                    sb.Append("——").Append(current.value);
                    current = current.next;
                }
                while (current != null);
                sb.AppendLine();
                levelHead = levelHead.down;
            }
            while (levelHead != null);

            return sb.ToString();
        }
    }
}
