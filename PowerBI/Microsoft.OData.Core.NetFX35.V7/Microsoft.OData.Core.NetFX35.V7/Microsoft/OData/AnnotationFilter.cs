using System;
using System.Linq;

namespace Microsoft.OData
{
	// Token: 0x02000005 RID: 5
	internal class AnnotationFilter
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000021C0 File Offset: 0x000003C0
		private AnnotationFilter(AnnotationFilterPattern[] prioritizedPatternsToMatch)
		{
			this.prioritizedPatternsToMatch = prioritizedPatternsToMatch;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021D0 File Offset: 0x000003D0
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

		// Token: 0x0600000E RID: 14 RVA: 0x0000224C File Offset: 0x0000044C
		internal static AnnotationFilter CreateInclueAllFilter()
		{
			return new AnnotationFilter.IncludeAllFilter();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002254 File Offset: 0x00000454
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

		// Token: 0x04000008 RID: 8
		private static readonly AnnotationFilter IncludeAll = new AnnotationFilter.IncludeAllFilter();

		// Token: 0x04000009 RID: 9
		private static readonly AnnotationFilter ExcludeAll = new AnnotationFilter.ExcludeAllFilter();

		// Token: 0x0400000A RID: 10
		private static readonly char[] AnnotationFilterPatternSeparator = new char[] { ',' };

		// Token: 0x0400000B RID: 11
		private readonly AnnotationFilterPattern[] prioritizedPatternsToMatch;

		// Token: 0x02000230 RID: 560
		private sealed class IncludeAllFilter : AnnotationFilter
		{
			// Token: 0x060016D9 RID: 5849 RVA: 0x00046554 File Offset: 0x00044754
			internal IncludeAllFilter()
				: base(new AnnotationFilterPattern[0])
			{
			}

			// Token: 0x060016DA RID: 5850 RVA: 0x00046562 File Offset: 0x00044762
			internal override bool Matches(string annotationName)
			{
				ExceptionUtils.CheckArgumentStringNotNullOrEmpty(annotationName, "annotationName");
				return true;
			}
		}

		// Token: 0x02000231 RID: 561
		private sealed class ExcludeAllFilter : AnnotationFilter
		{
			// Token: 0x060016DB RID: 5851 RVA: 0x00046554 File Offset: 0x00044754
			internal ExcludeAllFilter()
				: base(new AnnotationFilterPattern[0])
			{
			}

			// Token: 0x060016DC RID: 5852 RVA: 0x00046570 File Offset: 0x00044770
			internal override bool Matches(string annotationName)
			{
				ExceptionUtils.CheckArgumentStringNotNullOrEmpty(annotationName, "annotationName");
				return false;
			}
		}
	}
}
