# Billiano.Audio

[![ko-fi](https://img.shields.io/badge/Support_me_on-Ko--fi-red)](https://ko-fi.com/G2G1SRUJG)
[![](https://img.shields.io/badge/Check-NAudio-white)](https://github.com/naudio/NAudio)
[![](https://img.shields.io/badge/Check-CSCore-blue)](https://github.com/filoe/cscore)

General purpose audio utility library

## Features

- NAudio-CSCore converter, provides two-way conversion for similar classes/interfaces between both audio library


- CodecFactory, reimplement more cross-platform friendly CSCore `CodecFactory`


- ResamplingSampleProvider, combine NAudio `WdlResamplingSampleProvider`, `MonoToStereoSampleProvider`, and `StereoToMonoSampleProvider`


- WaveSampleBuffer, unsafely and memory efficiently convert between `byte[]` and `float[]`