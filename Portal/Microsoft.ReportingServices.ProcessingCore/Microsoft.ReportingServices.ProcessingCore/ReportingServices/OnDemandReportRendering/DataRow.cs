using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200027F RID: 639
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class DataRow : ReportElementCollectionBase<DataCell>, IDataRegionRow
	{
		// Token: 0x060018EC RID: 6380 RVA: 0x00066601 File Offset: 0x00064801
		internal DataRow(CustomReportItem owner, int rowIndex)
		{
			this.m_owner = owner;
			this.m_rowIndex = rowIndex;
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x00066617 File Offset: 0x00064817
		IDataRegionCell IDataRegionRow.GetIfExists(int cellIndex)
		{
			if (this.m_cachedDataCells != null && cellIndex >= 0 && cellIndex < this.Count)
			{
				return this.m_cachedDataCells[cellIndex];
			}
			return null;
		}

		// Token: 0x04000C90 RID: 3216
		protected CustomReportItem m_owner;

		// Token: 0x04000C91 RID: 3217
		protected int m_rowIndex;

		// Token: 0x04000C92 RID: 3218
		protected DataCell[] m_cachedDataCells;
	}
}
