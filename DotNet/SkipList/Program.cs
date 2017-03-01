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
            Console.WriteLine(sl.ToString());
            sl.Insert(2);
            Console.WriteLine(sl.ToString());
            sl.Insert(3);
            Console.WriteLine(sl.ToString());
            sl.Insert(4);
            Console.WriteLine(sl.ToString());
            sl.Insert(5);
            Console.WriteLine(sl.ToString());
            sl.Insert(6);
            Console.WriteLine(sl.ToString());
            sl.Insert(7);
            Console.WriteLine(sl.ToString());
            sl.Insert(8);
            Console.WriteLine(sl.ToString());
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
            public Node(int value = 0) : this(value, null, null)
            {
            }
        }
        private Node head_;
        private int level_;
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
            level_ = 0;
        }
        public void Clear()
        {
            head_ = null;
            level_ = 0;
        }
        private Node InsertLevelNode(Node curNode, int value)
        {
            Node newNode = new Node(value, null, curNode.next);
            curNode.next = newNode;
            return newNode;
        }
        private void InitDownLink(Stack<Node> nodes)
        {
            while (nodes.Count > 1)
            {
                Node node = nodes.Pop();
                node.down = nodes.Peek();
            }
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

            Stack<Node> newHeadNodes = new Stack<Node>(); // new head node stack
            Stack<Node> newNodes = new Stack<Node>(); // new value node stack
            int randomLevel = GetRandomLevel();
            int curLevel = randomLevel;
            Console.WriteLine("randomLevel : {0}, value : {1}", randomLevel, value);

            // new level, new head node, new value node
            for (; curLevel > level_; --curLevel)
            {
                Node newHeadNode = new Node();
                Node newNode = InsertLevelNode(newHeadNode, value);
                newHeadNodes.Push(newHeadNode);
                newNodes.Push(newNode);
            }

            // old level, new value node
            Node curNode = head_;
            for (; curLevel >= 0; --curLevel, curNode = curNode.down)
            {
                while (curNode.next != null && value > curNode.next.value)
                    curNode = curNode.next;

                Node newNode = InsertLevelNode(curNode, value);
                newNodes.Push(newNode);
            }

            // update head & level
            if (level_ < randomLevel)
            {
                head_ = newHeadNodes.Peek();
                level_ = randomLevel;
            }

            // init down links of new nodes & new headnodes
            InitDownLink(newHeadNodes);
            InitDownLink(newNodes);

            return true;
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
