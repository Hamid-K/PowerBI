using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000C2 RID: 194
	public enum MiningNodeType
	{
		// Token: 0x0400072B RID: 1835
		Model = 1,
		// Token: 0x0400072C RID: 1836
		Tree,
		// Token: 0x0400072D RID: 1837
		Interior,
		// Token: 0x0400072E RID: 1838
		Distribution,
		// Token: 0x0400072F RID: 1839
		Cluster,
		// Token: 0x04000730 RID: 1840
		Unknown,
		// Token: 0x04000731 RID: 1841
		ItemSet,
		// Token: 0x04000732 RID: 1842
		AssociationRule,
		// Token: 0x04000733 RID: 1843
		PredictableAttribute,
		// Token: 0x04000734 RID: 1844
		InputAttribute,
		// Token: 0x04000735 RID: 1845
		InputAttributeState,
		// Token: 0x04000736 RID: 1846
		Sequence = 13,
		// Token: 0x04000737 RID: 1847
		Transition,
		// Token: 0x04000738 RID: 1848
		TimeSeries,
		// Token: 0x04000739 RID: 1849
		TsTree,
		// Token: 0x0400073A RID: 1850
		NNetSubnetwork,
		// Token: 0x0400073B RID: 1851
		NNetInputLayer,
		// Token: 0x0400073C RID: 1852
		NNetHiddenLayer,
		// Token: 0x0400073D RID: 1853
		NNetOutputLayer,
		// Token: 0x0400073E RID: 1854
		NNetInputNode,
		// Token: 0x0400073F RID: 1855
		NNetHiddenNode,
		// Token: 0x04000740 RID: 1856
		NNetOutputNode,
		// Token: 0x04000741 RID: 1857
		NNetMarginalNode,
		// Token: 0x04000742 RID: 1858
		RegressionTreeRoot,
		// Token: 0x04000743 RID: 1859
		NaiveBayesMarginalStatNode,
		// Token: 0x04000744 RID: 1860
		ArimaRoot,
		// Token: 0x04000745 RID: 1861
		ArimaPeriodicStructure,
		// Token: 0x04000746 RID: 1862
		ArimaAutoRegressive,
		// Token: 0x04000747 RID: 1863
		ArimaMovingAverage,
		// Token: 0x04000748 RID: 1864
		CustomBase = 1000
	}
}
