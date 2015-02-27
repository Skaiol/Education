using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Domain.BotAi.Rules;

namespace TicTacToe.Domain.BotAi
{
    public abstract class BotProcessor
    {
        private readonly List<Action<MoveLocation>> _botListeners;

        public BotProcessor(TicTacToeGame game, List<MoveInfo> moves)
        {
            Moves = moves;
            Game = game;
            _botListeners = new List<Action<MoveLocation>>();
        }

        public TicTacToeGame Game { get; private set; }
        public List<MoveInfo> Moves { get; private set; }
        protected abstract List<BotRule> Rules { get; }

        public void RegisterBotMoveListener(Action<MoveLocation> listener)
        {
            _botListeners.Add(listener);
        }

        public void MakeMove()
        {
            MoveLocation nextMove = Rules.First(x => x.CheckCondition()).CalcLocation();
            Game.MakeMove(nextMove);
            _botListeners.ForEach(x => x(nextMove));
        }
    }
}