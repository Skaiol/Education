using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Domain.BotAi
{
    public sealed class FoolBot : BotProcessor
    {
        private readonly List<MoveLocation> _all;
        private readonly Random _random;

        public FoolBot(TicTacToeGame game, List<MoveInfo> moves) : base(game, moves)
        {
            _random = new Random();
            _all = Enum.GetValues(typeof (MoveLocation))
                .Cast<MoveLocation>()
                .ToList();
        }

        protected override MoveLocation CalcNextLocation()
        {
            List<MoveLocation> madeMoves = Moves.Select(x => x.Location).ToList();
            List<MoveLocation> exceptMoves = _all.Except(madeMoves).ToList();
            int index = _random.Next(exceptMoves.Count);
            return exceptMoves[index];
        }
    }
}