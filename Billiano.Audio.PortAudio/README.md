# Billiano.Audio.PortAudio

[![ko-fi](https://img.shields.io/badge/Support_me_on-Ko--fi-red)](https://ko-fi.com/G2G1SRUJG)
[![](https://img.shields.io/badge/Check-NAudio-white)](https://github.com/naudio/NAudio)
[![](https://img.shields.io/badge/Check-PortAudio-green)](https://portaudio.com)

Adapter for PortAudioSharp and NAudio `IWavePlayer`

## Example

```csharp
using (var reader = CodecFactory.Instance.GetCodec("test.mp3").ToWaveProvider())
{
    var backend = new PortAudioOut();
    player.Init(reader);
    player.Play();
}
```

