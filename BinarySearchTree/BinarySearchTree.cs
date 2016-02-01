using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructuresAndAlgorithmsInCSharp
{
  public class BinarySearchTree
  {
    public class Node
    {
      public int data;
      public Node left = null;
      public Node right = null;

      public Node(int i)
      {
        data = i;
      }

      public override string ToString()
      {
        return data.ToString();
      }

      // choose a next node for a given value from this node  
      // based on comparison with current data value.
      // return this node if its value equals i.
      public Node ChooseNode(int i)
      {
        if (i < data)
          return left;
        else if (i > data)
          return right;
        else // i == data
          return this;
      }

      // make a node as a (left or right) child of this node.
      public Node MakeLeftChild(Node child)
      {
        this.left = child;
        return child;
      }
      public Node MakeRightChild(Node child)
      {
        this.right = child;
        return child;
      }

      // substitude child node
      // return true if substituted
      public bool SubstituteChild(Node oldChild, Node newChild)
      {
        if (left == oldChild)
          MakeLeftChild(newChild);
        else if (right == oldChild)
          MakeRightChild(newChild);
        else return false;

        return true;
      }
    }

    // constructor
    // initialze root node to null.
    public BinarySearchTree()
    {
      root = null;
    }

    // return true if this tree is empty.
    public bool IsEmpty()
    {
      return root == null;
    }

    #region Find and Insert
    // find a node with value i.
    // if it does not exist, insert a node with value i 
    // depending on parameter insert.
    private Node FindInsert(int i, bool insert)
    {
      if (IsEmpty())
      {
        if (insert)
          return root = new Node(i);
        else
          return null;
      }
      else
      {
        Node current = root;
        while (true)
        {
          if (current.data == i)
            return current;
          else if (current.data > i)
          {
            if (current.left != null)
              current = current.left;
            else
            {
              if (insert)
                return current.MakeLeftChild(
                    new Node(i));
              else
                return null;
            }
          } // current.data > i
          else if (current.data < i)
          {
            if (current.right != null)
              current = current.right;
            else
            {
              if (insert)
                return current.MakeRightChild(
                    new Node(i));
              else
                return null;
            }
          } // current.data < i
        } // while
      } // tree not empty
    }

    // insert a node with value i.
    public Node Insert(int i)
    {
      return FindInsert(i, true);
    }

    // find the node equal to a given value.
    // return null if node not found.
    public Node Find(int i)
    {
      return FindInsert(i, false);
    }

    // find the parent node of a given value.
    // return null if its value equals root's value.
    // this value will be inserted as the child of parent node.
    private Node FindParent(int i)
    {
      if (IsEmpty())
        return null;

      Node current = root;
      Node next = root.ChooseNode(i);
      while (next != current && next != null)
      {
        current = next;
        next = next.ChooseNode(i);
      }

      return current == next ? null : current;
    }

    // find the node with minimum value.
    // return null if tree is empty.
    public Node FindMin()
    {
      return FindParent(int.MinValue);
    }

    // find the node with maximum value.
    // return null if tree is empty.
    public Node FindMax()
    {
      return FindParent(int.MaxValue);
    }
    #endregion

    #region Delete
    // delete the node with value i.
    // return the deleted node.
    private bool SubstituteChild(
        Node parent, Node oldChild, Node newChild)
    {
      if (parent == null)
        root = newChild;
      else
        return parent.SubstituteChild(oldChild, newChild);

      return true;
    }

    public Node Delete(int i)
    {
      Node parent = FindParent(i);
      Node delete = Find(i);

      if (delete != null)
      {
        if (delete.right == null)
          SubstituteChild(parent, delete, delete.left);
        else
        {
          Node right = delete.right;
          if (right.left == null)
          {
            right.MakeLeftChild(delete.left);
            SubstituteChild(parent, delete, right);
          }
          else
          {
            Node left = right.left;
            while (left.left != null)
            {
              right = left;
              left = right.left;
            }
            right.MakeLeftChild(left.right);

            // substitude delete with left
            left.MakeLeftChild(delete.left);
            left.MakeRightChild(delete.right);
            SubstituteChild(parent, delete, left);
          }
        }
      } // delete != null

      return delete;
    }
    #endregion

    #region Traversal
    // inorder traversal
    public void Inorder()
    {
      InorderTraverse(root);
    }

    public static void InorderTraverse(Node root)
    {
      if (root != null)
      {
        InorderTraverse(root.left);
        System.Console.WriteLine(root);
        InorderTraverse(root.right);
      }
    }

    // preorder traversal
    public void Preorder()
    {
      PreorderTraverse(root);
    }

    public static void PreorderTraverse(Node root)
    {
      if (root != null)
      {
        System.Console.WriteLine(root);
        PreorderTraverse(root.left);
        PreorderTraverse(root.right);
      }
    }

    // postorder traversal
    public void Postorder()
    {
      PostorderTraverse(root);
    }

    public static void PostorderTraverse(Node root)
    {
      if (root != null)
      {
        PostorderTraverse(root.left);
        PostorderTraverse(root.right);
        System.Console.WriteLine(root);
      }
    }
    #endregion

    // root node
    // default value is null
    private Node root = null;
  }
}
