namespace Psud
{
    public static class Parser
    {
        public static Sudoku PSN(string psn)
        {
            Sudoku result = new(new sbyte[9, 9]);
            string[] rows = psn.Split(',');
            for (sbyte y = 0; y < 9; y++)
            {
                for (sbyte x = 0; x < 9; x++)
                {
                    if (rows[y][x] == '#')
                    {
                        result.Board[y, x] = 0;
                    }
                    else
                    {
                        result.Board[y, x] = (sbyte)(rows[y][x] - '0');
                    }
                }
            }
            return result;
        }
    }
}