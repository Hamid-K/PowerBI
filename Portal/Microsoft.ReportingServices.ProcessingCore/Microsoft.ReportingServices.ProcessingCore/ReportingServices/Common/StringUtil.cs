using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020005D6 RID: 1494
	internal static class StringUtil
	{
		// Token: 0x060053C8 RID: 21448 RVA: 0x00160FF8 File Offset: 0x0015F1F8
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

		// Token: 0x060053C9 RID: 21449 RVA: 0x00161050 File Offset: 0x0015F250
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

		// Token: 0x060053CA RID: 21450 RVA: 0x00161090 File Offset: 0x0015F290
		public static string IncrementDigitSuffix(string value, int defaultSuffix)
		{
			int? digitSuffix = StringUtil.GetDigitSuffix(value);
			if (digitSuffix == null)
			{
				return StringUtil.SetDigitSuffix(value, defaultSuffix);
			}
			return StringUtil.SetDigitSuffix(value, digitSuffix.Value + 1);
		}

		// Token: 0x060053CB RID: 21451 RVA: 0x001610C4 File Offset: 0x0015F2C4
		public static string IncrementDigitSuffix(string value)
		{
			return StringUtil.IncrementDigitSuffix(value, 2);
		}

		// Token: 0x060053CC RID: 21452 RVA: 0x001610D0 File Offset: 0x0015F2D0
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

		// Token: 0x060053CD RID: 21453 RVA: 0x0016115A File Offset: 0x0015F35A
		public static string RemoveAmpersandEllipsis(string text)
		{
			return StringUtil.RemoveEllipsis(StringUtil.RemoveAmpersand(text));
		}

		// Token: 0x060053CE RID: 21454 RVA: 0x00161168 File Offset: 0x0015F368
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

		// Token: 0x060053CF RID: 21455 RVA: 0x00161190 File Offset: 0x0015F390
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

		// Token: 0x060053D0 RID: 21456 RVA: 0x001611DC File Offset: 0x0015F3DC
		public static string RemoveLastSubstring(string text, char separator)
		{
			int num = text.LastIndexOf(separator);
			if (num < 0)
			{
				return text;
			}
			return text.Substring(0, num);
		}

		// Token: 0x060053D1 RID: 21457 RVA: 0x00161200 File Offset: 0x0015F400
		public static string GetLastSubstring(string text, char separator)
		{
			int num = text.LastIndexOf(separator);
			if (num < 0)
			{
				return text;
			}
			return text.Substring(num + 1);
		}

		// Token: 0x060053D2 RID: 21458 RVA: 0x00161224 File Offset: 0x0015F424
		public static string FormatInvariant(string format, params object[] args)
		{
			return string.Format(CultureInfo.InvariantCulture, format, args);
		}

		// Token: 0x060053D3 RID: 21459 RVA: 0x00161232 File Offset: 0x0015F432
		public static string Join(string separator, IList<string> strings)
		{
			return StringUtil.Join(separator, strings, 0, strings.Count);
		}

		// Token: 0x060053D4 RID: 21460 RVA: 0x00161244 File Offset: 0x0015F444
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

		// Token: 0x060053D5 RID: 21461 RVA: 0x00161300 File Offset: 0x0015F500
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

		// Token: 0x060053D6 RID: 21462 RVA: 0x00161355 File Offset: 0x0015F555
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

		// Token: 0x060053D7 RID: 21463 RVA: 0x0016137E File Offset: 0x0015F57E
		public static string BuildErrorMessage(Exception e)
		{
			if (e.InnerException != null)
			{
				return StringUtil.BuildErrorMessage(e.InnerException) + "\r\n----------------------------\r\n" + e.Message;
			}
			return e.Message;
		}

		// Token: 0x060053D8 RID: 21464 RVA: 0x001613AA File Offset: 0x0015F5AA
		public static string MakeCLSNameFromFilename(string path)
		{
			return StringUtil.GetClsCompliantIdentifier(Path.GetFileNameWithoutExtension(path), null);
		}

		// Token: 0x060053D9 RID: 21465 RVA: 0x001613B8 File Offset: 0x0015F5B8
		public static StringComparer GetClsCompliantComparer()
		{
			return StringComparer.Ordinal;
		}

		// Token: 0x060053DA RID: 21466 RVA: 0x001613BF File Offset: 0x0015F5BF
		public static int CompareClsCompliantIdentifiers(string s1, string s2)
		{
			return StringUtil.CompareClsCompliantIdentifiers(s1, s2, false);
		}

		// Token: 0x060053DB RID: 21467 RVA: 0x001613CC File Offset: 0x0015F5CC
		public static int CompareClsCompliantIdentifiers(string s1, string s2, bool ignoreCase)
		{
			StringComparison stringComparison = (ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			return string.Compare(s1, s2, stringComparison);
		}

		// Token: 0x060053DC RID: 21468 RVA: 0x001613EC File Offset: 0x0015F5EC
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

		// Token: 0x060053DD RID: 21469 RVA: 0x00161468 File Offset: 0x0015F668
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

		// Token: 0x060053DE RID: 21470 RVA: 0x001614B4 File Offset: 0x0015F6B4
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

		// Token: 0x060053DF RID: 21471 RVA: 0x00161524 File Offset: 0x0015F724
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

		// Token: 0x060053E0 RID: 21472 RVA: 0x00161576 File Offset: 0x0015F776
		public static string SplitName(string name)
		{
			return Regex.Replace(name, "(\\p{Ll})(\\p{Lu})|_+", "$1 $2");
		}

		// Token: 0x060053E1 RID: 21473 RVA: 0x00161588 File Offset: 0x0015F788
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

		// Token: 0x04002A34 RID: 10804
		public static readonly string ClsCompliantIdentifierPattern = "[\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}][\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]*";

		// Token: 0x04002A35 RID: 10805
		private static readonly Regex m_digitSuffixRegex = new Regex("(?<suffix>\\d{1,9})$");

		// Token: 0x04002A36 RID: 10806
		private const string SuffixGroup = "suffix";

		// Token: 0x02000C0D RID: 3085
		private sealed class StringComparerWithOptions : EqualityComparer<string>
		{
			// Token: 0x0600864A RID: 34378 RVA: 0x00219A17 File Offset: 0x00217C17
			internal StringComparerWithOptions(CultureInfo culture, CompareOptions compareOptions)
			{
				this.m_compareInfo = culture.CompareInfo;
				this.m_options = compareOptions;
			}

			// Token: 0x0600864B RID: 34379 RVA: 0x00219A32 File Offset: 0x00217C32
			public override bool Equals(string x, string y)
			{
				return this.m_compareInfo.Compare(x, y, this.m_options) == 0;
			}

			// Token: 0x0600864C RID: 34380 RVA: 0x00219A4A File Offset: 0x00217C4A
			public override int GetHashCode(string obj)
			{
				return this.m_compareInfo.GetSortKey(obj, this.m_options).GetHashCode();
			}

			// Token: 0x04004814 RID: 18452
			private readonly CompareInfo m_compareInfo;

			// Token: 0x04004815 RID: 18453
			private readonly CompareOptions m_options;
		}
	}
}
