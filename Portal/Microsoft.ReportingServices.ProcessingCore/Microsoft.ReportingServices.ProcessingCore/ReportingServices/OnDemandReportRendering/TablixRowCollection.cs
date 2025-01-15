using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200034F RID: 847
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class TablixRowCollection : ReportElementCollectionBase<TablixRow>, IDataRegionRowCollection
	{
		// Token: 0x06002098 RID: 8344 RVA: 0x0007EB8D File Offset: 0x0007CD8D
		internal TablixRowCollection(Tablix owner)
		{
			this.m_owner = owner;
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x0007EB9C File Offset: 0x0007CD9C
		IDataRegionRow IDataRegionRowCollection.GetIfExists(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				return null;
			}
			return this[index];
		}

		// Token: 0x04001064 RID: 4196
		protected Tablix m_owner;
	}
}
