using Billiano.Audio;
using Billiano.Audio.FireForget;
using Billiano.Audio.PortAudio;

/*
 * This project is a sample code to Billiano.Audio packages
 * Run this sample only in DEBUG mode!
 */

var codecFactory = CodecFactory.CreateDefault();

Console.ReadLine();
Play("test.mp3");
Console.ReadLine();
Play("test_too.mp3");
Console.ReadLine();
return;

void Play(string path)
{
    using var reader = codecFactory.GetCodec(path);
    var source = reader.ToFireForgetSource();
    
    var player = new PortAudioOut();
    player.Init(source.ToWaveProvider());
    player.Play();
    player.PlaybackStopped += (sender, eventArgs) => player.Dispose();
}