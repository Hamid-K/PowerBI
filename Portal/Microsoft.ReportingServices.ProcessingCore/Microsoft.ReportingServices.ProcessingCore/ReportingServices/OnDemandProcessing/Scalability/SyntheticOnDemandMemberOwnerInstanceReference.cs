using System;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x020008AC RID: 2220
	internal class SyntheticOnDemandMemberOwnerInstanceReference : SyntheticOnDemandScopeInstanceReference, IOnDemandMemberOwnerInstanceReference, IReference<IOnDemandScopeInstance>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IReference<IOnDemandMemberOwnerInstance>
	{
		// Token: 0x0600794F RID: 31055 RVA: 0x001F37B2 File Offset: 0x001F19B2
		public SyntheticOnDemandMemberOwnerInstanceReference(IOnDemandMemberOwnerInstance memberOwner)
			: base(memberOwner)
		{
		}

		// Token: 0x06007950 RID: 31056 RVA: 0x001F37BB File Offset: 0x001F19BB
		IOnDemandMemberOwnerInstance IReference<IOnDemandMemberOwnerInstance>.Value()
		{
			return (IOnDemandMemberOwnerInstance)this.Value();
		}
	}
}
