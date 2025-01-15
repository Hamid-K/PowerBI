using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200000A RID: 10
	internal static class StringUtil
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002600 File Offset: 0x00000800
		public static int? GetDigitSuffix(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				Match match = StringUtil.m_digitSuffixRegex.Match(value);
				if (match.Success)
				{
					return new int?(int.Parse(match.Groups["suffix"].Value, CultureInfo.InvariantCulture));
				}
			}
			return null;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002658 File Offset: 0x00000858
		public static string SetDigitSuffix(string value, int suffix)
		{
			int? digitSuffix = StringUtil.GetDigitSuffix(value);
			string text = suffix.ToString(CultureInfo.InvariantCulture);
			if (digitSuffix == null)
			{
				return value + text;
			}
			return StringUtil.m_digitSuffixRegex.Replace(value, text);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002698 File Offset: 0x00000898
		public static string IncrementDigitSuffix(string value, int defaultSuffix)
		{
			int? digitSuffix = StringUtil.GetDigitSuffix(value);
			if (digitSuffix == null)
			{
				return StringUtil.SetDigitSuffix(value, defaultSuffix);
			}
			return StringUtil.SetDigitSuffix(value, digitSuffix.Value + 1);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000026CC File Offset: 0x000008CC
		public static string IncrementDigitSuffix(string value)
		{
			return StringUtil.IncrementDigitSuffix(value, 2);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000026D8 File Offset: 0x000008D8
		public static string TrimToMaxLength(string value, int maxLength)
		{
			value = value ?? string.Empty;
			if (value.Length <= maxLength)
			{
				return value;
			}
			Match match = StringUtil.m_digitSuffixRegex.Match(value);
			if (!match.Success)
			{
				return value.Substring(0, maxLength);
			}
			int num = maxLength - match.Groups["suffix"].Length;
			if (num < 0)
			{
				throw new ArgumentOutOfRangeException("maxLength");
			}
			return value.Substring(0, num) + match.Groups["suffix"].Value;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002762 File Offset: 0x00000962
		public static string RemoveAmpersandEllipsis(string text)
		{
			return StringUtil.RemoveEllipsis(StringUtil.RemoveAmpersand(text));
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002770 File Offset: 0x00000970
		public static string RemoveAmpersand(string text)
		{
			if (text != null)
			{
				int num = text.IndexOf('&');
				if (num >= 0)
				{
					return text.Remove(num, 1);
				}
			}
			return text;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002798 File Offset: 0x00000998
		public static string RemoveEllipsis(string text)
		{
			if (text != null)
			{
				int num = text.Length - 3;
				if (num > 0 && text[num] == '.' && text[num + 1] == '.' && text[num + 2] == '.')
				{
					return text.Remove(num);
				}
			}
			return text;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000027E4 File Offset: 0x000009E4
		public static string RemoveLastSubstring(string text, char separator)
		{
			int num = text.LastIndexOf(separator);
			if (num < 0)
			{
				return text;
			}
			return text.Substring(0, num);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002808 File Offset: 0x00000A08
		public static string GetLastSubstring(string text, char separator)
		{
			int num = text.LastIndexOf(separator);
			if (num < 0)
			{
				return text;
			}
			return text.Substring(num + 1);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000282C File Offset: 0x00000A2C
		public static string FormatInvariant(string format, params object[] args)
		{
			return string.Format(CultureInfo.InvariantCulture, format, args);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000283A File Offset: 0x00000A3A
		public static string Join(string separator, IList<string> strings)
		{
			return StringUtil.Join(separator, strings, 0, strings.Count);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000284C File Offset: 0x00000A4C
		public static string Join(string separator, IList<string> strings, int startIndex, int count)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (count < 0 || startIndex + count > strings.Count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count == 0)
			{
				return string.Empty;
			}
			int num = 0;
			int length = separator.Length;
			for (int i = 0; i < count; i++)
			{
				num += strings[i + startIndex].Length + length;
			}
			num -= length;
			if (num == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(num, num);
			stringBuilder.Append(strings[startIndex]);
			for (int j = 1; j < count; j++)
			{
				stringBuilder.Append(separator);
				stringBuilder.Append(strings[j + startIndex]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002908 File Offset: 0x00000B08
		public static string NormalizeLineBreaks(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}
			StringReader stringReader = new StringReader(s);
			StringWriter stringWriter = new StringWriter(CultureInfo.CurrentCulture);
			bool flag = true;
			string text;
			while ((text = stringReader.ReadLine()) != null)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringWriter.WriteLine();
				}
				stringWriter.Write(text);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000295D File Offset: 0x00000B5D
		public static bool EqualsTrimmedWithNormalizedLineBreaks(string s1, string s2)
		{
			if (s1 != null)
			{
				s1 = s1.Trim();
			}
			if (s2 != null)
			{
				s2 = s2.Trim();
			}
			return StringUtil.NormalizeLineBreaks(s1) == StringUtil.NormalizeLineBreaks(s2);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002986 File Offset: 0x00000B86
		public static string BuildErrorMessage(Exception e)
		{
			if (e.InnerException != null)
			{
				return StringUtil.BuildErrorMessage(e.InnerException) + "\r\n----------------------------\r\n" + e.Message;
			}
			return e.Message;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000029B2 File Offset: 0x00000BB2
		public static string MakeCLSNameFromFilename(string path)
		{
			return StringUtil.GetClsCompliantIdentifier(Path.GetFileNameWithoutExtension(path), null);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000029C0 File Offset: 0x00000BC0
		public static StringComparer GetClsCompliantComparer()
		{
			return StringComparer.Ordinal;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000029C7 File Offset: 0x00000BC7
		public static int CompareClsCompliantIdentifiers(string s1, string s2)
		{
			return StringUtil.CompareClsCompliantIdentifiers(s1, s2, false);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000029D4 File Offset: 0x00000BD4
		public static int CompareClsCompliantIdentifiers(string s1, string s2, bool ignoreCase)
		{
			StringComparison stringComparison = (ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			return string.Compare(s1, s2, stringComparison);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000029F4 File Offset: 0x00000BF4
		public static string GetClsCompliantIdentifier(string candidate, string defaultIdentifier)
		{
			StringBuilder stringBuilder = new StringBuilder();
			candidate = candidate ?? string.Empty;
			if (candidate.Length == 0 || !StringUtil.IsClsCompliantIdentifierChar(candidate[0], true))
			{
				stringBuilder.Append(defaultIdentifier);
			}
			for (int i = 0; i < candidate.Length; i++)
			{
				if (StringUtil.IsClsCompliantIdentifierChar(candidate[i], stringBuilder.Length == 0))
				{
					stringBuilder.Append(candidate[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002A70 File Offset: 0x00000C70
		public static bool IsClsCompliantIdentifier(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return false;
			}
			if (!StringUtil.IsClsCompliantIdentifierChar(s[0], true))
			{
				return false;
			}
			for (int i = 1; i < s.Length; i++)
			{
				if (!StringUtil.IsClsCompliantIdentifierChar(s[i], false))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002ABC File Offset: 0x00000CBC
		public static bool IsClsCompliantIdentifierChar(char c, bool firstChar)
		{
			switch (char.GetUnicodeCategory(c))
			{
			case UnicodeCategory.UppercaseLetter:
			case UnicodeCategory.LowercaseLetter:
			case UnicodeCategory.TitlecaseLetter:
			case UnicodeCategory.ModifierLetter:
			case UnicodeCategory.OtherLetter:
			case UnicodeCategory.LetterNumber:
				return true;
			case UnicodeCategory.NonSpacingMark:
			case UnicodeCategory.SpacingCombiningMark:
			case UnicodeCategory.DecimalDigitNumber:
			case UnicodeCategory.Format:
			case UnicodeCategory.ConnectorPunctuation:
				return !firstChar;
			}
			return false;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B2C File Offset: 0x00000D2C
		public static string GenerateUniqueName(string candidate, string baseName, Predicate<string> nameExists)
		{
			if (baseName == null)
			{
				throw new ArgumentNullException("baseName");
			}
			if (!string.IsNullOrEmpty(candidate))
			{
				if (!nameExists(candidate))
				{
					return candidate;
				}
				baseName = candidate;
			}
			int num = 1;
			for (;;)
			{
				candidate = baseName + num.ToString();
				if (!nameExists(candidate))
				{
					break;
				}
				num++;
			}
			return candidate;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002B7E File Offset: 0x00000D7E
		public static string SplitName(string name)
		{
			return Regex.Replace(name, "(\\p{Ll})(\\p{Lu})|_+", "$1 $2");
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002B90 File Offset: 0x00000D90
		public static IEqualityComparer<string> CreateComparer(CultureInfo culture, CompareOptions compareOptions)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			if (compareOptions == CompareOptions.None || compareOptions == CompareOptions.IgnoreCase)
			{
				return StringComparer.Create(culture, compareOptions == CompareOptions.IgnoreCase);
			}
			return new StringUtil.StringComparerWithOptions(culture, compareOptions);
		}

		// Token: 0x04000002 RID: 2
		public static readonly string ClsCompliantIdentifierPattern = "[\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}][\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]*";

		// Token: 0x04000003 RID: 3
		private static readonly Regex m_digitSuffixRegex = new Regex("(?<suffix>\\d{1,9})$");

		// Token: 0x04000004 RID: 4
		private const string SuffixGroup = "suffix";

		// Token: 0x02000108 RID: 264
		private sealed class StringComparerWithOptions : EqualityComparer<string>
		{
			// Token: 0x06000D2F RID: 3375 RVA: 0x0002C20F File Offset: 0x0002A40F
			internal StringComparerWithOptions(CultureInfo culture, CompareOptions compareOptions)
			{
				this.m_compareInfo = culture.CompareInfo;
				this.m_options = compareOptions;
			}

			// Token: 0x06000D30 RID: 3376 RVA: 0x0002C22A File Offset: 0x0002A42A
			public override bool Equals(string x, string y)
			{
				return this.m_compareInfo.Compare(x, y, this.m_options) == 0;
			}

			// Token: 0x06000D31 RID: 3377 RVA: 0x0002C242 File Offset: 0x0002A442
			public override int GetHashCode(string obj)
			{
				return this.m_compareInfo.GetSortKey(obj, this.m_options).GetHashCode();
			}

			// Token: 0x04000577 RID: 1399
			private readonly CompareInfo m_compareInfo;

			// Token: 0x04000578 RID: 1400
			private readonly CompareOptions m_options;
		}
	}
}
