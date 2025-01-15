using System;
using System.ComponentModel;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics.Clustering
{
	// Token: 0x0200003A RID: 58
	[ImmutableObject(true)]
	internal sealed class ClusteringDataRow : TrainDataRow
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x0000659B File Offset: 0x0000479B
		internal ClusteringDataRow(IDataRow sourceDataRow, bool useForClustering, double[] attributes = null)
			: base(sourceDataRow)
		{
			this.UseForClustering = useForClustering;
			this.ClusteringAttributes = attributes;
		}

		// Token: 0x0400012A RID: 298
		internal readonly bool UseForClustering;

		// Token: 0x0400012B RID: 299
		internal readonly double[] ClusteringAttributes;
	}
}
