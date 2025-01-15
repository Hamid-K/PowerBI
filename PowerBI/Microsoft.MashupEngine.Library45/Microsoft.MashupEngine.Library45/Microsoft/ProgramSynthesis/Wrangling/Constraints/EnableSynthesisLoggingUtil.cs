using System;
using System.IO;
using System.Linq;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning.Logging;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x0200022C RID: 556
	public static class EnableSynthesisLoggingUtil
	{
		// Token: 0x06000BF3 RID: 3059 RVA: 0x00024490 File Offset: 0x00022690
		public static LogListener GetLogListenerIfEnabled(this DSLOptions options, IFeature scoreFeature = null)
		{
			if (options == null || !options.SynthesisLogFilenamePrefix.HasValue)
			{
				return null;
			}
			return new LogListener(options.LogInfo, scoreFeature);
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x000244C0 File Offset: 0x000226C0
		public static void SaveLogToXMLIfEnabled(this DSLOptions options, LogListener logListener, Func<string> suffix = null)
		{
			if (options != null && options.SynthesisLogFilenamePrefix.HasValue)
			{
				string text = Path.Combine(new FileInfo(options.GetType().Assembly.Location).DirectoryName, "test-results");
				Directory.CreateDirectory(text);
				if (logListener != null)
				{
					logListener.SaveLogToXML(Path.Combine(text, string.Join(".", new string[]
					{
						options.SynthesisLogFilenamePrefix.Value,
						(suffix != null) ? suffix() : null
					}.Where((string s) => s != null)) + ".xml"));
				}
			}
		}
	}
}
