namespace ChipSecuritySystem;

public class ColorChip
{
    public ColorChip(Color startColor, Color endColor)
    {
        StartColor = startColor;
        EndColor = endColor;
    }

    public Color EndColor { get; private set; }
    public Color StartColor { get; private set; }
    
    public override string ToString()
    {
        return $"{this.StartColor}, {this.EndColor}";
    }
}