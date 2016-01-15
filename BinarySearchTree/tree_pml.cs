using System.Collections.Generic;
using System.Text;

namespace DataStructuresAndAlgorithmsInCSharp
{
    public interface IBinaryTreeNode
    {
        int Value { get; set; }
        IBinaryTreeNode LeftChild { get; }
        IBinaryTreeNode RightChild { get; }
    }

    public interface IBinaryTree
    {
        IBinaryTreeNode Root { get; }
    }

    class TreePrinter
    {
        class NullBinaryTreeNode : IBinaryTreeNode
        {
            private NullBinaryTreeNode() { }
            public int Value { get { return 0; } set {} }
            public IBinaryTreeNode LeftChild { get { return null; } }
            public IBinaryTreeNode RightChild { get { return null; } }
            public static readonly NullBinaryTreeNode Instance =
                new NullBinaryTreeNode();
        }

        public static string Print(IBinaryTree tree)
        {
            StringBuilder sb = new StringBuilder(1024);

            Stack<IBinaryTreeNode> nodes = new Stack<IBinaryTreeNode>(64);
            nodes.Push(tree.Root);

            while (nodes.Count > 0)
            {
                IBinaryTreeNode node = nodes.Pop();

                if (node == NullBinaryTreeNode.Instance)
                {
                    sb[sb.Length - 1] = ')';
                    sb.Append(" ");
                }
                else if (node == null)
                {
                    sb.Append("N ");
                }
                else
                {
                    sb.Append("(" + node.Value + " ");

                    nodes.Push(NullBinaryTreeNode.Instance);
                    nodes.Push(node.RightChild);
                    nodes.Push(node.LeftChild);
                }
            }

            return sb.ToString();
        }
    }
}
