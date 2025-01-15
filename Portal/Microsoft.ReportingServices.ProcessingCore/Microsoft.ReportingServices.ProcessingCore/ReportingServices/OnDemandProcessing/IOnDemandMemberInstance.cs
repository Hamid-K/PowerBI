using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x0200082E RID: 2094
	public interface IOnDemandMemberInstance : IOnDemandMemberOwnerInstance, IOnDemandScopeInstance, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007576 RID: 30070
		IOnDemandMemberInstanceReference GetNextMemberInstance();

		// Token: 0x06007577 RID: 30071
		IOnDemandScopeInstance GetCellInstance(IOnDemandMemberInstanceReference outerGroupInstanceRef, out IReference<IOnDemandScopeInstance> cellRef);

		// Token: 0x17002798 RID: 10136
		// (get) Token: 0x06007578 RID: 30072
		List<object> GroupExprValues { get; }
	}
}
