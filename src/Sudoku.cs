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
                                else if (x != square.x)
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
                            candidateLog.Add($"Row pointing: {number} - {log.Remove(log.Length - 2, 2).ToString()}");
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
                                    y = square.x;
                                }
                                else if (y != square.y)
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
                            candidateLog.Add($"Column pointing: {number} - {log.Remove(log.Length - 2, 2).ToString()}");
                            log.Clear();
                        }
                        RowClaiming:
                            continue;
                    }
                }
            }

            /*for (sbyte x = 0; x < 9; x++)
            {
                for (sbyte y = 0; y < 9; y++)
                {
                    (sbyte brx, sbyte bry, sbyte tlx, sbyte tly) box = Utilities.GetBox(x, y);
                    foreach (sbyte number in Candidates[x, y].ToArray())
                    {
                        foreach ((sbyte x, sbyte y) square in Utilities.IterateBoxIgnore(box.brx, box.bry, box.tlx, box.tly, x, y)) //checking row
                        {
                            if (Candidates[square.x, square.y].Contains(number) && square.x != x)
                            {
                                goto pointColumn;
                            }
                        }
                        for (sbyte horiz = 0; horiz < 9; horiz++)
                        {
                            if (Candidates[x, horiz].Contains(number) && !Utilities.InsideBox(box.brx, box.bry, box.tlx, box.tly, x, y))
                            {
                                log.Append($"({x}, {horiz}), ");
                                Candidates[x, horiz].Remove(number);
                            }
                        }
                        if (log.Length != 0)
                        {
                            candidateLog.Add($"Row pointing: {number} - {log.Remove(log.Length - 2, 2).ToString()}");
                            log.Clear();
                        }
                        pointColumn:
                        foreach ((sbyte x, sbyte y) square in Utilities.IterateBoxIgnore(box.brx, box.bry, box.tlx, box.tly, x, y)) //checking column
                        {
                            if (Candidates[square.x, square.y].Contains(number) && square.y != y)
                            {
                                goto claimRow;
                            }
                        }
                        for (sbyte vert = 0; vert < 9; vert++)
                        {
                            if (Candidates[vert, y].Contains(number) && !Utilities.InsideBox(box.brx, box.bry, box.tlx, box.tly, x, y))
                            {
                                log.Append($"({vert}, {y}), ");
                                Candidates[vert, y].Remove(number);
                            }
                        }
                        if (log.Length != 0)
                        {
                            candidateLog.Add($"Column pointing: {number} - {log.Remove(log.Length - 2, 2).ToString()}");
                            log.Clear();
                        }
                        claimRow:
                        (sbyte brx, sbyte bry, sbyte tlx, sbyte tly) rowClaim = (-1, -1, -1, -1);
                        for (sbyte horiz = 0; horiz < 9; horiz++)
                        {
                            if (Candidates[x, horiz].Contains(number))
                            {
                                if (rowClaim == (-1, -1, -1, -1))
                                {
                                    rowClaim = Utilities.GetBox(x, horiz);
                                }
                                else
                                {
                                    if (rowClaim != Utilities.GetBox(x, horiz))
                                    {
                                        goto claimColumn;
                                    }
                                }
                            }
                        }
                        if (rowClaim == (-1, -1, -1, -1))
                        {
                            goto claimColumn;
                        }
                        foreach ((sbyte x, sbyte y) square in Utilities.IterateBox(rowClaim.brx, rowClaim.bry, rowClaim.tlx, rowClaim.tly))
                        {
                            if (x != square.x && Candidates[square.x, square.y].Contains(number))
                            {
                                log.Append($"({square.x}, {square.y}), ");
                                Candidates[square.x, square.y].Remove(number);
                            }
                        }
                        rowClaim = (-1, -1, -1, -1);
                        if (log.Length != 0)
                        {
                            candidateLog.Add($"Row claiming: {number} - {log.Remove(log.Length - 2, 2).ToString()}");
                            log.Clear();
                        }
                        claimColumn:
                        (sbyte brx, sbyte bry, sbyte tlx, sbyte tly) columnClaim = (-1, -1, -1, -1);
                        for (sbyte vert = 0; vert < 9; vert++)
                        {
                            if (Candidates[vert, y].Contains(number))
                            {
                                if (columnClaim == (-1, -1, -1, -1))
                                {
                                    columnClaim = Utilities.GetBox(vert, y);
                                }
                                else
                                {
                                    if (columnClaim != Utilities.GetBox(vert, y))
                                    {
                                        goto nakedSubsetBox;
                                    }
                                }
                            }
                        }
                        if (columnClaim == (-1, -1, -1, -1))
                        {
                            goto nakedSubsetBox;
                        }
                        foreach ((sbyte x, sbyte y) square in Utilities.IterateBox(columnClaim.brx, columnClaim.bry, columnClaim.tlx, columnClaim.tly))
                        {
                            if (y != square.y && Candidates[square.x, square.y].Contains(number))
                            {
                                log.Append($"({square.x}, {square.y}), ");
                                Candidates[square.x, square.y].Remove(number);
                            }
                        }
                        columnClaim = (-1, -1, -1, -1);
                        if (log.Length != 0)
                        {
                            candidateLog.Add($"Column claiming: {number} - {log.Remove(log.Length - 2, 2).ToString()}");
                            log.Clear();
                        }
                    }
                }
            }
            nakedSubsetBox:*/


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