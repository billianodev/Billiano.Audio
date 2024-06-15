using Billiano.Audio;
using Billiano.Audio.FireForget;
using CSCore.Codecs;
using NAudio.Wave;

start:
using (var reader = CodecFactory.Instance.GetCodec("spacebar.mp3").ToWaveProvider())
{
    using (var player = new FireForgetPlayer(new WaveOutEvent(), reader.WaveFormat))
    {
        var delay = player.Play(reader.ToFireForgetSource());
        await Task.Delay(delay);
    }
}

// Console.ReadLine();

goto start;
