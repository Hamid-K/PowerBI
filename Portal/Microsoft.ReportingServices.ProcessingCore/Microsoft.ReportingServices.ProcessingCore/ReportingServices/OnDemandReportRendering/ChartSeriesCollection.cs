using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000224 RID: 548
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class ChartSeriesCollection : ReportElementCollectionBase<ChartSeries>, IDataRegionRowCollection
	{
		// Token: 0x060014AA RID: 5290 RVA: 0x00054610 File Offset: 0x00052810
		internal ChartSeriesCollection(Chart owner)
		{
			this.m_owner = owner;
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x0005461F File Offset: 0x0005281F
		IDataRegionRow IDataRegionRowCollection.GetIfExists(int seriesIndex)
		{
			if (this.m_chartSeriesCollection != null && seriesIndex >= 0 && seriesIndex < this.Count)
			{
				return this.m_chartSeriesCollection[seriesIndex];
			}
			return null;
		}

		// Token: 0x040009C1 RID: 2497
		protected Chart m_owner;

		// Token: 0x040009C2 RID: 2498
		protected ChartSeries[] m_chartSeriesCollection;
	}
}
