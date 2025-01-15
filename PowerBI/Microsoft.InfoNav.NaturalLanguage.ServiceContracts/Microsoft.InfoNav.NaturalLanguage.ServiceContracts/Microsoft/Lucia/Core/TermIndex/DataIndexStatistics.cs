using System;
using System.ComponentModel;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x0200015C RID: 348
	[ImmutableObject(true)]
	public sealed class DataIndexStatistics
	{
		// Token: 0x060006E9 RID: 1769 RVA: 0x0000BD6B File Offset: 0x00009F6B
		public DataIndexStatistics(int? indexedInstanceCount, double? averageStringLength, TimeSpan? indexBuildDuration)
		{
			this.IndexedInstanceCount = indexedInstanceCount;
			this.AverageStringLength = averageStringLength;
			this.IndexBuildDuration = indexBuildDuration;
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0000BD88 File Offset: 0x00009F88
		public int? IndexedInstanceCount { get; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x0000BD90 File Offset: 0x00009F90
		public double? AverageStringLength { get; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0000BD98 File Offset: 0x00009F98
		public TimeSpan? IndexBuildDuration { get; }
	}
}
