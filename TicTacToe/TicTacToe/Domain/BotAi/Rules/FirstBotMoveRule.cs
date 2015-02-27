using System.Collections.Generic;

namespace TicTacToe.Domain.BotAi.Rules
{
    public sealed class FirstBotMoveRule : BotRule
    {
        public FirstBotMoveRule(BotProcessor processor) : base(processor)
        {
        }

        public override bool CheckCondition()
        {
            return Processor.Game.MovesCount == 0;
        }

        public override MoveLocation CalcLocation()
        {
            return RandomAngle();
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