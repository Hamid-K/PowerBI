using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000112 RID: 274
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class GaugeMemberInstance : BaseInstance
	{
		// Token: 0x06000C24 RID: 3108 RVA: 0x00035010 File Offset: 0x00033210
		internal GaugeMemberInstance(GaugePanel owner, GaugeMember memberDef)
			: base(memberDef.ReportScope)
		{
			this.m_owner = owner;
			this.m_memberDef = memberDef;
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0003502C File Offset: 0x0003322C
		protected override void ResetInstanceCache()
		{
		}

		// Token: 0x04000543 RID: 1347
		protected GaugePanel m_owner;

		// Token: 0x04000544 RID: 1348
		protected GaugeMember m_memberDef;
	}
}
