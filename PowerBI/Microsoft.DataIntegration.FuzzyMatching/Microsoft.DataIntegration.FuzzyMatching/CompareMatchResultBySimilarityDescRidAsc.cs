using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000055 RID: 85
	[Serializable]
	internal sealed class CompareMatchResultBySimilarityDescRidAsc : IComparer<IMatchResult>
	{
		// Token: 0x0600032A RID: 810 RVA: 0x00010814 File Offset: 0x0000EA14
		public int Compare(IMatchResult x, IMatchResult y)
		{
			if (x.ComparisonResult.Similarity == y.ComparisonResult.Similarity)
			{
				if (x.RightRecordId == y.RightRecordId)
				{
					return 0;
				}
				if (x.RightRecordId <= y.RightRecordId)
				{
					return -1;
				}
				return 1;
			}
			else
			{
				if (x.ComparisonResult.Similarity <= y.ComparisonResult.Similarity)
				{
					return 1;
				}
				return -1;
			}
		}

		// Token: 0x0400011C RID: 284
		public static readonly CompareMatchResultBySimilarityDescRidAsc Instance = new CompareMatchResultBySimilarityDescRidAsc();
	}
}
