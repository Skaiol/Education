using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Domain.BotAi;
using TicTacToe.Domain.Exceptions;
using TicTacToe.Domain.Results;

namespace TicTacToe.Domain
{
    public sealed class TicTacToeGame
    {
        private readonly int _maxMoveCount;
        private readonly List<MoveInfo> _moves;
        private readonly Player _player1;
        private readonly Player _player2;
        private readonly List<List<MoveLocation>> _winConditions;
        private GameResult _gameResult;

        public TicTacToeGame(Player player1, Player player2)
        {
            _player2 = player2;
            _player1 = player1;
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
            if (_player1.IsBot || _player2.IsBot)
            {
                BotProcessor = new CleverEnoughBot(this, _moves);
            }
        }

        public bool IsFinished { get; private set; }

        public int MovesCount
        {
            get { return _moves.Count; }
        }

        public Player PlayerWhoMoves
        {
            get { return MovesCount%2 == 0 ? _player1 : _player2; }
        }

        public Player PlayerWhoLastMoved
        {
            get { return PlayerWhoMoves == _player1 ? _player2 : _player1; }
        }

        public BotProcessor BotProcessor { get; private set; }

        public void MakeMove(MoveLocation location)
        {
            if (IsFinished)
            {
                throw new DomainException("Игра уже завершена");
            }

            ValidateMove(location);
            _moves.Add(new MoveInfo
            {
                Location = location,
                Player = PlayerWhoMoves
            });
            ProcessMove();
        }

        public GameResult GetResults()
        {
            if (!IsFinished)
            {
                throw new DomainException("Игра еще не окончена!");
            }
            return _gameResult;
        }

        private void ValidateMove(MoveLocation location)
        {
            if (!_moves.Any())
            {
                return;
            }

            if (_moves.Select(x => x.Location).Contains(location))
            {
                throw new DomainException(string.Format("Игрок {0} пытается походить по уже хоженой тропе",
                    PlayerWhoMoves.Name));
            }
        }

        private void ProcessMove()
        {
            CheckIfLastPlayerWins();
            if (_moves.Count == _maxMoveCount)
            {
                IsFinished = true;
                if (_gameResult == null)
                {
                    _gameResult = new DrawnGameResult(MovesCount);
                }
            }
        }

        private void CheckIfLastPlayerWins()
        {
            List<MoveLocation> locations = _moves
                .Where(x => x.Player.Id == PlayerWhoLastMoved.Id)
                .Select(x => x.Location)
                .ToList();
            if (_winConditions.Any(x => !x.Except(locations).Any()))
            {
                IsFinished = true;
                _gameResult = new PlayerVictoryResult(PlayerWhoLastMoved, MovesCount);
            }
        }
    }
}