using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200012D RID: 301
	internal abstract class AnnotationFilterPattern : IComparable<AnnotationFilterPattern>
	{
		// Token: 0x060007CC RID: 1996 RVA: 0x00019D73 File Offset: 0x00017F73
		private AnnotationFilterPattern(string pattern, bool isExclude)
		{
			this.isExclude = isExclude;
			this.Pattern = pattern;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00019D89 File Offset: 0x00017F89
		internal virtual bool IsExclude
		{
			get
			{
				return this.isExclude;
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00019D94 File Offset: 0x00017F94
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

		// Token: 0x060007CF RID: 1999 RVA: 0x00019DE0 File Offset: 0x00017FE0
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

		// Token: 0x060007D0 RID: 2000 RVA: 0x00019E42 File Offset: 0x00018042
		internal static void Sort(AnnotationFilterPattern[] pattersToSort)
		{
			Array.Sort<AnnotationFilterPattern>(pattersToSort);
		}

		// Token: 0x060007D1 RID: 2001
		internal abstract bool Matches(string annotationName);

		// Token: 0x060007D2 RID: 2002 RVA: 0x00019E4C File Offset: 0x0001804C
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

		// Token: 0x060007D3 RID: 2003 RVA: 0x00019E9B File Offset: 0x0001809B
		private static bool RemoveExcludeOperator(ref string pattern)
		{
			if (pattern.get_Chars(0) == '-')
			{
				pattern = pattern.Substring(1);
				return true;
			}
			return false;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00019EB8 File Offset: 0x000180B8
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

		// Token: 0x04000304 RID: 772
		private const char NamespaceSeparator = '.';

		// Token: 0x04000305 RID: 773
		private const char ExcludeOperator = '-';

		// Token: 0x04000306 RID: 774
		private const string WildCard = "*";

		// Token: 0x04000307 RID: 775
		private const string DotStar = ".*";

		// Token: 0x04000308 RID: 776
		internal static readonly AnnotationFilterPattern IncludeAllPattern = new AnnotationFilterPattern.WildCardPattern(false);

		// Token: 0x04000309 RID: 777
		internal static readonly AnnotationFilterPattern ExcludeAllPattern = new AnnotationFilterPattern.WildCardPattern(true);

		// Token: 0x0400030A RID: 778
		protected readonly string Pattern;

		// Token: 0x0400030B RID: 779
		private readonly bool isExclude;

		// Token: 0x0200012E RID: 302
		private sealed class WildCardPattern : AnnotationFilterPattern
		{
			// Token: 0x060007D6 RID: 2006 RVA: 0x00019F9F File Offset: 0x0001819F
			internal WildCardPattern(bool isExclude)
				: base("*", isExclude)
			{
			}

			// Token: 0x060007D7 RID: 2007 RVA: 0x00019FAD File Offset: 0x000181AD
			internal override bool Matches(string annotationName)
			{
				return true;
			}
		}

		// Token: 0x0200012F RID: 303
		private sealed class StartsWithPattern : AnnotationFilterPattern
		{
			// Token: 0x060007D8 RID: 2008 RVA: 0x00019FB0 File Offset: 0x000181B0
			internal StartsWithPattern(string pattern, bool isExclude)
				: base(pattern, isExclude)
			{
			}

			// Token: 0x060007D9 RID: 2009 RVA: 0x00019FBA File Offset: 0x000181BA
			internal override bool Matches(string annotationName)
			{
				return annotationName.StartsWith(this.Pattern, 4);
			}
		}

		// Token: 0x02000130 RID: 304
		private sealed class ExactMatchPattern : AnnotationFilterPattern
		{
			// Token: 0x060007DA RID: 2010 RVA: 0x00019FC9 File Offset: 0x000181C9
			internal ExactMatchPattern(string pattern, bool isExclude)
				: base(pattern, isExclude)
			{
			}

			// Token: 0x060007DB RID: 2011 RVA: 0x00019FD3 File Offset: 0x000181D3
			internal override bool Matches(string annotationName)
			{
				return annotationName.Equals(this.Pattern, 4);
			}
		}
	}
}
