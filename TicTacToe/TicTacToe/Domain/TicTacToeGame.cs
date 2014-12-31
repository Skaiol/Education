using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Domain.Exceptions;
using TicTacToe.Domain.Results;

namespace TicTacToe.Domain
{
    public sealed class TicTacToeGame
    {
        private readonly int _maxMoveCount;
        private readonly List<MoveInfo> _moves;
        private readonly List<List<MoveLocation>> _winConditions;
        private GameResult _gameResult;

        public TicTacToeGame()
        {
            _moves = new List<MoveInfo>();
            _maxMoveCount = Enum.GetNames(typeof (MoveLocation)).Length;
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

        public bool GameIsFinished { get; private set; }

        public int MovesCount
        {
            get { return _moves.Count; }
        }

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

        public GameResult GetResults()
        {
            if (!GameIsFinished)
            {
                throw new DomainException("Игра еще не окончена!");
            }
            return _gameResult;
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

            if (_moves.Select(x => x.Location).Contains(info.Location))
            {
                throw new DomainException(string.Format("Игрок {0} походил по уже хоженой тропе", info.Player.Name));
            }
        }

        private void ProcessMove(MoveInfo info)
        {
            CheckIfLastPlayerWins(info);
            if (_moves.Count == _maxMoveCount)
            {
                GameIsFinished = true;
                if (_gameResult == null)
                {
                    _gameResult = new DrawnGameResult(MovesCount);
                }
            }
        }

        private void CheckIfLastPlayerWins(MoveInfo info)
        {
            List<MoveLocation> locations = _moves
                .Where(x => x.Player.Id == info.Player.Id)
                .Select(x => x.Location)
                .ToList();
            if (_winConditions.Any(x => !x.Except(locations).Any()))
            {
                GameIsFinished = true;
                _gameResult = new PlayerVictoryResult(info.Player, MovesCount);
            }
        }
    }
}