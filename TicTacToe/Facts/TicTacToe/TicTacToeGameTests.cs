using TicTacToe.Domain;
using TicTacToe.Domain.Exceptions;
using Xunit;

namespace Facts.TicTacToe
{
    public sealed class TicTacToeGameTests
    {
        private readonly TicTacToeGame _game;

        public TicTacToeGameTests()
        {
            _game = new TicTacToeGame();
        }

        [Fact]
        public void GetResults_GameIsNotFinished_Exception()
        {
            Assert.True(!_game.GameIsFinished);
            Assert.Throws<DomainException>(() => _game.GetResults());
        }

        [Fact]
        public void MakeMove_PlayerMovesTwoConsecutiveTimes_Exception()
        {
            var player = new Player("Player1");
            var move = new MoveInfo {Player = player};

            _game.MakeMove(move);
            Assert.Throws<DomainException>(() => _game.MakeMove(move));
        }

        [Fact]
        public void MakeMove_MoreThenTwoPlayers_Exception()
        {
            var player1 = new Player("Player1");
            var player2 = new Player("Player2");
            var player3 = new Player("Player3");
            var move1 = new MoveInfo {Player = player1, Location = MoveLocation.BottomCenter};
            var move2 = new MoveInfo {Player = player2, Location = MoveLocation.TopLeft};
            var move3 = new MoveInfo {Player = player3, Location = MoveLocation.TopRight};

            _game.MakeMove(move1);
            _game.MakeMove(move2);
            Assert.Throws<DomainException>(() => _game.MakeMove(move3));
        }

        [Fact]
        public void MakeMove_PlayerMovesTwoTimesInSameLocation_Exception()
        {
            var player = new Player("Player1");
            var move1 = new MoveInfo {Player = player, Location = MoveLocation.BottomCenter};
            var move2 = new MoveInfo {Player = player, Location = MoveLocation.BottomCenter};

            _game.MakeMove(move1);
            Assert.Throws<DomainException>(() => _game.MakeMove(move2));
        }
    }
}