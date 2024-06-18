# Billiano.Audio.PortAudio

[![ko-fi](https://img.shields.io/badge/Support_me_on-Ko--fi-red)](https://ko-fi.com/G2G1SRUJG)
[![](https://img.shields.io/badge/Check-NAudio-white)](https://github.com/naudio/NAudio)
[![](https://img.shields.io/badge/Check-PortAudio-green)](https://portaudio.com)

Implements PortAudioSharp as NAudio `IWavePlayer`

## Example

```csharp
using (var reader = new WaveFileReader("test.wav"))
{
    var backend = new PortAudioOut();
    player.Init(reader);
    player.Play();
}
```

---

## Important

⚠️ **While this project is in BETA state, it will often receive code breaking update! Sorry for the inconvenience..** ⚠️