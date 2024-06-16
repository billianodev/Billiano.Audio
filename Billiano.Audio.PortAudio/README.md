# Billiano.Audio.PortAudio

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

