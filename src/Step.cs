namespace Psud
{
    public class Step
    {
        public List<sbyte>[,] Candidates;
        public sbyte[,] Board;
        public string Reason;
        public (sbyte x, sbyte y)[] Highlight;
        public List<string> CandidateLog;

        public Step(List<sbyte>[,] candidates, sbyte[,] board, string reason, (sbyte, sbyte)[] highlight, List<string> candidateLog)
        {
            Candidates = candidates;
            Board = board;
            Reason = reason;
            Highlight = highlight;
            CandidateLog = candidateLog;
        }
    }
}