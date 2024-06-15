# Billiano.Audio

## Features

- Combine .NET audio libraries (bridge/convert between similar object)
  - NAudio (better for audio processing, imo)
  - CSCore (better for encoding/decoding, imo)
  

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
  - ~~More with FFmpeg~~

- Other
  - Easily cast between byte array and float array
  - Better resampler combine wdl resampler and mono-stereo resampling