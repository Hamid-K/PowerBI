using System;

namespace Microsoft.OData
{
	// Token: 0x02000004 RID: 4
	internal abstract class AnnotationFilterPattern : IComparable<AnnotationFilterPattern>
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000214F File Offset: 0x0000034F
		private AnnotationFilterPattern(string pattern, bool isExclude)
		{
			this.isExclude = isExclude;
			this.Pattern = pattern;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002165 File Offset: 0x00000365
		internal virtual bool IsExclude
		{
			get
			{
				return this.isExclude;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002170 File Offset: 0x00000370
		public int CompareTo(AnnotationFilterPattern other)
		{
			ExceptionUtils.CheckArgumentNotNull<AnnotationFilterPattern>(other, "other");
			int num = AnnotationFilterPattern.ComparePatternPriority(this.Pattern, other.Pattern);
			if (num != 0)
			{
				return num;
			}
			if (this.IsExclude == other.IsExclude)
			{
				return 0;
			}
			if (!this.IsExclude)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021BC File Offset: 0x000003BC
		internal static AnnotationFilterPattern Create(string pattern)
		{
			AnnotationFilterPattern.ValidatePattern(pattern);
			bool flag = AnnotationFilterPattern.RemoveExcludeOperator(ref pattern);
			if (pattern == "*")
			{
				if (!flag)
				{
					return AnnotationFilterPattern.IncludeAllPattern;
				}
				return AnnotationFilterPattern.ExcludeAllPattern;
			}
			else
			{
				if (pattern.EndsWith(".*", StringComparison.Ordinal))
				{
					return new AnnotationFilterPattern.StartsWithPattern(pattern.Substring(0, pattern.Length - 1), flag);
				}
				return new AnnotationFilterPattern.ExactMatchPattern(pattern, flag);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000221E File Offset: 0x0000041E
		internal static void Sort(AnnotationFilterPattern[] pattersToSort)
		{
			Array.Sort<AnnotationFilterPattern>(pattersToSort);
		}

		// Token: 0x0600000B RID: 11
		internal abstract bool Matches(string annotationName);

		// Token: 0x0600000C RID: 12 RVA: 0x00002228 File Offset: 0x00000428
		private static int ComparePatternPriority(string pattern1, string pattern2)
		{
			if (pattern1 == pattern2)
			{
				return 0;
			}
			if (pattern1 == "*")
			{
				return 1;
			}
			if (pattern2 == "*")
			{
				return -1;
			}
			if (pattern1.StartsWith(pattern2, StringComparison.Ordinal))
			{
				return -1;
			}
			if (pattern2.StartsWith(pattern1, StringComparison.Ordinal))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002277 File Offset: 0x00000477
		private static bool RemoveExcludeOperator(ref string pattern)
		{
			if (pattern[0] == '-')
			{
				pattern = pattern.Substring(1);
				return true;
			}
			return false;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002294 File Offset: 0x00000494
		private static void ValidatePattern(string pattern)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(pattern, "pattern");
			string text = pattern;
			AnnotationFilterPattern.RemoveExcludeOperator(ref text);
			if (text == "*")
			{
				return;
			}
			string[] array = text.Split(new char[] { '.' });
			int num = array.Length;
			if (num == 1)
			{
				throw new ArgumentException(Strings.AnnotationFilterPattern_InvalidPatternMissingDot(pattern));
			}
			for (int i = 0; i < num; i++)
			{
				string text2 = array[i];
				if (string.IsNullOrEmpty(text2))
				{
					throw new ArgumentException(Strings.AnnotationFilterPattern_InvalidPatternEmptySegment(pattern));
				}
				if (text2 != "*" && text2.Contains("*"))
				{
					throw new ArgumentException(Strings.AnnotationFilterPattern_InvalidPatternWildCardInSegment(pattern));
				}
				bool flag = i + 1 == num;
				if (text2 == "*" && !flag)
				{
					throw new ArgumentException(Strings.AnnotationFilterPattern_InvalidPatternWildCardMustBeInLastSegment(pattern));
				}
			}
		}

		// Token: 0x04000008 RID: 8
		internal static readonly AnnotationFilterPattern IncludeAllPattern = new AnnotationFilterPattern.WildCardPattern(false);

		// Token: 0x04000009 RID: 9
		internal static readonly AnnotationFilterPattern ExcludeAllPattern = new AnnotationFilterPattern.WildCardPattern(true);

		// Token: 0x0400000A RID: 10
		protected readonly string Pattern;

		// Token: 0x0400000B RID: 11
		private const char NamespaceSeparator = '.';

		// Token: 0x0400000C RID: 12
		private const char ExcludeOperator = '-';

		// Token: 0x0400000D RID: 13
		private const string WildCard = "*";

		// Token: 0x0400000E RID: 14
		private const string DotStar = ".*";

		// Token: 0x0400000F RID: 15
		private readonly bool isExclude;

		// Token: 0x0200026F RID: 623
		private sealed class WildCardPattern : AnnotationFilterPattern
		{
			// Token: 0x06001BED RID: 7149 RVA: 0x00055B5F File Offset: 0x00053D5F
			internal WildCardPattern(bool isExclude)
				: base("*", isExclude)
			{
			}

			// Token: 0x06001BEE RID: 7150 RVA: 0x00002393 File Offset: 0x00000593
			internal override bool Matches(string annotationName)
			{
				return true;
			}
		}

		// Token: 0x02000270 RID: 624
		private sealed class StartsWithPattern : AnnotationFilterPattern
		{
			// Token: 0x06001BEF RID: 7151 RVA: 0x00055B6D File Offset: 0x00053D6D
			internal StartsWithPattern(string pattern, bool isExclude)
				: base(pattern, isExclude)
			{
			}

			// Token: 0x06001BF0 RID: 7152 RVA: 0x00055B77 File Offset: 0x00053D77
			internal override bool Matches(string annotationName)
			{
				return annotationName.StartsWith(this.Pattern, StringComparison.Ordinal);
			}
		}

		// Token: 0x02000271 RID: 625
		private sealed class ExactMatchPattern : AnnotationFilterPattern
		{
			// Token: 0x06001BF1 RID: 7153 RVA: 0x00055B6D File Offset: 0x00053D6D
			internal ExactMatchPattern(string pattern, bool isExclude)
				: base(pattern, isExclude)
			{
			}

			// Token: 0x06001BF2 RID: 7154 RVA: 0x00055B86 File Offset: 0x00053D86
			internal override bool Matches(string annotationName)
			{
				return annotationName.Equals(this.Pattern, StringComparison.Ordinal);
			}
		}
	}
}
