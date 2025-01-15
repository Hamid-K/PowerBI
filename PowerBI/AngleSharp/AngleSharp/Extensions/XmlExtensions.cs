using System;

namespace AngleSharp.Extensions
{
	// Token: 0x020000FD RID: 253
	internal static class XmlExtensions
	{
		// Token: 0x06000835 RID: 2101 RVA: 0x00037E14 File Offset: 0x00036014
		public static bool IsPubidChar(this char c)
		{
			return c.IsAlphanumericAscii() || c == '-' || c == '\'' || c == '+' || c == ',' || c == '.' || c == '/' || c == ':' || c == '?' || c == '=' || c == '!' || c == '*' || c == '#' || c == '@' || c == '$' || c == '_' || c == '(' || c == ')' || c == ';' || c == '%' || c.IsSpaceCharacter();
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00037E90 File Offset: 0x00036090
		public static bool IsXmlNameStart(this char c)
		{
			return c.IsLetter() || c == ':' || c == '_' || c.IsInRange(192, 214) || c.IsInRange(216, 246) || c.IsInRange(248, 767) || c.IsInRange(880, 893) || c.IsInRange(895, 8191) || c.IsInRange(8204, 8205) || c.IsInRange(8304, 8591) || c.IsInRange(11264, 12271) || c.IsInRange(12289, 55295) || c.IsInRange(63744, 64975) || c.IsInRange(65008, 65533) || c.IsInRange(65536, 983039);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00037F9C File Offset: 0x0003619C
		public static bool IsXmlName(this char c)
		{
			return c.IsXmlNameStart() || c.IsDigit() || c == '-' || c == '.' || c == '·' || c.IsInRange(768, 879) || c.IsInRange(8255, 8256);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00037FF0 File Offset: 0x000361F0
		public static bool IsXmlName(this string str)
		{
			if (str.Length > 0 && str[0].IsXmlNameStart())
			{
				for (int i = 1; i < str.Length; i++)
				{
					if (!str[i].IsXmlName())
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00038038 File Offset: 0x00036238
		public static bool IsQualifiedName(this string str)
		{
			int num = str.IndexOf(':');
			if (num == -1)
			{
				return str.IsXmlName();
			}
			if (num > 0 && str[0].IsXmlNameStart())
			{
				for (int i = 1; i < num; i++)
				{
					if (!str[i].IsXmlName())
					{
						return false;
					}
				}
				num++;
			}
			if (str.Length > num && str[num++].IsXmlNameStart())
			{
				for (int j = num; j < str.Length; j++)
				{
					if (str[j] == ':' || !str[j].IsXmlName())
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x000380D5 File Offset: 0x000362D5
		public static bool IsXmlChar(this char chr)
		{
			return chr == '\t' || chr == '\n' || chr == '\r' || (chr >= ' ' && chr <= '\ud7ff') || (chr >= '\ue000' && chr <= '\ufffd');
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0003810C File Offset: 0x0003630C
		public static bool IsValidAsCharRef(this int chr)
		{
			return chr == 9 || chr == 10 || chr == 13 || (chr >= 32 && chr <= 55295) || (chr >= 57344 && chr <= 65533) || (chr >= 65536 && chr <= 1114111);
		}
	}
}
