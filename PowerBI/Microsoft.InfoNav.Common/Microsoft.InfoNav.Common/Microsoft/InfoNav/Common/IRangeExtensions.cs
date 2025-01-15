using System;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200005A RID: 90
	internal static class IRangeExtensions
	{
		// Token: 0x0600038D RID: 909 RVA: 0x00009AF8 File Offset: 0x00007CF8
		internal static bool Overlaps(this IRange x, IRange y)
		{
			return x.Overlaps(y.FirstIndex, y.LastIndex);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00009B0C File Offset: 0x00007D0C
		internal static bool Overlaps(this IRange x, int firstIndex, int lastIndex)
		{
			return x.FirstIndex <= lastIndex && firstIndex <= x.LastIndex;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00009B25 File Offset: 0x00007D25
		internal static bool RangeEquals(this IRange x, IRange y)
		{
			return x.FirstIndex == y.FirstIndex && x.LastIndex == y.LastIndex;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00009B45 File Offset: 0x00007D45
		internal static bool Contains(this IRange x, IRange y)
		{
			return x.Contains(y.FirstIndex, y.LastIndex);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00009B59 File Offset: 0x00007D59
		internal static bool Contains(this IRange x, int firstIndex, int lastIndex)
		{
			return x.FirstIndex <= firstIndex && x.LastIndex >= lastIndex;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00009B72 File Offset: 0x00007D72
		internal static bool IsContained(this IRange x, int firstIndex, int lastIndex)
		{
			return x.FirstIndex >= firstIndex && x.LastIndex <= lastIndex;
		}
	}
}
