namespace BabySounds.Client.Effects;

/// <summary>
/// Base class for all effects
/// </summary>
public abstract class Effect
{
    private readonly List<Slider> _sliders;
    protected float SampleRate { get; set; }
    public float Tempo { get; set; }
    public bool Enabled { get; set; }

    protected Effect()
    {
        _sliders = new List<Slider>();
        Enabled = true;
        Tempo = 120;
        SampleRate = 44100;
    }

    public IList<Slider> Sliders => _sliders;

    protected Slider AddSlider(
        float defaultValue,
        float minimum,
        float maximum,
        float increment,
        string description
    )
    {
        var slider = new Slider(defaultValue, minimum, maximum, increment, description);
        _sliders.Add(slider);
        return slider;
    }

    protected abstract string Name { get; }

    // helper base methods
    // these are primarily to enable derived classes to use a similar
    // syntax to JS effects
    protected float Slider1 => _sliders[0].Value;

    protected float Slider2 => _sliders[1].Value;

    protected float Slider3 => _sliders[2].Value;

    protected float Slider4 => _sliders[3].Value;

    protected float Slider5 => _sliders[4].Value;

    protected float Slider6 => _sliders[5].Value;

    protected float Slider7 => _sliders[6].Value;

    protected float Slider8 => _sliders[7].Value;

    protected float Min(float a, float b)
    {
        return Math.Min(a, b);
    }

    protected float Max(float a, float b)
    {
        return Math.Max(a, b);
    }

    protected float Abs(float a)
    {
        return Math.Abs(a);
    }

    protected float Exp(float a)
    {
        return (float)Math.Exp(a);
    }

    protected float Sqrt(float a)
    {
        return (float)Math.Sqrt(a);
    }

    protected float Sin(float a)
    {
        return (float)Math.Sin(a);
    }

    protected float Tan(float a)
    {
        return (float)Math.Tan(a);
    }

    protected float Cos(float a)
    {
        return (float)Math.Cos(a);
    }

    protected float Pow(float a, float b)
    {
        return (float)Math.Pow(a, b);
    }

    protected float Sign(float a)
    {
        return Math.Sign(a);
    }

    protected float Log(float a)
    {
        return (float)Math.Log(a);
    }

    protected float Pi => (float)Math.PI;

    protected void convolve_c(
        float[] buffer1,
        int offset1,
        float[] buffer2,
        int offset2,
        int count
    )
    {
        for (var i = 0; i < count * 2; i += 2)
        {
            var r = buffer1[offset1 + i];
            var im = buffer1[offset1 + i + 1];
            var cr = buffer2[offset2 + i];
            var ci = buffer2[offset2 + i + 1];
            buffer1[offset1 + i] = r * cr - im * ci;
            buffer1[offset1 + i + 1] = r * ci + im * cr;
        }
    }

    /// <summary>
    /// Should be called on effect load, sample rate changes, and start of playback
    /// </summary>
    internal virtual void Init()
    {
    }

    private volatile bool _sliderChanged;

    internal void SliderChanged()
    {
        _sliderChanged = true;
    }

    /// <summary>
    /// will be called when a slider value has been changed
    /// </summary>
    protected abstract void Slider();

    /// <summary>
    /// called before each block is processed
    /// </summary>
    /// <param name="samplesBlock">number of samples in this block</param>
    public virtual void Block(int samplesBlock)
    {
    }

    internal void OnSample(ref float left)
    {
        if (_sliderChanged)
        {
            Slider();
            _sliderChanged = false;
        }

        Sample(ref left);
    }

    /// <summary>
    /// called for each sample
    /// </summary>
    protected abstract void Sample(ref float spl0);

    public override string ToString()
    {
        return Name;
    }
}