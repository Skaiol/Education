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
        private string _player1Name;
        private bool _player1NameIsChecked;
        private Player _player2;
        private string _player2Name;
        private bool _player2NameIsChecked;
        private string _statusText;

        public MainViewModel()
        {
            StatusText = "Здесь проходит бой!";
            Player1NameIsChecked = true;
            Player2NameIsChecked = false;
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

        public bool Player1NameIsChecked
        {
            get { return _player1NameIsChecked; }
            private set
            {
                _player1NameIsChecked = value;
                RaisePropertyChanged(() => Player1NameIsChecked);
                Player1Name = value ? string.Empty : "Bot";
            }
        }

        public bool Player2NameIsChecked
        {
            get { return _player2NameIsChecked; }
            private set
            {
                _player2NameIsChecked = value;
                RaisePropertyChanged(() => Player2NameIsChecked);
                Player2Name = value ? string.Empty : "Bot";
            }
        }

        public string Player1Name
        {
            get { return _player1Name; }
            private set
            {
                _player1Name = value;
                RaisePropertyChanged(() => Player1Name);
            }
        }

        public string Player2Name
        {
            get { return _player2Name; }
            private set
            {
                _player2Name = value;
                RaisePropertyChanged(() => Player2Name);
            }
        }

        private void MakeMoveCommandExecute(MoveLocation location)
        {
            try
            {
                _game.MakeMove(location);
                MakePostMoveActions(location);
            }
            catch (DomainException ex)
            {
                StatusText = ex.Message;
            }
        }

        private void MakePostMoveActions(MoveLocation location)
        {
            FieldInfoViewModel field = Fields.Find(x => x.Location == location);
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
                if (_game.PlayerWhoMoves.IsBot)
                {
                    _game.BotProcessor.MakeMove();
                }
            }
        }

        private bool MakeMoveCommandCanExecute(MoveLocation location)
        {
            return _game != null && !_game.IsFinished;
        }

        private void NewGameCommandExecute()
        {
            _player1 = new Player(Player1Name, !Player1NameIsChecked);
            _player2 = new Player(Player2Name, !Player2NameIsChecked);
            _game = new TicTacToeGame(_player1, _player2);
            _game.BotProcessor.RegisterBotMoveListener(MakePostMoveActions);

            StatusText = string.Format("Ход игрока: {0}", _game.PlayerWhoMoves.Name);
            Fields.ForEach(x => x.Text = "");

            if (_game.PlayerWhoMoves.IsBot)
            {
                _game.BotProcessor.MakeMove();
            }
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