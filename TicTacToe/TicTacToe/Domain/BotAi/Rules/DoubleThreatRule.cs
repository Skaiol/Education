using System.Linq;

namespace TicTacToe.Domain.BotAi.Rules
{
    public sealed class DoubleThreatRule : BotRule
    {
        private readonly MoveLocation? _location;

        public DoubleThreatRule(BotProcessor processor) : base(processor)
        {
            _location = NotMadeMoves
                .FirstOrDefault(x => Constants.WinConditions
                    .Where(y => y.Except(MadeMoves).Any())
                    .Count(
                        y => BotMoves.Concat(new[] {x}).Except(y).Count() == 1) == 2);
        }

        public override bool CheckCondition()
        {
            return _location.HasValue;
        }

        public override MoveLocation CalcLocation()
        {
            return _location.Value;
        }
    }
}