namespace DataStructuresAndAlgorithmsInCSharp
{
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
      public override string ToString()
      {
        return Value.ToString();
      }
      public Node(int i)
      {
        this.data = i;
      }
      public int height = 0;
      public void UpdateHeight()
      {
        this.height = System.Math.Max(Height(left), Height(right)) + 1;
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
      System.Console.WriteLine(TreePrinter.Print(this));
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
            n = RotateLL(n); // LL
          else
            n = RotateLR(n); // LR
        }
      }
      else if (i > n.data)
      {
        n.right = Insert(i, n.right);
        if (Height(n.right) - Height(n.left) == 2)
        {
          if (i > n.right.data)
            n = RotateRR(n); // RR
          else
            n = RotateRL(n); // RL
        }
      }
      else
      {
        // duplicat value, do nothing
      }

      n.UpdateHeight();
      return n;
    }

    private Node RotateLL(Node n)
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
    private Node RotateRR(Node n)
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
