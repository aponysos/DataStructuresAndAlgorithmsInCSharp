namespace SkipList
{
  class Program
  {
    static void Main(string[] args)
    {
      TestSkipList();
      System.Console.Read();
    }

    static void TestSkipList()
    {
      SkipList sl = new SkipList();
      sl.Init();
      sl.Display();
      sl.Insert(1);
      sl.Insert(2);
      sl.Insert(3);
      sl.Insert(4);
      sl.Insert(5);
      sl.Insert(6);
      sl.Insert(7);
      sl.Insert(8);
      sl.Insert(9);
      sl.Display();
    }
  }
}
