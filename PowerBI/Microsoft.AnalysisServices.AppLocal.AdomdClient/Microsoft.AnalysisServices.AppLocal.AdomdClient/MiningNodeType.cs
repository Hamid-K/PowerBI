using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000C2 RID: 194
	public enum MiningNodeType
	{
		// Token: 0x04000738 RID: 1848
		Model = 1,
		// Token: 0x04000739 RID: 1849
		Tree,
		// Token: 0x0400073A RID: 1850
		Interior,
		// Token: 0x0400073B RID: 1851
		Distribution,
		// Token: 0x0400073C RID: 1852
		Cluster,
		// Token: 0x0400073D RID: 1853
		Unknown,
		// Token: 0x0400073E RID: 1854
		ItemSet,
		// Token: 0x0400073F RID: 1855
		AssociationRule,
		// Token: 0x04000740 RID: 1856
		PredictableAttribute,
		// Token: 0x04000741 RID: 1857
		InputAttribute,
		// Token: 0x04000742 RID: 1858
		InputAttributeState,
		// Token: 0x04000743 RID: 1859
		Sequence = 13,
		// Token: 0x04000744 RID: 1860
		Transition,
		// Token: 0x04000745 RID: 1861
		TimeSeries,
		// Token: 0x04000746 RID: 1862
		TsTree,
		// Token: 0x04000747 RID: 1863
		NNetSubnetwork,
		// Token: 0x04000748 RID: 1864
		NNetInputLayer,
		// Token: 0x04000749 RID: 1865
		NNetHiddenLayer,
		// Token: 0x0400074A RID: 1866
		NNetOutputLayer,
		// Token: 0x0400074B RID: 1867
		NNetInputNode,
		// Token: 0x0400074C RID: 1868
		NNetHiddenNode,
		// Token: 0x0400074D RID: 1869
		NNetOutputNode,
		// Token: 0x0400074E RID: 1870
		NNetMarginalNode,
		// Token: 0x0400074F RID: 1871
		RegressionTreeRoot,
		// Token: 0x04000750 RID: 1872
		NaiveBayesMarginalStatNode,
		// Token: 0x04000751 RID: 1873
		ArimaRoot,
		// Token: 0x04000752 RID: 1874
		ArimaPeriodicStructure,
		// Token: 0x04000753 RID: 1875
		ArimaAutoRegressive,
		// Token: 0x04000754 RID: 1876
		ArimaMovingAverage,
		// Token: 0x04000755 RID: 1877
		CustomBase = 1000
	}
}
