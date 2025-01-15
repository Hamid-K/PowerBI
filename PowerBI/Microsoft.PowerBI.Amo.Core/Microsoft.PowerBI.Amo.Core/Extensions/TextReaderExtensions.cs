using System;
using System.IO;

namespace Microsoft.AnalysisServices.Extensions
{
	// Token: 0x0200014D RID: 333
	internal static class TextReaderExtensions
	{
		// Token: 0x06001166 RID: 4454 RVA: 0x0003D088 File Offset: 0x0003B288
		public static void CopyTo(this TextReader reader, TextWriter writer, bool useBlockCopy = false)
		{
			if (useBlockCopy)
			{
				TextReaderExtensions.BlockCopy(reader, writer);
				return;
			}
			TextReaderExtensions.CopyLines(reader, writer);
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0003D09C File Offset: 0x0003B29C
		private static void CopyLines(TextReader reader, TextWriter writer)
		{
			string text;
			do
			{
				text = reader.ReadLine();
				if (text != null)
				{
					writer.WriteLine(text);
				}
			}
			while (text != null);
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x0003D0C0 File Offset: 0x0003B2C0
		private static void BlockCopy(TextReader reader, TextWriter writer)
		{
			char[] array = new char[4096];
			int num;
			do
			{
				num = reader.ReadBlock(array, 0, array.Length);
				if (num > 0)
				{
					writer.Write(array, 0, num);
				}
			}
			while (num > 0);
		}
	}
}
