using System;

namespace Microsoft.ProgramSynthesis.Utils.Clustering
{
	// Token: 0x02000608 RID: 1544
	internal enum SamplingStrategy
	{
		// Token: 0x04000FF5 RID: 4085
		Random,
		// Token: 0x04000FF6 RID: 4086
		ClusterFromUncovered,
		// Token: 0x04000FF7 RID: 4087
		GrowCluster,
		// Token: 0x04000FF8 RID: 4088
		GrowCoveringCluster,
		// Token: 0x04000FF9 RID: 4089
		MergeClusters,
		// Token: 0x04000FFA RID: 4090
		MergeCoveringClusters,
		// Token: 0x04000FFB RID: 4091
		SplitClusters,
		// Token: 0x04000FFC RID: 4092
		SplitCoveringClusters
	}
}
