// .NET port of Super-pitch JS effect included with Cockos REAPER

namespace BabySounds.Client.Effects;

internal sealed class SuperPitch : Effect
{
    private int _bufsize;
    private float _xfade;
    private int _bufloc0;
    private int _buffer0;
    private float _pitch;

    private float _denorm;
    private bool _filter;
    private float _v0;
    private float _h01, _h02, _h03, _h04;
    private float _a1, _a2, _a3, _b1, _b2;
    private float _t0;
    private float _drymix;
    private float _wetmix;
    private readonly float[] _buffer = new float[64000];

    public SuperPitch()
    {
        AddSlider(0, -100, 100, 1, "Pitch adjust (cents)");
        AddSlider(-10, -12, 12, 1, "Pitch adjust (semitones)");
        AddSlider(0, -8, 8, 1, "Pitch adjust (octaves)"); // mrh 12 octaves up causes issues, reigning this in a bit
        AddSlider(50, 1, 200, 1, "Window size (ms)"); // mrh minimum window size set to 1 as 0 seems to cause issues
        AddSlider(20, 0.05f, 50, 0.5f, "Overlap size (ms)");
        AddSlider(0, -120, 6, 1, "Wet mix (dB)");
        AddSlider(-120, -120, 6, 1, "Dry mix (dB)");
        var filterSlider = AddSlider(1, 0, 1, 1, "Filter"); // {NO,YES}filter

        filterSlider.DiscreteValueText.Add("Off");
        filterSlider.DiscreteValueText.Add("On");

    }

    protected override string Name => "SuperPitch";

    internal override void Init()
    {
        _bufsize = (int)SampleRate; // srate|0;
        _xfade = 100;
        _bufloc0 = 10000;

        _buffer0 = _bufloc0;
        _pitch = 1.0f;
        _denorm = Pow(10, -20);
        base.Init();
    }

    protected override void Slider()
    {
        _filter = Slider8 > 0.5;
        var bsnew = (int)(Math.Min(Slider4, 1000) * 0.001 * SampleRate);
        //   bsnew=(min(slider4,1000)*0.001*srate)|0;
        if (bsnew != _bufsize)
        {
            _bufsize = bsnew;
            _v0 = _buffer0 + _bufsize * 0.5f;
            if (_v0 > _bufloc0 + _bufsize)
            {
                _v0 -= _bufsize;
            }
        }

        _xfade = (int)(Slider5 * 0.001 * SampleRate);
        if (_xfade > bsnew * 0.5)
        {
            _xfade = bsnew * 0.5f;
        }

        var npitch = Pow(2, ((Slider2 + Slider1 * 0.01f) / 12 + Slider3));
        if (_pitch != npitch)
        {
            _pitch = npitch;
            var lppos = (_pitch > 1.0f) ? 1.0f / _pitch : _pitch;
            if (lppos < (0.1f / SampleRate))
            {
                lppos = 0.1f / SampleRate;
            }

            var r = 1.0f;
            var c = 1.0f / Tan(Pi * lppos * 0.5f);
            _a1 = 1.0f / (1.0f + r * c + c * c);
            _a2 = 2 * _a1;
            _a3 = _a1;
            _b1 = 2.0f * (1.0f - c * c) * _a1;
            _b2 = (1.0f - r * c + c * c) * _a1;
            _h01 = _h02 = _h03 = _h04 = 0;
        }

        _drymix = Pow(2, (Slider7 / 6));
        _wetmix = Pow(2, (Slider6 / 6));
    }

    protected override void Sample(ref float spl0)
    {
        var iv0 = (int)(_v0);
        var frac0 = _v0 - iv0;
        var iv02 = (iv0 >= (_bufloc0 + _bufsize - 1)) ? iv0 - _bufsize + 1 : iv0 + 1;

        var ren0 = (_buffer[iv0 + 0] * (1 - frac0) + _buffer[iv02 + 0] * frac0);
        var vr = _pitch;
        float tv, frac, tmp, tmp2;
        if (vr >= 1.0)
        {
            tv = _v0;
            if (tv > _buffer0) tv -= _bufsize;
            if (tv >= _buffer0 - _xfade && tv < _buffer0)
            {
                // xfade
                frac = (_buffer0 - tv) / _xfade;
                tmp = _v0 + _xfade;
                if (tmp >= _bufloc0 + _bufsize) tmp -= _bufsize;
                tmp2 = (tmp >= _bufloc0 + _bufsize - 1) ? _bufloc0 : tmp + 1;
                ren0 = ren0 * frac + (1 - frac) * (_buffer[(int)tmp + 0] * (1 - frac0) + _buffer[(int)tmp2 + 0] * frac0);
                if (tv + vr > _buffer0 + 1) _v0 += _xfade;
            }
        }
        else
        {
            // read pointer moving slower than write pointer
            tv = _v0;
            if (tv < _buffer0) tv += _bufsize;
            if (tv >= _buffer0 && tv < _buffer0 + _xfade)
            {
                // xfade
                frac = (tv - _buffer0) / _xfade;
                tmp = _v0 + _xfade;
                if (tmp >= _bufloc0 + _bufsize) tmp -= _bufsize;
                tmp2 = (tmp >= _bufloc0 + _bufsize - 1) ? _bufloc0 : tmp + 1;
                ren0 = ren0 * frac + (1 - frac) * (_buffer[(int)tmp + 0] * (1 - frac0) + _buffer[(int)tmp2 + 0] * frac0);
                if (tv + vr < _buffer0 + 1) _v0 += _xfade;
            }
        }


        if ((_v0 += vr) >= (_bufloc0 + _bufsize)) _v0 -= _bufsize;

        var os0 = spl0;
        if (_filter && _pitch > 1.0)
        {

            _t0 = spl0;
            spl0 = _a1 * spl0 + _a2 * _h01 + _a3 * _h02 - _b1 * _h03 - _b2 * _h04 + _denorm;
            _h02 = _h01;
            _h01 = _t0;
            _h04 = _h03;
            _h03 = spl0;
        }


        _buffer[_buffer0 + 0] = spl0; // write after reading it to avoid clicks

        spl0 = ren0 * _wetmix;

        if (_filter && _pitch < 1.0)
        {
            _t0 = spl0;
            spl0 = _a1 * spl0 + _a2 * _h01 + _a3 * _h02 - _b1 * _h03 - _b2 * _h04 + _denorm;
            _h02 = _h01;
            _h01 = _t0;
            _h04 = _h03;
            _h03 = spl0;
        }

        spl0 += os0 * _drymix;

        if ((_buffer0 += 1) >= (_bufloc0 + _bufsize)) _buffer0 -= _bufsize;

    }
}
