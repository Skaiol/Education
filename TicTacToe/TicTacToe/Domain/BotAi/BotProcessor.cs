using System;
using System.Collections.Generic;

namespace TicTacToe.Domain.BotAi
{
    public abstract class BotProcessor
    {
        private readonly List<Action<MoveLocation>> _botListeners;
        protected readonly TicTacToeGame Game;
        protected readonly List<MoveInfo> Moves;

        public BotProcessor(TicTacToeGame game, List<MoveInfo> moves)
        {
            Moves = moves;
            Game = game;
            _botListeners = new List<Action<MoveLocation>>();
        }

        public void RegisterBotMoveListener(Action<MoveLocation> listener)
        {
            _botListeners.Add(listener);
        }

        public void MakeMove()
        {
            var nextMove = CalcNextLocation();
            Game.MakeMove(nextMove);
            _botListeners.ForEach(x => x(nextMove));
        }

        protected abstract MoveLocation CalcNextLocation();
    }
}