﻿using System.Collections.Generic;
using TicTacToe.Domain.BotAi.Rules;

namespace TicTacToe.Domain.BotAi
{
    public sealed class NotSoFoolBot : BotProcessor
    {
        public NotSoFoolBot(TicTacToeGame game, List<MoveInfo> moves) : base(game, moves)
        {
        }

        protected override List<BotRule> Rules
        {
            get
            {
                return new List<BotRule>
                {
                    new EasyWinRule(this),
                    new EasyLooseRule(this),
                    new RandomMoveRule(this)
                };
            }
        }
    }
}