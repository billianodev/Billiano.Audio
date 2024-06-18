/*
 * This project is a sample code to Billiano.Audio packages
 * Run this sample only in DEBUG mode!
 */

/*
 * (Un)comment line below to switch between fire forget mode and direct output
 */
#define USE_FIRE_FORGET

using Billiano.Audio;
using Billiano.Audio.PortAudio;
using NAudio.Wave;

#if USE_FIRE_FORGET
using Billiano.Audio.FireForget;
#endif

var codecFactory = CodecFactory.CreateDefault();

#if USE_FIRE_FORGET
using var player = new PortAudioOut();
using var fireForgetPlayer = new FireForgetPlayer(player, new WaveFormat(44100, 1));
#endif

Console.ReadLine();
Play("test.mp3");
Console.ReadLine();
Play("test_too.mp3");
Console.ReadLine();
return;

void Play(string path)
{
    var reader = codecFactory.GetCodec(path);
    var source = reader.ToWaveCache();
    
#if USE_FIRE_FORGET
    fireForgetPlayer.Play(source);
#else
    var player = new PortAudioOut();
    player.Init(source.ToWaveProvider());
    player.Play();
    player.PlaybackStopped += (sender, eventArgs) => player.Dispose();
#endif
}