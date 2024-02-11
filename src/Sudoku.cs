using System.Text;

namespace Psud
{
    public class Sudoku
    {
        public sbyte[,] Board;
        public List<sbyte>[,] Candidates = new List<sbyte>[9, 9];
        public List<Step> Steps = new();
        private readonly List<string> StepCandidateLog = new();

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
            StepCandidateLog.Clear(); //starting new step
            Candidates = Calculate.Candidates(Board);
            bool naked = NakedSingles();
            bool hidden = HiddenSingles();
            if (naked || hidden) //solve again if naked single is found
            {
                Solve();
            }
        }
    
        private bool NakedSingles()
        {
            for (sbyte i = 0; i < 9; i++)
            {
                for (sbyte j = 0; j < 9; j++)
                {
                    if (Candidates[i, j].Count == 1)
                    {
                        Steps.Add(new Step(Candidates, Board, $"Naked Single: ({i}, {j})", new (sbyte, sbyte)[] { (i, j) }, StepCandidateLog));
                        Board[i, j] = Candidates[i, j][0];
                        return true;
                    }
                }
            }
            return false;
        }

        private bool HiddenSingles()
        {
            for (sbyte n = 1; n < 10; n++) //iterate over numbers 1-9
            {
                for (sbyte i = 0; i < 3; i++) //check by box
                {
                    for (sbyte j = 0; j < 3; j++)
                    {
                        bool alreadyNumber = false;
                        foreach ((sbyte x, sbyte y) in Utilities.IterateBox(i, j))
                        {
                            if (Board[x, y] == n)
                            {
                                alreadyNumber = true;
                                break;
                            }
                        }
                        if (alreadyNumber) //if number is already in box, hidden single is impossible
                        {
                            break;
                        }
                        bool found = false, hidden = true;
                        sbyte possibleX = 0, possibleY = 0;
                        foreach ((sbyte x, sbyte y) in Utilities.IterateBox(i, j)) //get squares in box
                        {
                            if (Candidates[x, y].Contains(n))
                            {
                                if (found)
                                {
                                    hidden = false;
                                    break; //go to next box, same number twice
                                }
                                else //first time number is found, could be hidden
                                {
                                    found = true;
                                    possibleX = x;
                                    possibleY = y;
                                }
                            }
                        }
                        if (hidden)
                        {
                            Steps.Add(new Step(Candidates, Board, $"Hidden Single in ({i}, {j}) Box: ({possibleX}, {possibleY})", new (sbyte, sbyte)[] { (possibleX, possibleY) }, StepCandidateLog));
                            Board[possibleX, possibleY] = n;
                            return true;
                        }
                    }
                }
                for (sbyte i = 0; i < 9; i++) //check by column
                {
                    bool alreadyNumber = false;
                    for (sbyte j = 0; j < 9; j++)
                    {
                        if (Board[i, j] == n)
                        {
                            alreadyNumber = true;
                            break;
                        }
                    }
                    if (alreadyNumber) //if number is already in column, hidden single is impossible
                    {
                        break;
                    }
                    bool found = false, hidden = true;
                    sbyte possibleX = 0, possibleY = 0;
                    for (sbyte j = 0; j < 9; j++)
                    {
                        if (Candidates[i, j].Contains(n))
                        {
                            if (found)
                            {
                                hidden = false;
                                break; //go to next column, same number twice
                            }
                            else //first time number is found, could be hidden
                            {
                                found = true;
                                possibleX = i;
                                possibleY = j;
                            }
                        }
                    }
                    if (hidden)
                    {
                        Steps.Add(new Step(Candidates, Board, $"Hidden Single in ({i}) Column: ({possibleX}, {possibleY})", new (sbyte, sbyte)[] { (possibleX, possibleY) }, StepCandidateLog));
                        Board[possibleX, possibleY] = n;
                        return true;
                    }
                }
                for (sbyte j = 0; j < 9; j++) //check by row
                {
                    bool alreadyNumber = false;
                    for (sbyte i = 0; i < 9; i++)
                    {
                        if (Board[i, j] == n)
                        {
                            alreadyNumber = true;
                            break;
                        }
                    }
                    if (alreadyNumber) //if number is already in row, hidden single is impossible
                    {
                        break;
                    }
                    bool found = false, hidden = true;
                    sbyte possibleX = 0, possibleY = 0;
                    for (sbyte i = 0; i < 9; i++)
                    {
                        if (Candidates[i, j].Contains(n))
                        {
                            if (found)
                            {
                                hidden = false;
                                break; //go to next row, same number twice
                            }
                            else //first time number is found, could be hidden
                            {
                                found = true;
                                possibleX = i;
                                possibleY = j;
                            }
                        }
                    }
                    if (hidden)
                    {
                        Steps.Add(new Step(Candidates, Board, $"Hidden Single in ({j}) Row: ({possibleX}, {possibleY})", new (sbyte, sbyte)[] { (possibleX, possibleY) }, StepCandidateLog));
                        Board[possibleX, possibleY] = n;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}