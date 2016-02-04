using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructuresAndAlgorithmsInCSharp
{
  internal class Program
  {
    static void Main(string[] args)
    {
      //TestBSTree();
      //TestAVLTree();
      TestRBTree();
      System.Console.Read();
    }

    static void TestBSTree()
    {
      System.Console.WriteLine("Begin Testing BinarySearchTree");

      BinarySearchTree bst = new BinarySearchTree();
      bst.Insert(23);
      bst.Insert(45);
      bst.Insert(16);
      bst.Insert(37);
      bst.Insert(3);
      bst.Insert(99);
      bst.Insert(22);

      bst.Delete(23);
      System.Console.WriteLine("Inorder traversal :");
      bst.Inorder();
      System.Console.WriteLine("Preorder traversal :");
      bst.Preorder();
      System.Console.WriteLine("Postrder traversal :");
      bst.Postorder();

      System.Console.WriteLine(
          "Minimum value : {0}", bst.FindMin().data);
      System.Console.WriteLine(
          "Maximum value : {0}", bst.FindMax().data);
    }

    static void TestAVLTree()
    {
      System.Console.WriteLine("Begin Testing AVLTree");

      AVLTree avlt = new AVLTree();
      avlt.Insert(3);
      avlt.Insert(23);
      avlt.Insert(45);
      avlt.Insert(16);
      avlt.Insert(37);
      avlt.Insert(22);
      avlt.Insert(99);
    }

    static void TestRBTree()
    {
      System.Console.WriteLine("Begin Testing RBTree");

      RBTree rbt = new RBTree();
      rbt.Insert(3);
      rbt.Insert(23);
      rbt.Insert(45);
      rbt.Insert(16);
      rbt.Insert(37);
      rbt.Insert(22);
      rbt.Insert(99);
    }
  }
}
