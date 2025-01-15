using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001B9 RID: 441
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class MapSpatialDataInstance : BaseInstance
	{
		// Token: 0x06001163 RID: 4451 RVA: 0x000489DA File Offset: 0x00046BDA
		internal MapSpatialDataInstance(MapSpatialData defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x000489F4 File Offset: 0x00046BF4
		protected override void ResetInstanceCache()
		{
		}

		// Token: 0x04000839 RID: 2105
		private MapSpatialData m_defObject;
	}
}
