using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Domain.BotAi.Rules
{
    public sealed class SecondBotMoveRule : BotRule
    {
        public SecondBotMoveRule(BotProcessor processor) : base(processor)
        {
        }

        public override bool CheckCondition()
        {
            return Processor.Game.MovesCount == 2;
        }

        public override MoveLocation CalcLocation()
        {
            List<MoveLocation> condition = Constants.WinConditions.First(x => x.Except(BotMoves).Count() == 2
                                                                              && x.Except(EnemyMoves).Count() == 3);
            return condition.Except(BotMoves).First();
        }
    }
}