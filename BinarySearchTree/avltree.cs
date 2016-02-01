/*
 * Created by SharpDevelop.
 * User: Aponysos
 * Date: 2010/6/27
 * Time: 15:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace DataStructuresAndAlgorithmsInCSharp
{
  using System.Collections.Generic;

  /// <summary>
  /// AVL Tree
  /// </summary>
  public class AVLTree : IBinaryTree
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
      public int height = 0;
      public void UpdateHeight()
      {
        this.height = Math.Max(Height(left), Height(right)) + 1;
      }
    }

    private static int Height(Node n)
    {
      return n == null ? -1 : n.height;
    }

    private Node root = null;

    public IBinaryTreeNode Root
    {
      get { return root; }
    }

    public AVLTree()
    {
    }

    public void Insert(int i)
    {
      root = Insert(i, root);
      Print();
    }

    private Node Insert(int i, Node n)
    {
      if (n == null)
        n = new Node(i);
      else if (i < n.data)
      {
        n.left = Insert(i, n.left);
        if (Height(n.left) - Height(n.right) == 2)
        {
          if (i < n.left.data)
            n = RotateWithLeftChild(n); // LL
          else
            n = DoubleRotateWithLeftChild(n); // LR
        }
      }
      else if (i > n.data)
      {
        n.right = Insert(i, n.right);
        if (Height(n.right) - Height(n.left) == 2)
        {
          if (i > n.right.data)
            n = RotateWithRightChild(n); // RR
          else
            n = DoubleRotateWithRightChild(n); // RL
        }
      }
      else
      {
        // duplicat value, do nothing
      }

      n.UpdateHeight();
      return n;
    }

    private Node RotateWithLeftChild(Node n)
    {
      Node leftChild = n.left;

      if (leftChild != null)
      {
        n.left = leftChild.right;
        leftChild.right = n;

        n.UpdateHeight();
        leftChild.UpdateHeight();

        return leftChild;
      }
      else
        return n;
    }
    private Node RotateWithRightChild(Node n)
    {
      Node rightChild = n.right;

      if (rightChild != null)
      {
        n.right = rightChild.left;
        rightChild.left = n;

        n.UpdateHeight();
        rightChild.UpdateHeight();

        return rightChild;
      }
      else
        return n;
    }
    private Node DoubleRotateWithLeftChild(Node n)
    {
      n.left = RotateWithRightChild(n.left);
      return RotateWithLeftChild(n);
    }
    private Node DoubleRotateWithRightChild(Node n)
    {
      n.right = RotateWithLeftChild(n.right);
      return RotateWithRightChild(n);
    }

    public void Print()
    {
      Console.WriteLine(TreePrinter.Print(this));
    }
  }
}
