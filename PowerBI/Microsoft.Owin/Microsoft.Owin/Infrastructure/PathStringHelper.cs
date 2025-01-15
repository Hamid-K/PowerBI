using System;

namespace Microsoft.Owin.Infrastructure
{
	// Token: 0x0200003B RID: 59
	internal static class PathStringHelper
	{
		// Token: 0x0600023E RID: 574 RVA: 0x000063E7 File Offset: 0x000045E7
		public static bool IsValidPathChar(char c)
		{
			return (int)c < PathStringHelper.ValidPathChars.Length && PathStringHelper.ValidPathChars[(int)c];
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000063FC File Offset: 0x000045FC
		public static bool IsPercentEncodedChar(string str, int index)
		{
			return index < str.Length - 2 && str[index] == '%' && PathStringHelper.IsHexadecimalChar(str[index + 1]) && PathStringHelper.IsHexadecimalChar(str[index + 2]);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00006434 File Offset: 0x00004634
		public static bool IsHexadecimalChar(char c)
		{
			return ('0' <= c && c <= '9') || ('A' <= c && c <= 'F') || ('a' <= c && c <= 'f');
		}

		// Token: 0x04000071 RID: 113
		private static bool[] ValidPathChars = new bool[]
		{
			false, false, false, false, false, false, false, false, false, false,
			false, false, false, false, false, false, false, false, false, false,
			false, false, false, false, false, false, false, false, false, false,
			false, false, false, true, false, false, true, false, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			false, true, false, false, true, true, true, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, false, false, false, false, true, false, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, true, true, false, false, false, true, false
		};
	}
}
