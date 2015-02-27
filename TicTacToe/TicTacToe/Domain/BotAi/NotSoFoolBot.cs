using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Domain.BotAi
{
    public sealed class NotSoFoolBot : BotProcessor
    {
        private readonly List<MoveLocation> _all;
        private readonly Random _random;
        private readonly List<List<MoveLocation>> _winConditions;

        public NotSoFoolBot(TicTacToeGame game, List<MoveInfo> moves) : base(game, moves)
        {
            _random = new Random();
            _all = Enum.GetValues(typeof (MoveLocation))
                .Cast<MoveLocation>()
                .ToList();
            _winConditions = new List<List<MoveLocation>>
            {
                new List<MoveLocation> {MoveLocation.TopLeft, MoveLocation.TopCenter, MoveLocation.TopRight},
                new List<MoveLocation> {MoveLocation.CenterLeft, MoveLocation.Center, MoveLocation.CenterRight},
                new List<MoveLocation> {MoveLocation.BottomLeft, MoveLocation.BottomCenter, MoveLocation.BottomRight},
                new List<MoveLocation> {MoveLocation.TopLeft, MoveLocation.CenterLeft, MoveLocation.BottomLeft},
                new List<MoveLocation> {MoveLocation.TopCenter, MoveLocation.Center, MoveLocation.BottomCenter},
                new List<MoveLocation> {MoveLocation.TopRight, MoveLocation.CenterRight, MoveLocation.BottomRight},
                new List<MoveLocation> {MoveLocation.TopLeft, MoveLocation.Center, MoveLocation.BottomRight},
                new List<MoveLocation> {MoveLocation.BottomLeft, MoveLocation.Center, MoveLocation.TopRight}
            };
        }

        protected override MoveLocation CalcNextLocation()
        {
            List<MoveLocation> madeMoves = Moves.ConvertAll(x => x.Location);
            List<MoveLocation> enemyMoves = Moves
                .Where(x => x.Player.Id == Game.PlayerWhoLastMoved.Id)
                .Select(x => x.Location)
                .ToList();
            List<MoveLocation> botMoves = madeMoves.Except(enemyMoves).ToList();

            List<MoveLocation> botPreWinCondition =
                _winConditions.FirstOrDefault(x => x.Except(botMoves).Count() == 1 && x.Except(madeMoves).Any());
            List<MoveLocation> enemyPreWinCondition =
                _winConditions.FirstOrDefault(x => x.Except(enemyMoves).Count() == 1 && x.Except(madeMoves).Any());

            if (botPreWinCondition == null)
            {
                return enemyPreWinCondition != null ? enemyPreWinCondition.Except(enemyMoves).First() : RandomMove();
            }
            return botPreWinCondition.Except(botMoves).First();
        }

        private MoveLocation RandomMove()
        {
            List<MoveLocation> madeMoves = Moves.ConvertAll(x => x.Location);
            List<MoveLocation> exceptMoves = _all.Except(madeMoves).ToList();
            int index = _random.Next(exceptMoves.Count);
            return exceptMoves[index];
        }
    }
}