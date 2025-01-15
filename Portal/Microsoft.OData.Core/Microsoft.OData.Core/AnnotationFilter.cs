using System;
using System.Linq;

namespace Microsoft.OData
{
	// Token: 0x02000003 RID: 3
	internal class AnnotationFilter
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private AnnotationFilter(AnnotationFilterPattern[] prioritizedPatternsToMatch)
		{
			this.prioritizedPatternsToMatch = prioritizedPatternsToMatch;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002060 File Offset: 0x00000260
		internal static AnnotationFilter Create(string filter)
		{
			if (string.IsNullOrEmpty(filter))
			{
				return AnnotationFilter.ExcludeAll;
			}
			AnnotationFilterPattern[] array = (from pattern in filter.Split(AnnotationFilter.AnnotationFilterPatternSeparator)
				select AnnotationFilterPattern.Create(pattern.Trim())).ToArray<AnnotationFilterPattern>();
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

		// Token: 0x06000003 RID: 3 RVA: 0x000020DC File Offset: 0x000002DC
		internal static AnnotationFilter CreateInclueAllFilter()
		{
			return new AnnotationFilter.IncludeAllFilter();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E4 File Offset: 0x000002E4
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

		// Token: 0x04000004 RID: 4
		private static readonly AnnotationFilter IncludeAll = new AnnotationFilter.IncludeAllFilter();

		// Token: 0x04000005 RID: 5
		private static readonly AnnotationFilter ExcludeAll = new AnnotationFilter.ExcludeAllFilter();

		// Token: 0x04000006 RID: 6
		private static readonly char[] AnnotationFilterPatternSeparator = new char[] { ',' };

		// Token: 0x04000007 RID: 7
		private readonly AnnotationFilterPattern[] prioritizedPatternsToMatch;

		// Token: 0x0200026C RID: 620
		private sealed class IncludeAllFilter : AnnotationFilter
		{
			// Token: 0x06001BE6 RID: 7142 RVA: 0x00055B1C File Offset: 0x00053D1C
			internal IncludeAllFilter()
				: base(new AnnotationFilterPattern[0])
			{
			}

			// Token: 0x06001BE7 RID: 7143 RVA: 0x00055B2A File Offset: 0x00053D2A
			internal override bool Matches(string annotationName)
			{
				ExceptionUtils.CheckArgumentStringNotNullOrEmpty(annotationName, "annotationName");
				return true;
			}
		}

		// Token: 0x0200026D RID: 621
		private sealed class ExcludeAllFilter : AnnotationFilter
		{
			// Token: 0x06001BE8 RID: 7144 RVA: 0x00055B1C File Offset: 0x00053D1C
			internal ExcludeAllFilter()
				: base(new AnnotationFilterPattern[0])
			{
			}

			// Token: 0x06001BE9 RID: 7145 RVA: 0x00055B38 File Offset: 0x00053D38
			internal override bool Matches(string annotationName)
			{
				ExceptionUtils.CheckArgumentStringNotNullOrEmpty(annotationName, "annotationName");
				return false;
			}
		}
	}
}
