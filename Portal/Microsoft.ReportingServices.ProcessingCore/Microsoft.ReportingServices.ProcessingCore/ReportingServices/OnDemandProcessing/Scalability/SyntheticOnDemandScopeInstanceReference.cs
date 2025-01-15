using System;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x020008AB RID: 2219
	internal class SyntheticOnDemandScopeInstanceReference : SyntheticReferenceBase<IOnDemandScopeInstance>
	{
		// Token: 0x0600794C RID: 31052 RVA: 0x001F3794 File Offset: 0x001F1994
		public SyntheticOnDemandScopeInstanceReference(IOnDemandScopeInstance scopeInstance)
		{
			this.m_value = scopeInstance;
		}

		// Token: 0x0600794D RID: 31053 RVA: 0x001F37A3 File Offset: 0x001F19A3
		public override IOnDemandScopeInstance Value()
		{
			return this.m_value;
		}

		// Token: 0x0600794E RID: 31054 RVA: 0x001F37AB File Offset: 0x001F19AB
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SyntheticOnDemandScopeInstanceReference;
		}

		// Token: 0x04003CE1 RID: 15585
		private readonly IOnDemandScopeInstance m_value;
	}
}
