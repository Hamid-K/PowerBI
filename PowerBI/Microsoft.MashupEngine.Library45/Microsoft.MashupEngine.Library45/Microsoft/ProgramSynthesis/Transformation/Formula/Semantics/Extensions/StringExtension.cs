using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions
{
	// Token: 0x020017D0 RID: 6096
	public static class StringExtension
	{
		// Token: 0x0600C927 RID: 51495 RVA: 0x002B1D14 File Offset: 0x002AFF14
		public static string After(this string subject, string findText)
		{
			if (subject == null || string.IsNullOrEmpty(findText))
			{
				return null;
			}
			int num = subject.IndexOf(findText, StringComparison.Ordinal);
			if (num < 0)
			{
				return subject;
			}
			return subject.Substring(num + findText.Length);
		}

		// Token: 0x0600C928 RID: 51496 RVA: 0x002B1D4C File Offset: 0x002AFF4C
		public static IEnumerable<string> AllPrefixes(this string subject, int? endIndex = null)
		{
			return from i in Enumerable.Range(1, (endIndex == null) ? subject.Length : Math.Min(endIndex.Value, subject.Length))
				select subject.Substring(0, i);
		}

		// Token: 0x0600C929 RID: 51497 RVA: 0x002B1DAC File Offset: 0x002AFFAC
		public static IEnumerable<string> AllSubstrings(this string subject)
		{
			return from i in Enumerable.Range(0, subject.Length)
				from len in Enumerable.Range(1, subject.Length - i)
				select subject.Substring(i, len);
		}

		// Token: 0x0600C92A RID: 51498 RVA: 0x002B1DF4 File Offset: 0x002AFFF4
		public static IEnumerable<Record<int, string>> AllSubstringsDetail(this string subject)
		{
			return from i in Enumerable.Range(0, subject.Length)
				from len in Enumerable.Range(1, subject.Length - i)
				select Record.Create<int, string>(i, subject.Substring(i, len));
		}

		// Token: 0x0600C92B RID: 51499 RVA: 0x002B1E3C File Offset: 0x002B003C
		public static IEnumerable<string> AllSuffixes(this string subject, int startIndex = 1)
		{
			return from i in Enumerable.Range(startIndex, subject.Length - startIndex)
				select subject.Substring(i);
		}

		// Token: 0x0600C92C RID: 51500 RVA: 0x002B1E7A File Offset: 0x002B007A
		public static IEnumerable<T> AppendDistinct<T>(this IEnumerable<T> source, T target)
		{
			return source.Union(target.Yield<T>());
		}

		// Token: 0x0600C92D RID: 51501 RVA: 0x002B1E88 File Offset: 0x002B0088
		public static string Before(this string subject, string findText)
		{
			if (subject == null || string.IsNullOrEmpty(findText))
			{
				return null;
			}
			int num = subject.IndexOf(findText, StringComparison.Ordinal);
			if (num < 0)
			{
				return subject;
			}
			return subject.Substring(0, num);
		}

		// Token: 0x0600C92E RID: 51502 RVA: 0x00056398 File Offset: 0x00054598
		public static string Concat(this string subject, string value)
		{
			return subject + value;
		}

		// Token: 0x0600C92F RID: 51503 RVA: 0x002B1EB9 File Offset: 0x002B00B9
		public static string Concat(this string subject, IEnumerable<string> values)
		{
			return subject + string.Concat(values);
		}

		// Token: 0x0600C930 RID: 51504 RVA: 0x002B1EC7 File Offset: 0x002B00C7
		public static bool ContainsIgnoreCase(this string subject, string target)
		{
			return subject.IndexOf(target, StringComparison.OrdinalIgnoreCase) >= 0;
		}

		// Token: 0x0600C931 RID: 51505 RVA: 0x002B1ED7 File Offset: 0x002B00D7
		public static int IndexOfIgnoreCase(this string subject, string target)
		{
			return subject.IndexOf(target, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600C932 RID: 51506 RVA: 0x002B1EE1 File Offset: 0x002B00E1
		public static int IndexOfOrdinal(this string subject, string target)
		{
			return subject.IndexOf(target, StringComparison.Ordinal);
		}

		// Token: 0x0600C933 RID: 51507 RVA: 0x002B1EEB File Offset: 0x002B00EB
		public static bool IsNullOrEmpty(this string subject)
		{
			return string.IsNullOrEmpty(subject);
		}

		// Token: 0x0600C934 RID: 51508 RVA: 0x002B1EF3 File Offset: 0x002B00F3
		public static string Repeat(this char subject, int count)
		{
			return new string(subject, count);
		}

		// Token: 0x0600C935 RID: 51509 RVA: 0x002B1EFC File Offset: 0x002B00FC
		public static string Repeat(this string subject, int count)
		{
			return string.Concat(Enumerable.Repeat<string>(subject, count));
		}

		// Token: 0x0600C936 RID: 51510 RVA: 0x002B1F0A File Offset: 0x002B010A
		public static string Splice(this string subject, int startIndex, string replacement)
		{
			return subject.Splice(startIndex, replacement, replacement.Length);
		}

		// Token: 0x0600C937 RID: 51511 RVA: 0x002B1F1A File Offset: 0x002B011A
		public static string Splice(this string subject, int startIndex, string replacement, int removeCount)
		{
			return subject.Remove(startIndex, removeCount).Insert(startIndex, replacement);
		}

		// Token: 0x0600C938 RID: 51512 RVA: 0x002B1F2C File Offset: 0x002B012C
		public static IEnumerable<string> Indent(this IEnumerable<string> subject, int length = 4, bool skipFirstLine = false)
		{
			if (subject != null)
			{
				return subject.Select((string s) => s.Indent(length, skipFirstLine));
			}
			return Utils.Empty<string>();
		}

		// Token: 0x0600C939 RID: 51513 RVA: 0x002B1F68 File Offset: 0x002B0168
		public static string Indent(this string subject, int length = 4, bool skipFirstLine = false)
		{
			if (subject == null)
			{
				return string.Empty;
			}
			string newLine = Environment.NewLine;
			string text = ' '.Repeat(length);
			string text2 = subject.Replace(newLine, newLine + text);
			if (!skipFirstLine)
			{
				return text + text2;
			}
			return text2;
		}

		// Token: 0x0600C93A RID: 51514 RVA: 0x002B1FA8 File Offset: 0x002B01A8
		public static string IndentSkipFirstLine(this string subject, int length = 4)
		{
			return subject.Indent(length, true);
		}

		// Token: 0x0600C93B RID: 51515 RVA: 0x002B1FB2 File Offset: 0x002B01B2
		public static bool AllDelimiters(this IEnumerable<char> subject)
		{
			if (subject == null)
			{
				return false;
			}
			return subject.All((char c) => c.IsDelimiter());
		}

		// Token: 0x0600C93C RID: 51516 RVA: 0x002B1FDE File Offset: 0x002B01DE
		public static bool AnyDelimiters(this IEnumerable<char> subject)
		{
			if (subject == null)
			{
				return false;
			}
			return subject.Any((char c) => c.IsDelimiter());
		}

		// Token: 0x0600C93D RID: 51517 RVA: 0x002B200A File Offset: 0x002B020A
		public static bool IsCurrencySymbol(this char subject)
		{
			return subject.ToUnicodeCategory() == UnicodeCategory.CurrencySymbol;
		}

		// Token: 0x0600C93E RID: 51518 RVA: 0x002B2016 File Offset: 0x002B0216
		public static bool IsCurrencySymbol(this string subject)
		{
			return subject.Length == 1 && subject[0].IsCurrencySymbol();
		}

		// Token: 0x0600C93F RID: 51519 RVA: 0x002B2030 File Offset: 0x002B0230
		public static bool IsDelimiter(this char subject)
		{
			bool flag = subject == '\n' || subject == '\t';
			if (!flag)
			{
				UnicodeCategory unicodeCategory = subject.ToUnicodeCategory();
				bool flag2 = unicodeCategory - UnicodeCategory.SpaceSeparator <= 2 || unicodeCategory - UnicodeCategory.ConnectorPunctuation <= 10;
				flag = flag2;
			}
			return flag;
		}

		// Token: 0x0600C940 RID: 51520 RVA: 0x002B206D File Offset: 0x002B026D
		public static bool IsDelimiter(this char? subject)
		{
			return subject != null && subject.Value.IsDelimiter();
		}

		// Token: 0x0600C941 RID: 51521 RVA: 0x002B2088 File Offset: 0x002B0288
		public static bool IsLetter(this char subject)
		{
			UnicodeCategory unicodeCategory = subject.ToUnicodeCategory();
			return unicodeCategory <= UnicodeCategory.OtherLetter || unicodeCategory == UnicodeCategory.LetterNumber;
		}

		// Token: 0x0600C942 RID: 51522 RVA: 0x002B20AC File Offset: 0x002B02AC
		public static bool IsNumber(this char subject)
		{
			return subject.ToUnicodeCategory() == UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x0600C943 RID: 51523 RVA: 0x002B20B7 File Offset: 0x002B02B7
		public static bool IsWhitespace(this char subject)
		{
			return subject == '\n' || subject == '\t' || subject.ToUnicodeCategory() == UnicodeCategory.SpaceSeparator;
		}

		// Token: 0x0600C944 RID: 51524 RVA: 0x002B20D0 File Offset: 0x002B02D0
		public static IEnumerable<string> PrependId(this IEnumerable<string> items, int startId = 1)
		{
			if (items == null)
			{
				return null;
			}
			items = items.ToReadOnlyList<string>();
			if (!items.Any<string>())
			{
				return new string[] { "<none>" };
			}
			int maxPositionLength = items.Select((string _, int idx) => (idx + startId).ToString().Length).Max();
			int indent = maxPositionLength + 2;
			return items.Select(delegate(string item, int idx)
			{
				int num = idx + startId;
				return ((maxPositionLength > 1) ? num.ToString().PadLeft(maxPositionLength) : num.ToString()) + ": " + item.ToString().IndentSkipFirstLine(indent);
			}).ToList<string>();
		}

		// Token: 0x0600C945 RID: 51525 RVA: 0x002B2150 File Offset: 0x002B0350
		public static string RenderNumbered(this IEnumerable<string> items, int startId = 1)
		{
			return items.PrependId(startId).ToJoinNewlineString();
		}

		// Token: 0x0600C946 RID: 51526 RVA: 0x002B215E File Offset: 0x002B035E
		public static IEnumerable<char> TakeWhileDelimiter(this string subject)
		{
			IEnumerable<char> enumerable;
			if (subject == null)
			{
				enumerable = null;
			}
			else
			{
				enumerable = subject.TakeWhile((char c) => c.IsDelimiter());
			}
			return enumerable ?? new char[0];
		}

		// Token: 0x0600C947 RID: 51527 RVA: 0x002B2198 File Offset: 0x002B0398
		public static Dictionary<TKey, IReadOnlyList<TResultValue>> ToLookupDictionary<TKey, TValue, TResultValue>(this IEnumerable<TValue> subject, Func<TValue, TKey> keySelector, Func<TValue, IReadOnlyList<TResultValue>> valueSelector)
		{
			return subject.GroupBy(keySelector).ToDictionary((IGrouping<TKey, TValue> c) => c.Key, (IGrouping<TKey, TValue> c) => c.SelectMany(valueSelector).ToReadOnlyList<TResultValue>());
		}

		// Token: 0x0600C948 RID: 51528 RVA: 0x002B21E9 File Offset: 0x002B03E9
		public static IEnumerable<UnicodeCategory> ToUnicodeCategory(this string subject)
		{
			Func<char, UnicodeCategory> func;
			if ((func = StringExtension.<>O.<0>__ToUnicodeCategory) == null)
			{
				func = (StringExtension.<>O.<0>__ToUnicodeCategory = new Func<char, UnicodeCategory>(StringExtension.ToUnicodeCategory));
			}
			return subject.Select(func);
		}

		// Token: 0x0600C949 RID: 51529 RVA: 0x002B220C File Offset: 0x002B040C
		public static UnicodeCategory ToUnicodeCategory(this char subject)
		{
			return CharUnicodeInfo.GetUnicodeCategory(subject);
		}

		// Token: 0x0600C94A RID: 51530 RVA: 0x00284B4E File Offset: 0x00282D4E
		public static IEnumerable<int> IndexRange(this string subject)
		{
			return Utils.Range(0, subject.Length - 1);
		}

		// Token: 0x020017D1 RID: 6097
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04004F0C RID: 20236
			public static Func<char, UnicodeCategory> <0>__ToUnicodeCategory;
		}
	}
}
