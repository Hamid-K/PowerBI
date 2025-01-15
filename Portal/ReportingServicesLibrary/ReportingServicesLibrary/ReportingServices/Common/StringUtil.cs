using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000367 RID: 871
	internal static class StringUtil
	{
		// Token: 0x06001CA3 RID: 7331 RVA: 0x00073858 File Offset: 0x00071A58
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

		// Token: 0x06001CA4 RID: 7332 RVA: 0x000738B0 File Offset: 0x00071AB0
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

		// Token: 0x06001CA5 RID: 7333 RVA: 0x000738F0 File Offset: 0x00071AF0
		public static string IncrementDigitSuffix(string value, int defaultSuffix)
		{
			int? digitSuffix = StringUtil.GetDigitSuffix(value);
			if (digitSuffix == null)
			{
				return StringUtil.SetDigitSuffix(value, defaultSuffix);
			}
			return StringUtil.SetDigitSuffix(value, digitSuffix.Value + 1);
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x00073924 File Offset: 0x00071B24
		public static string IncrementDigitSuffix(string value)
		{
			return StringUtil.IncrementDigitSuffix(value, 2);
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x00073930 File Offset: 0x00071B30
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

		// Token: 0x06001CA8 RID: 7336 RVA: 0x000739BA File Offset: 0x00071BBA
		public static string RemoveAmpersandEllipsis(string text)
		{
			return StringUtil.RemoveEllipsis(StringUtil.RemoveAmpersand(text));
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x000739C8 File Offset: 0x00071BC8
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

		// Token: 0x06001CAA RID: 7338 RVA: 0x000739F0 File Offset: 0x00071BF0
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

		// Token: 0x06001CAB RID: 7339 RVA: 0x00073A3C File Offset: 0x00071C3C
		public static string RemoveLastSubstring(string text, char separator)
		{
			int num = text.LastIndexOf(separator);
			if (num < 0)
			{
				return text;
			}
			return text.Substring(0, num);
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x00073A60 File Offset: 0x00071C60
		public static string GetLastSubstring(string text, char separator)
		{
			int num = text.LastIndexOf(separator);
			if (num < 0)
			{
				return text;
			}
			return text.Substring(num + 1);
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x00073A84 File Offset: 0x00071C84
		public static string FormatInvariant(string format, params object[] args)
		{
			return string.Format(CultureInfo.InvariantCulture, format, args);
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x00073A92 File Offset: 0x00071C92
		public static string Join(string separator, IList<string> strings)
		{
			return StringUtil.Join(separator, strings, 0, strings.Count);
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x00073AA4 File Offset: 0x00071CA4
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

		// Token: 0x06001CB0 RID: 7344 RVA: 0x00073B60 File Offset: 0x00071D60
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

		// Token: 0x06001CB1 RID: 7345 RVA: 0x00073BB5 File Offset: 0x00071DB5
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

		// Token: 0x06001CB2 RID: 7346 RVA: 0x00073BDE File Offset: 0x00071DDE
		public static string BuildErrorMessage(Exception e)
		{
			if (e.InnerException != null)
			{
				return StringUtil.BuildErrorMessage(e.InnerException) + "\r\n----------------------------\r\n" + e.Message;
			}
			return e.Message;
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x00073C0A File Offset: 0x00071E0A
		public static string MakeCLSNameFromFilename(string path)
		{
			return StringUtil.GetClsCompliantIdentifier(Path.GetFileNameWithoutExtension(path), null);
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x00073C18 File Offset: 0x00071E18
		public static StringComparer GetClsCompliantComparer()
		{
			return StringComparer.Ordinal;
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x00073C1F File Offset: 0x00071E1F
		public static int CompareClsCompliantIdentifiers(string s1, string s2)
		{
			return StringUtil.CompareClsCompliantIdentifiers(s1, s2, false);
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x00073C2C File Offset: 0x00071E2C
		public static int CompareClsCompliantIdentifiers(string s1, string s2, bool ignoreCase)
		{
			StringComparison stringComparison = (ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			return string.Compare(s1, s2, stringComparison);
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x00073C4C File Offset: 0x00071E4C
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

		// Token: 0x06001CB8 RID: 7352 RVA: 0x00073CC8 File Offset: 0x00071EC8
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

		// Token: 0x06001CB9 RID: 7353 RVA: 0x00073D14 File Offset: 0x00071F14
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

		// Token: 0x06001CBA RID: 7354 RVA: 0x00073D84 File Offset: 0x00071F84
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
				candidate = baseName + num;
				if (!nameExists(candidate))
				{
					break;
				}
				num++;
			}
			return candidate;
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x00073DD5 File Offset: 0x00071FD5
		public static string SplitName(string name)
		{
			return Regex.Replace(name, "(\\p{Ll})(\\p{Lu})|_+", "$1 $2");
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x00073DE7 File Offset: 0x00071FE7
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

		// Token: 0x04000BD2 RID: 3026
		public static readonly string ClsCompliantIdentifierPattern = "[\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}][\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]*";

		// Token: 0x04000BD3 RID: 3027
		private static readonly Regex m_digitSuffixRegex = new Regex("(?<suffix>\\d{1,9})$");

		// Token: 0x04000BD4 RID: 3028
		private const string SuffixGroup = "suffix";

		// Token: 0x020004F8 RID: 1272
		private sealed class StringComparerWithOptions : EqualityComparer<string>
		{
			// Token: 0x060024CA RID: 9418 RVA: 0x00086E58 File Offset: 0x00085058
			internal StringComparerWithOptions(CultureInfo culture, CompareOptions compareOptions)
			{
				this.m_compareInfo = culture.CompareInfo;
				this.m_options = compareOptions;
			}

			// Token: 0x060024CB RID: 9419 RVA: 0x00086E73 File Offset: 0x00085073
			public override bool Equals(string x, string y)
			{
				return this.m_compareInfo.Compare(x, y, this.m_options) == 0;
			}

			// Token: 0x060024CC RID: 9420 RVA: 0x00086E8B File Offset: 0x0008508B
			public override int GetHashCode(string obj)
			{
				return this.m_compareInfo.GetSortKey(obj, this.m_options).GetHashCode();
			}

			// Token: 0x040011B3 RID: 4531
			private readonly CompareInfo m_compareInfo;

			// Token: 0x040011B4 RID: 4532
			private readonly CompareOptions m_options;
		}
	}
}
