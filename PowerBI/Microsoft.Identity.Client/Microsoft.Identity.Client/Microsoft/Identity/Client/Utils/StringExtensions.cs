using System;
using System.Text;

namespace Microsoft.Identity.Client.Utils
{
	// Token: 0x020001D1 RID: 465
	internal static class StringExtensions
	{
		// Token: 0x06001464 RID: 5220 RVA: 0x000455F3 File Offset: 0x000437F3
		public static byte[] ToByteArray(this string stringInput)
		{
			return StringExtensions.utf8Encoding.GetBytes(stringInput);
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x00045600 File Offset: 0x00043800
		public static string NullIfEmpty(this string s)
		{
			if (!string.IsNullOrEmpty(s))
			{
				return s;
			}
			return null;
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0004560D File Offset: 0x0004380D
		public static string NullIfWhiteSpace(this string s)
		{
			if (!string.IsNullOrWhiteSpace(s))
			{
				return s;
			}
			return null;
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0004561A File Offset: 0x0004381A
		public static bool Contains(this string source, string toCheck, StringComparison comp)
		{
			return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
		}

		// Token: 0x04000854 RID: 2132
		private static UTF8Encoding utf8Encoding = new UTF8Encoding();
	}
}
