using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analizator_Algorytmow_Bak_Teska
{
    class Sort
    {
        public int[] BubbleSort(ref double[] T)
        {
            int ODCounter = 0;
            int MemoryCounter = 0;
            for (int i = 0; i < T.Length - 1; i++)
            {
                for (int j = 0; j < T.Length - i - 1; j++)
                {
                    ODCounter++;
                    if (T[j] > T[j + 1])
                    {
                        MemoryCounter++;
                        double temp = T[j];
                        T[j] = T[j + 1];
                        T[j + 1] = temp;
                    }
                }
            }
            return new int[] { ODCounter, MemoryCounter };

        }
        public int[] LibrarySort(ref double[] T)
        {
            int ODCounter = 0;
            int MemoryCounter = 2 + 2 * T.Length;
            double CopyOfElement;
            int j;
            double[] CopyT = new double[T.Length * 2];

            for (int i = 0; i < CopyT.Length; i++)
            {
                if (i % 2 == 0)
                {
                    CopyT[i] = T[i / 2];
                }
            }

            for (int i = 1; i < CopyT.Length; i++)
            {
                j = i;
                CopyOfElement = CopyT[i];
                while ((j > 0) && CopyOfElement < CopyT[j - 1])
                {
                    ODCounter++;
                    CopyT[j] = CopyT[j - 1];
                    j--;
                }
                CopyT[j] = CopyOfElement;
            }

            int Temp = 0;
            MemoryCounter++;
            for (int i = 0; i < CopyT.Length; i++)
            {
                if (CopyT[i] != 0)
                {
                    T[Temp] = CopyT[i];
                    ++Temp;
                }
            }

            return new int[] { ODCounter / 2, MemoryCounter };
        }
        public int[] GnomeSort(ref double[] T)
        {
            int ODCounter = 0;
            int MemoryCounter = 0;
            for (int i = 1; i < T.Length;)
            {
                ODCounter++;
                if (T[i - 1] <= T[i])
                    ++i;
                else
                {
                    MemoryCounter++;
                    double temp = T[i];
                    T[i] = T[i - 1];
                    T[i - 1] = temp;
                    --i;
                    if (i == 0)
                        i = 1;
                }
            }
            return new int[] { ODCounter, MemoryCounter };
        }
        public int[] InsertionSort(ref double[] T)
        {
            int k;
            double copyElementToSwap;
            int ODCounter = 0;
            int MemoryCounter = 1;
            for (int i = 0; i < T.Length; i++)
            {
                copyElementToSwap = T[i];
                k = i;
                while ((k > 0) && (copyElementToSwap < T[k - 1]))
                {
                    ODCounter++;
                    T[k] = T[k - 1];
                    k--;
                }
                if (k > 0)
                {
                    ODCounter++;
                }    
                
                T[k] = copyElementToSwap;
            }
            return new int[] { ODCounter, MemoryCounter };
        }

        public int[] QuickSort<P>(ref double[] T, int d, int g) where P : IComparable<P>
        {
            int i = d;
            int j = g;
            double elementKeeper;
            int indexPivot = (d + g) / 2;
            double pivot = T[indexPivot];
            int ODCounter = 0;
            int MemoryCounter = 5;
            do
            {
                while (pivot.CompareTo(T[i]) > 0)
                {
                    i++;
                    ODCounter++;
                }
                ODCounter++;
                while (pivot.CompareTo(T[j]) < 0)
                {
                    j--;
                    ODCounter++;
                }
                ODCounter++;
                if (i <= j)
                {
                    elementKeeper = T[i];
                    T[i] = T[j];
                    T[j] = elementKeeper;
                    i++;
                    j--;
                }
            } while (i < j);

            if (d < j)
            {
                int[] result = QuickSort<P>(ref T, d, j);
                ODCounter += result[0];
                MemoryCounter += result[1];
            }
            if (i < g)
            {
                int[] result = QuickSort<P>(ref T, i, g);
                ODCounter += result[0];
                MemoryCounter += result[1];
            }

            return new int[] { ODCounter, MemoryCounter };
        }
    }
}
