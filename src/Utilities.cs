namespace Psud
{
    public static class Utilities
    {
        public static bool InsideBox(sbyte brx, sbyte bry, sbyte tlx, sbyte tly, sbyte x, sbyte y)
        {
            return brx >= x && bry >= y && tlx <= x && tly <= y;
        }

        public static IEnumerable<(sbyte, sbyte)> IterateBox(sbyte brx, sbyte bry, sbyte tlx, sbyte tly)
        {
            for (sbyte i = tlx; i <= brx; i++)
            {
                for (sbyte j = tly; j <= bry; j++)
                {
                    yield return (i, j);
                }
            }
        }

        public static void CandidatesDebug(List<sbyte>[,] candidates, sbyte[,] board)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" y   0   1   2   3   4   5   6   7   8");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("x\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            for (sbyte x = 0; x < 9; x++)
            {
                Console.Write("    ");
                for (sbyte y = 0; y < 9; y++)
                {
                    for (sbyte number = 1; number <= 3; number++)
                    {
                        if (candidates[x, y].Contains(number))
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write(number);
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        else
                        {
                            Console.Write(' ');
                        }
                    }
                    if ((y + 1) % 3 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("║");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.Write("|");
                    }
                }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(x);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("   ");
                for (sbyte y = 0; y < 9; y++)
                {
                    for (sbyte number = 4; number <= 6; number++)
                    {
                        if (candidates[x, y].Contains(number))
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write(number);
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        else
                        {
                            Console.Write(' ');
                        }
                    }
                    if ((y + 1) % 3 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("║");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.Write("|");
                    }
                }
                Console.WriteLine();
                Console.Write("    ");
                for (sbyte y = 0; y < 9; y++)
                {
                    for (sbyte number = 7; number <= 9; number++)
                    {
                        if (candidates[x, y].Contains(number))
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write(number);
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        else
                        {
                            Console.Write(' ');
                        }
                    }
                    if ((y + 1) % 3 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("║");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.Write("|");
                    }
                }
                if ((x + 1) % 3 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\n    ====================================");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.WriteLine("\n    ------------------------------------");
                }
            }
        }
    }
}