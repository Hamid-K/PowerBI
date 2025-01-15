using System;
using System.IO;

namespace Microsoft.AnalysisServices.AdomdClient.Extensions
{
	// Token: 0x02000158 RID: 344
	internal static class TextReaderExtensions
	{
		// Token: 0x060010D8 RID: 4312 RVA: 0x0003A784 File Offset: 0x00038984
		public static void CopyTo(this TextReader reader, TextWriter writer, bool useBlockCopy = false)
		{
			if (useBlockCopy)
			{
				TextReaderExtensions.BlockCopy(reader, writer);
				return;
			}
			TextReaderExtensions.CopyLines(reader, writer);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0003A798 File Offset: 0x00038998
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

		// Token: 0x060010DA RID: 4314 RVA: 0x0003A7BC File Offset: 0x000389BC
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
