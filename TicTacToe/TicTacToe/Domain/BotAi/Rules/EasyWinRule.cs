using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Domain.BotAi.Rules
{
    public sealed class EasyWinRule : BotRule
    {
        private readonly List<MoveLocation> _preWinCondition;

        public EasyWinRule(BotProcessor processor) : base(processor)
        {
            _preWinCondition = Constants.WinConditions
                .FirstOrDefault(x => x.Except(BotMoves).Count() == 1 && x.Except(MadeMoves).Any());
        }

        public override bool CheckCondition()
        {
            return _preWinCondition != null;
        }

        public override MoveLocation CalcLocation()
        {
            return _preWinCondition.Except(BotMoves).First();
        }
    }
}