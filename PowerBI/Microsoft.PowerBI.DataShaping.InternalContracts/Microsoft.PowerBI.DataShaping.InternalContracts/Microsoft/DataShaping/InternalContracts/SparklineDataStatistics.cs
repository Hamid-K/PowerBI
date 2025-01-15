using System;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x0200000B RID: 11
	internal sealed class SparklineDataStatistics
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000021B1 File Offset: 0x000003B1
		internal SparklineDataStatistics(int sparklineCount, int sparklinesTotalPointsCount)
		{
			this.SparklineCount = sparklineCount;
			this.SparklinesTotalPointsCount = sparklinesTotalPointsCount;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021C7 File Offset: 0x000003C7
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000021CF File Offset: 0x000003CF
		public int SparklineCount { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021D8 File Offset: 0x000003D8
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000021E0 File Offset: 0x000003E0
		public int SparklinesTotalPointsCount { get; set; }

		// Token: 0x04000030 RID: 48
		public static readonly SparklineDataStatistics Empty = new SparklineDataStatistics(0, 0);
	}
}
