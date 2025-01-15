using System;

namespace Microsoft.OData
{
	// Token: 0x02000006 RID: 6
	internal abstract class AnnotationFilterPattern : IComparable<AnnotationFilterPattern>
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000022BF File Offset: 0x000004BF
		private AnnotationFilterPattern(string pattern, bool isExclude)
		{
			this.isExclude = isExclude;
			this.Pattern = pattern;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022D5 File Offset: 0x000004D5
		internal virtual bool IsExclude
		{
			get
			{
				return this.isExclude;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022E0 File Offset: 0x000004E0
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

		// Token: 0x06000014 RID: 20 RVA: 0x0000232C File Offset: 0x0000052C
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

		// Token: 0x06000015 RID: 21 RVA: 0x0000238E File Offset: 0x0000058E
		internal static void Sort(AnnotationFilterPattern[] pattersToSort)
		{
			Array.Sort<AnnotationFilterPattern>(pattersToSort);
		}

		// Token: 0x06000016 RID: 22
		internal abstract bool Matches(string annotationName);

		// Token: 0x06000017 RID: 23 RVA: 0x00002398 File Offset: 0x00000598
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

		// Token: 0x06000018 RID: 24 RVA: 0x000023E7 File Offset: 0x000005E7
		private static bool RemoveExcludeOperator(ref string pattern)
		{
			if (pattern.get_Chars(0) == '-')
			{
				pattern = pattern.Substring(1);
				return true;
			}
			return false;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002404 File Offset: 0x00000604
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

		// Token: 0x0400000C RID: 12
		internal static readonly AnnotationFilterPattern IncludeAllPattern = new AnnotationFilterPattern.WildCardPattern(false);

		// Token: 0x0400000D RID: 13
		internal static readonly AnnotationFilterPattern ExcludeAllPattern = new AnnotationFilterPattern.WildCardPattern(true);

		// Token: 0x0400000E RID: 14
		protected readonly string Pattern;

		// Token: 0x0400000F RID: 15
		private const char NamespaceSeparator = '.';

		// Token: 0x04000010 RID: 16
		private const char ExcludeOperator = '-';

		// Token: 0x04000011 RID: 17
		private const string WildCard = "*";

		// Token: 0x04000012 RID: 18
		private const string DotStar = ".*";

		// Token: 0x04000013 RID: 19
		private readonly bool isExclude;

		// Token: 0x02000233 RID: 563
		private sealed class WildCardPattern : AnnotationFilterPattern
		{
			// Token: 0x060016E0 RID: 5856 RVA: 0x00046597 File Offset: 0x00044797
			internal WildCardPattern(bool isExclude)
				: base("*", isExclude)
			{
			}

			// Token: 0x060016E1 RID: 5857 RVA: 0x00002503 File Offset: 0x00000703
			internal override bool Matches(string annotationName)
			{
				return true;
			}
		}

		// Token: 0x02000234 RID: 564
		private sealed class StartsWithPattern : AnnotationFilterPattern
		{
			// Token: 0x060016E2 RID: 5858 RVA: 0x000465A5 File Offset: 0x000447A5
			internal StartsWithPattern(string pattern, bool isExclude)
				: base(pattern, isExclude)
			{
			}

			// Token: 0x060016E3 RID: 5859 RVA: 0x000465AF File Offset: 0x000447AF
			internal override bool Matches(string annotationName)
			{
				return annotationName.StartsWith(this.Pattern, 4);
			}
		}

		// Token: 0x02000235 RID: 565
		private sealed class ExactMatchPattern : AnnotationFilterPattern
		{
			// Token: 0x060016E4 RID: 5860 RVA: 0x000465A5 File Offset: 0x000447A5
			internal ExactMatchPattern(string pattern, bool isExclude)
				: base(pattern, isExclude)
			{
			}

			// Token: 0x060016E5 RID: 5861 RVA: 0x000465BE File Offset: 0x000447BE
			internal override bool Matches(string annotationName)
			{
				return annotationName.Equals(this.Pattern, 4);
			}
		}
	}
}
