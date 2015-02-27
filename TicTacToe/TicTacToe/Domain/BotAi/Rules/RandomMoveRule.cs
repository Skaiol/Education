namespace TicTacToe.Domain.BotAi.Rules
{
    public sealed class RandomMoveRule : BotRule
    {
        public RandomMoveRule(BotProcessor processor) : base(processor)
        {
        }

        public override bool CheckCondition()
        {
            return true;
        }

        public override MoveLocation CalcLocation()
        {
            int index = Random.Next(NotMadeMoves.Count);
            return NotMadeMoves[index];
        }
    }
}