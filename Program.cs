using Psud;

class Program
{
    public static void Main()
    {
        string input = File.ReadAllText("input.txt");
        Sudoku sud = Parser.PSN(input);
        Utilities.CandidatesDebug(Calculate.Candidates(sud.Board), sud.Board);
    }
}