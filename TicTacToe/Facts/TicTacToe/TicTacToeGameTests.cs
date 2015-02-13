using TicTacToe.Domain;
using TicTacToe.Domain.Exceptions;
using TicTacToe.Domain.Results;
using Xunit;

namespace Facts.TicTacToe
{
    public sealed class TicTacToeGameTests
    {
        private readonly TicTacToeGame _game;
        private readonly Player _player1;
        private readonly Player _player2;

        public TicTacToeGameTests()
        {
            _player1 = new Player("Player 1");
            _player2 = new Player("Player 2");
            _game = new TicTacToeGame(_player1, _player2);
        }

        [Fact]
        public void GetResults_GameIsNotFinished_Exception()
        {
            Assert.True(!_game.IsFinished);
            Assert.Throws<DomainException>(() => _game.GetResults());
        }

        [Fact]
        public void MakeMove_PlayerMovesInMovedLocation_Exception()
        {
            _game.MakeMove(MoveLocation.BottomCenter);

            Assert.Throws<DomainException>(() => _game.MakeMove(MoveLocation.BottomCenter));
        }

        [Fact]
        public void SimpleGame_PlayerWins()
        {
            _game.MakeMove(MoveLocation.TopLeft);
            _game.MakeMove(MoveLocation.BottomRight);
            _game.MakeMove(MoveLocation.BottomLeft);
            _game.MakeMove(MoveLocation.CenterLeft);
            _game.MakeMove(MoveLocation.TopRight);
            _game.MakeMove(MoveLocation.TopCenter);
            _game.MakeMove(MoveLocation.Center);
            var result = (PlayerVictoryResult) _game.GetResults();

            Assert.True(_game.IsFinished);
            Assert.Equal(7, _game.MovesCount);
            Assert.Equal(_player1, result.Winner);
        }

        [Fact]
        public void SimpleGame_DrawnGame()
        {
            _game.MakeMove(MoveLocation.Center);
            _game.MakeMove(MoveLocation.BottomRight);
            _game.MakeMove(MoveLocation.TopLeft);
            _game.MakeMove(MoveLocation.BottomLeft);
            _game.MakeMove(MoveLocation.BottomCenter);
            _game.MakeMove(MoveLocation.TopCenter);
            _game.MakeMove(MoveLocation.CenterLeft);
            _game.MakeMove(MoveLocation.CenterRight);
            _game.MakeMove(MoveLocation.TopRight);

            Assert.True(_game.IsFinished);
            Assert.Equal(9, _game.MovesCount);
            Assert.IsType(typeof (DrawnGameResult), _game.GetResults());
        }
    }
}