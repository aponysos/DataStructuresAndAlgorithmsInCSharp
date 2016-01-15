using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortingAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Tester t = new Tester(100);

            System.Console.WriteLine("Selection Sort");
            t.Test(new SelectionSorter());
            System.Console.WriteLine();

            System.Console.WriteLine("Bubble Sort");
            t.Test(new BubbleSorter());
            System.Console.WriteLine();

            System.Console.WriteLine("Insertion Sort");
            t.Test(new InsertionSorter());
            System.Console.WriteLine();

            System.Console.WriteLine("Shell Sort");
            t.Test(new ShellSorter());
            System.Console.WriteLine();

            System.Console.WriteLine("Heap Sort");
            t.Test(new HeapSorter());
            System.Console.WriteLine();

            System.Console.WriteLine("Quick Sort");
            t.Test(new QuickSorter());
            System.Console.WriteLine();

            System.Console.WriteLine("Merge Sort");
            t.Test(new MergeSorter());
            System.Console.WriteLine();

            System.Console.Read();
        }
    }

    class Counter
    {
        private ulong comp_ = 0;
        private ulong swap_ = 0;
        private ulong copy_ = 0;
        public ulong CompTimes
        {
            get
            {
                return comp_;
            }
        }
        public ulong SwapTimes
        {
            get
            {
                return swap_;
            }
        }
        public ulong CopyTimes
        {
            get
            {
                return copy_;
            }
        }
        public void Reset()
        {
            comp_ = copy_ = 0;
        }
        public void Comp()
        {
            ++comp_;
        }
        public void Swap()
        {
            ++swap_;
        }
        public void Copy()
        {
            ++copy_;
        }
    }

    abstract class Sorter
    {
        private Counter c_ = new Counter();
        protected int[] array_;

        protected int Length
        {
            get
            {
                return array_.Length;
            }
        }

        protected int Value(int index)
        {
            return array_[index];
        }

        protected int Comp(int lhs, int rhs)
        {
            c_.Comp();
            return lhs.CompareTo(rhs);
        }
        protected int CompByIndex(int lhs, int rhs)
        {
            return Comp(array_[lhs], array_[rhs]);
        }

        protected void SwapByIndex(int from, int to)
        {
            if (from == to)
                return;

            c_.Swap();
            int temp = array_[to];
            array_[to] = array_[from];
            array_[from] = temp;
        }

        protected bool OrderSwapByIndex(int from, int to)
        {
            if (CompByIndex(from, to) > 0)
            {
                SwapByIndex(from, to);
                return true;
            }
            else
                return false;
        }

        protected void CopyOutByIndex(int from, ref int to)
        {
            c_.Copy();
            to = array_[from];
        }

        protected void CopyInByIndex(int from, int to)
        {
            c_.Copy();
            array_[to] = from;
        }

        protected abstract void Sort_();

        // return a copy of internal counter
        public Counter Sort(int[] array)
        {
            array_ = array;
            c_.Reset();

            Sort_();

            return c_;
        }
    }

    class SelectionSorter : Sorter
    {
        protected override void Sort_()
        {
            for (int i = 0; i < Length - 1; ++i)
            {
                int min = i;
                for (int j = i + 1; j < Length; ++j)
                {
                    if (CompByIndex(min, j) > 0)
                        min = j;
                }
                SwapByIndex(i, min);
            }
        }
    }

    class BubbleSorter : Sorter
    {
        protected override void Sort_()
        {
            for (int i = 0; i < Length - 1; ++i)
            {
                for (int j = Length - 1; j > i; --j)
                {
                    OrderSwapByIndex(j - 1, j);
                }
            }
        }
    }

    class InsertionSorter : Sorter
    {
        protected override void Sort_()
        {
            for (int i = 1; i < Length - 1; ++i)
            {
                for (int j = i + 1; j > 0; --j)
                {
                    if (!OrderSwapByIndex(j - 1, j))
                        break;
                }
            }
        }
    }

    class ShellSorter : Sorter
    {
        protected override void Sort_()
        {
            const int GAP_SHRINK_FACTOR = 2;
            for (int g = Length / GAP_SHRINK_FACTOR; 
                g > 0; g /= GAP_SHRINK_FACTOR)
            {
                for (int h = 0; h < g; ++h)
                {
                    // insertion sort
                    for (int i = h + g; i < Length - g; i += g)
                    {
                        for (int j = i + g; j > h; j -= g)
                        {
                            if (!OrderSwapByIndex(j - g, j))
                                break;
                        }
                    }
                }
            }
        }
    }

    class HeapSorter : Sorter
    {
        // transform the array into a heap
        private void MakeHeap()
        {
            for (int l = (Length - 1) / 2; l >= 0; --l)
                SiftUp(l, Length);
        }

        // sift up the heap from top
        private void SiftUp(int top, int length)
        {
            while (true)
            {
                // make the parent larger than children
                int max = top;
                int left = top * 2 + 1;
                int right = top * 2 + 2;
                if (left < length && CompByIndex(left, max) > 0)
                    max = left;
                if (right < length && CompByIndex(right, max) > 0)
                    max = right;
                SwapByIndex(top, max);

                // heap done
                if (top == max)
                    return;

                // move down
                top = max;
            }
        }

        protected override void Sort_()
        {
            // initialize heap
            MakeHeap();

            for (int i = Length - 1; i > 0 ; --i)
            {
                // top of heap is maximum
                SwapByIndex(0, i);

                // sift up from top
                SiftUp(0, i - 1);
            }
        }
    }

    class QuickSorter : Sorter
    {
        private void QuickSort(int start, int end)
        {
            if (start >= end)
                return;
            else if (start + 1 == end)
                OrderSwapByIndex(start, end);
            else
            {
                int l = start;
                int r = end;
                int p = (l + r) / 2;
                int pivot = Value(p);

                while (l < r)
                {
                    while (l <= end && Comp(Value(l), pivot) <= 0)
                        ++l;
                    while (r >= start && Comp(pivot, Value(r)) < 0)
                        --r;

                    if (l < r)
                        SwapByIndex(l++, r--);
                }

                QuickSort(start, r - 1);
                QuickSort(l, end);
            }
        }

        protected override void Sort_()
        {
            QuickSort(0, Length - 1);
        }
    }

    class MergeSorter : Sorter
    {
        private void RecMergeSort(int lbound, int ubound)
        {
            if (lbound == ubound)
                return;
            else if (lbound + 1 == ubound)
                OrderSwapByIndex(lbound, ubound);
            else
            {
                int mid = (lbound + ubound) / 2;
                RecMergeSort(lbound, mid);
                RecMergeSort(mid + 1, ubound);
                Merge(lbound, mid, ubound);
            }
        }

        private void Merge(int lbound, int mid, int ubound)
        {
            int[] tmp = new int[Length];
            int i = lbound, j = mid + 1, k = lbound;

            while (i <= mid && j <= ubound)
            {
                if (CompByIndex(i, j) <= 0)
                    CopyOutByIndex(i++, ref tmp[k++]);
                else
                    CopyOutByIndex(j++, ref tmp[k++]);
            }

            for (; i <= mid; ++i)
                CopyOutByIndex(i, ref tmp[k++]);
            for (; j <= ubound; ++j)
                CopyOutByIndex(j, ref tmp[k++]);

            for (k = lbound; k <= ubound; ++k)
                CopyInByIndex(tmp[k], k);
        }

        protected override void Sort_()
        {
            RecMergeSort(0, Length - 1);
        }
    }

    class Sampler
    {
        private int size_ = 0;
        private int[] array_;

        private void Reset()
        {
            for (int i = 0; i < array_.Length; ++i)
                array_[i] = 0;
        }

        public Sampler(int size)
        {
            size_ = size;
            array_ = new int[size_];
            Reset();
        }

        public int[] GetSamples()
        {
            Reset();
            for (int i = 0; i < size_; ++i)
            {
                // compute initial position j
                int j = size_ / 5 + i;
                j = j * j;
                j %= size_;

                // find next empty position after j
                while (array_[j] != 0)
                    j = (j + 1) % (size_);

                array_[j] = i + 1;
            }
            return array_;
        }

        public override string ToString()
        {
            System.Text.StringBuilder sb =
                new System.Text.StringBuilder(array_.Length * 2);
            foreach (int i in array_)
                sb.Append(i + " ");
            return sb.ToString();
        }
    }

    class Tester
    {
        Sampler smp_;

        public Tester(int size)
        {
            smp_ = new Sampler(size);
        }

        private static void Output(int[] array, string prefix)
        {
            System.Console.Write(prefix);
            foreach (int i in array)
                System.Console.Write("{0} ", i);
            System.Console.WriteLine();
        }

        public void Test(Sorter s)
        {
            int[] a = smp_.GetSamples();
            //Output(a, "Samples : ");

            Counter c = s.Sort(a);
            //Output(a, "Results : ");

            System.Console.WriteLine(
                "CompTimes : {0}", c.CompTimes);
            System.Console.WriteLine(
                "SwapTimes : {0}", c.SwapTimes);
            System.Console.WriteLine(
                "CopyTimes : {0}", c.CopyTimes);
        }
    }

}
