using GalaSoft.MvvmLight;
using TicTacToe.Domain;

namespace TicTacToe.ViewModel
{
    public sealed class FieldInfoViewModel : ViewModelBase
    {
        private string _text;

        public FieldInfoViewModel(MoveLocation location)
        {
            Location = location;
        }

        public MoveLocation Location { get; private set; }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged(() => Text);
            }
        }
    }
}