using System;
using System.Collections.Generic;

namespace Sudoku
{
    public class Sudoku
    {
        public int[,] table = {
                 { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
                ,{ 4, 5, 6, 7, 8, 9, 1, 2, 3 }
                ,{ 7, 8, 9, 1, 2, 3, 4, 5, 6 }
                ,{ 2, 3, 4, 5, 6, 7, 8, 9, 1 }
                ,{ 5, 6, 7, 8, 9, 1, 2, 3, 4 }
                ,{ 8, 9, 1, 2, 3, 4, 5, 6, 7 }
                ,{ 3, 4, 5, 6, 7, 8, 9, 1, 2 }
                ,{ 6, 7, 8, 9, 1, 2, 3, 4, 5 }
                ,{ 9, 1, 2, 3, 4, 5, 6, 7, 8 }
        };

        public void Show()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(table[i, j] + " ");
                    Console.Write((j + 1) % 3 == 0 ? "|" : " ");
                    Console.Write(" ");
                }
                Console.WriteLine((i + 1) % 3 == 0 ? "\n---------------------------------" : "");
            }
        }
    }

    public static class Hider
    {
        static Random rnd = new Random();

        public static void Hide(this Sudoku sudoku)
        {
            for (int i = 0; i < 9; i++)
            {
                int hidedInRow = rnd.Next(4, 7);
                List<int> toHide = new List<int>();
                for (int h = 0; h <= hidedInRow; h++)
                {
                    int rand = rnd.Next(0, 9);
                    while (toHide.Contains(rand))
                        rand = rnd.Next(0, 9);
                    toHide.Add(rand);
                }
                foreach (var item in toHide)
                {
                    sudoku.table[i, item] = 0;
                }
            }
        }
    }

    public class Switcher
    {
        int numberOfSwaps;
        public Sudoku Sudoku;
        public Switcher(int numberOfSwaps)
        {
            this.numberOfSwaps = numberOfSwaps;
            Sudoku = new Sudoku();
        }
        static Random rnd = new Random((int)(DateTime.Now.Ticks));

        static int RandomGenerator(Random rnd, out int first, out int second)
        {
            first = rnd.Next(0, 3);
            second = rnd.Next(0, 3);
            while (second == first)
                second = rnd.Next(0, 3);
            return rnd.Next(0, 3);
        }

        public void Swap()
        {
            for (int i = 0; i <= numberOfSwaps; i++)
            {
                int swapCase = rnd.Next(1, 5);
                switch (swapCase)
                {
                    case 1:
                        SwitchRowSmall(ref this.Sudoku);
                        break;
                    case 2:
                        SwitchRowBig(ref this.Sudoku);
                        break;
                    case 3:
                        SwitchRowCol(ref this.Sudoku);
                        SwitchRowBig(ref this.Sudoku);
                        SwitchRowCol(ref this.Sudoku);
                        break;
                    case 4:
                        SwitchRowCol(ref this.Sudoku);
                        SwitchRowSmall(ref this.Sudoku);
                        SwitchRowCol(ref this.Sudoku);
                        break;
                    case 5:
                        SwitchRowCol(ref this.Sudoku);
                        break;
                }
            }
        }

        void SwitchRowCol(ref Sudoku sudoku)
        {
            int[,] newArr = new int[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    newArr[i, j] = sudoku.table[j, i];
                }
            }
            sudoku.table = newArr;
        }

        void SwitchRowSmall(ref Sudoku sudoku)
        {
            int firstRowToSwap;
            int secondRowToSwap;
            int rowBig = RandomGenerator(rnd, out firstRowToSwap, out secondRowToSwap);
            int[] newArr = new int[9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    newArr[i] = sudoku.table[rowBig * 3 + firstRowToSwap, i];
                    sudoku.table[rowBig * 3 + firstRowToSwap, i] = sudoku.table[rowBig * 3 + secondRowToSwap, i];
                    sudoku.table[rowBig * 3 + secondRowToSwap, i] = newArr[i];
                }
            }
        }
        
        void SwitchRowBig(ref Sudoku sudoku)
        {
            int firstRowToSwap;
            int secondRowToSwap;
            RandomGenerator(rnd, out firstRowToSwap, out secondRowToSwap);
            int[] newArr = new int[9];
            for (int k = 0; k < 3; k++)
            {
                for (int i = 0; i < 9; i++)
                {
                    newArr[i] = sudoku.table[firstRowToSwap * 3 + k, i];
                    sudoku.table[firstRowToSwap * 3 + k, i] = sudoku.table[secondRowToSwap * 3 + k, i];
                    sudoku.table[secondRowToSwap * 3 + k, i] = newArr[i];
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Switcher switcher = new Switcher(65);
            Console.WriteLine("initial");
            switcher.Sudoku.Show();

            switcher.Swap();
            Console.WriteLine("\nswaped");
            switcher.Sudoku.Show();

            switcher.Sudoku.Hide();
            Console.WriteLine("\nhided");
            switcher.Sudoku.Show();

            Console.ReadLine();
        }
    }
}
