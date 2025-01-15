using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.InfoNav
{
	// Token: 0x0200002A RID: 42
	public static class StringUtil
	{
		// Token: 0x06000211 RID: 529 RVA: 0x00006846 File Offset: 0x00004A46
		[DebuggerStepThrough]
		internal static string FormatInvariant(string format, object arg0)
		{
			return string.Format(CultureInfo.InvariantCulture, format, new object[] { arg0 });
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000685D File Offset: 0x00004A5D
		[DebuggerStepThrough]
		internal static string FormatInvariant(string format, object arg0, object arg1)
		{
			return string.Format(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1 });
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00006878 File Offset: 0x00004A78
		[DebuggerStepThrough]
		internal static string FormatInvariant(string format, object arg0, object arg1, object arg2)
		{
			return string.Format(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1, arg2 });
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00006897 File Offset: 0x00004A97
		[DebuggerStepThrough]
		public static string FormatInvariant(string format, params object[] args)
		{
			return string.Format(CultureInfo.InvariantCulture, format, args);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000068A5 File Offset: 0x00004AA5
		[DebuggerStepThrough]
		internal static bool EqualsOrdinalIgnoreCase(string arg0, string arg1)
		{
			return string.Equals(arg0, arg1, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000068AF File Offset: 0x00004AAF
		[DebuggerStepThrough]
		internal static bool EqualsOrdinal(string arg0, string arg1)
		{
			return string.Equals(arg0, arg1, StringComparison.Ordinal);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000068B9 File Offset: 0x00004AB9
		[DebuggerStepThrough]
		internal static bool StartsWithOrdinalIgnoreCase(string originalString, string prefixString)
		{
			return originalString.StartsWith(prefixString, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000068C3 File Offset: 0x00004AC3
		[DebuggerStepThrough]
		internal static bool StartsWithOrdinal(string originalString, string prefixString)
		{
			return originalString.StartsWith(prefixString, StringComparison.Ordinal);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000068CD File Offset: 0x00004ACD
		[DebuggerStepThrough]
		internal static bool StartsWith(string originalString, string prefixString, StringComparer stringComparer)
		{
			return originalString.Length >= prefixString.Length && (prefixString.Length == 0 || stringComparer.Equals(prefixString, originalString.Substring(0, prefixString.Length)));
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000068FD File Offset: 0x00004AFD
		[DebuggerStepThrough]
		internal static bool EndsWith(string originalString, string searchString, StringComparer stringComparer)
		{
			return originalString.Length >= searchString.Length && (searchString.Length == 0 || stringComparer.Equals(searchString, originalString.Substring(originalString.Length - searchString.Length)));
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00006934 File Offset: 0x00004B34
		[DebuggerStepThrough]
		internal static bool IsClsCompliantIdentifier(string s)
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

		// Token: 0x0600021C RID: 540 RVA: 0x00006980 File Offset: 0x00004B80
		[DebuggerStepThrough]
		internal static bool IsClsCompliantIdentifierChar(char c, bool firstChar)
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

		// Token: 0x0600021D RID: 541 RVA: 0x000069F0 File Offset: 0x00004BF0
		[DebuggerStepThrough]
		internal static string DeriveClsCompliantName(string s, string fallbackName)
		{
			StringBuilder stringBuilder = new StringBuilder(s.Length);
			foreach (char c in s)
			{
				if (stringBuilder.Length > 0 && (char.IsSeparator(c) || c == '.' || c == '/'))
				{
					stringBuilder.Append('_');
				}
				else if (StringUtil.IsClsCompliantIdentifierChar(c, stringBuilder.Length == 0))
				{
					stringBuilder.Append(c);
				}
				else if (stringBuilder.Length == 0 && StringUtil.IsClsCompliantIdentifierChar(c, false))
				{
					stringBuilder.Append(fallbackName);
					stringBuilder.Append(c);
				}
			}
			if (stringBuilder.Length <= 0)
			{
				return fallbackName;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00006A95 File Offset: 0x00004C95
		[DebuggerStepThrough]
		internal static string MakeUniqueName(string candidateName, ISet<string> namesInUse)
		{
			return StringUtil.MakeUniqueName(candidateName, new Func<string, bool>(namesInUse.Contains));
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00006AAC File Offset: 0x00004CAC
		[DebuggerStepThrough]
		internal static string MakeUniqueName(string candidateName, Func<string, bool> isUsedPredicate)
		{
			string text = candidateName;
			int num = 1;
			while (isUsedPredicate(candidateName))
			{
				candidateName = text + num++.ToString(CultureInfo.InvariantCulture);
			}
			return candidateName;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00006AE3 File Offset: 0x00004CE3
		[DebuggerStepThrough]
		internal static StringBuilder AppendFormatInvariant(this StringBuilder builder, string format, params object[] args)
		{
			builder.AppendFormat(CultureInfo.InvariantCulture, format, args);
			return builder;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00006AF4 File Offset: 0x00004CF4
		[DebuggerStepThrough]
		internal static StringBuilder AppendMany(this StringBuilder builder, IEnumerable<object> items, string delimiter)
		{
			bool flag = false;
			foreach (object obj in items)
			{
				if (flag)
				{
					builder.Append(delimiter);
				}
				else
				{
					flag = true;
				}
				builder.Append(obj);
			}
			return builder;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00006B50 File Offset: 0x00004D50
		[DebuggerStepThrough]
		internal static StringBuilder AppendMany<T>(this StringBuilder builder, IEnumerable<T> items, string delimiter, Func<T, string> toString)
		{
			bool flag = false;
			foreach (T t in items)
			{
				if (flag)
				{
					builder.Append(delimiter);
				}
				else
				{
					flag = true;
				}
				builder.Append(toString(t));
			}
			return builder;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00006BB4 File Offset: 0x00004DB4
		[DebuggerStepThrough]
		internal static string ToCommaDelimitedText<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, string pairsDelimiter = null, string keyValueDelimiter = null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				if (stringBuilder.Length > 0)
				{
					if (pairsDelimiter == null)
					{
						stringBuilder.AppendLine();
					}
					else
					{
						stringBuilder.Append(pairsDelimiter);
					}
				}
				stringBuilder.Append(keyValuePair.Key);
				stringBuilder.Append(keyValueDelimiter ?? ", ");
				stringBuilder.Append(keyValuePair.Value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00006C58 File Offset: 0x00004E58
		[DebuggerStepThrough]
		internal static string Indent(this string text, string indent = null)
		{
			if (indent == null)
			{
				indent = "\t";
			}
			StringBuilder stringBuilder = new StringBuilder(text.Length + 50);
			using (StringReader stringReader = new StringReader(text))
			{
				string text2;
				while ((text2 = stringReader.ReadLine()) != null)
				{
					stringBuilder.Append(indent);
					stringBuilder.AppendLine(text2);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00006CC4 File Offset: 0x00004EC4
		[DebuggerStepThrough]
		internal static string StringJoin<T>(this IEnumerable<T> items, string separator = null)
		{
			return string.Join<T>(separator ?? Environment.NewLine, items);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00006CD8 File Offset: 0x00004ED8
		[DebuggerStepThrough]
		internal static string Trim(this string text, ref int startIndex, ref int endIndex)
		{
			while (startIndex < endIndex)
			{
				if (!char.IsWhiteSpace(text[startIndex]))
				{
					break;
				}
				startIndex++;
			}
			while (endIndex >= startIndex && char.IsWhiteSpace(text[endIndex]))
			{
				endIndex--;
			}
			return text.Substring(startIndex, endIndex - startIndex + 1);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00006D30 File Offset: 0x00004F30
		[DebuggerStepThrough]
		internal static string NormalizeWhiteSpace(this string text)
		{
			text = text.Trim();
			int length = text.Length;
			StringBuilder stringBuilder = new StringBuilder(length);
			bool flag = false;
			int i = 0;
			while (i < length)
			{
				char c = text[i];
				if (!char.IsWhiteSpace(c))
				{
					flag = false;
					goto IL_003B;
				}
				if (!flag)
				{
					c = ' ';
					flag = true;
					goto IL_003B;
				}
				IL_0044:
				i++;
				continue;
				IL_003B:
				stringBuilder.Append(c);
				goto IL_0044;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00006D8F File Offset: 0x00004F8F
		[DebuggerStepThrough]
		internal static string EscapeIdentifier(string identifier)
		{
			if (StringUtil.IsClsCompliantIdentifier(identifier))
			{
				return identifier;
			}
			return StringUtil.FormatInvariant("[{0}]", identifier.Replace("]", "]]"));
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00006DB5 File Offset: 0x00004FB5
		public static string ToStringInvariant(this long value)
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00006DC3 File Offset: 0x00004FC3
		public static string ToStringInvariant(this int value)
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00006DD1 File Offset: 0x00004FD1
		public static string ToStringInvariant(this bool value)
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00006DDF File Offset: 0x00004FDF
		public static string ToStringInvariant(this double value)
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00006DED File Offset: 0x00004FED
		public static string ToStringInvariant(this decimal value)
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00006DFB File Offset: 0x00004FFB
		public static string ToStringInvariant(this DateTime value)
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00006E09 File Offset: 0x00005009
		public static string ToStringInvariant(this DateTime value, string format)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0:" + format + "}", new object[] { value });
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00006E34 File Offset: 0x00005034
		public static string ToStringInvariant(this double value, string format)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0:" + format + "}", new object[] { value });
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00006E5F File Offset: 0x0000505F
		public static MemoryStream AsStream(this string value)
		{
			return new MemoryStream(Encoding.UTF8.GetBytes(value));
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00006E74 File Offset: 0x00005074
		public static string BreakIdentifierIntoWords(string name, TextInfo textInfo, bool capitalizeWord = false)
		{
			StringBuilder stringBuilder = new StringBuilder(name.Length);
			bool flag = true;
			bool flag2 = false;
			int i = 0;
			while (i < name.Length)
			{
				char c = name[i];
				bool flag3 = i + 1 < name.Length && char.IsLower(name[i + 1]);
				bool flag4 = false;
				if (flag)
				{
					if (!StringUtil.IsSeparator(c))
					{
						goto IL_008B;
					}
				}
				else
				{
					if (StringUtil.IsSeparator(c))
					{
						c = ' ';
						flag4 = true;
						goto IL_008B;
					}
					if ((char.IsUpper(c) || char.IsDigit(c)) && (flag2 || flag3))
					{
						stringBuilder.Append(' ');
						flag = true;
						goto IL_008B;
					}
					goto IL_008B;
				}
				IL_00EA:
				i++;
				continue;
				IL_008B:
				flag2 = char.IsLower(c);
				if (flag)
				{
					if (capitalizeWord)
					{
						stringBuilder.Append(textInfo.ToUpper(c));
					}
					else if (flag3)
					{
						stringBuilder.Append(textInfo.ToLower(c));
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				else if (flag2)
				{
					stringBuilder.Append(textInfo.ToLower(c));
				}
				else
				{
					stringBuilder.Append(c);
				}
				flag = flag4;
				goto IL_00EA;
			}
			string text = stringBuilder.ToString().Trim();
			if (text.Length != 0)
			{
				return text;
			}
			return name;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00006F98 File Offset: 0x00005198
		private static bool IsSeparator(char c)
		{
			UnicodeCategory unicodeCategory = char.GetUnicodeCategory(c);
			return unicodeCategory - UnicodeCategory.SpaceSeparator <= 2 || unicodeCategory - UnicodeCategory.ConnectorPunctuation <= 6;
		}
	}
}
