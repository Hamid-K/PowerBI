using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000061 RID: 97
	internal static class StringUtil
	{
		// Token: 0x060003B7 RID: 951 RVA: 0x00015C28 File Offset: 0x00013E28
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

		// Token: 0x060003B8 RID: 952 RVA: 0x00015C80 File Offset: 0x00013E80
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

		// Token: 0x060003B9 RID: 953 RVA: 0x00015CC0 File Offset: 0x00013EC0
		public static string IncrementDigitSuffix(string value, int defaultSuffix)
		{
			int? digitSuffix = StringUtil.GetDigitSuffix(value);
			if (digitSuffix == null)
			{
				return StringUtil.SetDigitSuffix(value, defaultSuffix);
			}
			return StringUtil.SetDigitSuffix(value, digitSuffix.Value + 1);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00015CF4 File Offset: 0x00013EF4
		public static string IncrementDigitSuffix(string value)
		{
			return StringUtil.IncrementDigitSuffix(value, 2);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00015D00 File Offset: 0x00013F00
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

		// Token: 0x060003BC RID: 956 RVA: 0x00015D8A File Offset: 0x00013F8A
		public static string RemoveAmpersandEllipsis(string text)
		{
			return StringUtil.RemoveEllipsis(StringUtil.RemoveAmpersand(text));
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00015D98 File Offset: 0x00013F98
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

		// Token: 0x060003BE RID: 958 RVA: 0x00015DC0 File Offset: 0x00013FC0
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

		// Token: 0x060003BF RID: 959 RVA: 0x00015E0C File Offset: 0x0001400C
		public static string RemoveLastSubstring(string text, char separator)
		{
			int num = text.LastIndexOf(separator);
			if (num < 0)
			{
				return text;
			}
			return text.Substring(0, num);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00015E30 File Offset: 0x00014030
		public static string GetLastSubstring(string text, char separator)
		{
			int num = text.LastIndexOf(separator);
			if (num < 0)
			{
				return text;
			}
			return text.Substring(num + 1);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00015E54 File Offset: 0x00014054
		public static string FormatInvariant(string format, params object[] args)
		{
			return string.Format(CultureInfo.InvariantCulture, format, args);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00015E62 File Offset: 0x00014062
		public static string Join(string separator, IList<string> strings)
		{
			return StringUtil.Join(separator, strings, 0, strings.Count);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00015E74 File Offset: 0x00014074
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

		// Token: 0x060003C4 RID: 964 RVA: 0x00015F30 File Offset: 0x00014130
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

		// Token: 0x060003C5 RID: 965 RVA: 0x00015F85 File Offset: 0x00014185
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

		// Token: 0x060003C6 RID: 966 RVA: 0x00015FAE File Offset: 0x000141AE
		public static string BuildErrorMessage(Exception e)
		{
			if (e.InnerException != null)
			{
				return StringUtil.BuildErrorMessage(e.InnerException) + "\r\n----------------------------\r\n" + e.Message;
			}
			return e.Message;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00015FDA File Offset: 0x000141DA
		public static string MakeCLSNameFromFilename(string path)
		{
			return StringUtil.GetClsCompliantIdentifier(Path.GetFileNameWithoutExtension(path), null);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00015FE8 File Offset: 0x000141E8
		public static StringComparer GetClsCompliantComparer()
		{
			return StringComparer.Ordinal;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00015FEF File Offset: 0x000141EF
		public static int CompareClsCompliantIdentifiers(string s1, string s2)
		{
			return StringUtil.CompareClsCompliantIdentifiers(s1, s2, false);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00015FFC File Offset: 0x000141FC
		public static int CompareClsCompliantIdentifiers(string s1, string s2, bool ignoreCase)
		{
			StringComparison stringComparison = (ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			return string.Compare(s1, s2, stringComparison);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0001601C File Offset: 0x0001421C
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

		// Token: 0x060003CC RID: 972 RVA: 0x00016098 File Offset: 0x00014298
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

		// Token: 0x060003CD RID: 973 RVA: 0x000160E4 File Offset: 0x000142E4
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

		// Token: 0x060003CE RID: 974 RVA: 0x00016154 File Offset: 0x00014354
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

		// Token: 0x060003CF RID: 975 RVA: 0x000161A6 File Offset: 0x000143A6
		public static string SplitName(string name)
		{
			return Regex.Replace(name, "(\\p{Ll})(\\p{Lu})|_+", "$1 $2");
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000161B8 File Offset: 0x000143B8
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

		// Token: 0x040000FF RID: 255
		public static readonly string ClsCompliantIdentifierPattern = "[\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}][\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]*";

		// Token: 0x04000100 RID: 256
		private static readonly Regex m_digitSuffixRegex = new Regex("(?<suffix>\\d{1,9})$");

		// Token: 0x04000101 RID: 257
		private const string SuffixGroup = "suffix";

		// Token: 0x02000326 RID: 806
		private sealed class StringComparerWithOptions : EqualityComparer<string>
		{
			// Token: 0x06001738 RID: 5944 RVA: 0x00036FB5 File Offset: 0x000351B5
			internal StringComparerWithOptions(CultureInfo culture, CompareOptions compareOptions)
			{
				this.m_compareInfo = culture.CompareInfo;
				this.m_options = compareOptions;
			}

			// Token: 0x06001739 RID: 5945 RVA: 0x00036FD0 File Offset: 0x000351D0
			public override bool Equals(string x, string y)
			{
				return this.m_compareInfo.Compare(x, y, this.m_options) == 0;
			}

			// Token: 0x0600173A RID: 5946 RVA: 0x00036FE8 File Offset: 0x000351E8
			public override int GetHashCode(string obj)
			{
				return this.m_compareInfo.GetSortKey(obj, this.m_options).GetHashCode();
			}

			// Token: 0x04000726 RID: 1830
			private readonly CompareInfo m_compareInfo;

			// Token: 0x04000727 RID: 1831
			private readonly CompareOptions m_options;
		}
	}
}
