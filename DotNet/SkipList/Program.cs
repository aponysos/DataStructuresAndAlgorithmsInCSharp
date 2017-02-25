using System;

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
        }
        private Node head_;
        private Random rand_ = new Random();
        public SkipList()
        {
        }
        public void Init()
        {
            head_ = new Node(0, null, null);
        }
        public void Clear()
        {
            head_ = null;
        }
        public bool Search(int value)
        {
            return false;
        }
        public bool Insert(int value)
        {
            return false;
        }
        public bool Remove(int value)
        {
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
