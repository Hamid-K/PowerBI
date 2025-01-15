using System;
using System.Linq;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000007 RID: 7
	internal static class StringUtils
	{
		// Token: 0x06000129 RID: 297 RVA: 0x000039F5 File Offset: 0x00001BF5
		public static string RemoveControlCharacters(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				return input;
			}
			return new string(input.Where((char c) => !char.IsControl(c) || char.IsWhiteSpace(c)).ToArray<char>());
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00003A30 File Offset: 0x00001C30
		public static string RemoveControlAndWhitespaceCharacters(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				return input;
			}
			return new string(input.Where((char c) => !char.IsControl(c) && !char.IsWhiteSpace(c)).ToArray<char>());
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00003A6B File Offset: 0x00001C6B
		public static string RemoveControlAndNonSpacesWhitespaceCharacters(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				return input;
			}
			return new string(input.Where((char c) => c == ' ' || (!char.IsControl(c) && !char.IsWhiteSpace(c))).ToArray<char>());
		}
	}
}
