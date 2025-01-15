using System;
using System.Collections;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.DotNet4
{
	// Token: 0x020000AB RID: 171
	public static class StructuralComparisons
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x00027F30 File Offset: 0x00026130
		public static IComparer StructuralComparer
		{
			get
			{
				IComparer comparer = StructuralComparisons.s_StructuralComparer;
				if (comparer == null)
				{
					comparer = new StructuralComparer();
					StructuralComparisons.s_StructuralComparer = comparer;
				}
				return comparer;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x00027F54 File Offset: 0x00026154
		public static IEqualityComparer StructuralEqualityComparer
		{
			get
			{
				IEqualityComparer equalityComparer = StructuralComparisons.s_StructuralEqualityComparer;
				if (equalityComparer == null)
				{
					equalityComparer = new StructuralEqualityComparer();
					StructuralComparisons.s_StructuralEqualityComparer = equalityComparer;
				}
				return equalityComparer;
			}
		}

		// Token: 0x0400017C RID: 380
		private static IComparer s_StructuralComparer;

		// Token: 0x0400017D RID: 381
		private static IEqualityComparer s_StructuralEqualityComparer;
	}
}
