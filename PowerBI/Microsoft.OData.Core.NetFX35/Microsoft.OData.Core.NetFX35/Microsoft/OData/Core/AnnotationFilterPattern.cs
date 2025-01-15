using System;

namespace Microsoft.OData.Core
{
	// Token: 0x02000005 RID: 5
	internal abstract class AnnotationFilterPattern : IComparable<AnnotationFilterPattern>
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002227 File Offset: 0x00000427
		private AnnotationFilterPattern(string pattern, bool isExclude)
		{
			this.isExclude = isExclude;
			this.Pattern = pattern;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000223D File Offset: 0x0000043D
		internal virtual bool IsExclude
		{
			get
			{
				return this.isExclude;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002248 File Offset: 0x00000448
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

		// Token: 0x0600000E RID: 14 RVA: 0x00002294 File Offset: 0x00000494
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
				if (pattern.EndsWith(".*", 4))
				{
					return new AnnotationFilterPattern.StartsWithPattern(pattern.Substring(0, pattern.Length - 1), flag);
				}
				return new AnnotationFilterPattern.ExactMatchPattern(pattern, flag);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022F6 File Offset: 0x000004F6
		internal static void Sort(AnnotationFilterPattern[] pattersToSort)
		{
			Array.Sort<AnnotationFilterPattern>(pattersToSort);
		}

		// Token: 0x06000010 RID: 16
		internal abstract bool Matches(string annotationName);

		// Token: 0x06000011 RID: 17 RVA: 0x00002300 File Offset: 0x00000500
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
			if (pattern1.StartsWith(pattern2, 4))
			{
				return -1;
			}
			if (pattern2.StartsWith(pattern1, 4))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000234F File Offset: 0x0000054F
		private static bool RemoveExcludeOperator(ref string pattern)
		{
			if (pattern.get_Chars(0) == '-')
			{
				pattern = pattern.Substring(1);
				return true;
			}
			return false;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000236C File Offset: 0x0000056C
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

		// Token: 0x04000006 RID: 6
		private const char NamespaceSeparator = '.';

		// Token: 0x04000007 RID: 7
		private const char ExcludeOperator = '-';

		// Token: 0x04000008 RID: 8
		private const string WildCard = "*";

		// Token: 0x04000009 RID: 9
		private const string DotStar = ".*";

		// Token: 0x0400000A RID: 10
		internal static readonly AnnotationFilterPattern IncludeAllPattern = new AnnotationFilterPattern.WildCardPattern(false);

		// Token: 0x0400000B RID: 11
		internal static readonly AnnotationFilterPattern ExcludeAllPattern = new AnnotationFilterPattern.WildCardPattern(true);

		// Token: 0x0400000C RID: 12
		protected readonly string Pattern;

		// Token: 0x0400000D RID: 13
		private readonly bool isExclude;

		// Token: 0x02000006 RID: 6
		private sealed class WildCardPattern : AnnotationFilterPattern
		{
			// Token: 0x06000015 RID: 21 RVA: 0x00002453 File Offset: 0x00000653
			internal WildCardPattern(bool isExclude)
				: base("*", isExclude)
			{
			}

			// Token: 0x06000016 RID: 22 RVA: 0x00002461 File Offset: 0x00000661
			internal override bool Matches(string annotationName)
			{
				return true;
			}
		}

		// Token: 0x02000007 RID: 7
		private sealed class StartsWithPattern : AnnotationFilterPattern
		{
			// Token: 0x06000017 RID: 23 RVA: 0x00002464 File Offset: 0x00000664
			internal StartsWithPattern(string pattern, bool isExclude)
				: base(pattern, isExclude)
			{
			}

			// Token: 0x06000018 RID: 24 RVA: 0x0000246E File Offset: 0x0000066E
			internal override bool Matches(string annotationName)
			{
				return annotationName.StartsWith(this.Pattern, 4);
			}
		}

		// Token: 0x02000008 RID: 8
		private sealed class ExactMatchPattern : AnnotationFilterPattern
		{
			// Token: 0x06000019 RID: 25 RVA: 0x0000247D File Offset: 0x0000067D
			internal ExactMatchPattern(string pattern, bool isExclude)
				: base(pattern, isExclude)
			{
			}

			// Token: 0x0600001A RID: 26 RVA: 0x00002487 File Offset: 0x00000687
			internal override bool Matches(string annotationName)
			{
				return annotationName.Equals(this.Pattern, 4);
			}
		}
	}
}
