using System;
using System.Text;

namespace DynamicProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            TestLCS();
        }
        static void TestLCS()
        {
            Console.WriteLine("TestLCS:");
            LCSAlgorithm lcs = new LCSAlgorithm("ABCBDAB", "BDCABA");
            lcs.Compute();
            lcs.DisplayArray();
            Console.WriteLine(lcs.LongestCommonSubstring);
        }
    }
    class LCSAlgorithm
    {
        private string s_;
        private string t_;
        private int ls_;
        private int lt_;
        private int [,] arr_;
        private StringBuilder lcs_;
        public string LongestCommonSubstring
        {
            get { return lcs_.ToString(); }
        }
        public LCSAlgorithm(string s, string t)
        {
            s_ = s;
            t_ = t;
            ls_ = s.Length;
            lt_ = t.Length;
            arr_ = new int[ls_ + 1, lt_ + 1];
            for (int i = 0; i <= ls_; ++i)
                arr_[i, 0] = 0;
            for (int j = 0; j <= lt_; ++j)
                arr_[0, j] = 0;
            lcs_ = new StringBuilder();
        }
        public void DisplayArray()
        {
            for (int i = 0; i <= ls_; ++i)
            {
                for (int j = 0; j <= lt_; ++j)
                    Console.Write(arr_[i, j]);
                Console.WriteLine();
            }
        }
        public void Compute()
        {
            for (int i = 1; i <= ls_; ++i)
                for (int j = 1; j <= lt_; ++j)
                    if (s_[i] == t_[j])
                        arr_[i, j] = arr_[i - 1, j - 1] + 1;
                    else
                        arr_[i, j] = 0;
            int z = 0;
            for (int i = 1; i <= ls_; ++i)
                for (int j = 1; j <= lt_; ++j)
                    if (arr_[i, j] > z)
                    {
                        z = arr_[i, j];
                        lcs_ = new StringBuilder(s_.Substring(i - z + 1, z - 1));
                    }
                    else if (arr_[i, j] == z)
                        lcs_.Append(s_.Substring(i - z + 1, z - 1));
        }
    }
}
