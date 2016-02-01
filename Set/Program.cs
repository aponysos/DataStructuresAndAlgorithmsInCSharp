using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructuresAndAlgorithmsInCSharp
{
  class Program
  {
    static void Main(string[] args)
    {
      Set a = new Set();
      a.Add(1);
      a.Add(2);
      a.Add(3);
      Console.WriteLine("a : " + a);
      Set b = new Set();
      b.Add(2);
      b.Add(3);
      Console.WriteLine("b : " + b);
      Set c = new Set();
      c = a.Union(b);
      Console.WriteLine("a.Union(b) : " + c);
      c = a.Intersection(b);
      Console.WriteLine("a.Intersection(b) : " + c);
      c = a.Difference(b);
      Console.WriteLine("a.Difference(b) : " + c);
      bool flag;
      flag = a.IsSubsetOf(b);
      Console.WriteLine("a.IsSubsetOf(b) : " + flag);
      flag = b.IsSubsetOf(a);
      Console.WriteLine("b.IsSubsetOf(a) : " + flag);

      Console.Read();
    }
  }

  public class Set
  {
    private BitArray data = new BitArray(5);

    public void Add(int element)
    {
      data[element] = true;
    }

    public void Remove(int element)
    {
      data[element] = false;
    }

    public bool IsMember(int element)
    {
      return data[element];
    }

    private delegate bool BooleanOperation(bool b1, bool b2);

    private Set BinaryOperation(Set set, BooleanOperation bop)
    {
      Set result = new Set();
      for (int i = 0; i < data.Count; ++i)
        result.data[i] = bop(this.data[i], set.data[i]);
      return result;
    }

    public Set Union(Set set)
    {
      return BinaryOperation(
          set, delegate(bool b1, bool b2)
          {
            return b1 || b2;
          }
          );
    }

    public Set Intersection(Set set)
    {
      return BinaryOperation(
          set, delegate(bool b1, bool b2)
          {
            return b1 && b2;
          }
          );
    }

    public Set Difference(Set set)
    {
      return BinaryOperation(
          set, delegate(bool b1, bool b2)
          {
            return b1 && !b2;
          }
          );
    }

    public bool IsSubsetOf(Set set)
    {
      for (int i = 0; i < data.Count; ++i)
        if (this.data[i] && !set.data[i])
          return false;
      return true;
    }

    public override string ToString()
    {
      string s = "";
      for (int i = 0; i < data.Count; ++i)
        if (data[i]) s += i;
      return s;
    }
  } // class Set
} // namespace DataStructuresAndAlgorithmsInCSharp
