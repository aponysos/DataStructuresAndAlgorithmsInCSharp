using System;
using System.Collections.Generic;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            TestTopologicalTraverse();
            TestDepthFirstTraverse();
            TestBreadthFirstTraverse();
            TestMST();
            //Console.Read();
        }
        static void TestTopologicalTraverse()
        {
            Console.WriteLine("TestTopologicalTraverse");
            Graph g = new Graph();
            g.AddVertex("CS1");
            g.AddVertex("CS2");
            g.AddVertex("DS");
            g.AddVertex("OS");
            g.AddVertex("ALG");
            g.AddVertex("AL");
            g.AddEdge(0, 1);
            g.AddEdge(1, 2);
            g.AddEdge(1, 5);
            g.AddEdge(2, 3);
            g.AddEdge(2, 4);
            g.TopologicalTraverse();
        }
        static void TestDepthFirstTraverse()
        {
            Console.WriteLine("TestDepthFirstTraverse");
            Graph g = new Graph();
            g.AddVertex("A");
            g.AddVertex("B");
            g.AddVertex("C");
            g.AddVertex("D");
            g.AddVertex("E");
            g.AddVertex("F");
            g.AddVertex("G");
            g.AddVertex("H");
            g.AddVertex("I");
            g.AddVertex("J");
            g.AddVertex("K");
            g.AddVertex("L");
            g.AddVertex("M");
            g.AddEdge(0, 1);
            g.AddEdge(1, 2);
            g.AddEdge(2, 3);
            g.AddEdge(0, 4);
            g.AddEdge(4, 5);
            g.AddEdge(5, 6);
            g.AddEdge(0, 7);
            g.AddEdge(7, 8);
            g.AddEdge(8, 9);
            g.AddEdge(0, 10);
            g.AddEdge(10, 11);
            g.AddEdge(11, 12);
            g.DepthFirstTraverse();
        }
        static void TestBreadthFirstTraverse()
        {
            Console.WriteLine("TestBreadthFirstTraverse");
            Graph g = new Graph();
            g.AddVertex("A");
            g.AddVertex("B");
            g.AddVertex("C");
            g.AddVertex("D");
            g.AddVertex("E");
            g.AddVertex("F");
            g.AddVertex("G");
            g.AddVertex("H");
            g.AddVertex("I");
            g.AddVertex("J");
            g.AddVertex("K");
            g.AddVertex("L");
            g.AddVertex("M");
            g.AddEdge(0, 1);
            g.AddEdge(1, 2);
            g.AddEdge(2, 3);
            g.AddEdge(0, 4);
            g.AddEdge(4, 5);
            g.AddEdge(5, 6);
            g.AddEdge(0, 7);
            g.AddEdge(7, 8);
            g.AddEdge(8, 9);
            g.AddEdge(0, 10);
            g.AddEdge(10, 11);
            g.AddEdge(11, 12);
            g.BreadthFirstTraverse();
        }
        static void TestMST()
        {
            Console.WriteLine("TestMST");
            Graph g = new Graph();
            g.AddVertex("A");
            g.AddVertex("B");
            g.AddVertex("C");
            g.AddVertex("D");
            g.AddVertex("E");
            g.AddVertex("F");
            g.AddVertex("G");
            g.AddEdge(0, 1, 2);
            g.AddEdge(0, 3, 1);
            g.AddEdge(1, 3, 3);
            g.AddEdge(1, 4, 10);
            g.AddEdge(2, 0, 4);
            g.AddEdge(2, 5, 5);
            g.AddEdge(3, 2, 2);
            g.AddEdge(3, 4, 2);
            g.AddEdge(3, 5, 8);
            g.AddEdge(3, 6, 4);
            g.AddEdge(6, 5, 1);
            g.AddEdge(4, 6, 6);
            g.MST();
        }

    }

    public class Vertex
    {
        public string label_;
        public Vertex(string label)
        {
            this.label_ = label;
        }
        public override string ToString()
        {
            return label_;
        }
    }
    public class Graph
    {
        public int maxVertices_;
        public int numVertices_;
        public Vertex[] vertices_;
        public int[,] adjMatrix_;
        public Graph(int maxVertices = 20)
        {
            maxVertices_ = maxVertices;
            vertices_ = new Vertex[maxVertices_];
            adjMatrix_ = new int[maxVertices_, maxVertices_];
            numVertices_ = 0;
            for (int i = 0; i < maxVertices_; ++i)
                for (int j = 0; j < maxVertices_; ++j)
                    adjMatrix_[i, j] = 0;
        }
        public bool AddVertex(string label)
        {
            if (numVertices_ < maxVertices_ - 1)
            {
                vertices_[numVertices_++] = new Vertex(label);
                return true;
            }
            else
                return false;
        }
        public bool AddEdge(int from, int to, int weight = 1)
        {
            if (numVertices_ < maxVertices_ - 1)
            {
                adjMatrix_[from, to] = weight;
                return true;
            }
            else
                return false;
        }
        public void TopologicalTraverse()
        {
            TopologicalTraverser traverser = new TopologicalTraverser(this);
            traverser.Process();
        }
        public void DepthFirstTraverse()
        {
            DepthFirstTraverser traverser = new DepthFirstTraverser(this);
            traverser.Process();
        }
        public void BreadthFirstTraverse()
        {
            BreadthFirstTraverser traverser = new BreadthFirstTraverser(this);
            traverser.Process();
        }
        public void MST()
        {
            MinimumSpanningTree mst = new MinimumSpanningTree(this);
            mst.Process();
        }
    }
    public abstract class GraphProcessor
    {
        protected Graph g_; // graph object
        protected int nv_; // # of vertices
        protected GraphProcessor(Graph g)
        {
            g_ = g;
            nv_ = g.numVertices_;
        }
        protected abstract void Process_();
        protected abstract void Output_();
        public void Process()
        {
            Process_();
            Output_();
        }
    }
    public abstract class GraphTraverser : GraphProcessor
    {
        protected bool[] visited_; // if vertex is visited
        protected List<int> l_; // list of of vertices in visiting order 
        protected GraphTraverser(Graph g)
            : base(g)
        {
            visited_ = new bool[nv_];
            for (int i = 0; i < nv_; ++i)
                visited_[i] = false;
            l_ = new List<int>(nv_);
        }
        protected abstract void Traverse_();
        protected override void Process_()
        {
            Traverse_();
        }
        protected override void Output_()
        {
            foreach (int i in l_)
                Console.WriteLine(g_.vertices_[i]);
        }
        protected void VisitVertex(int i)
        {
            if (visited_[i])
                throw new InvalidOperationException("vertex has been visited.");
            visited_[i] = true;
            l_.Add(i);
        }
    }
    public class TopologicalTraverser : GraphTraverser
    {
        public TopologicalTraverser(Graph g)
            : base(g)
        {
        }
        protected override void Traverse_()
        {
            while (l_.Count < nv_)
                for (int i = 0; i < nv_ && !visited_[i]; ++i)
                {
                    if (!HasOutgoingEdge(i))
                        VisitVertex(i);
                }
            l_.Reverse();
        }
        private bool HasOutgoingEdge(int i)
        {
            for (int j = 0; j < nv_; ++j)
                if (!visited_[j] && g_.adjMatrix_[i, j] > 0)
                    return true;
            return false;
        }
    }
    public class DepthFirstTraverser : GraphTraverser
    {
        public DepthFirstTraverser(Graph g)
            : base(g)
        {
        }
        protected override void Traverse_()
        {
            Stack<int> s = new Stack<int>(nv_);
            s.Push(0);
            VisitVertex(0);

            while (s.Count > 0)
            {
                int cur = s.Pop();
                for (int i = 0; i < nv_; ++i)
                    if (!visited_[i] && g_.adjMatrix_[cur, i] > 0)
                    {
                        s.Push(i);
                        VisitVertex(i);
                    }
            }
        }
    }
    public class BreadthFirstTraverser : GraphTraverser
    {
        public BreadthFirstTraverser(Graph g)
            : base(g)
        {
        }
        protected override void Traverse_()
        {
            Queue<int> q = new Queue<int>(nv_);
            q.Enqueue(0);
            VisitVertex(0);

            while (q.Count > 0)
            {
                int cur = q.Dequeue();
                for (int i = 0; i < nv_; ++i)
                    if (!visited_[i] && g_.adjMatrix_[cur, i] > 0)
                    {
                        q.Enqueue(i);
                        VisitVertex(i);
                    }
            }
        }
    }
    public class MinimumSpanningTree : GraphProcessor
    {
        public MinimumSpanningTree(Graph g)
            : base(g)
        {
        }
        protected override void Process_()
        {
            MST();
        }
        protected override void Output_()
        {
        }
        private void MST()
        {
        }
    }
}
