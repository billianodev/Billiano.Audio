using Billiano.Audio;
using Billiano.Audio.FireForget;
using CSCore.Codecs;
using NAudio.Wave;



using (var reader = CodecFactory.Instance.GetCodec("test.mp3").ToWaveProvider())
{
    var backend = new WasapiOut();
    var source = reader.ToFireForgetSource();
    var player = new FireForgetPlayer(backend, new WaveFormat());

    start:
    Console.ReadKey(true);
    player.Play(source);
    goto start;
}


