using System;

namespace Microsoft.ProgramSynthesis.Utils.Clustering
{
	// Token: 0x0200061D RID: 1565
	public static class LinkageCriterion<TData>
	{
		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060021F2 RID: 8690 RVA: 0x00060C2D File Offset: 0x0005EE2D
		public static LinkageCriterion<TData>.Type Complete { get; } = (Dendrogram<TData> bigCluster, Dendrogram<TData> smallCluster, Func<Dendrogram<TData>, Dendrogram<TData>, double> costLookupFunc) => Math.Max(costLookupFunc(bigCluster.LeftChild, smallCluster), costLookupFunc(bigCluster.RightChild, smallCluster));

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060021F3 RID: 8691 RVA: 0x00060C34 File Offset: 0x0005EE34
		public static LinkageCriterion<TData>.Type GroupAverage { get; } = (Dendrogram<TData> bigCluster, Dendrogram<TData> smallCluster, Func<Dendrogram<TData>, Dendrogram<TData>, double> costLookupFunc) => (bigCluster.LeftChild.PointsCount * costLookupFunc(bigCluster.LeftChild, smallCluster) + bigCluster.RightChild.PointsCount * costLookupFunc(bigCluster.RightChild, smallCluster)) / bigCluster.PointsCount;

		// Token: 0x0200061E RID: 1566
		// (Invoke) Token: 0x060021F6 RID: 8694
		public delegate double Type(Dendrogram<TData> bigCluster, Dendrogram<TData> smallCluster, Func<Dendrogram<TData>, Dendrogram<TData>, double> linkageCostLookupFunc);
	}
}
