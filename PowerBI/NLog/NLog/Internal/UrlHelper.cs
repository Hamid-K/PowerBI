using System;
using System.Text;

namespace NLog.Internal
{
	// Token: 0x0200014C RID: 332
	internal static class UrlHelper
	{
		// Token: 0x06000FF8 RID: 4088 RVA: 0x00029150 File Offset: 0x00027350
		public static void EscapeDataEncode(string source, StringBuilder target, UrlHelper.EscapeEncodingOptions options)
		{
			if (string.IsNullOrEmpty(source))
			{
				return;
			}
			bool flag = UrlHelper.Contains(options, UrlHelper.EscapeEncodingOptions.LowerCaseHex);
			bool flag2 = UrlHelper.Contains(options, UrlHelper.EscapeEncodingOptions.SpaceAsPlus);
			bool flag3 = UrlHelper.Contains(options, UrlHelper.EscapeEncodingOptions.NLogLegacy);
			char[] array = null;
			byte[] array2 = null;
			char[] array3 = (flag ? UrlHelper.hexLowerChars : UrlHelper.hexUpperChars);
			foreach (char c in source)
			{
				target.Append(c);
				if (!UrlHelper.IsSimpleCharOrNumber(c))
				{
					if (flag2 && c == ' ')
					{
						target[target.Length - 1] = '+';
					}
					else if (!UrlHelper.IsAllowedChar(options, c))
					{
						if (flag3)
						{
							UrlHelper.HandleLegacyEncoding(target, c, array3);
						}
						else
						{
							if (array == null)
							{
								array = new char[1];
							}
							array[0] = c;
							if (array2 == null)
							{
								array2 = new byte[8];
							}
							UrlHelper.WriteWideChars(target, array, array2, array3);
						}
					}
				}
			}
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0002921D File Offset: 0x0002741D
		private static bool Contains(UrlHelper.EscapeEncodingOptions options, UrlHelper.EscapeEncodingOptions option)
		{
			return (options & option) == option;
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00029228 File Offset: 0x00027428
		private static void WriteWideChars(StringBuilder target, char[] charArray, byte[] byteArray, char[] hexChars)
		{
			int bytes = Encoding.UTF8.GetBytes(charArray, 0, 1, byteArray, 0);
			for (int i = 0; i < bytes; i++)
			{
				byte b = byteArray[i];
				if (i == 0)
				{
					target[target.Length - 1] = '%';
				}
				else
				{
					target.Append('%');
				}
				target.Append(hexChars[(b & 240) >> 4]);
				target.Append(hexChars[(int)(b & 15)]);
			}
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00029294 File Offset: 0x00027494
		private static void HandleLegacyEncoding(StringBuilder target, char ch, char[] hexChars)
		{
			if (ch < 'Ā')
			{
				target[target.Length - 1] = '%';
				target.Append(hexChars[(int)((ch >> 4) & '\u000f')]);
				target.Append(hexChars[(int)(ch & '\u000f')]);
				return;
			}
			target[target.Length - 1] = '%';
			target.Append('u');
			target.Append(hexChars[(int)((ch >> 12) & '\u000f')]);
			target.Append(hexChars[(int)((ch >> 8) & '\u000f')]);
			target.Append(hexChars[(int)((ch >> 4) & '\u000f')]);
			target.Append(hexChars[(int)(ch & '\u000f')]);
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0002932C File Offset: 0x0002752C
		private static bool IsAllowedChar(UrlHelper.EscapeEncodingOptions options, char ch)
		{
			bool flag = (options & UrlHelper.EscapeEncodingOptions.UriString) == UrlHelper.EscapeEncodingOptions.UriString;
			bool flag2 = (options & UrlHelper.EscapeEncodingOptions.LegacyRfc2396) == UrlHelper.EscapeEncodingOptions.LegacyRfc2396;
			if (flag)
			{
				if (!flag2 && "-._~".IndexOf(ch) >= 0)
				{
					return true;
				}
				if (flag2 && "-_.!~*'()".IndexOf(ch) >= 0)
				{
					return true;
				}
			}
			else
			{
				if (!flag2 && ":/?#[]@!$&'()*+,;=".IndexOf(ch) >= 0)
				{
					return true;
				}
				if (flag2 && ";/?:@&=+$,".IndexOf(ch) >= 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00029395 File Offset: 0x00027595
		private static bool IsSimpleCharOrNumber(char ch)
		{
			return (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9');
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x000293BC File Offset: 0x000275BC
		public static UrlHelper.EscapeEncodingOptions GetUriStringEncodingFlags(bool escapeDataNLogLegacy, bool spaceAsPlus, bool escapeDataRfc3986)
		{
			UrlHelper.EscapeEncodingOptions escapeEncodingOptions = UrlHelper.EscapeEncodingOptions.UriString;
			if (escapeDataNLogLegacy)
			{
				escapeEncodingOptions |= UrlHelper.EscapeEncodingOptions.NLogLegacy;
			}
			else if (!escapeDataRfc3986)
			{
				escapeEncodingOptions |= UrlHelper.EscapeEncodingOptions.LegacyRfc2396 | UrlHelper.EscapeEncodingOptions.LowerCaseHex;
			}
			if (spaceAsPlus)
			{
				escapeEncodingOptions |= UrlHelper.EscapeEncodingOptions.SpaceAsPlus;
			}
			return escapeEncodingOptions;
		}

		// Token: 0x04000445 RID: 1093
		private const string RFC2396ReservedMarks = ";/?:@&=+$,";

		// Token: 0x04000446 RID: 1094
		private const string RFC3986ReservedMarks = ":/?#[]@!$&'()*+,;=";

		// Token: 0x04000447 RID: 1095
		private const string RFC2396UnreservedMarks = "-_.!~*'()";

		// Token: 0x04000448 RID: 1096
		private const string RFC3986UnreservedMarks = "-._~";

		// Token: 0x04000449 RID: 1097
		private static readonly char[] hexUpperChars = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F'
		};

		// Token: 0x0400044A RID: 1098
		private static readonly char[] hexLowerChars = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'a', 'b', 'c', 'd', 'e', 'f'
		};

		// Token: 0x02000289 RID: 649
		[Flags]
		public enum EscapeEncodingOptions
		{
			// Token: 0x0400070E RID: 1806
			None = 0,
			// Token: 0x0400070F RID: 1807
			UriString = 1,
			// Token: 0x04000710 RID: 1808
			LegacyRfc2396 = 2,
			// Token: 0x04000711 RID: 1809
			LowerCaseHex = 4,
			// Token: 0x04000712 RID: 1810
			SpaceAsPlus = 8,
			// Token: 0x04000713 RID: 1811
			NLogLegacy = 23
		}
	}
}
