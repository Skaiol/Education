using System;
using System.Windows.Media;

namespace GreenLight.Domain
{
    public sealed class Lamp
    {
        private readonly SolidColorBrush _initialColor;

        public Lamp(SolidColorBrush initialColor)
        {
            _initialColor = initialColor;
            Color = initialColor;
        }

        public SolidColorBrush Color { get; private set; }
        public event EventHandler StateChanged;

        public void TurnOn()
        {
            Color = _initialColor;
            ColorChanged();
        }

        public void TurnOff()
        {
            Color = Brushes.Black;
            ColorChanged();
        }

        public void Toggle()
        {
            Color = Color == Brushes.Black ? _initialColor : Brushes.Black;
            ColorChanged();
        }

        private void ColorChanged()
        {
            if (StateChanged != null)
            {
                StateChanged(this, null);
            }
        }
    }
}