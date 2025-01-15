using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Microsoft.Mashup.Engine1.Library.Uris.Internal
{
	// Token: 0x020002CE RID: 718
	internal class DomainNameHelper
	{
		// Token: 0x06001C78 RID: 7288 RVA: 0x000020FD File Offset: 0x000002FD
		private DomainNameHelper()
		{
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x000446F4 File Offset: 0x000428F4
		internal static string ParseCanonicalName(string str, int start, int end, ref bool loopback)
		{
			string text = null;
			for (int i = end - 1; i >= start; i--)
			{
				if (str[i] >= 'A' && str[i] <= 'Z')
				{
					text = str.Substring(start, end - start).ToLower(CultureInfo.InvariantCulture);
					break;
				}
				if (str[i] == ':')
				{
					end = i;
				}
			}
			if (text == null)
			{
				text = str.Substring(start, end - start);
			}
			if (text == "localhost" || text == "loopback")
			{
				loopback = true;
				return "localhost";
			}
			return text;
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x00044780 File Offset: 0x00042980
		internal unsafe static bool IsValid(char* name, ushort pos, ref int returnedEnd, ref bool notCanonical, bool notImplicitFile)
		{
			char* ptr = name + pos;
			char* ptr2 = ptr;
			char* ptr3 = name + returnedEnd;
			while (ptr2 < ptr3)
			{
				char c = *ptr2;
				if (c > '\u007f')
				{
					return false;
				}
				if (c == '/' || c == '\\' || (notImplicitFile && (c == ':' || c == '?' || c == '#')))
				{
					ptr3 = ptr2;
					break;
				}
				ptr2++;
			}
			if (ptr3 == ptr)
			{
				return false;
			}
			for (;;)
			{
				ptr2 = ptr;
				while (ptr2 < ptr3 && *ptr2 != '.')
				{
					ptr2++;
				}
				if (ptr == ptr2 || (long)(ptr2 - ptr) > 63L || !DomainNameHelper.IsASCIILetterOrDigit(*(ptr++), ref notCanonical))
				{
					break;
				}
				while (ptr < ptr2)
				{
					if (!DomainNameHelper.IsValidDomainLabelCharacter(*(ptr++), ref notCanonical))
					{
						return false;
					}
				}
				ptr++;
				if (ptr >= ptr3)
				{
					goto Block_13;
				}
			}
			return false;
			Block_13:
			returnedEnd = (int)((ushort)((long)(ptr3 - name)));
			return true;
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x00044830 File Offset: 0x00042A30
		internal unsafe static bool IsValidByIri(char* name, ushort pos, ref int returnedEnd, ref bool notCanonical, bool notImplicitFile)
		{
			char* ptr = name + pos;
			char* ptr2 = ptr;
			char* ptr3 = name + returnedEnd;
			while (ptr2 < ptr3)
			{
				char c = *ptr2;
				if (c == '/' || c == '\\' || (notImplicitFile && (c == ':' || c == '?' || c == '#')))
				{
					ptr3 = ptr2;
					break;
				}
				ptr2++;
			}
			if (ptr3 == ptr)
			{
				return false;
			}
			for (;;)
			{
				ptr2 = ptr;
				int num = 0;
				bool flag = false;
				while (ptr2 < ptr3 && *ptr2 != '.' && *ptr2 != '。' && *ptr2 != '．' && *ptr2 != '｡')
				{
					num++;
					if (*ptr2 > 'ÿ')
					{
						num++;
					}
					if (*ptr2 >= '\u00a0')
					{
						flag = true;
					}
					ptr2++;
				}
				if (ptr == ptr2 || (flag ? (num + 4) : num) > 63 || (*(ptr++) < '\u00a0' && !DomainNameHelper.IsASCIILetterOrDigit(*(ptr - 1), ref notCanonical)))
				{
					break;
				}
				while (ptr < ptr2)
				{
					if (*(ptr++) < '\u00a0' && !DomainNameHelper.IsValidDomainLabelCharacter(*(ptr - 1), ref notCanonical))
					{
						return false;
					}
				}
				ptr++;
				if (ptr >= ptr3)
				{
					goto Block_20;
				}
			}
			return false;
			Block_20:
			returnedEnd = (int)((ushort)((long)(ptr3 - name)));
			return true;
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x0004493C File Offset: 0x00042B3C
		internal unsafe static string IdnEquivalent(string hostname)
		{
			bool flag = true;
			bool flag2 = false;
			char* ptr = hostname;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return DomainNameHelper.IdnEquivalent(ptr, 0, hostname.Length, ref flag, ref flag2);
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x00044970 File Offset: 0x00042B70
		internal unsafe static string IdnEquivalent(char* hostname, int start, int end, ref bool allAscii, ref bool atLeastOneValidIdn)
		{
			string text = null;
			string text2 = DomainNameHelper.IdnEquivalent(hostname, start, end, ref allAscii, ref text);
			if (text2 != null)
			{
				string text4;
				string text3 = (text4 = (allAscii ? text2 : text));
				char* ptr = text4;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				int length = text3.Length;
				int i = 0;
				int num = 0;
				bool flag = false;
				do
				{
					bool flag2 = false;
					bool flag3 = false;
					flag = false;
					for (i = num; i < length; i++)
					{
						char c = ptr[i];
						if (!flag3)
						{
							flag3 = true;
							if (i + 3 < length && DomainNameHelper.IsIdnAce(ptr, i))
							{
								i += 4;
								flag2 = true;
								continue;
							}
						}
						if (c == '.' || c == '。' || c == '．' || c == '｡')
						{
							flag = true;
							break;
						}
					}
					if (flag2)
					{
						try
						{
							new IdnMapping().GetUnicode(new string(ptr, num, i - num));
							atLeastOneValidIdn = true;
							break;
						}
						catch (ArgumentException)
						{
						}
					}
					num = i + ((flag > false) ? 1 : 0);
				}
				while (num < length);
				text4 = null;
			}
			else
			{
				atLeastOneValidIdn = false;
			}
			return text2;
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x00044A80 File Offset: 0x00042C80
		internal unsafe static string IdnEquivalent(char* hostname, int start, int end, ref bool allAscii, ref string bidiStrippedHost)
		{
			string text = null;
			if (end <= start)
			{
				return text;
			}
			int i = start;
			allAscii = true;
			while (i < end)
			{
				if (hostname[i] > '\u007f')
				{
					allAscii = false;
					break;
				}
				i++;
			}
			if (!allAscii)
			{
				IdnMapping idnMapping = new IdnMapping();
				bidiStrippedHost = InternalUri.StripBidiControlCharacter(hostname, start, end - start);
				string ascii;
				try
				{
					ascii = idnMapping.GetAscii(bidiStrippedHost);
					if (!InternalServicePointManager.AllowDangerousUnicodeDecompositions && DomainNameHelper.ContainsCharactersUnsafeForNormalizedHost(ascii))
					{
						throw new UriFormatException("net_uri_BadUnicodeHostForIdn");
					}
				}
				catch (ArgumentException)
				{
					throw new UriFormatException(SR.GetString("net_uri_BadUnicodeHostForIdn"));
				}
				return ascii;
			}
			string text2 = new string(hostname, start, end - start);
			if (text2 == null)
			{
				return null;
			}
			return text2.ToLowerInvariant();
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x00044B30 File Offset: 0x00042D30
		private static bool IsIdnAce(string input, int index)
		{
			return input[index] == 'x' && input[index + 1] == 'n' && input[index + 2] == '-' && input[index + 3] == '-';
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x00044B67 File Offset: 0x00042D67
		private unsafe static bool IsIdnAce(char* input, int index)
		{
			return input[index] == 'x' && input[index + 1] == 'n' && input[index + 2] == '-' && input[index + 3] == '-';
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x00044BA0 File Offset: 0x00042DA0
		internal unsafe static string UnicodeEquivalent(string idnHost, char* hostname, int start, int end)
		{
			IdnMapping idnMapping = new IdnMapping();
			try
			{
				return idnMapping.GetUnicode(idnHost);
			}
			catch (ArgumentException)
			{
			}
			bool flag = true;
			return DomainNameHelper.UnicodeEquivalent(hostname, start, end, ref flag, ref flag);
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x00044BE0 File Offset: 0x00042DE0
		internal unsafe static string UnicodeEquivalent(char* hostname, int start, int end, ref bool allAscii, ref bool atLeastOneValidIdn)
		{
			IdnMapping idnMapping = new IdnMapping();
			allAscii = true;
			atLeastOneValidIdn = false;
			string text = null;
			if (end <= start)
			{
				return text;
			}
			string text2 = InternalUri.StripBidiControlCharacter(hostname, start, end - start);
			string text3 = null;
			int num = 0;
			int i = 0;
			int length = text2.Length;
			bool flag = false;
			do
			{
				bool flag2 = true;
				bool flag3 = false;
				bool flag4 = false;
				flag = false;
				for (i = num; i < length; i++)
				{
					char c = text2[i];
					if (!flag4)
					{
						flag4 = true;
						if (i + 3 < length && c == 'x' && DomainNameHelper.IsIdnAce(text2, i))
						{
							flag3 = true;
						}
					}
					if (flag2 && c > '\u007f')
					{
						flag2 = false;
						allAscii = false;
					}
					if (c == '.' || c == '。' || c == '．' || c == '｡')
					{
						flag = true;
						break;
					}
				}
				if (!flag2)
				{
					string text4 = text2.Substring(num, i - num);
					try
					{
						text4 = idnMapping.GetAscii(text4);
					}
					catch (ArgumentException)
					{
						throw new UriFormatException(SR.GetString("net_uri_BadUnicodeHostForIdn"));
					}
					text3 += idnMapping.GetUnicode(text4);
					if (flag)
					{
						text3 += ".";
					}
				}
				else
				{
					bool flag5 = false;
					if (flag3)
					{
						try
						{
							text3 += idnMapping.GetUnicode(text2.Substring(num, i - num));
							if (flag)
							{
								text3 += ".";
							}
							flag5 = true;
							atLeastOneValidIdn = true;
						}
						catch (ArgumentException)
						{
						}
					}
					if (!flag5)
					{
						text3 += text2.Substring(num, i - num).ToLowerInvariant();
						if (flag)
						{
							text3 += ".";
						}
					}
				}
				num = i + ((flag > false) ? 1 : 0);
			}
			while (num < length);
			return text3;
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x00044D94 File Offset: 0x00042F94
		private static bool IsASCIILetterOrDigit(char character, ref bool notCanonical)
		{
			if ((character >= 'a' && character <= 'z') || (character >= '0' && character <= '9'))
			{
				return true;
			}
			if (character >= 'A' && character <= 'Z')
			{
				notCanonical = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x00044DBC File Offset: 0x00042FBC
		private static bool IsValidDomainLabelCharacter(char character, ref bool notCanonical)
		{
			if ((character >= 'a' && character <= 'z') || (character >= '0' && character <= '9') || character == '-' || character == '_')
			{
				return true;
			}
			if (character >= 'A' && character <= 'Z')
			{
				notCanonical = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x00044DEE File Offset: 0x00042FEE
		internal static bool ContainsCharactersUnsafeForNormalizedHost(string host)
		{
			return host.IndexOfAny(DomainNameHelper.s_UnsafeForNormalizedHost) != -1;
		}

		// Token: 0x0400097B RID: 2427
		private const char c_DummyChar = '\uffff';

		// Token: 0x0400097C RID: 2428
		internal const string Localhost = "localhost";

		// Token: 0x0400097D RID: 2429
		internal const string Loopback = "loopback";

		// Token: 0x0400097E RID: 2430
		private static readonly char[] s_UnsafeForNormalizedHost = new char[] { '\\', '/', '?', '@', '#', ':', '[', ']' };
	}
}
