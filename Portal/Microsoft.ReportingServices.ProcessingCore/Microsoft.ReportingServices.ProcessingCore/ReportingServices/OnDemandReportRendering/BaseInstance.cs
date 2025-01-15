using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000309 RID: 777
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class BaseInstance
	{
		// Token: 0x06001C9F RID: 7327 RVA: 0x00071D4E File Offset: 0x0006FF4E
		internal BaseInstance(IReportScope reportScope)
		{
			this.m_reportScope = reportScope;
		}

		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x00071D5D File Offset: 0x0006FF5D
		internal virtual IReportScopeInstance ReportScopeInstance
		{
			get
			{
				return this.m_reportScope.ReportScopeInstance;
			}
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x00071D6A File Offset: 0x0006FF6A
		internal virtual void SetNewContext()
		{
			this.ResetInstanceCache();
		}

		// Token: 0x06001CA2 RID: 7330
		protected abstract void ResetInstanceCache();

		// Token: 0x04000F08 RID: 3848
		internal IReportScope m_reportScope;
	}
}
