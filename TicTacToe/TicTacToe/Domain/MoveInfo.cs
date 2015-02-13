namespace TicTacToe.Domain
{
    public sealed class MoveInfo
    {
        public MoveLocation Location { get; set; }
        public Player Player { get; set; }
    }
}