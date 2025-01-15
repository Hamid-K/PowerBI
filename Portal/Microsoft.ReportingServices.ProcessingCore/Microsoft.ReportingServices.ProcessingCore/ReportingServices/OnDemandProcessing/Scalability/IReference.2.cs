using System;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000882 RID: 2178
	public interface IReference<T> : IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060077EE RID: 30702
		T Value();
	}
}
