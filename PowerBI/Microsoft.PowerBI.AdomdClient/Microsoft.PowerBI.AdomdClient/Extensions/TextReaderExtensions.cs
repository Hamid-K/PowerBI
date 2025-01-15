using System;
using System.IO;

namespace Microsoft.AnalysisServices.AdomdClient.Extensions
{
	// Token: 0x02000158 RID: 344
	internal static class TextReaderExtensions
	{
		// Token: 0x060010CB RID: 4299 RVA: 0x0003A454 File Offset: 0x00038654
		public static void CopyTo(this TextReader reader, TextWriter writer, bool useBlockCopy = false)
		{
			if (useBlockCopy)
			{
				TextReaderExtensions.BlockCopy(reader, writer);
				return;
			}
			TextReaderExtensions.CopyLines(reader, writer);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0003A468 File Offset: 0x00038668
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

		// Token: 0x060010CD RID: 4301 RVA: 0x0003A48C File Offset: 0x0003868C
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
