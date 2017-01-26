using System.IO;
using NAudio.Lame;
using NAudio.Wave;

namespace PhotoStoryToBloomConverter
{
	class Mp3Encoder : IAudioEncoder
	{
		public string Encode(string sourcePath, string destPathWithoutExtension, AudioOptions audioOptions = AudioOptions.None)
		{
			var outputPath = destPathWithoutExtension + "." + FormatName;
			var reader = new WaveFileReader(sourcePath);

//			var mediaType = MediaFoundationEncoder.SelectMediaType(
//				AudioSubtypes.MFAudioFormat_MP3,
//				new WaveFormat(44100, 1),
//				0);
//			if (mediaType != null)
//			{
//				using (var enc = new MediaFoundationEncoder(mediaType))
//				{
//					enc.Encode(outputPath, reader);
//				}
//			}
//			else
//			{
//				Debug.WriteLine("mediaType null");
//				Console.WriteLine("mediaType null");
//			}

//			var desiredBitRate = 0; // ask for lowest available bitrate 
//			MediaFoundationEncoder.EncodeToMp3(reader, outputPath, desiredBitRate); 

//			var waveFormat = reader.WaveFormat;
//			waveFormat.;

			using (var writer32 = new LameMP3FileWriter(GetOutputPath(outputPath, "32"), reader.WaveFormat, 32))
				reader.CopyTo(writer32);

			reader = new WaveFileReader(sourcePath);
			using (var writer64 = new LameMP3FileWriter(GetOutputPath(outputPath, "64"), reader.WaveFormat, 64))
				reader.CopyTo(writer64);

			reader = new WaveFileReader(sourcePath);
			ConvertWav(reader, sourcePath, new WaveFormat(22000, 32, 1));
			using (var writer32mono = new LameMP3FileWriter(GetOutputPath(outputPath, "32mono"), reader.WaveFormat, 32))
				reader.CopyTo(writer32mono);

			reader = new WaveFileReader(sourcePath);
			ConvertWav(reader, sourcePath, new WaveFormat(22000, 32, 1));
			using (var writer64mono = new LameMP3FileWriter(GetOutputPath(outputPath, "64mono"), reader.WaveFormat, 64))
				reader.CopyTo(writer64mono);
//			using (var writer = new LameMP3FileWriter(outputPath, new WaveFormat(22000, 16, 1), LAMEPreset.V9))
			//using (var writer = new LameMP3FileWriter(outputPath, new WaveFormat(22000, 8, 1), LAMEPreset.VBR_50))
			return outputPath;
		}

		private void ConvertWav(WaveStream stream, string outputPath, WaveFormat target)
		{
			WaveFormatConversionStream conversionStream = new WaveFormatConversionStream(target, stream);
			WaveFileWriter.CreateWaveFile(outputPath, conversionStream);
		}

		private string GetOutputPath(string path, string extra = "")
		{
			return Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path) + "." + extra + Path.GetExtension(path));
		}

		public string FormatName
		{
			get { return "mp3"; }
		}
	}
}
