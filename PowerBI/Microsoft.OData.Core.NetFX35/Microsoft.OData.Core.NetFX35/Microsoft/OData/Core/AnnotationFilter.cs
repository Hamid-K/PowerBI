using System;
using System.Linq;

namespace Microsoft.OData.Core
{
	// Token: 0x02000002 RID: 2
	internal class AnnotationFilter
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		private AnnotationFilter(AnnotationFilterPattern[] prioritizedPatternsToMatch)
		{
			this.prioritizedPatternsToMatch = prioritizedPatternsToMatch;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020EC File Offset: 0x000002EC
		internal static AnnotationFilter Create(string filter)
		{
			if (string.IsNullOrEmpty(filter))
			{
				return AnnotationFilter.ExcludeAll;
			}
			AnnotationFilterPattern[] array = Enumerable.ToArray<AnnotationFilterPattern>(Enumerable.Select<string, AnnotationFilterPattern>(filter.Split(AnnotationFilter.AnnotationFilterPatternSeparator), (string pattern) => AnnotationFilterPattern.Create(pattern.Trim())));
			AnnotationFilterPattern.Sort(array);
			if (array[0] == AnnotationFilterPattern.IncludeAllPattern)
			{
				return AnnotationFilter.IncludeAll;
			}
			if (array[0] == AnnotationFilterPattern.ExcludeAllPattern)
			{
				return AnnotationFilter.ExcludeAll;
			}
			return new AnnotationFilter(array);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002166 File Offset: 0x00000366
		internal static AnnotationFilter CreateInclueAllFilter()
		{
			return new AnnotationFilter.IncludeAllFilter();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002170 File Offset: 0x00000370
		internal virtual bool Matches(string annotationName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(annotationName, "annotationName");
			foreach (AnnotationFilterPattern annotationFilterPattern in this.prioritizedPatternsToMatch)
			{
				if (annotationFilterPattern.Matches(annotationName))
				{
					return !annotationFilterPattern.IsExclude;
				}
			}
			return false;
		}

		// Token: 0x04000001 RID: 1
		private static readonly AnnotationFilter IncludeAll = new AnnotationFilter.IncludeAllFilter();

		// Token: 0x04000002 RID: 2
		private static readonly AnnotationFilter ExcludeAll = new AnnotationFilter.ExcludeAllFilter();

		// Token: 0x04000003 RID: 3
		private static readonly char[] AnnotationFilterPatternSeparator = new char[] { ',' };

		// Token: 0x04000004 RID: 4
		private readonly AnnotationFilterPattern[] prioritizedPatternsToMatch;

		// Token: 0x02000003 RID: 3
		private sealed class IncludeAllFilter : AnnotationFilter
		{
			// Token: 0x06000007 RID: 7 RVA: 0x000021EF File Offset: 0x000003EF
			internal IncludeAllFilter()
				: base(new AnnotationFilterPattern[0])
			{
			}

			// Token: 0x06000008 RID: 8 RVA: 0x000021FD File Offset: 0x000003FD
			internal override bool Matches(string annotationName)
			{
				ExceptionUtils.CheckArgumentStringNotNullOrEmpty(annotationName, "annotationName");
				return true;
			}
		}

		// Token: 0x02000004 RID: 4
		private sealed class ExcludeAllFilter : AnnotationFilter
		{
			// Token: 0x06000009 RID: 9 RVA: 0x0000220B File Offset: 0x0000040B
			internal ExcludeAllFilter()
				: base(new AnnotationFilterPattern[0])
			{
			}

			// Token: 0x0600000A RID: 10 RVA: 0x00002219 File Offset: 0x00000419
			internal override bool Matches(string annotationName)
			{
				ExceptionUtils.CheckArgumentStringNotNullOrEmpty(annotationName, "annotationName");
				return false;
			}
		}
	}
}
