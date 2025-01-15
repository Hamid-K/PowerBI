using System;

namespace Microsoft.AspNet.OData.Query.Expressions
{
	// Token: 0x020000F7 RID: 247
	internal class ClrSafeFunctions
	{
		// Token: 0x06000861 RID: 2145 RVA: 0x0002098A File Offset: 0x0001EB8A
		public static string SubstringStart(string str, int startIndex)
		{
			if (startIndex < 0)
			{
				startIndex = 0;
			}
			if (startIndex > str.Length)
			{
				return string.Empty;
			}
			return str.Substring(startIndex);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000209AC File Offset: 0x0001EBAC
		public static string SubstringStartAndLength(string str, int startIndex, int length)
		{
			if (startIndex < 0)
			{
				startIndex = 0;
			}
			int length2 = str.Length;
			if (startIndex > length2)
			{
				return string.Empty;
			}
			length = Math.Min(length, length2 - startIndex);
			if (length < 0)
			{
				return string.Empty;
			}
			return str.Substring(startIndex, length);
		}
	}
}
