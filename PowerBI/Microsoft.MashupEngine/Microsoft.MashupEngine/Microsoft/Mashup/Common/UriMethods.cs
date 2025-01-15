using System;
using System.Text;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C35 RID: 7221
	public static class UriMethods
	{
		// Token: 0x17002D15 RID: 11541
		// (get) Token: 0x0600B445 RID: 46149 RVA: 0x002493D6 File Offset: 0x002475D6
		public static bool IsFourPointFive
		{
			get
			{
				if (UriMethods.isFourPointFive == null)
				{
					UriMethods.isFourPointFive = new bool?(Uri.EscapeDataString("(").Length > 1);
				}
				return UriMethods.isFourPointFive.Value;
			}
		}

		// Token: 0x0600B446 RID: 46150 RVA: 0x0024940C File Offset: 0x0024760C
		public static string EscapeDataString(string str, bool legacyBehavior)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (str.Length == 0)
			{
				return string.Empty;
			}
			int num = 0;
			bool flag = false;
			StringBuilder stringBuilder = null;
			for (int i = 0; i <= str.Length; i++)
			{
				char c = ((i == str.Length) ? '\0' : str[i]);
				bool flag2 = UriMethods.IsUnreserved(c, legacyBehavior);
				if ((!flag2 || flag) && (flag2 || !flag || c == '\0'))
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(2 * str.Length);
					}
					UriMethods.AppendString(stringBuilder, str, num, i - num, flag);
					if (!flag2 && c > '\0' && c < '\u0080')
					{
						UriMethods.AppendHexChar(stringBuilder, (int)c);
						num = i + 1;
					}
					else
					{
						flag = !flag2;
						num = i;
					}
				}
			}
			if (stringBuilder != null)
			{
				return stringBuilder.ToString();
			}
			return str;
		}

		// Token: 0x0600B447 RID: 46151 RVA: 0x002494D4 File Offset: 0x002476D4
		private static bool IsUnreserved(char c, bool legacyBehavior)
		{
			if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9'))
			{
				return true;
			}
			if (legacyBehavior)
			{
				return "-_.!~*'()".IndexOf(c) >= 0;
			}
			return "-._~".IndexOf(c) >= 0;
		}

		// Token: 0x0600B448 RID: 46152 RVA: 0x00249527 File Offset: 0x00247727
		private static void AppendHexChar(StringBuilder sb, int c)
		{
			sb.Append('%');
			sb.Append("0123456789ABCDEF"[(c & 240) >> 4]);
			sb.Append("0123456789ABCDEF"[c & 15]);
		}

		// Token: 0x0600B449 RID: 46153 RVA: 0x00249564 File Offset: 0x00247764
		private static void AppendString(StringBuilder sb, string str, int start, int length, bool inEncoding)
		{
			if (!inEncoding)
			{
				sb.Append(str, start, length);
				return;
			}
			byte[] array = new byte[length * 6];
			int bytes = Encoding.UTF8.GetBytes(str, start, length, array, 0);
			for (int i = 0; i < bytes; i++)
			{
				UriMethods.AppendHexChar(sb, (int)array[i]);
			}
		}

		// Token: 0x04005BB4 RID: 23476
		private const string HexUpperChars = "0123456789ABCDEF";

		// Token: 0x04005BB5 RID: 23477
		private const string LegacyUnreserved = "-_.!~*'()";

		// Token: 0x04005BB6 RID: 23478
		private const string CorrectUnreserved = "-._~";

		// Token: 0x04005BB7 RID: 23479
		private static bool? isFourPointFive;
	}
}
