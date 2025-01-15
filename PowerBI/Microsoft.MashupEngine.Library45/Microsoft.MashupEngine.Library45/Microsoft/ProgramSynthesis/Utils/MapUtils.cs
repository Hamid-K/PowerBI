using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004AD RID: 1197
	public static class MapUtils
	{
		// Token: 0x06001ACF RID: 6863 RVA: 0x00050CC8 File Offset: 0x0004EEC8
		public static IEnumerable<KeyValuePair<string, string>> ReadMapFromStream(Stream stream, bool randomize = false, int randomSeed = 0, double fraction = 1.0, bool ignoreDuplicates = true, Encoding encoding = null)
		{
			Random random = new Random(randomSeed);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			IEnumerable<KeyValuePair<string, string>> enumerable;
			try
			{
				using (StreamReader streamReader = ((encoding == null) ? new StreamReader(stream) : new StreamReader(stream, encoding)))
				{
					string text;
					while ((text = streamReader.ReadLine()) != null)
					{
						string text2 = streamReader.ReadLine();
						string text3 = streamReader.ReadLine();
						if (text2 == null || text3 == null || text3 != "")
						{
							throw new Exception("Incorrect Format");
						}
						if (!randomize || random.NextDouble() <= fraction)
						{
							if (!dictionary.ContainsKey(text))
							{
								dictionary.Add(text, text2);
							}
							else if (!ignoreDuplicates)
							{
								Trace.WriteLine("Warning! examples with same input {0}", text);
							}
						}
					}
					enumerable = dictionary;
				}
			}
			finally
			{
				if (stream != null)
				{
					((IDisposable)stream).Dispose();
				}
			}
			return enumerable;
		}
	}
}
