using System.Collections.Generic;
using TicTacToe.Domain.BotAi.Rules;

namespace TicTacToe.Domain.BotAi
{
    public sealed class SmartEnoughBot : BotProcessor
    {
        public SmartEnoughBot(TicTacToeGame game, List<MoveInfo> moves) : base(game, moves)
        {
        }

        protected override List<BotRule> Rules
        {
            get
            {
                return new List<BotRule>
                {
                    new FirstBotMoveRule(this),
                    new FirstEnemyMoveRule(this),
                    new SecondBotMoveRule(this),
                    new EasyWinRule(this),
                    new EasyLooseRule(this),
                    new DoubleThreatRule(this),
                    new RandomMoveRule(this)
                };
            }
        }
    }
}