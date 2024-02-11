namespace Psud
{
    public static class Calculate
    {
        public static List<sbyte>[,] Candidates(sbyte[,] board)
        {
            List<sbyte>[,] candidates = new List<sbyte>[9, 9];
            for (sbyte x = 0; x < 9; x++)
            {
                for (sbyte y = 0; y < 9; y++)
                {
                    candidates[x, y] = new List<sbyte>();
                    if (board[x, y] != 0)
                    {
                        continue;
                    }
                    for (sbyte number = 1; number <= 9; number++)
                    {
                        for (sbyte horiz = 0; horiz < 9; horiz++)
                        {
                            if (board[x, horiz] == number)
                            {
                                goto nextNumber;
                            }
                        }
                        for (sbyte vert = 0; vert < 9; vert++)
                        {
                            if (board[vert, y] == number)
                            {
                                goto nextNumber;
                            }
                        }
                        for (sbyte i = 0; i < 3; i++)
                        {
                            for (sbyte j = 0; j < 3; j++)
                            {
                                if (Utilities.InsideBox((sbyte)(2 + i * 3), (sbyte)(2 + j * 3), (sbyte)(i * 3), (sbyte)(j * 3), x, y))
                                {
                                    foreach ((sbyte, sbyte) square in Utilities.IterateBox((sbyte)(2 + i * 3), (sbyte)(2 + j * 3), (sbyte)(i * 3), (sbyte)(j * 3)))
                                    {
                                        if (board[square.Item1, square.Item2] == number)
                                        {
                                            goto nextNumber;
                                        }
                                    }
                                }
                            }
                        }
                        candidates[x, y].Add(number);
                        nextNumber:
                            continue;
                    }
                }
            }
            return candidates;
        }
    }
}