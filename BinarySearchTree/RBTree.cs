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
      public Node father = null;
      public int Value
      {
        get { return data; }
        set { data = value; }
      }
      public IBinaryTreeNode LeftChild
      {
        get { return left; }
      }
      public IBinaryTreeNode RightChild
      {
        get { return right; }
      }
      public int Color
      {
        get;
        set;
      }
      public Node(int i)
      {
        this.data = i;
        this.Color = BLACK; // 0 is black, 1 is red
      }
    }

    const int BLACK = 0;
    const int RED = 1;

    private Node root = null;

    public IBinaryTreeNode Root
    {
      get { return root; }
    }

    public void Insert(int i)
    {
      root = Insert(i, root);
    }

    private Node Insert(int i, Node n)
    {
      if (n == null)
        n = new Node(i);
      else if (i < n.data)
      {
        n.left = Insert(i, n.left);
        n.left.father = n;
        n.left.Color = RED;
        if (n.Color == RED)
        {
          if (n.father.right.Color == RED)
          {
            n.father.Color = RED;
            n.father.right.Color = BLACK;
            n.Color = BLACK;
          }
          else
          {
            if (i < n.left.data)
              n = RotateLL(n); // LL
            else
              n = RotateLR(n); // LR
          }
        }
      }
      else if (i > n.data)
      {
        n.right = Insert(i, n.right);
        n.right.father = n;
        n.right.Color = RED;
        if (n.Color == RED)
        {
          if (n.father.left.Color == RED)
          {
            n.father.Color = RED;
            n.father.left.Color = BLACK;
            n.Color = BLACK;
          }
          else
          {
            if (i > n.right.data)
              n = RotateRR(n); // RR
            else
              n = RotateRL(n); // RL
          }
        }
      }
      else
      {
        // duplicat value, do nothing
      }

      return n;
    }

    private Node RotateLL(Node n)
    {
      Node leftChild = n.left;

      if (leftChild != null)
      {
        n.left = leftChild.right;
        leftChild.right = n;

        return leftChild;
      }
      else
        return n;
    }
    private Node RotateRR(Node n)
    {
      Node rightChild = n.right;

      if (rightChild != null)
      {
        n.right = rightChild.left;
        rightChild.left = n;

        return rightChild;
      }
      else
        return n;
    }
    private Node RotateLR(Node n)
    {
      n.left = RotateRR(n.left);
      return RotateLL(n);
    }
    private Node RotateRL(Node n)
    {
      n.right = RotateLL(n.right);
      return RotateRR(n);
    }
  }
}
