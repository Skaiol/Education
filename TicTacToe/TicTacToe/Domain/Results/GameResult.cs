namespace TicTacToe.Domain.Results
{
    public abstract class GameResult
    {
        public int MovesCount { get; private set; }
        public abstract GameResultType Type { get; }

        protected GameResult(int movesCount)
        {
            MovesCount = movesCount;
        }
    }
}