# Billiano.Audio.FireForget

A library that implements fire and forget audio playback by caching audio source and playing it with audio mixer (all with one audio out)

Support both CSCore and NAudio

---

## Example

```csharp
using (var reader = CodecFactory.Instance.GetCodec("spacebar.mp3").ToWaveProvider())
{
    using (var player = new FireForgetPlayer(new WaveOutEvent(), reader.WaveFormat))
    {
        var delay = player.Play(reader.ToFireForgetSource());
        await Task.Delay(delay);
    }
}
```