using System.Text;

namespace Psud
{
    public class Sudoku
    {
        public sbyte[,] Board;
        public List<sbyte>[,] Candidates = new List<sbyte>[9, 9];
        public List<Step> Steps = new List<Step>();

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
            //beginner
            //pointing
            //claiming
            refresh:
            Candidates = Calculate.Candidates(Board);
            List<string> candidateLog = new List<string>();
            StringBuilder log = new StringBuilder();


            for (sbyte number = 1; number <= 9; number++)
            {
                for (sbyte i = 0; i < 3; i++)
                {
                    for (sbyte j = 0; j < 3; j++)
                    {
                        sbyte x = -1;
                        foreach ((sbyte x, sbyte y) square in Utilities.IterateBox((sbyte)(i * 3), (sbyte)(j * 3), (sbyte)(i * 3 + 2), (sbyte)(j * 3 + 2))) //checking row
                        {
                            if (Candidates[square.x, square.y].Contains(number))
                            {
                                if (x == -1)
                                {
                                    x = square.x;
                                }
                                if (x != square.x)
                                {
                                    goto ColumnPointing;
                                }
                            }
                        }
                        if (x == -1)
                        {
                            goto ColumnPointing;
                        }
                        for (sbyte row = 0; row < 9; row++)
                        {
                            if (Candidates[x, row].Contains(number) && !Utilities.InsideBox((sbyte)(i * 3), (sbyte)(j * 3), (sbyte)(i * 3 + 2), (sbyte)(j * 3 + 2), x, row))
                            {
                                log.Append($"({x}, {row}), ");
                                Candidates[x, row].Remove(number);
                            }
                        }
                        if (log.Length != 0)
                        {
                            candidateLog.Add($"Row pointing: {number}, Row {x} - {log.Remove(log.Length - 2, 2).ToString()}");
                            log.Clear();
                        }
                        ColumnPointing:
                        sbyte y = -1;
                        foreach ((sbyte x, sbyte y) square in Utilities.IterateBox((sbyte)(i * 3), (sbyte)(j * 3), (sbyte)(i * 3 + 2), (sbyte)(j * 3 + 2))) //checking column
                        {
                            if (Candidates[square.x, square.y].Contains(number))
                            {
                                if (y == -1)
                                {
                                    y = square.y;
                                }
                                if (y != square.y)
                                {
                                    goto RowClaiming;
                                }
                            }
                        }
                        if (y == -1)
                        {
                            goto RowClaiming;
                        }
                        for (sbyte column = 0; column < 9; column++)
                        {
                            if (Candidates[column, y].Contains(number) && !Utilities.InsideBox((sbyte)(i * 3), (sbyte)(j * 3), (sbyte)(i * 3 + 2), (sbyte)(j * 3 + 2), column, y))
                            {
                                log.Append($"({column}, {y}), ");
                                Candidates[column, y].Remove(number);
                            }
                        }
                        if (log.Length != 0)
                        {
                            candidateLog.Add($"Column pointing: {number}, Column {y} - {log.Remove(log.Length - 2, 2).ToString()}");
                            log.Clear();
                        }
                    }
                }
                RowClaiming:
                for (sbyte x = 0; x < 9; x++)
                {
                    (sbyte brx, sbyte bry, sbyte tlx, sbyte tly) box = (-1, -1, -1, -1);
                    for (sbyte y = 0; y < 9; y++)
                    {
                        if (Candidates[x, y].Contains(number))
                        {
                            if (box == (-1, -1, -1, -1))
                            {
                                box = Utilities.GetBox(x, y);
                            }
                            if (box != Utilities.GetBox(x, y))
                            {
                                goto ColumnClaiming;
                            }
                        }
                    }
                    if (box == (-1, -1, -1, -1))
                    {
                        goto ColumnClaiming;
                    }
                    foreach ((sbyte x, sbyte y) square in Utilities.IterateBox(box.brx, box.bry, box.tlx, box.tly))
                    {
                        if (x != square.x && Candidates[square.x, square.y].Contains(number))
                        {
                            log.Append($"({square.x}, {square.y}), ");
                            Candidates[square.x, square.y].Remove(number);
                        }
                    }
                    if (log.Length != 0)
                    {
                        candidateLog.Add($"Row claiming: {number}, Row {x} - {log.Remove(log.Length - 2, 2).ToString()}");
                        log.Clear();
                    }
                }
                ColumnClaiming:
                for (sbyte y = 0; y < 9; y++)
                {
                    (sbyte brx, sbyte bry, sbyte tlx, sbyte tly) box = (-1, -1, -1, -1);
                    for (sbyte x = 0; x < 9; x++)
                    {
                        if (Candidates[x, y].Contains(number))
                        {
                            if (box == (-1, -1, -1, -1))
                            {
                                box = Utilities.GetBox(x, y);
                            }
                            if (box != Utilities.GetBox(x, y))
                            {
                                goto EndOfLoop;
                            }
                        }
                    }
                    if (box == (-1, -1, -1, -1))
                    {
                        goto EndOfLoop;
                    }
                    foreach ((sbyte x, sbyte y) square in Utilities.IterateBox(box.brx, box.bry, box.tlx, box.tly))
                    {
                        if (y != square.y && Candidates[square.x, square.y].Contains(number))
                        {
                            log.Append($"({square.x}, {square.y}), ");
                            Candidates[square.x, square.y].Remove(number);
                        }
                    }
                    box = (-1, -1, -1, -1);
                    if (log.Length != 0)
                    {
                        candidateLog.Add($"Column claiming: {number}, Column {y} - {log.Remove(log.Length - 2, 2).ToString()}");
                        log.Clear();
                    }
                }
                EndOfLoop:
                continue;
            }

            //naked subsets, this is the broken code:
            for (sbyte i = 0; i < 3; i++)
            {
                for (sbyte j = 0; j < 3; j++)
                {
                    List<NakedSubset> subsets = new List<NakedSubset>();
                    foreach ((sbyte x, sbyte y) square in Utilities.IterateBoxIgnore((sbyte)(i * 3), (sbyte)(j * 3), (sbyte)(i * 3 + 2), (sbyte)(j * 3 + 2), (sbyte)(i * 3), (sbyte)(j * 3)))
                    {
                        if (subsets.Count == 0)
                        {
                            if (Candidates[square.x, square.y].Count != 0)
                            {
                                subsets.Add(new NakedSubset(Candidates[square.x, square.y], 1));
                            }
                            continue;
                        }
                        if (Candidates[square.x, square.y].Count == 0)
                        {
                            continue;
                        }
                        for (int index = 0; index < subsets.Count; index++)
                        {
                            if (Enumerable.SequenceEqual(Candidates[square.x, square.y], subsets[index].Candidates))
                            {
                                subsets[index].Count++;
                                goto Continue;
                            }
                        }
                        subsets.Add(new NakedSubset(Candidates[square.x, square.y], 1));
                        Continue:
                        continue;
                    }
                    for (int index = 0; index < subsets.Count; index++)
                    {
                        if (subsets[index].Count != subsets[index].Candidates.Count)
                        {
                            continue;
                        }
                        foreach ((sbyte x, sbyte y) square in Utilities.IterateBox((sbyte)(i * 3), (sbyte)(j * 3), (sbyte)(i * 3 + 2), (sbyte)(j * 3 + 2)))
                        {
                            if (!Enumerable.SequenceEqual(Candidates[square.x, square.y], subsets[index].Candidates))
                            {
                                foreach (sbyte candidate in subsets[index].Candidates)
                                {
                                    if (Candidates[square.x, square.y].Contains(candidate))
                                    {
                                        log.Append($"{candidate} - ({square.x}, {square.y}), ");
                                        Candidates[square.x, square.y].Remove(candidate);
                                    }
                                }
                            }
                        }
                    }
                    if (log.Length != 0)
                    {
                        candidateLog.Add($"Naked subset(s) in box: ({i}, {j}) - {log.Remove(log.Length - 2, 2).ToString()}");
                        log.Clear();
                    }
                }
            }

            
            for (sbyte x = 0; x < 9; x++)
            {
                List<NakedSubset> subsets = new List<NakedSubset>();
                for (sbyte y = 1; y < 9; y++)
                {
                    if (subsets.Count == 0)
                        {
                            if (Candidates[x, y].Count != 0)
                            {
                                subsets.Add(new NakedSubset(Candidates[x, y], 1));
                            }
                            continue;
                        }
                        if (Candidates[x, y].Count == 0)
                        {
                            continue;
                        }
                        for (int index = 0; index < subsets.Count; index++)
                        {
                            if (Enumerable.SequenceEqual(Candidates[x, y], subsets[index].Candidates))
                            {
                                subsets[index].Count++;
                                goto Continue;
                            }
                        }
                        subsets.Add(new NakedSubset(Candidates[x, y], 1));
                        Continue:
                        continue;
                }
                for (int index = 0; index < subsets.Count; index++)
                {
                    if (subsets[index].Count != subsets[index].Candidates.Count)
                    {
                        continue;
                    }
                    for (sbyte y = 0; y < 9; y++)
                    {
                        if (!Enumerable.SequenceEqual(Candidates[x, y], subsets[index].Candidates))
                        {
                            foreach (sbyte candidate in subsets[index].Candidates)
                            {
                                if (Candidates[x, y].Contains(candidate))
                                {
                                    log.Append($"{candidate} - ({x}, {y}), ");
                                    Candidates[x, y].Remove(candidate);
                                }
                            }
                        }
                    }
                }
                if (log.Length != 0)
                {
                    candidateLog.Add($"Naked subset(s) in row: {x} - {log.Remove(log.Length - 2, 2).ToString()}");
                    log.Clear();
                }
            }


            //beginner
            //hidden singles
            //naked singles
            for (sbyte x = 0; x < 9; x++)
            {
                for (sbyte y = 0; y < 9; y++)
                {
                    if (Candidates[x, y].Count == 1) //naked single
                    {
                        Steps.Add(new Step(Candidates, Board, $"Naked single: {Candidates[x, y][0]}", new (sbyte, sbyte)[] { (x, y) }, candidateLog));
                        Board[x, y] = Candidates[x, y][0];
                        goto refresh;
                    }
                    (sbyte brx, sbyte bry, sbyte tlx, sbyte tly) box = Utilities.GetBox(x, y);
                    foreach (sbyte number in Candidates[x, y]) //hidden single in box
                    {
                        foreach ((sbyte x, sbyte y) square in Utilities.IterateBoxIgnore(box.brx, box.bry, box.tlx, box.tly, x, y))
                        {
                            if (Candidates[square.x, square.y].Contains(number))
                            {
                                goto checkRow;
                            }
                        }
                        Steps.Add(new Step(Candidates, Board, $"Hidden single in box: {number}", new (sbyte, sbyte)[] { (x, y) }, candidateLog));
                        Board[x, y] = number;
                        goto refresh;
                        checkRow:
                        for (sbyte horiz = 0; horiz < 9; horiz++)
                        {
                            if (Candidates[x, horiz].Contains(number) && y != horiz)
                            {
                                goto checkColumn;
                            }
                        }
                        Steps.Add(new Step(Candidates, Board, $"Hidden single in row: {number}", new (sbyte, sbyte)[] { (x, y) }, candidateLog));
                        Board[x, y] = number;
                        goto refresh;
                        checkColumn:
                        for (sbyte vert = 0; vert < 9; vert++)
                        {
                            if (Candidates[vert, y].Contains(number) && x != vert)
                            {
                                goto notHidden;
                            }
                        }
                        Steps.Add(new Step(Candidates, Board, $"Hidden single in column: {number}", new (sbyte, sbyte)[] { (x, y) }, candidateLog));
                        Board[x, y] = number;
                        goto refresh;
                        notHidden:
                        continue;
                    }
                }
            }
        }
    }
}