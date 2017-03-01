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
            while (randomLevel <= level_ && rand_.NextDouble() < PROB)
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
        private void InitDownLink(Queue<Node> nodes)
        {
            while (nodes.Count > 1)
            {
                Node node = nodes.Dequeue();
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

            int randomLevel = GetRandomLevel();
            Console.WriteLine("level_ : {0}, randomLevel : {1}, value : {2}", level_, randomLevel, value);

            Queue<Node> newNodes = new Queue<Node>(); // new value nodes queue
            Node newHeadNode = null; // new head node if not null

            // new level, new head node, new value node
            if (randomLevel > level_)
            {
                newHeadNode = new Node();
                newHeadNode.down = head_;

                Node newNode = InsertLevelNode(newHeadNode, value);
                newNodes.Enqueue(newNode);
            }

            // old level, new value node
            Node curNode = head_;
            int curLevel = level_;
            for (; curNode != null; curNode = curNode.down, --curLevel)
            {
                while (curNode.next != null && value > curNode.next.value)
                    curNode = curNode.next;

                if (curLevel <= randomLevel) // new node in this level
                {
                    Node newNode = InsertLevelNode(curNode, value);
                    newNodes.Enqueue(newNode);
                }
            }

            // init down links of new nodes
            InitDownLink(newNodes);

            // update head & level
            if (newHeadNode != null)
            {
                head_ = newHeadNode;
                level_ = randomLevel;
            }

            return true;
        }
        public bool Remove(int value)
        {
            if (head_.next == null)
                return false;

            Node curNode = head_;
            do
            {
                while (curNode.next != null && value > curNode.next.value)
                    curNode = curNode.next;

                if (curNode.next != null && value == curNode.next.value)
                    curNode.next = curNode.next.next;

                curNode = curNode.down;
            }
            while (curNode != null);

            return true;
        }
        public bool Search(int value)
        {
            if (head_.next == null)
                return false;

            Node curNode = head_;
            do
            {
                while (curNode.next != null && value > curNode.next.value)
                    curNode = curNode.next;

                if (curNode.next != null && value == curNode.next.value)
                    return true;

                curNode = curNode.down;
            }
            while (curNode != null);

            return false;
        }
        public override string ToString()
        {
            if (head_.next == null)
                return "";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            Node curHead = head_;
            do
            {
                Node curNode = curHead.next;
                do
                {
                    sb.Append("——").Append(curNode.value);
                    curNode = curNode.next;
                }
                while (curNode != null);
                sb.AppendLine();
                curHead = curHead.down;
            }
            while (curHead != null);

            return sb.ToString();
        }
    }
}
