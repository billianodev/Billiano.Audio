using Billiano.Audio.FireForget;
using Billiano.Audio.PortAudio;
using CSCore.Codecs;

Console.ReadLine();
Play("test.mp3");
Console.ReadLine();
Play("test_too.mp3");
Console.ReadLine();
return;

void Play(string path)
{
    using var reader = CodecFactory.Instance.GetCodec(path);
    var source = reader.ToFireForgetSource();
    
    var player = new PortAudioOut();
    player.Init(source.ToWaveProvider());
    player.Play();
    player.PlaybackStopped += (sender, eventArgs) => player.Dispose();
}