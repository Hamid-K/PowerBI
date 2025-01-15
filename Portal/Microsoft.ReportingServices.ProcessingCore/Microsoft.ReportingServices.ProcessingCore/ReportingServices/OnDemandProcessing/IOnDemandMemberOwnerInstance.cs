using System;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x0200082D RID: 2093
	public interface IOnDemandMemberOwnerInstance : IOnDemandScopeInstance, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007575 RID: 30069
		IOnDemandMemberInstanceReference GetFirstMemberInstance(ReportHierarchyNode rifMember);
	}
}
