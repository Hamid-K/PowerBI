using System;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Diagnostics;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200008A RID: 138
	[Serializable]
	internal class StateManagerStatistics : StatisticsBase
	{
		// Token: 0x060005B1 RID: 1457 RVA: 0x00019DE4 File Offset: 0x00017FE4
		public override void Reset()
		{
			this.NonDistinctSignaturesIndexed = 0L;
			this.DistinctSignaturesIndexed = 0L;
			this.RightRecordCacheHits = 0L;
			this.RightRecordCacheMisses = 0L;
			this.RidListCacheHits = 0L;
			this.RidListCacheMisses = 0L;
		}

		// Token: 0x040001EC RID: 492
		[Statistic(0)]
		public long NonDistinctSignaturesIndexed;

		// Token: 0x040001ED RID: 493
		[Statistic(0)]
		public long DistinctSignaturesIndexed;

		// Token: 0x040001EE RID: 494
		[Statistic(0)]
		public long RightRecordCacheHits;

		// Token: 0x040001EF RID: 495
		[Statistic(0)]
		public long RightRecordCacheMisses;

		// Token: 0x040001F0 RID: 496
		[Statistic(0)]
		public long RidListCacheHits;

		// Token: 0x040001F1 RID: 497
		[Statistic(0)]
		public long RidListCacheMisses;
	}
}
