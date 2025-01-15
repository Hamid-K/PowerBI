using System;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.Json;

namespace Microsoft.Mashup.Engine1
{
	// Token: 0x0200022F RID: 559
	internal static class StreamReaderExtensions
	{
		// Token: 0x06000BC8 RID: 3016 RVA: 0x0001B960 File Offset: 0x00019B60
		public static int PeekNonWhitespace(this StreamReader reader)
		{
			int num;
			while (JsonTokenizer.IsWhite(num = reader.Peek()))
			{
				reader.Read();
			}
			return num;
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0001B988 File Offset: 0x00019B88
		public static bool ReadUntil(this StreamReader reader, string stringToFind)
		{
			int num = 0;
			while (!reader.EndOfStream)
			{
				if (reader.Read() == (int)stringToFind[num])
				{
					num++;
					if (num == stringToFind.Length)
					{
						return true;
					}
				}
				else
				{
					num = 0;
				}
			}
			return false;
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0001B9C4 File Offset: 0x00019BC4
		public static bool PeekUntilAnyChar(this StreamReader reader, long maxCount, params char[] charsToReadTo)
		{
			int num = 0;
			while (!reader.EndOfStream)
			{
				int nextResult = reader.Peek();
				if (charsToReadTo.Any((char c) => (int)c == nextResult))
				{
					return true;
				}
				reader.Read();
				if ((long)num >= maxCount)
				{
					return false;
				}
			}
			return false;
		}
	}
}
