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

        public static IEnumerable<(sbyte, sbyte)> IterateBoxIgnore(sbyte brx, sbyte bry, sbyte tlx, sbyte tly, sbyte x, sbyte y)
        {
            for (sbyte i = tlx; i <= brx; i++)
            {
                for (sbyte j = tly; j <= bry; j++)
                {
                    if (i != x || j != y)
                    {
                        yield return (i, j);
                    }
                }
            }
        }

        public static (sbyte brx, sbyte bry, sbyte tlx, sbyte tly) GetBox(sbyte x, sbyte y)
        {
            sbyte tlx = (sbyte)(Math.Floor((decimal)x / 3) * 3);
            sbyte tly = (sbyte)(Math.Floor((decimal)y / 3) * 3);
            return ((sbyte)(tlx + 2), (sbyte)(tly + 2), tlx, tly);
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
                    if (candidates[x, y].Count == 0)
                    {
                        Console.Write("   ");
                    }
                    else
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
                    if (candidates[x, y].Count == 0)
                    {
                        Console.Write($" {board[x, y]} ");
                    }
                    else
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
                    if (candidates[x, y].Count == 0)
                    {
                        Console.Write("   ");
                    }
                    else
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

        public static string ToDirections(sbyte x, sbyte y)
        {
            string result = "";
            sbyte diffX, diffY;
            if (x <= 2)
            {
                result += 't';
                diffX = 0;
            }
            else if (x <= 5)
            {
                result += 'm';
                diffX = 3;
            }
            else
            {
                result += 'b';
                diffX = 6;
            }
            if (y <= 2)
            {
                result += 'l';
                diffY = 0;
            }
            else if (y <= 5)
            {
                result += 'm';
                diffY = 3;
            }
            else
            {
                result += 'r';
                diffY = 6;
            }
            result += (x - diffX) switch {
                0 => " t",
                1 => " m",
                _ => " b"
            };
            result += (y - diffY) switch {
                0 => 'l',
                1 => 'm',
                _ => 'r'
            };
            return result;
        }

        public static void CandidatesHighlight(List<sbyte>[,] candidates, sbyte[,] board, (sbyte x, sbyte y)[] highlight)
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
                    ConsoleColor color = ConsoleColor.Green;
                    if (highlight.Contains((x, y)))
                    {
                        color = ConsoleColor.Magenta;
                    }
                    if (candidates[x, y].Count == 0)
                    {
                        if (color == ConsoleColor.Magenta)
                        {
                            Console.BackgroundColor = color;
                        }
                        Console.Write("   ");
                    }
                    else
                    {
                        for (sbyte number = 1; number <= 3; number++)
                        {
                            if (candidates[x, y].Contains(number))
                            {
                                Console.BackgroundColor = color;
                                Console.Write(number);
                                Console.BackgroundColor = ConsoleColor.Black;
                            }
                            else
                            {
                                if (color == ConsoleColor.Magenta)
                                {
                                    Console.BackgroundColor = color;
                                }
                                Console.Write(' ');
                            }
                        }
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
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
                    ConsoleColor color = ConsoleColor.Green;
                    if (highlight.Contains((x, y)))
                    {
                        color = ConsoleColor.Magenta;
                    }
                    if (candidates[x, y].Count == 0)
                    {
                        if (color == ConsoleColor.Magenta)
                        {
                            Console.BackgroundColor = color;
                        }
                        Console.Write($" {board[x, y]} ");
                    }
                    else
                    {
                        for (sbyte number = 4; number <= 6; number++)
                        {
                            if (candidates[x, y].Contains(number))
                            {
                                Console.BackgroundColor = color;
                                Console.Write(number);
                                Console.BackgroundColor = ConsoleColor.Black;
                            }
                            else
                            {
                                if (color == ConsoleColor.Magenta)
                                {
                                    Console.BackgroundColor = color;
                                }
                                Console.Write(' ');
                            }
                        }
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
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
                    ConsoleColor color = ConsoleColor.Green;
                    if (highlight.Contains((x, y)))
                    {
                        color = ConsoleColor.Magenta;
                    }
                    if (candidates[x, y].Count == 0)
                    {
                        if (color == ConsoleColor.Magenta)
                        {
                            Console.BackgroundColor = color;
                        }
                        Console.Write("   ");
                    }
                    else
                    {
                        for (sbyte number = 7; number <= 9; number++)
                        {
                            if (candidates[x, y].Contains(number))
                            {
                                Console.BackgroundColor = color;
                                Console.Write(number);
                                Console.BackgroundColor = ConsoleColor.Black;
                            }
                            else
                            {
                                if (color == ConsoleColor.Magenta)
                                {
                                    Console.BackgroundColor = color;
                                }
                                Console.Write(' ');
                            }
                        }
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
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