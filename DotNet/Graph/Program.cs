using System;
using System.Collections.Generic;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test Graph:");
            TestGraph();
            //Console.Read();
        }
        static void TestGraph()
        {
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
            g.TopologicalSort();
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
        public void TopologicalSort()
        {
            TopologicalTraverser traverser = new TopologicalTraverser(this);
            traverser.Traverse();
        }
    }
    public abstract class GraphTraverser
    {
        protected Graph g_;
        protected int nv_; // # of vertices
        protected bool[] visited_; // if vertex is visited
        protected List<int> l_; // list of of vertices in visiting order 
        protected GraphTraverser(Graph g)
        {
            g_ = g;
            nv_ = g.numVertices_;
            visited_ = new bool[nv_];
            for (int i = 0; i < nv_; ++i)
                visited_[i] = false;
            l_ = new List<int>(nv_);
        }
        protected abstract void Traverse_();
        public void Traverse()
        {
            Traverse_();
            Output();
        }
        protected void VisitVertex(int i)
        {
            if (visited_[i])
                throw new InvalidOperationException("vertex has been visited.");
            visited_[i] = true;
            l_.Add(i);
        }
        protected void Output()
        {
            foreach (int i in l_)
                Console.WriteLine(g_.vertices_[i]);
        }
        protected bool HasOutgoingEdge(int i)
        {
            for (int j = 0; j < nv_; ++j)
                if (!visited_[j] && g_.adjMatrix_[i, j] > 0)
                    return true;
            return false;
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
    }
}
