using System;
using System.Linq;

namespace Microsoft.Data.OData
{
	// Token: 0x0200012A RID: 298
	internal class AnnotationFilter
	{
		// Token: 0x060007C3 RID: 1987 RVA: 0x00019C23 File Offset: 0x00017E23
		private AnnotationFilter(AnnotationFilterPattern[] prioritizedPatternsToMatch)
		{
			this.prioritizedPatternsToMatch = prioritizedPatternsToMatch;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00019C40 File Offset: 0x00017E40
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

		// Token: 0x060007C5 RID: 1989 RVA: 0x00019CBC File Offset: 0x00017EBC
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

		// Token: 0x040002FF RID: 767
		private static readonly AnnotationFilter IncludeAll = new AnnotationFilter.IncludeAllFilter();

		// Token: 0x04000300 RID: 768
		private static readonly AnnotationFilter ExcludeAll = new AnnotationFilter.ExcludeAllFilter();

		// Token: 0x04000301 RID: 769
		private static readonly char[] AnnotationFilterPatternSeparator = new char[] { ',' };

		// Token: 0x04000302 RID: 770
		private readonly AnnotationFilterPattern[] prioritizedPatternsToMatch;

		// Token: 0x0200012B RID: 299
		private sealed class IncludeAllFilter : AnnotationFilter
		{
			// Token: 0x060007C8 RID: 1992 RVA: 0x00019D3B File Offset: 0x00017F3B
			internal IncludeAllFilter()
				: base(new AnnotationFilterPattern[0])
			{
			}

			// Token: 0x060007C9 RID: 1993 RVA: 0x00019D49 File Offset: 0x00017F49
			internal override bool Matches(string annotationName)
			{
				ExceptionUtils.CheckArgumentStringNotNullOrEmpty(annotationName, "annotationName");
				return true;
			}
		}

		// Token: 0x0200012C RID: 300
		private sealed class ExcludeAllFilter : AnnotationFilter
		{
			// Token: 0x060007CA RID: 1994 RVA: 0x00019D57 File Offset: 0x00017F57
			internal ExcludeAllFilter()
				: base(new AnnotationFilterPattern[0])
			{
			}

			// Token: 0x060007CB RID: 1995 RVA: 0x00019D65 File Offset: 0x00017F65
			internal override bool Matches(string annotationName)
			{
				ExceptionUtils.CheckArgumentStringNotNullOrEmpty(annotationName, "annotationName");
				return false;
			}
		}
	}
}
