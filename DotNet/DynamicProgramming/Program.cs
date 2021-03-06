﻿using System;
using System.Text;
using System.Collections.Generic;

namespace DynamicProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            TestLCS();
            TestHuffmanEncoding();
            TestKnapsackProblem();
        }
        static void TestLCS()
        {
            Console.WriteLine("TestLCS:");
            LCSAlgorithm lcs = new LCSAlgorithm("ABCBDAB", "BDCABA");
            lcs.Compute();
            Console.WriteLine(lcs.LongestCommonSubsquence);
        }
        static void TestHuffmanEncoding()
        {
            Console.WriteLine("TestHuffmanEncoding:");
            HuffmanTree huffman = new HuffmanTree();
            huffman.AddNode("a", 45);
            huffman.AddNode("m", 15);
            huffman.AddNode("r", 8);
            huffman.AddNode("s", 9);
            huffman.AddNode("t", 13);
            huffman.Construct();
            huffman.OutputEncoding();
        }
        static void TestKnapsackProblem()
        {
            Console.WriteLine("TestKnapsackProblem:");
            KnapsackProblem ksp = new KnapsackProblem(
                25, 
                new string[] {"Frieze", "Saxony", "Shag", "Loop"}, 
                new int[] {12, 9, 13, 10}, 
                new double[] {1.75, 1.82, 1.5, 1.77}
            );
            ksp.Compute();
        }
    }
    class LCSAlgorithm
    {
        private string s_;
        private string t_;
        private int ls_;
        private int lt_;
        private int [,] arr_;
        private StringBuilder lcs_;
        public string LongestCommonSubsquence
        {
            get { return lcs_.ToString(); }
        }
        public LCSAlgorithm(string s, string t)
        {
            s_ = s;
            t_ = t;
            ls_ = s.Length;
            lt_ = t.Length;
            arr_ = new int[ls_ + 1, lt_ + 1];
            for (int i = 0; i <= ls_; ++i)
                arr_[i, 0] = 0;
            for (int j = 0; j <= lt_; ++j)
                arr_[0, j] = 0;
            lcs_ = new StringBuilder();
        }
        public void Compute()
        {
            ComputeArray(); // compute internal array
            DisplayArray(); // display internal array
            BackTrack(ls_, lt_); // back track the subsequence
        }
        private void ComputeArray()
        {
            for (int i = 1; i <= ls_; ++i)
                for (int j = 1; j <= lt_; ++j)
                    if (s_[i - 1] == t_[j - 1])
                        arr_[i, j] = arr_[i - 1, j - 1] + 1;
                    else
                        arr_[i, j] = Math.Max(arr_[i, j - 1], arr_[i - 1, j]);
        }
        private void DisplayArray()
        {
            Console.WriteLine("  {0}", t_);
            for (int i = 0; i <= ls_; ++i)
            {
                if (i > 0)
                    Console.Write(s_[i - 1]);
                else
                    Console.Write(" ");
                for (int j = 0; j <= lt_; ++j)
                    Console.Write(arr_[i, j]);
                Console.WriteLine();
            }
        }
        private void BackTrack(int i, int j)
        {
            if (i == 0 || j == 0)
                return;
            else if (s_[i - 1] == t_[j - 1])
            {
                lcs_.Insert(0, s_[i - 1]); // prepend
                BackTrack(i - 1, j - 1);
            }
            else if (arr_[i, j - 1] > arr_[i - 1, j])
                BackTrack(i, j - 1);
            else
                BackTrack(i - 1, j);
        }
    }
    class HuffmanTree
    {
        public class Node
        {
            public Node left;
            public Node right;
            public string letter;
            public int weight;
            public string code;
            public Node(string l = "", int w = 0)
            {
                letter = l;
                weight = w;
                left = right = null;
                code = "";
            }
        }
        List<Node> letterNodes_;
        List<Node> treeNodes_;
        public HuffmanTree()
        {
            letterNodes_ = new List<Node>();
            treeNodes_ = new List<Node>();
        }
        public void AddNode(string letter, int weight)
        {
            Node newNode = new Node(letter, weight);
            letterNodes_.Add(newNode);
            treeNodes_.Add(newNode);
        }
        public void Construct()
        {
            treeNodes_.Sort((a, b) => {return a.weight - b.weight;});
            while (treeNodes_.Count > 1)
            {
                // construct new node from top-2 nodes
                Node newNode = new Node();
                Node left = treeNodes_[0];
                Node right = treeNodes_[1];
                newNode.left = left;
                newNode.right = right;
                newNode.weight = left.weight + right.weight;
                // remove top-2 nodes
                treeNodes_.RemoveAt(0);
                treeNodes_.RemoveAt(0);
                // add new nodes
                treeNodes_.Add(newNode);
                // re-sort list
                treeNodes_.Sort((a, b) => {return a.weight - b.weight;});
            }
        }
        public void OutputEncoding()
        {
            if (treeNodes_.Count != 1) return;
            // depth-first traverse
            Stack<Node> s = new Stack<Node>();
            s.Push(treeNodes_[0]); // push root node
            while (s.Count > 0)
            {
                Node cur = s.Pop();
                if (cur.left != null)
                {
                    cur.left.code = cur.code + "0";
                    s.Push(cur.left);
                }
                if (cur.right != null)
                {
                    cur.right.code = cur.code + "1";
                    s.Push(cur.right);
                }
            }
            foreach (Node n in letterNodes_)
                Console.WriteLine("{0} : {1}", n.letter, n.code);
        }
    }
    class KnapsackProblem
    {
        private int quantity_;
        private string[] items_;
        private int[] units_;
        private double[] values_;
        public KnapsackProblem(int quantity, string[] items, int[] units, double[] values)
        {
            quantity_ = quantity;
            items_ = items;
            units_ = units;
            values_ = values;
        }
        public void Compute()
        {
            List<Carpet> rugs = new List<Carpet>();
            for (int i = 0; i < items_.Length; ++i)
                rugs.Add(new Carpet(items_[i], units_[i], values_[i]));
            Knapsack ks = new Knapsack(quantity_);
            ks.Fill(rugs);
            ks.OutputItems();
        }
    }
    public class Carpet
    {
        public string item_;
        public int unit_;
        public double value_;
        public Carpet(string i, int u, double v)
        {
            item_ = i;
            unit_ = u;
            value_ = v;
        }
        public override string ToString()
        {
            return string.Format("{0} : {1} : {2}", item_, unit_, value_);
        }
    }
    public class Knapsack
    {
        private int quantity_;
        private List<Carpet> items_;
        public Knapsack(int q)
        {
            quantity_ = q;
            items_ = new List<Carpet>();
        }
        public void Fill(List<Carpet> rugs)
        {
            rugs.Sort((c1, c2) => 
            {
                return c1.value_/c1.unit_ > c2.value_/c2.unit_ ? 1 : -1;
            });
            int curQuatity = 0;
            foreach (Carpet c in rugs)
            {
                if (curQuatity + c.unit_ <= quantity_)
                {
                    curQuatity += c.unit_;
                    items_.Add(c);
                }
                else
                {
                    int unitLeft = quantity_ - curQuatity;
                    double valueLeft = c.value_ * unitLeft;
                    c.unit_ = unitLeft;
                    curQuatity += c.unit_;
                    items_.Add(c);
                }
            }
        }
        public void OutputItems()
        {
            foreach (Carpet c in items_)
                Console.WriteLine(c);
        }
    }
}
