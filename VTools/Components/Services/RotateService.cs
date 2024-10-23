namespace VTools.Components.Services;

public class RotateService(int value = 0)
{
    public int Value { get; private set; } = value;

    public string GetRotateTransform() => Value == 0 ? "transform: rotate(0);" : $"transform: rotate({Value}deg)";

    public void RotateMore90()
    {
        Value += 90;

        if (Value >= 360)
        {
            Value = 0;
        }
    }

    public void RotateLess90()
    {
        if (Value <= 0)
        {
            Value = 360;
        }

        Value -= 90;
    }
}