using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000286 RID: 646
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class DataMemberInstance : BaseInstance
	{
		// Token: 0x0600190E RID: 6414 RVA: 0x00066AA0 File Offset: 0x00064CA0
		internal DataMemberInstance(CustomReportItem owner, DataMember memberDef)
			: base(memberDef.ReportScope)
		{
			this.m_owner = owner;
			this.m_memberDef = memberDef;
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x00066ABC File Offset: 0x00064CBC
		protected override void ResetInstanceCache()
		{
		}

		// Token: 0x04000C9E RID: 3230
		protected CustomReportItem m_owner;

		// Token: 0x04000C9F RID: 3231
		protected DataMember m_memberDef;
	}
}
