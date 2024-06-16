# Billiano.Audio

[![ko-fi](https://img.shields.io/badge/Support_me_on-Ko--fi-red)](https://ko-fi.com/G2G1SRUJG)
[![](https://img.shields.io/badge/Check-NAudio-white)](https://github.com/naudio/NAudio)
[![](https://img.shields.io/badge/Check-CSCore-blue)](https://github.com/filoe/cscore)
[![](https://img.shields.io/badge/Check-PortAudio-green)](https://portaudio.com)

Collection of audio utils and library

## Features

- Combine .NET audio libraries (bridge/convert between similar object)
    - NAudio (better for audio processing, imo)
    - CSCore (better for encoding/decoding, imo)
    - PortAudioSharp (cross-platform audio playback)


- Read common codecs (through CSCore `CodecFactory`)
    - MP3
    - WAVE (PCM, IeeeFloat, ...)
    - FLAC
    - AIFF
    - AAC
    - AC3
    - WMA
    - Ogg
    - More with MediaFoundation

- Other
    - Easily cast between byte array and float array
    - Better resampler combine wdl resampler and mono-stereo resampling


- Planned
  - Ffmpeg support with CSCore
