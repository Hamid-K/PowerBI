using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200027C RID: 636
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class DataRowCollection : ReportElementCollectionBase<DataRow>, IDataRegionRowCollection
	{
		// Token: 0x060018E2 RID: 6370 RVA: 0x0006637B File Offset: 0x0006457B
		internal DataRowCollection(CustomReportItem owner)
		{
			this.m_owner = owner;
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0006638A File Offset: 0x0006458A
		IDataRegionRow IDataRegionRowCollection.GetIfExists(int rowIndex)
		{
			if (this.m_cachedDataRows != null && rowIndex >= 0 && rowIndex < this.Count)
			{
				return this.m_cachedDataRows[rowIndex];
			}
			return null;
		}

		// Token: 0x04000C8C RID: 3212
		protected CustomReportItem m_owner;

		// Token: 0x04000C8D RID: 3213
		protected DataRow[] m_cachedDataRows;
	}
}
