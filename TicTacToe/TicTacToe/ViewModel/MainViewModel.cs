using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using TicTacToe.Domain;
using TicTacToe.Domain.Exceptions;
using TicTacToe.Domain.Results;

namespace TicTacToe.ViewModel
{
    public sealed class MainViewModel : ViewModelBase
    {
        private const string Krestik = "X";
        private const string Nolik = "O";
        private TicTacToeGame _game;
        private Player _player1;
        private Player _player2;
        private string _statusText;

        public MainViewModel()
        {
            StatusText = "Здесь проходит бой!";
            MakeMoveCommand = new RelayCommand<MoveLocation>(MakeMoveCommandExecute, MakeMoveCommandCanExecute);
            NewGameCommand = new RelayCommand(NewGameCommandExecute, NewGameCommandCanExecute);
            ExitCommand = new RelayCommand(ExitCommandExecute);
            Fields = new List<FieldInfoViewModel>
            {
                new FieldInfoViewModel(MoveLocation.TopLeft),
                new FieldInfoViewModel(MoveLocation.TopCenter),
                new FieldInfoViewModel(MoveLocation.TopRight),
                new FieldInfoViewModel(MoveLocation.CenterLeft),
                new FieldInfoViewModel(MoveLocation.Center),
                new FieldInfoViewModel(MoveLocation.CenterRight),
                new FieldInfoViewModel(MoveLocation.BottomLeft),
                new FieldInfoViewModel(MoveLocation.BottomCenter),
                new FieldInfoViewModel(MoveLocation.BottomRight),
            };
        }

        public List<FieldInfoViewModel> Fields { get; private set; }

        public ICommand MakeMoveCommand { get; private set; }
        public ICommand NewGameCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        public string StatusText
        {
            get { return _statusText; }
            private set
            {
                _statusText = value;
                RaisePropertyChanged(() => StatusText);
            }
        }

        public string Player1Name { get; set; }
        public string Player2Name { get; set; }

        private void MakeMoveCommandExecute(MoveLocation location)
        {
            try
            {
                _game.MakeMove(location);
                var field = Fields.Find(x => x.Location == location);
                field.Text = _player1 == _game.PlayerWhoLastMoved ? Krestik : Nolik;
                if (_game.IsFinished)
                {
                    GameResult result = _game.GetResults();
                    switch (result.Type)
                    {
                        case GameResultType.DrawnGame:
                            StatusText = "Игра окончена. Ничья.";
                            break;
                        case GameResultType.PlayerVictory:
                            StatusText = string.Format("Игра окончена. Победил {0}",
                                ((PlayerVictoryResult) result).Winner.Name);
                            break;
                    }
                }
                else
                {
                    StatusText = string.Format("Ход игрока: {0}", _game.PlayerWhoMoves.Name);
                }
            }
            catch (DomainException ex)
            {
                StatusText = ex.Message;
            }
        }

        private bool MakeMoveCommandCanExecute(MoveLocation location)
        {
            return _game != null && !_game.IsFinished;
        }

        private void NewGameCommandExecute()
        {
            _player1 = new Player(Player1Name);
            _player2 = new Player(Player2Name);
            _game = new TicTacToeGame(_player1, _player2);
            StatusText = string.Format("Ход игрока: {0}", _game.PlayerWhoMoves.Name);
        }

        private bool NewGameCommandCanExecute()
        {
            return !string.IsNullOrWhiteSpace(Player1Name) && !string.IsNullOrWhiteSpace(Player2Name);
        }

        private void ExitCommandExecute()
        {
            Application.Current.Shutdown();
        }
    }
}