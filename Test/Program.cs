using Billiano.Audio;
using Billiano.Audio.FireForget;
using Billiano.Audio.PortAudio;
using CSCore.Codecs;

using (var reader = CodecFactory.Instance.GetCodec("test.mp3").ToWaveProvider())
{
    var backend = new PortAudioOut(latency: 10);
    var source = reader.ToFireForgetSource();
    var player = new FireForgetPlayer(backend, reader.WaveFormat);

    start:
    Console.ReadKey(true);
    player.Play(source);
    goto start;
}


