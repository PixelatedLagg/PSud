using Psud;

class Program
{
    public static void Main()
    {
        string input = File.ReadAllText("input.txt");
        Sudoku sud = Parser.PSN(input);
        DateTime before = DateTime.Now;
        sud.Solve();
        DateTime after = DateTime.Now;

        /*Step step = sud.Steps[sud.Steps.Count - 1];
        Utilities.CandidatesHighlight(step.Candidates, step.Board, step.Highlight);
        Console.WriteLine(step.Reason);
        foreach (string log in step.CandidateLog)
        {
            Console.WriteLine(log);
        }*/
        Console.WriteLine($"Solved in {(after - before).Milliseconds}ms");
        int i = 0;
        while (true)
        {
            Step step = sud.Steps[i];
            Utilities.CandidatesHighlight(step.Candidates, step.Board, step.Highlight);
            Console.WriteLine(step.Reason);
            foreach (string log in step.CandidateLog)
            {
                Console.WriteLine(log);
            }
            ConsoleKey key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.DownArrow)
            {
                if (i != sud.Steps.Count - 1)
                {
                    i++;
                }
            }
            else if (key == ConsoleKey.UpArrow)
            {
                if (i != 0)
                {
                    i--;
                }
            }
            Console.Clear();
        }
    }
}