namespace BabySounds.Client.Effects;

public sealed class Slider
{
    private readonly List<string> _discreteValueText;

    public Slider(
        float defaultValue,
        float minimum,
        float maximum,
        float increment,
        string description
    )
    {
        this.Default = defaultValue;
        this.Value = defaultValue;
        this.Minimum = minimum;
        this.Maximum = maximum;
        this.Increment = increment;
        this.Description = description;
        this._discreteValueText = new List<string>();
    }

    public string Description { get; set; }
    public float Default { get; private set; }
    public float Minimum { get; private set; }
    public float Maximum { get; private set; }
    public float Increment { get; private set; }
    public float Value { get; set; }

    public IList<string> DiscreteValueText => _discreteValueText;
}
