using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Domain.Exceptions;

namespace TicTacToe.Domain
{
    public sealed class TicTacToeGame
    {
        private readonly int _maxMoveCount;
        private readonly List<MoveInfo> _moves;

        public TicTacToeGame()
        {
            _moves = new List<MoveInfo>();
            _maxMoveCount = Enum.GetNames(typeof (MoveLocation)).Length;
        }

        public bool GameIsFinished { get; private set; }

        public void MakeMove(MoveInfo info)
        {
            if (GameIsFinished)
            {
                throw new DomainException("Игра уже завершена");
            }

            ValidateMove(info);
            _moves.Add(info);
            ProcessMove(info);
        }

        private void ValidateMove(MoveInfo info)
        {
            if (!_moves.Any())
            {
                return;
            }

            if (_moves
                .Concat(new List<MoveInfo> {info})
                .Select(x => x.Player.Id)
                .Distinct()
                .Count() > 2)
            {
                throw new DomainException("В игре участвуют более 2-х игроков");
            }

            MoveInfo lastMove = _moves.Last();
            if (lastMove.Player.Id == info.Player.Id)
            {
                throw new DomainException(string.Format("Игрок {0} походил 2 раза подряд", info.Player.Name));
            }
        }

        private void ProcessMove(MoveInfo info)
        {
            if (_moves.Count == _maxMoveCount)
            {
                GameIsFinished = true;
            }
        }
    }
}