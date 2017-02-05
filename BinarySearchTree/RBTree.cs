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
      public override string ToString()
      {
        return (this.Color == RED ? "R" : "") + Value.ToString();
      }
      public Node(int i, int Color)
      {
        this.data = i;
        this.Color = Color; // 0 is black, 1 is red
      }
    }

    const int BLACK = 0;
    const int RED = 1;

    private Node root = null;

    public IBinaryTreeNode Root
    {
      get { return root; }
    }

    private static bool IsBlack(Node n)
    {
      return n == null || n.Color == BLACK;
    }
    private static bool IsRed(Node n)
    {
      return n != null && n.Color == RED;
    }

    public void Insert(int i)
    {
      if (root == null)
        root = new Node(i, BLACK); // root node must be BLACK
      else
        root = Insert(i, root);

      System.Console.WriteLine(TreePrinter.Print(this));
    }

    private Node Insert(int i, Node n)
    {
      if (n == null)
        n = new Node(i, RED); // new node is RED
      else if (i < n.data)
      {
        n.left = Insert(i, n.left);
        n.left.father = n;
        if (IsRed(n.left) && (IsRed(n.left.left) || IsRed(n.left.right)))
        {
          if (IsRed(n.right))
          {
            n.Color = RED;
            n.right.Color = BLACK;
            n.left.Color = BLACK;
          }
          else
          {
            if (IsRed(n.left.left))
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
        if (IsRed(n.right) && (IsRed(n.right.left) || IsRed(n.right.right)))
        {
          if (IsRed(n.left))
          {
            n.Color = RED;
            n.left.Color = BLACK;
            n.right.Color = BLACK;
          }
          else
          {
            if (IsRed(n.right.right))
              n = RotateRR(n); // RR
            else
              n = RotateRL(n); // RL
          }
        }
      }
      else
      {
        // duplicate value, do nothing
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

        leftChild.Color = BLACK;
        n.Color = RED;
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

        rightChild.Color = BLACK;
        n.Color = RED;
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
