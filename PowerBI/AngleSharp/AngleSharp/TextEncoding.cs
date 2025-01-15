using System;
using System.Collections.Generic;
using System.Text;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp
{
	// Token: 0x02000014 RID: 20
	public static class TextEncoding
	{
		// Token: 0x0600007E RID: 126 RVA: 0x00002B5F File Offset: 0x00000D5F
		public static bool IsUnicode(this Encoding encoding)
		{
			return encoding == TextEncoding.Utf16Be || encoding == TextEncoding.Utf16Le;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002B74 File Offset: 0x00000D74
		public static Encoding Parse(string content)
		{
			string text = string.Empty;
			int num = 0;
			for (int i = num; i < content.Length - 7; i++)
			{
				if (content.Substring(i).StartsWith(AttributeNames.Charset, StringComparison.OrdinalIgnoreCase))
				{
					num = i + 7;
					break;
				}
			}
			if (num > 0 && num < content.Length)
			{
				int num2 = num;
				while (num2 < content.Length - 1 && content[num2].IsSpaceCharacter())
				{
					num++;
					num2++;
				}
				if (content[num] != '=')
				{
					return TextEncoding.Parse(content.Substring(num));
				}
				num++;
				int num3 = num;
				while (num3 < content.Length && content[num3].IsSpaceCharacter())
				{
					num++;
					num3++;
				}
				if (num < content.Length)
				{
					if (content[num] == '"')
					{
						content = content.Substring(num + 1);
						int num4 = content.IndexOf('"');
						if (num4 != -1)
						{
							text = content.Substring(0, num4);
						}
					}
					else if (content[num] == '\'')
					{
						content = content.Substring(num + 1);
						int num5 = content.IndexOf('\'');
						if (num5 != -1)
						{
							text = content.Substring(0, num5);
						}
					}
					else
					{
						content = content.Substring(num);
						int num6 = 0;
						int num7 = 0;
						while (num7 < content.Length && !content[num7].IsSpaceCharacter() && content[num7] != ';')
						{
							num6++;
							num7++;
						}
						text = content.Substring(0, num6);
					}
				}
			}
			if (!TextEncoding.IsSupported(text))
			{
				return null;
			}
			return TextEncoding.Resolve(text);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002CFF File Offset: 0x00000EFF
		public static bool IsSupported(string charset)
		{
			return TextEncoding.encodings.ContainsKey(charset);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002D0C File Offset: 0x00000F0C
		public static Encoding Resolve(string charset)
		{
			Encoding encoding = null;
			if (charset != null && TextEncoding.encodings.TryGetValue(charset, out encoding))
			{
				return encoding;
			}
			return TextEncoding.Utf8;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002D34 File Offset: 0x00000F34
		private static Encoding GetEncoding(string name)
		{
			Encoding encoding;
			try
			{
				encoding = Encoding.GetEncoding(name);
			}
			catch
			{
				encoding = TextEncoding.Utf8;
			}
			return encoding;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002D64 File Offset: 0x00000F64
		private static Dictionary<string, Encoding> CreateEncodings()
		{
			Dictionary<string, Encoding> dictionary = new Dictionary<string, Encoding>(StringComparer.OrdinalIgnoreCase);
			dictionary.Add("unicode-1-1-utf-8", TextEncoding.Utf8);
			dictionary.Add("utf-8", TextEncoding.Utf8);
			dictionary.Add("utf8", TextEncoding.Utf8);
			dictionary.Add("utf-16be", TextEncoding.Utf16Be);
			dictionary.Add("utf-16", TextEncoding.Utf16Le);
			dictionary.Add("utf-16le", TextEncoding.Utf16Le);
			dictionary.Add("dos-874", TextEncoding.Windows874);
			dictionary.Add("iso-8859-11", TextEncoding.Windows874);
			dictionary.Add("iso8859-11", TextEncoding.Windows874);
			dictionary.Add("iso885911", TextEncoding.Windows874);
			dictionary.Add("tis-620", TextEncoding.Windows874);
			dictionary.Add("windows-874", TextEncoding.Windows874);
			dictionary.Add("cp1250", TextEncoding.Windows1250);
			dictionary.Add("windows-1250", TextEncoding.Windows1250);
			dictionary.Add("x-cp1250", TextEncoding.Windows1250);
			dictionary.Add("cp1251", TextEncoding.Windows1251);
			dictionary.Add("windows-1251", TextEncoding.Windows1251);
			dictionary.Add("x-cp1251", TextEncoding.Windows1251);
			dictionary.Add("x-user-defined", TextEncoding.Windows1252);
			dictionary.Add("ansi_x3.4-1968", TextEncoding.Windows1252);
			dictionary.Add("ascii", TextEncoding.Windows1252);
			dictionary.Add("cp1252", TextEncoding.Windows1252);
			dictionary.Add("cp819", TextEncoding.Windows1252);
			dictionary.Add("csisolatin1", TextEncoding.Windows1252);
			dictionary.Add("ibm819", TextEncoding.Windows1252);
			dictionary.Add("iso-8859-1", TextEncoding.Windows1252);
			dictionary.Add("iso-ir-100", TextEncoding.Windows1252);
			dictionary.Add("iso8859-1", TextEncoding.Windows1252);
			dictionary.Add("iso88591", TextEncoding.Windows1252);
			dictionary.Add("iso_8859-1", TextEncoding.Windows1252);
			dictionary.Add("iso_8859-1:1987", TextEncoding.Windows1252);
			dictionary.Add("l1", TextEncoding.Windows1252);
			dictionary.Add("latin1", TextEncoding.Windows1252);
			dictionary.Add("us-ascii", TextEncoding.Windows1252);
			dictionary.Add("windows-1252", TextEncoding.Windows1252);
			dictionary.Add("x-cp1252", TextEncoding.Windows1252);
			dictionary.Add("cp1253", TextEncoding.Windows1253);
			dictionary.Add("windows-1253", TextEncoding.Windows1253);
			dictionary.Add("x-cp1253", TextEncoding.Windows1253);
			dictionary.Add("cp1254", TextEncoding.Windows1254);
			dictionary.Add("csisolatin5", TextEncoding.Windows1254);
			dictionary.Add("iso-8859-9", TextEncoding.Windows1254);
			dictionary.Add("iso-ir-148", TextEncoding.Windows1254);
			dictionary.Add("iso8859-9", TextEncoding.Windows1254);
			dictionary.Add("iso88599", TextEncoding.Windows1254);
			dictionary.Add("iso_8859-9", TextEncoding.Windows1254);
			dictionary.Add("iso_8859-9:1989", TextEncoding.Windows1254);
			dictionary.Add("l5", TextEncoding.Windows1254);
			dictionary.Add("latin5", TextEncoding.Windows1254);
			dictionary.Add("windows-1254", TextEncoding.Windows1254);
			dictionary.Add("x-cp1254", TextEncoding.Windows1254);
			dictionary.Add("cp1255", TextEncoding.Windows1255);
			dictionary.Add("windows-1255", TextEncoding.Windows1255);
			dictionary.Add("x-cp1255", TextEncoding.Windows1255);
			dictionary.Add("cp1256", TextEncoding.Windows1256);
			dictionary.Add("windows-1256", TextEncoding.Windows1256);
			dictionary.Add("x-cp1256", TextEncoding.Windows1256);
			dictionary.Add("cp1257", TextEncoding.Windows1257);
			dictionary.Add("windows-1257", TextEncoding.Windows1257);
			dictionary.Add("x-cp1257", TextEncoding.Windows1257);
			dictionary.Add("cp1258", TextEncoding.Windows1258);
			dictionary.Add("windows-1258", TextEncoding.Windows1258);
			dictionary.Add("x-cp1258", TextEncoding.Windows1258);
			Encoding encoding = TextEncoding.GetEncoding("macintosh");
			dictionary.Add("csmacintosh", encoding);
			dictionary.Add("mac", encoding);
			dictionary.Add("macintosh", encoding);
			dictionary.Add("x-mac-roman", encoding);
			Encoding encoding2 = TextEncoding.GetEncoding("x-mac-cyrillic");
			dictionary.Add("x-mac-cyrillic", encoding2);
			dictionary.Add("x-mac-ukrainian", encoding2);
			Encoding encoding3 = TextEncoding.GetEncoding("cp866");
			dictionary.Add("866", encoding3);
			dictionary.Add("cp866", encoding3);
			dictionary.Add("csibm866", encoding3);
			dictionary.Add("ibm866", encoding3);
			dictionary.Add("csisolatin2", TextEncoding.Latin2);
			dictionary.Add("iso-8859-2", TextEncoding.Latin2);
			dictionary.Add("iso-ir-101", TextEncoding.Latin2);
			dictionary.Add("iso8859-2", TextEncoding.Latin2);
			dictionary.Add("iso88592", TextEncoding.Latin2);
			dictionary.Add("iso_8859-2", TextEncoding.Latin2);
			dictionary.Add("iso_8859-2:1987", TextEncoding.Latin2);
			dictionary.Add("l2", TextEncoding.Latin2);
			dictionary.Add("latin2", TextEncoding.Latin2);
			dictionary.Add("csisolatin3", TextEncoding.Latin3);
			dictionary.Add("iso-8859-3", TextEncoding.Latin3);
			dictionary.Add("iso-ir-109", TextEncoding.Latin3);
			dictionary.Add("iso8859-3", TextEncoding.Latin3);
			dictionary.Add("iso88593", TextEncoding.Latin3);
			dictionary.Add("iso_8859-3", TextEncoding.Latin3);
			dictionary.Add("iso_8859-3:1988", TextEncoding.Latin3);
			dictionary.Add("l3", TextEncoding.Latin3);
			dictionary.Add("latin3", TextEncoding.Latin3);
			dictionary.Add("csisolatin4", TextEncoding.Latin4);
			dictionary.Add("iso-8859-4", TextEncoding.Latin4);
			dictionary.Add("iso-ir-110", TextEncoding.Latin4);
			dictionary.Add("iso8859-4", TextEncoding.Latin4);
			dictionary.Add("iso88594", TextEncoding.Latin4);
			dictionary.Add("iso_8859-4", TextEncoding.Latin4);
			dictionary.Add("iso_8859-4:1988", TextEncoding.Latin4);
			dictionary.Add("l4", TextEncoding.Latin4);
			dictionary.Add("latin4", TextEncoding.Latin4);
			dictionary.Add("csisolatincyrillic", TextEncoding.Latin5);
			dictionary.Add("cyrillic", TextEncoding.Latin5);
			dictionary.Add("iso-8859-5", TextEncoding.Latin5);
			dictionary.Add("iso-ir-144", TextEncoding.Latin5);
			dictionary.Add("iso8859-5", TextEncoding.Latin5);
			dictionary.Add("iso88595", TextEncoding.Latin5);
			dictionary.Add("iso_8859-5", TextEncoding.Latin5);
			dictionary.Add("iso_8859-5:1988", TextEncoding.Latin5);
			Encoding encoding4 = TextEncoding.GetEncoding("iso-8859-6");
			dictionary.Add("arabic", encoding4);
			dictionary.Add("asmo-708", encoding4);
			dictionary.Add("csiso88596e", encoding4);
			dictionary.Add("csiso88596i", encoding4);
			dictionary.Add("csisolatinarabic", encoding4);
			dictionary.Add("ecma-114", encoding4);
			dictionary.Add("iso-8859-6", encoding4);
			dictionary.Add("iso-8859-6-e", encoding4);
			dictionary.Add("iso-8859-6-i", encoding4);
			dictionary.Add("iso-ir-127", encoding4);
			dictionary.Add("iso8859-6", encoding4);
			dictionary.Add("iso88596", encoding4);
			dictionary.Add("iso_8859-6", encoding4);
			dictionary.Add("iso_8859-6:1987", encoding4);
			Encoding encoding5 = TextEncoding.GetEncoding("iso-8859-7");
			dictionary.Add("csisolatingreek", encoding5);
			dictionary.Add("ecma-118", encoding5);
			dictionary.Add("elot_928", encoding5);
			dictionary.Add("greek", encoding5);
			dictionary.Add("greek8", encoding5);
			dictionary.Add("iso-8859-7", encoding5);
			dictionary.Add("iso-ir-126", encoding5);
			dictionary.Add("iso8859-7", encoding5);
			dictionary.Add("iso88597", encoding5);
			dictionary.Add("iso_8859-7", encoding5);
			dictionary.Add("iso_8859-7:1987", encoding5);
			dictionary.Add("sun_eu_greek", encoding5);
			Encoding encoding6 = TextEncoding.GetEncoding("iso-8859-8");
			dictionary.Add("csiso88598e", encoding6);
			dictionary.Add("csisolatinhebrew", encoding6);
			dictionary.Add("hebrew", encoding6);
			dictionary.Add("iso-8859-8", encoding6);
			dictionary.Add("iso-8859-8-e", encoding6);
			dictionary.Add("iso-ir-138", encoding6);
			dictionary.Add("iso8859-8", encoding6);
			dictionary.Add("iso88598", encoding6);
			dictionary.Add("iso_8859-8", encoding6);
			dictionary.Add("iso_8859-8:1988", encoding6);
			dictionary.Add("visual", encoding6);
			Encoding encoding7 = TextEncoding.GetEncoding("iso-8859-8-i");
			dictionary.Add("csiso88598i", encoding7);
			dictionary.Add("iso-8859-8-i", encoding7);
			dictionary.Add("logical", encoding7);
			Encoding encoding8 = TextEncoding.GetEncoding("iso-8859-13");
			dictionary.Add("iso-8859-13", encoding8);
			dictionary.Add("iso8859-13", encoding8);
			dictionary.Add("iso885913", encoding8);
			Encoding encoding9 = TextEncoding.GetEncoding("iso-8859-15");
			dictionary.Add("csisolatin9", encoding9);
			dictionary.Add("iso-8859-15", encoding9);
			dictionary.Add("iso8859-15", encoding9);
			dictionary.Add("iso885915", encoding9);
			dictionary.Add("iso_8859-15", encoding9);
			dictionary.Add("l9", encoding9);
			Encoding encoding10 = TextEncoding.GetEncoding("koi8-r");
			dictionary.Add("cskoi8r", encoding10);
			dictionary.Add("koi", encoding10);
			dictionary.Add("koi8", encoding10);
			dictionary.Add("koi8-r", encoding10);
			dictionary.Add("koi8_r", encoding10);
			dictionary.Add("koi8-u", TextEncoding.GetEncoding("koi8-u"));
			Encoding encoding11 = TextEncoding.GetEncoding("x-cp20936");
			dictionary.Add("chinese", encoding11);
			dictionary.Add("csgb2312", encoding11);
			dictionary.Add("csiso58gb231280", encoding11);
			dictionary.Add("gb2312", encoding11);
			dictionary.Add("gb_2312", encoding11);
			dictionary.Add("gb_2312-80", encoding11);
			dictionary.Add("gbk", encoding11);
			dictionary.Add("iso-ir-58", encoding11);
			dictionary.Add("x-gbk", encoding11);
			dictionary.Add("hz-gb-2312", TextEncoding.GetEncoding("hz-gb-2312"));
			dictionary.Add("gb18030", TextEncoding.Gb18030);
			dictionary.Add("big5", TextEncoding.Big5);
			dictionary.Add("big5-hkscs", TextEncoding.Big5);
			dictionary.Add("cn-big5", TextEncoding.Big5);
			dictionary.Add("csbig5", TextEncoding.Big5);
			dictionary.Add("x-x-big5", TextEncoding.Big5);
			Encoding encoding12 = TextEncoding.GetEncoding("iso-2022-jp");
			dictionary.Add("csiso2022jp", encoding12);
			dictionary.Add("iso-2022-jp", encoding12);
			Encoding encoding13 = TextEncoding.GetEncoding("iso-2022-kr");
			dictionary.Add("csiso2022kr", encoding13);
			dictionary.Add("iso-2022-kr", encoding13);
			Encoding encoding14 = TextEncoding.GetEncoding("iso-2022-cn");
			dictionary.Add("iso-2022-cn", encoding14);
			dictionary.Add("iso-2022-cn-ext", encoding14);
			dictionary.Add("shift_jis", TextEncoding.GetEncoding("shift_jis"));
			Encoding encoding15 = TextEncoding.GetEncoding("euc-jp");
			dictionary.Add("euc-jp", encoding15);
			return dictionary;
		}

		// Token: 0x04000013 RID: 19
		public static readonly Encoding Utf8 = new UTF8Encoding(false);

		// Token: 0x04000014 RID: 20
		public static readonly Encoding Utf16Be = new UnicodeEncoding(true, false);

		// Token: 0x04000015 RID: 21
		public static readonly Encoding Utf16Le = new UnicodeEncoding(false, false);

		// Token: 0x04000016 RID: 22
		public static readonly Encoding Utf32Le = TextEncoding.GetEncoding("UTF-32LE");

		// Token: 0x04000017 RID: 23
		public static readonly Encoding Utf32Be = TextEncoding.GetEncoding("UTF-32BE");

		// Token: 0x04000018 RID: 24
		public static readonly Encoding Gb18030 = TextEncoding.GetEncoding("GB18030");

		// Token: 0x04000019 RID: 25
		public static readonly Encoding Big5 = TextEncoding.GetEncoding("big5");

		// Token: 0x0400001A RID: 26
		public static readonly Encoding Windows874 = TextEncoding.GetEncoding("windows-874");

		// Token: 0x0400001B RID: 27
		public static readonly Encoding Windows1250 = TextEncoding.GetEncoding("windows-1250");

		// Token: 0x0400001C RID: 28
		public static readonly Encoding Windows1251 = TextEncoding.GetEncoding("windows-1251");

		// Token: 0x0400001D RID: 29
		public static readonly Encoding Windows1252 = TextEncoding.GetEncoding("windows-1252");

		// Token: 0x0400001E RID: 30
		public static readonly Encoding Windows1253 = TextEncoding.GetEncoding("windows-1253");

		// Token: 0x0400001F RID: 31
		public static readonly Encoding Windows1254 = TextEncoding.GetEncoding("windows-1254");

		// Token: 0x04000020 RID: 32
		public static readonly Encoding Windows1255 = TextEncoding.GetEncoding("windows-1255");

		// Token: 0x04000021 RID: 33
		public static readonly Encoding Windows1256 = TextEncoding.GetEncoding("windows-1256");

		// Token: 0x04000022 RID: 34
		public static readonly Encoding Windows1257 = TextEncoding.GetEncoding("windows-1257");

		// Token: 0x04000023 RID: 35
		public static readonly Encoding Windows1258 = TextEncoding.GetEncoding("windows-1258");

		// Token: 0x04000024 RID: 36
		public static readonly Encoding Latin2 = TextEncoding.GetEncoding("iso-8859-2");

		// Token: 0x04000025 RID: 37
		public static readonly Encoding Latin3 = TextEncoding.GetEncoding("iso-8859-3");

		// Token: 0x04000026 RID: 38
		public static readonly Encoding Latin4 = TextEncoding.GetEncoding("iso-8859-4");

		// Token: 0x04000027 RID: 39
		public static readonly Encoding Latin5 = TextEncoding.GetEncoding("iso-8859-5");

		// Token: 0x04000028 RID: 40
		public static readonly Encoding Latin13 = TextEncoding.GetEncoding("iso-8859-13");

		// Token: 0x04000029 RID: 41
		public static readonly Encoding UsAscii = TextEncoding.GetEncoding("us-ascii");

		// Token: 0x0400002A RID: 42
		public static readonly Encoding Korean = TextEncoding.GetEncoding("ks_c_5601-1987");

		// Token: 0x0400002B RID: 43
		private static readonly Dictionary<string, Encoding> encodings = TextEncoding.CreateEncodings();
	}
}
