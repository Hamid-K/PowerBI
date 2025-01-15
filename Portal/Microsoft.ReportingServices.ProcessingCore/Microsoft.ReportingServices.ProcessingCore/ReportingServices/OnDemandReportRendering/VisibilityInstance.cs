using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200037F RID: 895
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class VisibilityInstance : BaseInstance
	{
		// Token: 0x0600224F RID: 8783 RVA: 0x00083E36 File Offset: 0x00082036
		internal VisibilityInstance(IReportScope reportScope)
			: base(reportScope)
		{
		}

		// Token: 0x1700136E RID: 4974
		// (get) Token: 0x06002250 RID: 8784
		public abstract bool CurrentlyHidden { get; }

		// Token: 0x1700136F RID: 4975
		// (get) Token: 0x06002251 RID: 8785
		public abstract bool StartHidden { get; }

		// Token: 0x06002252 RID: 8786 RVA: 0x00083E3F File Offset: 0x0008203F
		protected override void ResetInstanceCache()
		{
			this.m_cachedStartHidden = false;
			this.m_cachedCurrentlyHidden = false;
		}

		// Token: 0x040010F2 RID: 4338
		protected bool m_cachedStartHidden;

		// Token: 0x040010F3 RID: 4339
		protected bool m_startHiddenValue;

		// Token: 0x040010F4 RID: 4340
		protected bool m_cachedCurrentlyHidden;

		// Token: 0x040010F5 RID: 4341
		protected bool m_currentlyHiddenValue;
	}
}
