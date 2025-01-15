using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200019F RID: 415
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class MapMemberInstance : BaseInstance
	{
		// Token: 0x060010BB RID: 4283 RVA: 0x000470E5 File Offset: 0x000452E5
		internal MapMemberInstance(MapDataRegion owner, MapMember memberDef)
			: base(memberDef.ReportScope)
		{
			this.m_owner = owner;
			this.m_memberDef = memberDef;
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00047101 File Offset: 0x00045301
		protected override void ResetInstanceCache()
		{
		}

		// Token: 0x040007ED RID: 2029
		protected MapDataRegion m_owner;

		// Token: 0x040007EE RID: 2030
		protected MapMember m_memberDef;
	}
}
