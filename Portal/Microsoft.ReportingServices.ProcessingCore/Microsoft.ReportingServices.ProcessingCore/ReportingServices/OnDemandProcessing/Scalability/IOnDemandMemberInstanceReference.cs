using System;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200087A RID: 2170
	public interface IOnDemandMemberInstanceReference : IOnDemandMemberOwnerInstanceReference, IReference<IOnDemandScopeInstance>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IReference<IOnDemandMemberOwnerInstance>, IReference<IOnDemandMemberInstance>
	{
	}
}
