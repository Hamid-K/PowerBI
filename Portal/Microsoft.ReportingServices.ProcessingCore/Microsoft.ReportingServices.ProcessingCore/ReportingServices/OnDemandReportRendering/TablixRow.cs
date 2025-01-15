using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000354 RID: 852
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class TablixRow : ReportElementCollectionBase<TablixCell>, IDataRegionRow
	{
		// Token: 0x060020AB RID: 8363 RVA: 0x0007F041 File Offset: 0x0007D241
		internal TablixRow(Tablix owner, int rowIndex)
		{
			this.m_owner = owner;
			this.m_rowIndex = rowIndex;
		}

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x060020AC RID: 8364
		public abstract ReportSize Height { get; }

		// Token: 0x060020AD RID: 8365 RVA: 0x0007F057 File Offset: 0x0007D257
		IDataRegionCell IDataRegionRow.GetIfExists(int index)
		{
			return this.GetIfExists(index);
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x0007F060 File Offset: 0x0007D260
		internal virtual IDataRegionCell GetIfExists(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				return null;
			}
			return this[index];
		}

		// Token: 0x0400106A RID: 4202
		protected Tablix m_owner;

		// Token: 0x0400106B RID: 4203
		protected int m_rowIndex;
	}
}
