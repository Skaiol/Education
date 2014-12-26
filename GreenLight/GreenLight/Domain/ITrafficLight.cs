namespace GreenLight.Domain
{
    public interface ITrafficLight
    {
        bool IsEnabled { get; }
        bool IsBlinkingYellow { get; }
        Lamp Green { get; }
        Lamp Yellow { get; }
        Lamp Red { get; }
    }
}