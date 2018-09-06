﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Sudoku
    {
        public List<List<int>> table;
        public void Show()
        {
            for (int i = 0; i < table.Count; i++)
            {
                List<int> innterList = table[i];
                for (int j = 0; j < innterList.Count; j++)
                {
                    Console.Write(innterList[j] + " ");
                    Console.Write((j + 1) % 3 == 0 ? "|" : " ");
                    Console.Write(" ");
                }
                Console.WriteLine((i + 1) % 3 == 0 ? "\n---------------------------------" : "");
            }
        }
        public Sudoku()
        {
            table = new List<List<int>> {
                  new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
                , new List<int> { 4, 5, 6, 7, 8, 9, 1, 2, 3 }
                , new List<int> { 7, 8, 9, 1, 2, 3, 4, 5, 6 }
                , new List<int> { 2, 3, 4, 5, 6, 7, 8, 9, 1 }
                , new List<int> { 5, 6, 7, 8, 9, 1, 2, 3, 4 }
                , new List<int> { 8, 9, 1, 2, 3, 4, 5, 6, 7 }
                , new List<int> { 3, 4, 5, 6, 7, 8, 9, 1, 2 }
                , new List<int> { 6, 7, 8, 9, 1, 2, 3, 4, 5 }
                , new List<int> { 9, 1, 2, 3, 4, 5, 6, 7, 8 } };
        }
    }
    public static  class Hider
    {
        static Random rnd = new Random();
        
        public static  void Hide(this Sudoku sudoku)
        {
            foreach (var row in sudoku.table)
            {
                int hidedInRow = rnd.Next(4, 8);
                List<int> toHide = new List<int>();
                for (int i = 0; i <= hidedInRow; i++)
                {
                    int rand = rnd.Next(0, 9);
                    while (toHide.Contains(rand))
                        rand = rnd.Next(0, 9);
                        toHide.Add(rand);
                }
                foreach (var item in toHide)
                {
                    row[item] = 0;
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
            first = rnd.Next(1, 4);
            second = rnd.Next(1, 4);
            while (second == first)
                second = rnd.Next(1, 4);
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
                        SwitchColumnBig(ref this.Sudoku);
                        break;
                    case 4:
                        SwitchColumnSmall(ref this.Sudoku);
                        break;
                }
            }
        }

        void SwitchRowSmall(ref Sudoku sudoku)
        {
            int firstRowToSwap;
            int secondRowToSwap;
            int rowBig = RandomGenerator(rnd, out firstRowToSwap, out secondRowToSwap);

            sudoku.table.Insert(0, new List<int>());
            sudoku.table[0] = sudoku.table[rowBig * 3 + firstRowToSwap];
            sudoku.table[rowBig * 3 + firstRowToSwap] = sudoku.table[rowBig * 3 + secondRowToSwap];
            sudoku.table[rowBig * 3 + secondRowToSwap] = sudoku.table[0];
            sudoku.table.RemoveAt(0);
        }

        void SwitchRowBig(ref Sudoku sudoku)
        {
            int firstRowToSwap;
            int secondRowToSwap;

            RandomGenerator(rnd, out firstRowToSwap, out secondRowToSwap);
            sudoku.table.InsertRange(0, new List<List<int>> { new List<int>(), new List<int>(), new List<int>() });
            for (int i = 0; i < 3; i++)
            {
                sudoku.table[i] = sudoku.table[firstRowToSwap * 3 + i];
                sudoku.table[firstRowToSwap * 3 + i] = sudoku.table[secondRowToSwap * 3 + i];
                sudoku.table[secondRowToSwap * 3 + i] = sudoku.table[i];
            }
            sudoku.table.RemoveRange(0, 3);
        }
        void SwitchColumnBig(ref Sudoku sudoku)
        {
            int firstColToSwap;
            int secondColToSwap;
            RandomGenerator(rnd, out firstColToSwap, out secondColToSwap);
            foreach (var row in sudoku.table)
            {
                row.InsertRange(0, new List<int> { 0, 0, 0 });
                for (int i = 0; i < 3; i++)
                {
                    row[i] = row[firstColToSwap * 3 + i];
                    row[firstColToSwap * 3 + i] = row[secondColToSwap * 3 + i];
                    row[secondColToSwap * 3 + i] = row[i];
                }
                row.RemoveRange(0, 3);
            }

        }
        void SwitchColumnSmall(ref Sudoku sudoku)
        {
            int firstRowToSwap;
            int secondRowToSwap;
            int columnBig = RandomGenerator(rnd, out firstRowToSwap, out secondRowToSwap);

            foreach (var row in sudoku.table)
            {
                row.Insert(0, 0);
                row[0] = row[columnBig * 3 + firstRowToSwap];
                row[columnBig * 3 + firstRowToSwap] = row[columnBig * 3 + secondRowToSwap];
                row[columnBig * 3 + secondRowToSwap] = row[0];
                row.RemoveAt(0);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Switcher initializer = new Switcher(16);
            initializer.Sudoku.Show();
            Console.WriteLine("\n");
            initializer.Swap();
            initializer.Sudoku.Show();

            initializer.Sudoku.Hide();

            Console.WriteLine("\n");
            initializer.Sudoku.Show();
            Console.ReadLine();
        }
    }
}
