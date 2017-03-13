using System;

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
            LCSAlgorithm lcs = new LCSAlgorithm("", "");
            Console.WriteLine(lcs.LongestCommonSubstring);
        }
    }
    class LCSAlgorithm
    {
        private string s_;
        private string t_;
        private string lcs_;
        public string LongestCommonSubstring
        {
            get { return lcs_; }
        }
        public LCSAlgorithm(string s, string t)
        {
            s_ = s;
            t_ = t;
        }
        public void Compute()
        {
        }
    }
}
