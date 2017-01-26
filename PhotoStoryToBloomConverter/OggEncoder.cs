using System;
using SIL.CommandLineProcessing;
using SIL.IO;
using SIL.Progress;

namespace PhotoStoryToBloomConverter
{
	[Flags]
	public enum AudioOptions
	{
		None = 0,
		OptimizeForSpeechOnly = 1
	}

	public interface IAudioEncoder
	{
		string Encode(string sourcePath, string destPathWithoutExtension, AudioOptions audioOptions = AudioOptions.None);
		string FormatName { get; }
	}

	public class OggEncoder : IAudioEncoder
	{
		public string Encode(string sourcePath, string destPathWithoutExtension)
		{
			string args = string.Format("\"{0}\" \"{1}.{2}\"", sourcePath, destPathWithoutExtension, FormatName);
			string exePath = FileLocator.GetFileDistributedWithApplication("sox", "sox.exe");
			var result = CommandLineRunner.Run(exePath, args, "", 60, new ConsoleProgress());
			if (result.StandardError.Contains("FAIL"))
				return null;
			return destPathWithoutExtension + "." + FormatName;
		}

		public string Encode(string sourcePath, string destPathWithoutExtension, AudioOptions audioOptions = AudioOptions.None)
		{
			throw new NotImplementedException();
		}

		public string FormatName
		{
			get { return "ogg"; }
		}
	}
}
