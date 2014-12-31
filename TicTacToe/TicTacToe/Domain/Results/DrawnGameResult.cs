namespace TicTacToe.Domain.Results
{
    public sealed class DrawnGameResult : GameResult
    {
        public DrawnGameResult(int movesCount) : base(movesCount)
        {
        }

        public override GameResultType Type
        {
            get { return GameResultType.DrawnGame; }
        }
    }
}