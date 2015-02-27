using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Domain.BotAi.Rules
{
    public sealed class EasyLooseRule : BotRule
    {
        private readonly List<MoveLocation> _preLooseCondition;

        public EasyLooseRule(BotProcessor processor) : base(processor)
        {
            _preLooseCondition = Constants.WinConditions
                .FirstOrDefault(x => x.Except(EnemyMoves).Count() == 1 && x.Except(MadeMoves).Any());
        }

        public override bool CheckCondition()
        {
            return _preLooseCondition != null;
        }

        public override MoveLocation CalcLocation()
        {
            return _preLooseCondition.Except(EnemyMoves).First();
        }
    }
}