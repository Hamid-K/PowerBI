using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.Reporting
{
	// Token: 0x020000CB RID: 203
	internal static class StringUtil
	{
		// Token: 0x06000CF8 RID: 3320 RVA: 0x000218D0 File Offset: 0x0001FAD0
		public static int? GetDigitSuffix(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				Match match = StringUtil.DigitSuffixRegex.Match(value);
				if (match.Success)
				{
					return new int?(int.Parse(match.Groups["suffix"].Value, CultureInfo.InvariantCulture));
				}
			}
			return null;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00021928 File Offset: 0x0001FB28
		public static string SetDigitSuffix(string value, int suffix)
		{
			int? digitSuffix = StringUtil.GetDigitSuffix(value);
			string text = suffix.ToString(CultureInfo.InvariantCulture);
			if (digitSuffix == null)
			{
				return value + text;
			}
			return StringUtil.DigitSuffixRegex.Replace(value, text);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00021968 File Offset: 0x0001FB68
		public static string IncrementDigitSuffix(string value, int defaultSuffix)
		{
			int? digitSuffix = StringUtil.GetDigitSuffix(value);
			if (digitSuffix == null)
			{
				return StringUtil.SetDigitSuffix(value, defaultSuffix);
			}
			return StringUtil.SetDigitSuffix(value, digitSuffix.Value + 1);
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0002199C File Offset: 0x0001FB9C
		public static string IncrementDigitSuffix(string value)
		{
			return StringUtil.IncrementDigitSuffix(value, 2);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x000219A8 File Offset: 0x0001FBA8
		public static string TrimToMaxLength(string value, int maxLength)
		{
			value = value ?? string.Empty;
			if (value.Length <= maxLength)
			{
				return value;
			}
			Match match = StringUtil.DigitSuffixRegex.Match(value);
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

		// Token: 0x06000CFD RID: 3325 RVA: 0x00021A32 File Offset: 0x0001FC32
		public static string FormatInvariant(string format, params object[] args)
		{
			return string.Format(CultureInfo.InvariantCulture, format, args);
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00021A40 File Offset: 0x0001FC40
		public static StringComparer GetClsCompliantComparer()
		{
			return StringComparer.Ordinal;
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00021A47 File Offset: 0x0001FC47
		public static int CompareClsCompliantIdentifiers(string s1, string s2)
		{
			return StringUtil.CompareClsCompliantIdentifiers(s1, s2, false);
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00021A54 File Offset: 0x0001FC54
		public static int CompareClsCompliantIdentifiers(string s1, string s2, bool ignoreCase)
		{
			StringComparison stringComparison = (ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			return string.Compare(s1, s2, stringComparison);
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00021A74 File Offset: 0x0001FC74
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

		// Token: 0x06000D02 RID: 3330 RVA: 0x00021AF0 File Offset: 0x0001FCF0
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

		// Token: 0x06000D03 RID: 3331 RVA: 0x00021B3C File Offset: 0x0001FD3C
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

		// Token: 0x06000D04 RID: 3332 RVA: 0x00021BAC File Offset: 0x0001FDAC
		public static bool IsNullOrWhiteSpace(this string str)
		{
			if (str == null)
			{
				return true;
			}
			for (int i = 0; i < str.Length; i++)
			{
				if (!char.IsWhiteSpace(str[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x00021BE0 File Offset: 0x0001FDE0
		public static string GenerateUniqueName(string candidate, Predicate<string> nameExists)
		{
			return StringUtil.GenerateUniqueName(candidate, candidate, nameExists);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x00021BEC File Offset: 0x0001FDEC
		public static string GenerateUniqueName(string candidate, string baseName, Predicate<string> nameExists)
		{
			ArgumentValidation.CheckNotNull<string>(baseName, "baseName");
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

		// Token: 0x06000D07 RID: 3335 RVA: 0x00021C3C File Offset: 0x0001FE3C
		public static string Join<T>(IEnumerable<T> enumerable, string delimiter)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (T t in enumerable)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(delimiter);
				}
				stringBuilder.Append(t.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00021CB0 File Offset: 0x0001FEB0
		public static bool In(this string search, params string[] strings)
		{
			return strings.Contains(search);
		}

		// Token: 0x0400096F RID: 2415
		private const string SuffixGroup = "suffix";

		// Token: 0x04000970 RID: 2416
		internal static readonly string ClsCompliantIdentifierPattern = "[\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}][\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]*";

		// Token: 0x04000971 RID: 2417
		private static readonly Regex DigitSuffixRegex = new Regex("(?<suffix>\\d{1,9})$");
	}
}
