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
            Console.WriteLine(lcs.LongestCommonSubsquence);
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
        public string LongestCommonSubsquence
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
        public void Compute()
        {
            ComputeArray(); // compute internal array
            DisplayArray(); // display internal array
            BackTrack(ls_, lt_); // back track the subsequence
        }
        private void ComputeArray()
        {
            for (int i = 1; i <= ls_; ++i)
                for (int j = 1; j <= lt_; ++j)
                    if (s_[i - 1] == t_[j - 1])
                        arr_[i, j] = arr_[i - 1, j - 1] + 1;
                    else
                        arr_[i, j] = Math.Max(arr_[i, j - 1], arr_[i - 1, j]);
        }
        private void DisplayArray()
        {
            Console.WriteLine("  {0}", t_);
            for (int i = 0; i <= ls_; ++i)
            {
                if (i > 0)
                    Console.Write(s_[i - 1]);
                else
                    Console.Write(" ");
                for (int j = 0; j <= lt_; ++j)
                    Console.Write(arr_[i, j]);
                Console.WriteLine();
            }
        }
        private void BackTrack(int i, int j)
        {
            if (i == 0 || j == 0)
                return;
            else if (s_[i - 1] == t_[j - 1])
            {
                lcs_.Insert(0, s_[i - 1]); // prepend
                BackTrack(i - 1, j - 1);
            }
            else if (arr_[i, j - 1] > arr_[i - 1, j])
                BackTrack(i, j - 1);
            else
                BackTrack(i - 1, j);
        }
    }
}
