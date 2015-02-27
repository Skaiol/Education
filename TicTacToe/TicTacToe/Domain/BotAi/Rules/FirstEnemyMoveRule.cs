using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Domain.BotAi.Rules
{
    public sealed class FirstEnemyMoveRule : BotRule
    {
        public FirstEnemyMoveRule(BotProcessor processor) : base(processor)
        {
        }

        public override bool CheckCondition()
        {
            return Processor.Game.MovesCount == 1;
        }

        public override MoveLocation CalcLocation()
        {
            return Processor.Moves.Select(x => x.Location).Contains(MoveLocation.Center)
                ? RandomAngle()
                : MoveLocation.Center;
        }

        private MoveLocation RandomAngle()
        {
            var angleMoves = new List<MoveLocation>
            {
                MoveLocation.TopLeft,
                MoveLocation.TopRight,
                MoveLocation.BottomLeft,
                MoveLocation.BottomRight
            };
            int index = Random.Next(angleMoves.Count);
            return angleMoves[index];
        }
    }
}