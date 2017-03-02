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
            g.TopSort();
        }
    }

    public class Graph
    {
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
        private Vertex[] vertices_;
        private int[,] adjMatrix_;
        private const int MAX_NUM_VERTICES = 20;
        private int numVertices_;
        public Graph()
        {
            vertices_ = new Vertex[MAX_NUM_VERTICES];
            adjMatrix_ = new int[MAX_NUM_VERTICES, MAX_NUM_VERTICES];
            numVertices_ = 0;
            for (int i = 0; i < MAX_NUM_VERTICES; ++i)
                for (int j = 0; j < MAX_NUM_VERTICES; ++j)
                    adjMatrix_[i, j] = 0;
        }
        public bool AddVertex(string label)
        {
            if (numVertices_ < MAX_NUM_VERTICES - 1)
            {
                vertices_[numVertices_++] = new Vertex(label);
                return true;
            }
            else
                return false;
        }
        public bool AddEdge(int from, int to, int weight = 1)
        {
            if (numVertices_ < MAX_NUM_VERTICES - 1)
            {
                adjMatrix_[from, to] = weight;
                return true;
            }
            else
                return false;
        }
        public void TopSort()
        {
            
        }
    }
}
