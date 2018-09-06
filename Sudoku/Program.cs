using System;
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
                for (int j=0;j<innterList.Count; j++)
                {
                    Console.Write(innterList[j] + " ");
                    Console.Write((j + 1) % 3 == 0 ? "|" : " ");
                    Console.Write(" ");
                }
                Console.WriteLine((i+1)%3==0?"\n---------------------------------":"");
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
            ,new List<int>{3,4,5,6,7,8,9,1,2 }
            ,new List<int>{6,7,8,9,1,2,3,4,5 }
            ,new List<int>{9,1,2,3,4,5,6,7,8 } };
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
        
        static void RandomGenerator(Random rnd, int range, out int first, out int second)
        {
            first = rnd.Next(1, range + 1);
            second = rnd.Next(1, range + 1);
            while(second == first)
            second = rnd.Next(1, range + 1);
        }

        public void Swap()
        {
            for (int i = 0; i < numberOfSwaps; i++)
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

        public void SwitchRowSmall(ref Sudoku sudoku)
        {
            int firstRowToSwap;
            int secondRowToSwap;
            RandomGenerator(rnd, 9, out firstRowToSwap, out secondRowToSwap);

            sudoku.table.Insert(0, new List<int>());
            sudoku.table[0] = sudoku.table[firstRowToSwap];
            sudoku.table[firstRowToSwap] = sudoku.table[secondRowToSwap];
            sudoku.table[secondRowToSwap] = sudoku.table[0];
            sudoku.table.RemoveAt(0);
        }

        public void SwitchRowBig(ref Sudoku sudoku)
        {
            int firstRowToSwap;
            int secondRowToSwap;

            RandomGenerator(rnd, 3, out firstRowToSwap, out secondRowToSwap);
            sudoku.table.InsertRange(0, new List<List<int>> { new List<int>(), new List<int>(), new List<int>() });
            for (int i = 0; i < 3; i++)
            {
                sudoku.table[i] = sudoku.table[firstRowToSwap * 3 + i];
                sudoku.table[firstRowToSwap * 3 + i] = sudoku.table[secondRowToSwap * 3 + i];
                sudoku.table[secondRowToSwap * 3 + i] = sudoku.table[i];
            }
            sudoku.table.RemoveRange(0, 3);
        }
        public void SwitchColumnBig(ref Sudoku sudoku)
        {
            int firstColToSwap;
            int secondColToSwap;
            RandomGenerator(rnd, 3, out firstColToSwap, out secondColToSwap);
            foreach(var row in sudoku.table)
            {
                row.InsertRange(0, new List<int> { 0, 0, 0 });
                for (int i = 0; i < 3; i++)
                {
                    row[i] = row[firstColToSwap * 3 + i];
                    row[firstColToSwap*3 + i] = row[secondColToSwap*3 + i];
                    row[secondColToSwap*3 + i] = row[i];
                }
                row.RemoveRange(0, 3);
            }

        }
        public void SwitchColumnSmall(ref Sudoku sudoku)
        {
            int firstRowToSwap;
            int secondRowToSwap;
            RandomGenerator(rnd, 9, out firstRowToSwap, out secondRowToSwap);
            foreach (var row in sudoku.table)
            {
                row.Insert(0, 0);
                row[0] = row[firstRowToSwap];
                row[firstRowToSwap] = row[secondRowToSwap];
                row[secondRowToSwap] = row[0];
                row.RemoveAt(0);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Switcher initializer = new Switcher(6);
            initializer.Swap();
            initializer.Sudoku.Show();

            Console.ReadLine();
        }
    }
}
