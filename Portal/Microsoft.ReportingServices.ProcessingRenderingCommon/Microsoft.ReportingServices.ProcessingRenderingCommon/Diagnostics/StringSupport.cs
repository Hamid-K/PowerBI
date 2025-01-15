using System;
using System.Globalization;
using System.Text;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200002E RID: 46
	internal static class StringSupport
	{
		// Token: 0x06000148 RID: 328 RVA: 0x00005E02 File Offset: 0x00004002
		public static bool StartsWith(string str, string prefix, bool ignoreCase, CultureInfo culture)
		{
			if (str == null || prefix == null)
			{
				throw new ArgumentException("StringStartsWith can't accept null parameters");
			}
			return str.Length >= prefix.Length && string.Compare(str, 0, prefix, 0, prefix.Length, ignoreCase, culture) == 0;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00005E3C File Offset: 0x0000403C
		public static bool EndsWith(string str, string postfix, bool ignoreCase, CultureInfo culture)
		{
			if (str == null || postfix == null)
			{
				throw new ArgumentException("StringEndsWith can't accept null parameters");
			}
			return str.Length >= postfix.Length && string.Compare(str, str.Length - postfix.Length, postfix, 0, postfix.Length, ignoreCase, culture) == 0;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005E8A File Offset: 0x0000408A
		public static byte[] ToUnicodeArray(string s)
		{
			if (s == null)
			{
				return null;
			}
			return Encoding.Unicode.GetBytes(s);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005E9C File Offset: 0x0000409C
		public static string FromUnicodeArray(byte[] u)
		{
			if (u == null)
			{
				return null;
			}
			return Encoding.Unicode.GetString(u);
		}
	}
}
