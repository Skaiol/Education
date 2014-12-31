namespace TicTacToe.Domain.Results
{
    public sealed class PlayerVictoryResult : GameResult
    {
        public PlayerVictoryResult(Player winner, int movesCount) : base(movesCount)
        {
            Winner = winner;
        }

        public override GameResultType Type
        {
            get { return GameResultType.PlayerVictory; }
        }

        public Player Winner { get; private set; }
    }
}