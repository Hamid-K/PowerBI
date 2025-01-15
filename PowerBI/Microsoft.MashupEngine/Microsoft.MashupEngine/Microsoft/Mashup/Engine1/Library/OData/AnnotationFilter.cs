using System;
using System.Linq;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000718 RID: 1816
	internal class AnnotationFilter
	{
		// Token: 0x06003632 RID: 13874 RVA: 0x000ACE6C File Offset: 0x000AB06C
		public static bool Matches(string incluedAnnotationsPattern, string annotation)
		{
			bool flag;
			try
			{
				flag = AnnotationFilter.Create(incluedAnnotationsPattern).Matches(annotation);
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewDataFormatError(ex.Message, ListValue.New(new Value[]
				{
					TextValue.New(incluedAnnotationsPattern),
					TextValue.New(annotation)
				}), ex);
			}
			return flag;
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x000ACEC4 File Offset: 0x000AB0C4
		private AnnotationFilter(AnnotationFilter.AnnotationFilterPattern[] prioritizedPatternsToMatch)
		{
			this.prioritizedPatternsToMatch = prioritizedPatternsToMatch;
		}

		// Token: 0x06003634 RID: 13876 RVA: 0x000ACED4 File Offset: 0x000AB0D4
		internal static AnnotationFilter Create(string filter)
		{
			if (string.IsNullOrEmpty(filter))
			{
				return AnnotationFilter.ExcludeAll;
			}
			AnnotationFilter.AnnotationFilterPattern[] array = (from pattern in filter.Split(AnnotationFilter.AnnotationFilterPatternSeparator)
				select AnnotationFilter.AnnotationFilterPattern.Create(pattern.Trim())).ToArray<AnnotationFilter.AnnotationFilterPattern>();
			AnnotationFilter.AnnotationFilterPattern.Sort(array);
			if (array[0] == AnnotationFilter.AnnotationFilterPattern.IncludeAllPattern)
			{
				return AnnotationFilter.IncludeAll;
			}
			if (array[0] == AnnotationFilter.AnnotationFilterPattern.ExcludeAllPattern)
			{
				return AnnotationFilter.ExcludeAll;
			}
			return new AnnotationFilter(array);
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x000ACF50 File Offset: 0x000AB150
		internal static AnnotationFilter CreateInclueAllFilter()
		{
			return new AnnotationFilter.IncludeAllFilter();
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x000ACF58 File Offset: 0x000AB158
		internal virtual bool Matches(string annotationName)
		{
			foreach (AnnotationFilter.AnnotationFilterPattern annotationFilterPattern in this.prioritizedPatternsToMatch)
			{
				if (annotationFilterPattern.Matches(annotationName))
				{
					return !annotationFilterPattern.IsExclude;
				}
			}
			return false;
		}

		// Token: 0x04001BD8 RID: 7128
		private static readonly AnnotationFilter IncludeAll = new AnnotationFilter.IncludeAllFilter();

		// Token: 0x04001BD9 RID: 7129
		private static readonly AnnotationFilter ExcludeAll = new AnnotationFilter.ExcludeAllFilter();

		// Token: 0x04001BDA RID: 7130
		private static readonly char[] AnnotationFilterPatternSeparator = new char[] { ',' };

		// Token: 0x04001BDB RID: 7131
		private readonly AnnotationFilter.AnnotationFilterPattern[] prioritizedPatternsToMatch;

		// Token: 0x02000719 RID: 1817
		private sealed class IncludeAllFilter : AnnotationFilter
		{
			// Token: 0x06003638 RID: 13880 RVA: 0x000ACFB8 File Offset: 0x000AB1B8
			internal IncludeAllFilter()
				: base(new AnnotationFilter.AnnotationFilterPattern[0])
			{
			}

			// Token: 0x06003639 RID: 13881 RVA: 0x00002139 File Offset: 0x00000339
			internal override bool Matches(string annotationName)
			{
				return true;
			}
		}

		// Token: 0x0200071A RID: 1818
		private sealed class ExcludeAllFilter : AnnotationFilter
		{
			// Token: 0x0600363A RID: 13882 RVA: 0x000ACFB8 File Offset: 0x000AB1B8
			internal ExcludeAllFilter()
				: base(new AnnotationFilter.AnnotationFilterPattern[0])
			{
			}

			// Token: 0x0600363B RID: 13883 RVA: 0x00002105 File Offset: 0x00000305
			internal override bool Matches(string annotationName)
			{
				return false;
			}
		}

		// Token: 0x0200071B RID: 1819
		private abstract class AnnotationFilterPattern : IComparable<AnnotationFilter.AnnotationFilterPattern>
		{
			// Token: 0x0600363C RID: 13884 RVA: 0x000ACFC6 File Offset: 0x000AB1C6
			private AnnotationFilterPattern(string pattern, bool isExclude)
			{
				this.isExclude = isExclude;
				this.Pattern = pattern;
			}

			// Token: 0x170012BB RID: 4795
			// (get) Token: 0x0600363D RID: 13885 RVA: 0x000ACFDC File Offset: 0x000AB1DC
			internal virtual bool IsExclude
			{
				get
				{
					return this.isExclude;
				}
			}

			// Token: 0x0600363E RID: 13886 RVA: 0x000ACFE4 File Offset: 0x000AB1E4
			public int CompareTo(AnnotationFilter.AnnotationFilterPattern other)
			{
				int num = AnnotationFilter.AnnotationFilterPattern.ComparePatternPriority(this.Pattern, other.Pattern);
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

			// Token: 0x0600363F RID: 13887 RVA: 0x000AD024 File Offset: 0x000AB224
			internal static AnnotationFilter.AnnotationFilterPattern Create(string pattern)
			{
				AnnotationFilter.AnnotationFilterPattern.ValidatePattern(pattern);
				bool flag = AnnotationFilter.AnnotationFilterPattern.RemoveExcludeOperator(ref pattern);
				if (pattern == "*")
				{
					if (!flag)
					{
						return AnnotationFilter.AnnotationFilterPattern.IncludeAllPattern;
					}
					return AnnotationFilter.AnnotationFilterPattern.ExcludeAllPattern;
				}
				else
				{
					if (pattern.EndsWith(".*", StringComparison.Ordinal))
					{
						return new AnnotationFilter.AnnotationFilterPattern.StartsWithPattern(pattern.Substring(0, pattern.Length - 1), flag);
					}
					return new AnnotationFilter.AnnotationFilterPattern.ExactMatchPattern(pattern, flag);
				}
			}

			// Token: 0x06003640 RID: 13888 RVA: 0x000AD086 File Offset: 0x000AB286
			internal static void Sort(AnnotationFilter.AnnotationFilterPattern[] pattersToSort)
			{
				Array.Sort<AnnotationFilter.AnnotationFilterPattern>(pattersToSort);
			}

			// Token: 0x06003641 RID: 13889
			internal abstract bool Matches(string annotationName);

			// Token: 0x06003642 RID: 13890 RVA: 0x000AD090 File Offset: 0x000AB290
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

			// Token: 0x06003643 RID: 13891 RVA: 0x000AD0DF File Offset: 0x000AB2DF
			private static bool RemoveExcludeOperator(ref string pattern)
			{
				if (pattern[0] == '-')
				{
					pattern = pattern.Substring(1);
					return true;
				}
				return false;
			}

			// Token: 0x06003644 RID: 13892 RVA: 0x000AD0FC File Offset: 0x000AB2FC
			private static void ValidatePattern(string pattern)
			{
				string text = pattern;
				AnnotationFilter.AnnotationFilterPattern.RemoveExcludeOperator(ref text);
				if (text == "*")
				{
					return;
				}
				string[] array = text.Split(new char[] { '.' });
				int num = array.Length;
				if (num == 1)
				{
					throw new ArgumentException(Strings.ODataAnnotationInvalidPatternMissingDot(pattern));
				}
				for (int i = 0; i < num; i++)
				{
					string text2 = array[i];
					if (string.IsNullOrEmpty(text2))
					{
						throw new ArgumentException(Strings.ODataAnnotationInvalidPatternEmptySegment(pattern));
					}
					if (text2 != "*" && text2.Contains("*"))
					{
						throw new ArgumentException(Strings.ODataAnnotationInvalidPatternWildCardInSegment(pattern));
					}
					bool flag = i + 1 == num;
					if (text2 == "*" && !flag)
					{
						throw new ArgumentException(Strings.ODataAnnotationInvalidPatternWildCardMustBeInLastSegment(pattern));
					}
				}
			}

			// Token: 0x04001BDC RID: 7132
			internal static readonly AnnotationFilter.AnnotationFilterPattern IncludeAllPattern = new AnnotationFilter.AnnotationFilterPattern.WildCardPattern(false);

			// Token: 0x04001BDD RID: 7133
			internal static readonly AnnotationFilter.AnnotationFilterPattern ExcludeAllPattern = new AnnotationFilter.AnnotationFilterPattern.WildCardPattern(true);

			// Token: 0x04001BDE RID: 7134
			protected readonly string Pattern;

			// Token: 0x04001BDF RID: 7135
			private const char NamespaceSeparator = '.';

			// Token: 0x04001BE0 RID: 7136
			private const char ExcludeOperator = '-';

			// Token: 0x04001BE1 RID: 7137
			private const string WildCard = "*";

			// Token: 0x04001BE2 RID: 7138
			private const string DotStar = ".*";

			// Token: 0x04001BE3 RID: 7139
			private readonly bool isExclude;

			// Token: 0x0200071C RID: 1820
			private sealed class WildCardPattern : AnnotationFilter.AnnotationFilterPattern
			{
				// Token: 0x06003646 RID: 13894 RVA: 0x000AD1E7 File Offset: 0x000AB3E7
				internal WildCardPattern(bool isExclude)
					: base("*", isExclude)
				{
				}

				// Token: 0x06003647 RID: 13895 RVA: 0x00002139 File Offset: 0x00000339
				internal override bool Matches(string annotationName)
				{
					return true;
				}
			}

			// Token: 0x0200071D RID: 1821
			private sealed class StartsWithPattern : AnnotationFilter.AnnotationFilterPattern
			{
				// Token: 0x06003648 RID: 13896 RVA: 0x000AD1F5 File Offset: 0x000AB3F5
				internal StartsWithPattern(string pattern, bool isExclude)
					: base(pattern, isExclude)
				{
				}

				// Token: 0x06003649 RID: 13897 RVA: 0x000AD1FF File Offset: 0x000AB3FF
				internal override bool Matches(string annotationName)
				{
					return annotationName.StartsWith(this.Pattern, StringComparison.Ordinal);
				}
			}

			// Token: 0x0200071E RID: 1822
			private sealed class ExactMatchPattern : AnnotationFilter.AnnotationFilterPattern
			{
				// Token: 0x0600364A RID: 13898 RVA: 0x000AD1F5 File Offset: 0x000AB3F5
				internal ExactMatchPattern(string pattern, bool isExclude)
					: base(pattern, isExclude)
				{
				}

				// Token: 0x0600364B RID: 13899 RVA: 0x000AD20E File Offset: 0x000AB40E
				internal override bool Matches(string annotationName)
				{
					return annotationName.Equals(this.Pattern, StringComparison.Ordinal);
				}
			}
		}
	}
}
