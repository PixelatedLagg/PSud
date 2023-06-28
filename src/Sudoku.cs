namespace Psud
{
    public class Sudoku
    {
        public sbyte[,] Board;
        public Sudoku(sbyte[,] board)
        {
            Board = board;
        }

        public void Debug()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" y    0    1    2    3    4    5    6    7    8");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("x\n");
            for (sbyte x = 0; x < 9; x++)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(x);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(' ');
                for (sbyte y = 0; y < 9; y++)
                {
                    Console.Write($"    {Board[x, y]}");
                }
                Console.WriteLine('\n');
            }
        }

        public void Solve()
        {
            
        }
    }
}