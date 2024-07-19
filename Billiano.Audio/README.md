# Billiano.Audio

[![ko-fi](https://img.shields.io/badge/Support_me_on-Ko--fi-red)](https://ko-fi.com/G2G1SRUJG)
[![](https://img.shields.io/badge/Check-NAudio-white)](https://github.com/naudio/NAudio)

General purpose audio utility library

## Features

- `CodecFactory`, reimplement more cross-platform friendly CSCore `CodecFactory`
    - WAVE (from NAudio.Core)
    - MP3 (from NLayer.NAudioSupport)
    - Vorbis (from NAudio.Vorbis)
    - More from Billiano.Audio.Codecs packages or by implementing your own with `CodecFactory.Register`


- `FireForgetPlayer` and `WaveCache`, cache and reuse audio to be player repeatedly


- `ResamplingSampleProvider`, combine NAudio `WdlResamplingSampleProvider`, `MonoToStereoSampleProvider`, and `StereoToMonoSampleProvider`

---

## Important

⚠️ **While this project is in BETA state, it will often receive code breaking update! Sorry for the inconvenience** ⚠️