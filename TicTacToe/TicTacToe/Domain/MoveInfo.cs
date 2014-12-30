namespace TicTacToe.Domain
{
    public sealed class MoveInfo
    {
        public Player Player { get; set; }
        public MoveLocation Location { get; set; }
    }
}