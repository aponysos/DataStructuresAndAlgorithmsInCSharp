namespace SkipList
{
  public class SkipList
  {
    public class Node
    {
      public int value;
      public Node down;
      public Node next;

      public Node()
      {
        value = 0;
        down = null;
        next = null;
      }
      public Node(int value, Node down, Node next)
      {
        this.value = value;
        this.down = down;
        this.next = next;
      }
    }

    private Node head;
    private System.Random rand = new System.Random();

    public SkipList()
    {
    }
    public bool Init()
    {
      head = new Node(0, null, null);
      return true;
    }
    public bool Clear()
    {
      head = null;
      return true;
    }

    public bool Insert(int value)
    {
      if (head.next == null)
      {
        Node newNode = new Node(value, null, null);
        head.next = newNode;
        return true;
      }

      Node current = head;
      if (head.next.next != null)
      {
        //If random > 0.7, raise the hight of level                
        double random = rand.NextDouble();      //get random between 0.0 and 1.0                
        if (random > 0.75)
        {
          //Check vaule if exists in highest level
          current = head.next;
          while (current != null)
          {
            if (value == current.value)
            {
              return false;
            }
            current = current.next;
          }

          current = head;

          if (head.next.value < value)
          {
            Node newNode = new Node(head.next.value, null, null);
            newNode.down = head.next;
            head.next = newNode;
            newNode.next = new Node(value, null, null);
          }
          else
          {
            Node newNode = new Node(value, null, null);
            newNode.next = new Node(head.next.value, null, null);
            newNode.next.down = head.next;
            head.next = newNode;
          }

          //If value insert to the first note of list
          if (head.next.value == value)
          {
            current = head.next;
            while (current.next.down != null)
            {
              Node newNode = new Node(value, null, null);
              newNode.next = current.next.down;
              current.down = newNode;
              current = current.down;
            }
            return true;
          }

          current = head.next.down;
          Node upNode = head.next.next;
          do
          {
            while (current.next != null && value > current.next.value)
            {
              current = current.next;
            }
            if (current.next != null && value == current.next.value)
            {
              upNode.down = current.next;
              return false;
            }
            Node newNode = new Node(value, null, null);
            upNode.down = newNode;
            upNode = newNode;
            newNode.next = current.next;
            current.next = newNode;
            current = current.down;
          } while (current != null);

          return true;
        }
      }

      current = head;
      do
      {
        while (current.next != null && value > current.next.value)
        {
          current = current.next;
        }
        if (current.next != null && value == current.next.value)
        {
          return false;
        }
        //insert in the lowest level
        if (current.down == null)
        {
          Node newNode = new Node(value, null, null);
          newNode.next = current.next;
          current.next = newNode;
          return true;
        }

        //while gap>3, insert in current and lower level
        if (current.down.next != null && current.down.next.next != null && value > current.down.next.next.value)
        {
          Node rootNode = new Node(value, null, null);
          rootNode.next = current.next;
          current.next = rootNode;
          current = current.down.next.next;
          while (current != null)
          {
            while (current.next != null && value > current.next.value)
            {
              current = current.next;
            }
            if (current.next != null && value == current.next.value)
            {
              return false;
            }
            Node newNode = new Node(value, null, null);
            rootNode.down = newNode;
            rootNode = newNode;
            newNode.next = current.next;
            current.next = newNode;
            current = current.down;
          }
          return true;
        }
        current = current.down;
      } while (current != null);

      return true;
    }

    public bool Search(int value)
    {
      if (head.next == null)
      {
        return false;
      }

      Node current = head;
      do
      {
        while (current.next != null && value > current.next.value)
        {
          current = current.next;
        }
        if (current.next != null && value == current.next.value)
        {
          return true;
        }
        current = current.down;
      } while (current != null);
      return false;
    }

    public bool Remove(int value)
    {
      if (head.next == null)
      {
        return false;
      }
      Node current = head;
      do
      {
        while (current.next != null && value > current.next.value)
        {
          current = current.next;
        }
        if (current.next != null && value == current.next.value)
        {
          current.next = current.next.next;
          current = current.down;
          continue;
        }
        current = current.down;
      }
      while (current != null);
      return true;
    }

    public string Display()
    {
      if (head.next == null)
      {
        return "";
      }

      Node current = head;
      Node level = head.next;
      System.Text.StringBuilder rlt = new System.Text.StringBuilder();
      while (level != null)
      {
        current = level;
        do
        {
          rlt.Append("——").Append(current.value);
          current = current.next;
        } while (current != null);
        rlt.AppendLine();
        level = level.down;
      }

      System.Console.WriteLine(rlt);
      return rlt.ToString();
    }
  }
}
