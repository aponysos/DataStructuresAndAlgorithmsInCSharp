using System;

namespace DataStructuresAndAlgorithmsInCSharp
{
  class RBTree : IBinaryTree
  {
    public class Node : IBinaryTreeNode
    {
      public int data;
      public Node left = null;
      public Node right = null;
      public IBinaryTreeNode LeftChild
      {
        get { return left; }
      }
      public IBinaryTreeNode RightChild
      {
        get { return right; }
      }
      public int Value
      {
        get { return data; }
        set { data = value; }
      }
      public Node(int i)
      {
        this.data = i;
      }
    }

    private Node root = null;

    public IBinaryTreeNode Root
    {
      get { return root; }
    }

  }
}
