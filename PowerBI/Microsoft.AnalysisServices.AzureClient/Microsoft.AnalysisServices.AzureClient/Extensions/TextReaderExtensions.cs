using System;
using System.IO;

namespace Microsoft.AnalysisServices.AzureClient.Extensions
{
	// Token: 0x0200003B RID: 59
	internal static class TextReaderExtensions
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x00008F4C File Offset: 0x0000714C
		public static void CopyTo(this TextReader reader, TextWriter writer, bool useBlockCopy = false)
		{
			if (useBlockCopy)
			{
				TextReaderExtensions.BlockCopy(reader, writer);
				return;
			}
			TextReaderExtensions.CopyLines(reader, writer);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00008F60 File Offset: 0x00007160
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

		// Token: 0x060001D5 RID: 469 RVA: 0x00008F84 File Offset: 0x00007184
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
