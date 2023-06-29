namespace Psud
{
    public class Step
    {
        public List<sbyte>[,] Candidates;
        public sbyte[,] Board;
        public string Reason = "";
        public (int x, int y)[] Highlight;

        public Step(List<sbyte>[,] candidates, sbyte[,] board)
        {
            Candidates = candidates;
            Board = board;
        }
    }
}