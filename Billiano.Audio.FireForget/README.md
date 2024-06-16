# Billiano.Audio.FireForget

[![ko-fi](https://img.shields.io/badge/Support_me_on-Ko--fi-red)](https://ko-fi.com/G2G1SRUJG)
[![](https://img.shields.io/badge/Check-NAudio-white)](https://github.com/naudio/NAudio)
[![](https://img.shields.io/badge/Check-CSCore-blue)](https://github.com/filoe/cscore)

A library that implements fire and forget audio playback by caching audio source and playing it with audio mixer (all with one audio out)

Support both CSCore and NAudio

---

## Example

```csharp
using (var reader = CodecFactory.Instance.GetCodec("test.mp3").ToWaveProvider())
{
    using (var player = new FireForgetPlayer(new WaveOutEvent(), reader.WaveFormat))
    {
        var delay = player.Play(reader.ToFireForgetSource());
        await Task.Delay(delay);
    }
}
```