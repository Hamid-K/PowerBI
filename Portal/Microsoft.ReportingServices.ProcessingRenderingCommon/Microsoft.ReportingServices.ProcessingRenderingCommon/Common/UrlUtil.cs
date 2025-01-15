using System;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000009 RID: 9
	internal static class UrlUtil
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00004473 File Offset: 0x00002673
		public static string UrlEncode(string input)
		{
			if (input == null)
			{
				return null;
			}
			return Uri.EscapeDataString(input);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004480 File Offset: 0x00002680
		public static string UrlDecode(string input)
		{
			if (input == null)
			{
				return null;
			}
			input = input.Replace("+", " ");
			return Uri.UnescapeDataString(input);
		}
	}
}
